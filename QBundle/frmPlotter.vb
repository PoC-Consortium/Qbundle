Imports System.ComponentModel

Public Class frmPlotter
    Private Sub btnPath_Click(sender As Object, e As EventArgs) Handles btnPath.Click
        Dim FD As New FolderBrowserDialog
        If FD.ShowDialog() = DialogResult.OK Then
            txtPath.Text = FD.SelectedPath
            If IO.Path.GetPathRoot(FD.SelectedPath) = FD.SelectedPath Then
                MsgBox("Xplotter does not allow to plot directly to root path of a drive. Create a directory and put your plots in there.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong path")
            End If
            Dim FreeSpace As Long = My.Computer.FileSystem.GetDriveInfo(txtPath.Text).TotalFreeSpace
            Dim nonces As Long = Math.Floor(FreeSpace / 1024 / 256)
            nonces = Math.Floor(nonces / 8) 'make it devidable by 8
            nonces = nonces * 8
            HSSize.Maximum = nonces

            HSSize.Minimum = 8
            HSSize.Value = CInt(nonces)
            lblTotalNonces.Text = nonces.ToString

            Dim noncespace As Long = nonces * 256 * 1024

            lblSize.Text = GetReadableSpace(noncespace) & " / " & GetReadableSpace(noncespace)
        End If


    End Sub

    Private Function GetReadableSpace(ByVal space As Double) As String
        Dim unit As String = "Byte"
        If space > 1024 Then
            space = space / 1024
            unit = "KiB"
        End If
        If space > 1024 Then
            space = space / 1024
            unit = "MiB"
        End If

        If space > 1024 Then
            space = space / 1024
            unit = "GiB"
        End If

        If space > 1024 Then
            space = space / 1024
            unit = "TiB"
        End If
        If space > 1024 Then
            space = space / 1024
            unit = "PiB"
        End If

        space = Math.Round(space, 2)
        Return space.ToString & " " & unit

    End Function



    Private Sub frmPlotter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nrThreads.Maximum = Environment.ProcessorCount
        'nrThreads.Value = Q.settings.Cpulimit

        Select Case Environment.ProcessorCount
            Case 1
                nrThreads.Value = 1
            Case 2
                nrThreads.Value = 1
            Case 4
                nrThreads.Value = 3
            Case Else
                nrThreads.Value = Environment.ProcessorCount - 1
        End Select

        lstPlots.Items.Clear()
        If Q.settings.Plots <> "" Then
            Dim buffer() As String = Split(Q.settings.Plots, "|")
            For Each plot As String In buffer
                If plot.Length > 1 Then
                    lstPlots.Items.Add(plot)
                End If
            Next
        End If

        cmlAccounts.Items.Clear()
        Dim mnuitm As ToolStripMenuItem
        For Each account As QB.clsAccounts.Account In Q.Accounts.AccArray
            mnuitm = New ToolStripMenuItem
            mnuitm.Name = account.AccountName
            mnuitm.Text = account.AccountName
            AddHandler(mnuitm.Click), AddressOf SelectAccountID
            cmlAccounts.Items.Add(mnuitm)
        Next
        txtStartNonce.Text = GetStartNonce()

        Try
            nrRam.Maximum = Math.Round((My.Computer.Info.TotalPhysicalMemory / 1024 / 1024 / 1024))
            Dim freeram As Integer = Math.Floor(My.Computer.Info.AvailablePhysicalMemory / 1024 / 1024 / 1024) - 1
            If freeram < 1 Then freeram = 1
            nrRam.Value = freeram
        Catch ex As Exception

        End Try



    End Sub
    Private Sub SelectAccountID(sender As Object, e As EventArgs)

        txtAccount.Text = Q.Accounts.GetAccountID(sender.text)

    End Sub
    Private Sub btnStartPotting_Click(sender As Object, e As EventArgs) Handles btnStartPotting.Click
        If Not Q.App.isInstalled(QGlobal.AppNames.Xplotter) Then
            If MsgBox("Xplotter is not installed yet. Do you want to download and install it now?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Download Xplotter") = MsgBoxResult.Yes Then


                Dim s As frmDownloadExtract = New frmDownloadExtract
                s.Appid = QGlobal.AppNames.Xplotter
                Dim res As DialogResult
                Me.Hide()
                res = s.ShowDialog
                Me.Show()
                If res = DialogResult.Cancel Then
                    Exit Sub
                ElseIf res = DialogResult.Abort Then
                    MsgBox("Something went wrong. Internet connection might have been lost.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
                    Exit Sub
                End If
                Q.App.SetLocalInfo() 'update so it is installed
            Else
                Exit Sub
            End If
        End If
        'ok Xplotter is now installed
        If Q.settings.DynPlotEnabled Then
            MsgBox("Dynamic plotting is enabled. You need to disable dynamic plotting while plotting like this.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Dynamic plotting")
            Exit Sub
        End If
        'check path
        Dim Path As String = txtPath.Text
        If IO.Path.GetPathRoot(Path) = Path Then
            MsgBox("Xplotter does not allow to plot directly to root path of a drive. Create a directory and put your plots in there.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong path")
            Exit Sub
        End If
        Dim nonces As Double = HSSize.Value
        Dim StartNonce As Double = 0
        Try
            StartNonce = CDbl(txtStartNonce.Text)
        Catch ex As Exception
            MsgBox("The start nonce is not a numeric value. Please set a correct value", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Path non existing.")
            Exit Sub
        End Try
        Try
            If Not IO.Directory.Exists(txtPath.Text) Then
                MsgBox("The path for the plotfile does not exist. Please select a valid path.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Path non existing.")
                Exit Sub
            End If
        Catch ex As Exception

        End Try

        'check size ok
        Try
            Dim FreeSpace As Double = My.Computer.FileSystem.GetDriveInfo(Path).TotalFreeSpace
            Dim tobeused As Double = nonces * 1024 * 256
            If tobeused > FreeSpace Then
                MsgBox("Free space on drive have changed and the plotfile will now be to big. Please lower the size of plotfile or select a new path.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Free space changed.")
                Exit Sub
            End If
        Catch ex As Exception

            Exit Sub
            End Try


        Try
            If txtAccount.Text.Length < 1 Or Val(txtAccount.Text) = 0 Then
                MsgBox("You need to enter a valid accountID", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Account ID.")
                Exit Sub
            End If
        Catch ex As Exception
            Exit Sub
        End Try

        'check nonce overlap.
        If checkPlotOverLapp() = False Then
            MsgBox("You are trying to write a plot that overlap with an existing one. Please change your Startnonce.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Free space changed.")
            Exit Sub
        End If

        Try
            Dim freeram As Integer = Math.Floor(My.Computer.Info.AvailablePhysicalMemory / 1024 / 1024 / 1024)
            If freeram < nrRam.Value Then
                MsgBox("You are trying to use more ram than what is currently free in your computer. Please free up ram or lower your settings.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Free Ram")
                Exit Sub
            End If

        Catch ex As Exception

        End Try
        ' All checks ok now lets start Xplotter
        If chkAddtoPlottfiles.Checked = True Then
            Dim filePath As String = txtPath.Text
            If Not filePath.EndsWith("\") Then filePath &= "\"
            filePath &= txtAccount.Text & "_" 'account 
            filePath &= txtStartNonce.Text & "_" 'startnonce
            filePath &= CStr(HSSize.Value) & "_" 'length
            filePath &= CStr(HSSize.Value) 'stagger
            Q.settings.Plots &= filePath & "|"
            Q.settings.SaveSettings()
            lstPlots.Items.Add(filePath)
        End If

        Try
            Dim p As Process = New Process
            p.StartInfo.WorkingDirectory = QGlobal.AppDir & "Xplotter"
            Dim thepath As String = txtPath.Text
            If thepath.Contains(" ") Then thepath = Chr(34) & thepath & Chr(34)
            p.StartInfo.Arguments = "-id " & txtAccount.Text & " -sn " & txtStartNonce.Text & " -n " & CStr(HSSize.Value) & " -t " & nrThreads.Value.ToString & " -path " & thepath & " -mem " & nrRam.Value.ToString & "G"
            p.StartInfo.UseShellExecute = True
            If QGlobal.CPUInstructions.AVX Then
                p.StartInfo.FileName = QGlobal.AppDir & "Xplotter\XPlotter_avx.exe"
            Else
                p.StartInfo.FileName = QGlobal.AppDir & "Xplotter\XPlotter_sse.exe"
            End If
            p.StartInfo.Verb = "runas"
            p.Start()
        Catch ex As Exception
            MsgBox("Failed to start Xplotter.")
        End Try


    End Sub
    Private Function GetStartNonce() As Double

        Return Generic.GetStartNonce(txtAccount.Text, HSSize.Value - 1)

    End Function
    Private Function checkPlotOverLapp() As Boolean

        Dim Plotfiles() As String
        Dim AccountID As String = txtAccount.Text
        Dim StartNonce As Double = CDbl(txtStartNonce.Text)
        Dim EndNonce As Double = StartNonce + HSSize.Value - 1
        Dim PStartNonce As Double = 0
        Dim PEndNonce As Double = 0


        'Within Plotrange
        'Startnonce  <------------------>  EndNonce
        '    PStartnonce <------------------------------->PEndnonce 

        'Total Overlap
        ' Startnonce <------------------------------->Endnonce 
        '      PStartnonce  <------------------>  PEndNonce

        Try
            If Q.settings.Plots.Length > 0 Then
                Plotfiles = Split(Q.settings.Plots, "|")
                For Each Plot As String In Plotfiles
                    If Plot.Length > 1 Then
                        Dim N() As String = Split(IO.Path.GetFileName(Plot), "_")
                        If UBound(N) = 3 Then
                            If N(0) = Trim(AccountID) Then
                                PStartNonce = CDbl(N(1))
                                PEndNonce = PStartNonce + CDbl(N(2)) - 1
                                If StartNonce >= PStartNonce And StartNonce <= PEndNonce Then
                                    'startnonce within a plotrange
                                    Return False
                                End If
                                If EndNonce >= PStartNonce And EndNonce <= PEndNonce Then
                                    'Endnonce within a plotrange
                                    Return False
                                End If
                                If PStartNonce > StartNonce And PEndNonce < EndNonce Then
                                    'We overlap totaly
                                    Return False
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Return False
        End Try



        Return True
    End Function



    Private Sub btnAccounts_Click(sender As Object, e As EventArgs) Handles btnAccounts.Click

        Try
            Me.cmlAccounts.Show(Me.btnAccounts, Me.btnAccounts.PointToClient(Cursor.Position))
        Catch ex As Exception

        End Try


    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Me.cmImport.Show(Me.btnImport, Me.btnImport.PointToClient(Cursor.Position))


    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If lstPlots.SelectedIndex = -1 Then
            MsgBox("You need to select a plot to remove.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Nothing to remove")
            Exit Sub
        End If

        If MsgBox("Are you sure you want to remove selected plot?" & vbCrLf & "It will not be deleted from disk.", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Remove plotfile") = MsgBoxResult.Yes Then
            lstPlots.Items.RemoveAt(lstPlots.SelectedIndex)
            Q.settings.Plots = ""
            For t As Integer = 0 To lstPlots.Items.Count - 1
                Q.settings.Plots &= lstPlots.Items.Item(t) & "|"
            Next
            Q.settings.SaveSettings()

        End If

        txtStartNonce.Text = GetStartNonce()
    End Sub

    Private Sub txtAccount_TextChanged(sender As Object, e As EventArgs) Handles txtAccount.TextChanged
        txtStartNonce.Text = GetStartNonce()
    End Sub

    Private Sub HSSize_Scroll(sender As Object, e As EventArgs) Handles HSSize.Scroll

    End Sub

    Private Sub HSSize_ValueChanged(sender As Object, e As EventArgs) Handles HSSize.ValueChanged
        Try
            Dim FreeSpace As Double = My.Computer.FileSystem.GetDriveInfo(txtPath.Text).TotalFreeSpace
            lblTotalNonces.Text = HSSize.Value.ToString
            Dim Space As Double = HSSize.Value

            Space = Space * 256
            Space = Space * 1024
            lblSize.Text = GetReadableSpace(Space) & " / " & GetReadableSpace(FreeSpace)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ImportFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportFileToolStripMenuItem.Click
        Dim ofd As New OpenFileDialog
        If ofd.ShowDialog = DialogResult.OK Then
            If IO.File.Exists(ofd.FileName) Then
                lstPlots.Items.Add(ofd.FileName)
                Q.settings.Plots &= ofd.FileName & "|"
                txtStartNonce.Text = GetStartNonce()
                Q.settings.SaveSettings()
            End If
        End If

    End Sub


    Private Sub ImportFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportFolderToolStripMenuItem.Click
        Dim ofd As New FolderBrowserDialog
        If ofd.ShowDialog = DialogResult.OK Then
            If IO.Directory.Exists(ofd.SelectedPath) Then
                Dim fileEntries As String() = IO.Directory.GetFiles(ofd.SelectedPath)
                For Each file As String In fileEntries
                    lstPlots.Items.Add(file)
                    Q.settings.Plots &= file & "|"
                Next
                txtStartNonce.Text = GetStartNonce()
                Q.settings.SaveSettings()
            End If
        End If
    End Sub
End Class