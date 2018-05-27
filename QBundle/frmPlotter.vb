

Public Class frmPlotter

    Private Sub btnPath_Click(sender As Object, e As EventArgs) Handles btnPath.Click
        Dim FD As New FolderBrowserDialog
        If FD.ShowDialog() = DialogResult.OK Then
            txtPath.Text = FD.SelectedPath
            HSSize.Enabled = True
            If IO.Path.GetPathRoot(FD.SelectedPath) = FD.SelectedPath Then
                MsgBox("Xplotter does not allow to plot directly to root path of a drive. Create a directory and put your plots in there.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong path")
            End If
            If Not Generic.PlotDriveTypeOk(FD.SelectedPath) Then
                MsgBox("The drive format is not NTFS. Please use another drive or reformat it to NTFS.")
            End If

            If Generic.DriveCompressed(FD.SelectedPath) Then
                Dim Msg As String = "The selected path is on a NTFS compressed drive or folder."
                Msg &= " This is not supported by Xplotter." & vbCrLf & vbCrLf
                MsgBox(Msg, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Compressed drive")
            End If

            Dim FreeSpace As Long = Generic.GetDiskspace(txtPath.Text)  '= My.Computer.FileSystem.GetDriveInfo(txtPath.Text).TotalFreeSpace
            Dim nonces As Long = CLng(Math.Floor(FreeSpace / 1024 / 256))
            nonces = CLng(Math.Floor(nonces / 8)) 'make it devidable by 8
            nonces = nonces * 8
            If nonces < 8 Then
                MsgBox("Free space on drive is to low for plotfiles", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "No free space.")
                nonces = 8
            End If

            HSSize.Maximum = CInt(nonces)

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



        UpdatePlotList()


        cmlAccounts.Items.Clear()
        Dim mnuitm As ToolStripMenuItem
        For Each account As QB.clsAccounts.Account In Q.Accounts.AccArray
            mnuitm = New ToolStripMenuItem
            mnuitm.Name = account.AccountName
            mnuitm.Text = account.AccountName
            AddHandler(mnuitm.Click), AddressOf SelectAccountID
            cmlAccounts.Items.Add(mnuitm)
        Next
        txtStartNonce.Text = CStr(GetStartNonce())

        Try
            nrRam.Maximum = CDec(Math.Round((My.Computer.Info.TotalPhysicalMemory / 1024 / 1024 / 1024)))
            Dim freeram As Integer = CInt(Math.Floor(My.Computer.Info.AvailablePhysicalMemory / 1024 / 1024 / 1024) - 1)
            If freeram < 1 Then freeram = 1
            If freeram > 4 Then freeram = 4
            nrRam.Value = freeram
        Catch ex As Exception

        End Try

        If QGlobal.CPUInstructions.SSE Then lblcputype.Text = "SSE"
        If QGlobal.CPUInstructions.AVX Then lblcputype.Text = "AVX"
        If QGlobal.CPUInstructions.AVX2 Then lblcputype.Text = "AVX2"




    End Sub

    Private Sub SelectAccountID(sender As Object, e As EventArgs)

        Dim mnuitm As ToolStripMenuItem = Nothing
        Try
            mnuitm = DirectCast(sender, ToolStripMenuItem)
        Catch ex As Exception
            Generic.WriteDebug(ex)
            Exit Sub
        End Try
        txtAccount.Text = Q.Accounts.GetAccountID(mnuitm.Text)

    End Sub
    Private Sub btnStartPotting_Click(sender As Object, e As EventArgs) Handles btnStartPotting.Click
        StartPlotting()
    End Sub

    Private Sub StartPlotting()
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
        If Not IO.Directory.Exists(txtPath.Text) Then
            MsgBox("Please select a valid path to store the plotfile to.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong path")
            Exit Sub
        End If
        If IO.Path.GetPathRoot(Path) = Path Then
            MsgBox("Xplotter does not allow to plot directly to root path of a drive. Create a directory and put your plots in there.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong path")
            Exit Sub
        End If

        If Generic.DriveCompressed(Path) Then
            Dim Msg As String = "The selected path is on a NTFS compressed drive or folder."
            Msg &= " This is not supported by Xplotter." & vbCrLf & vbCrLf
            MsgBox(Msg, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Compressed drive")
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
            Dim FreeSpace As Long = Generic.GetDiskspace(Path) ' = My.Computer.FileSystem.GetDriveInfo(Path).TotalFreeSpace


            Dim tobeused As Double = nonces * 1024 * 256
            If tobeused > CDbl(FreeSpace) Then
                MsgBox("Free space on drive have changed and the plotfile will now be to big. Please lower the size of plotfile or select a new path.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Free space changed.")
                Exit Sub
            End If
        Catch ex As Exception

            Exit Sub
        End Try

        Dim account As String = txtAccount.Text
        If UCase(account).StartsWith("BURST-") Then account = Q.Accounts.ConvertRSToId(account)
        Try
            If account.Length < 1 Or Val(account) = 0 Then
                MsgBox("You need to enter a valid accountID", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Account ID.")
                Exit Sub
            End If
        Catch ex As Exception
            Exit Sub
        End Try

        'check nonce overlap.
        If checkPlotOverLapp() = False Then
            MsgBox("You are trying to write a plot that overlap with an existing one. Please change your Startnonce.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Plot overlap")
            Exit Sub
        End If

        Try
            Dim freeram As Integer = CInt(Math.Floor(My.Computer.Info.AvailablePhysicalMemory / 1024 / 1024 / 1024))
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
            filePath &= account & "_" 'account 
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
            Dim Arguments As String = "-id " & account 'account id
            Arguments &= " -sn " & txtStartNonce.Text 'start nonce
            Arguments &= " -n " & CStr(HSSize.Value) ' amount of nonces
            Arguments &= " -t " & nrThreads.Value.ToString 'threadss
            Arguments &= " -path " & thepath ' path
            Arguments &= " -mem " & nrRam.Value.ToString & "G" 'memory usage
            If radPoC2.Checked = True Then
                Arguments &= " -poc2"
            End If

            p.StartInfo.Arguments = Arguments
            p.StartInfo.UseShellExecute = True
            If QGlobal.CPUInstructions.AVX2 Then
                p.StartInfo.FileName = QGlobal.AppDir & "Xplotter\XPlotter_avx2.exe"
            ElseIf QGlobal.CPUInstructions.AVX Then
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
            If Not IsNothing(Q.settings.Plots) Then
                If Q.settings.Plots.Length > 6 Then
                    Plotfiles = Split(Q.settings.Plots, "|")
                    If Not IsNothing(Plotfiles) Then
                        For Each Plot As String In Plotfiles
                            If Plot.Length > 6 Then
                                If IO.File.Exists(Plot) Then
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
                            End If
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
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

        If MsgBox("Are you sure you want to remove selected plot(s)?" & vbCrLf & "It will not be deleted from disk.", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Remove plotfile") = MsgBoxResult.Yes Then
            Q.settings.Plots = ""
            For i As Integer = 0 To lstPlots.Items.Count - 1
                If Not lstPlots.GetSelected(i) = True Then
                    Q.settings.Plots &= lstPlots.Items.Item(i).ToString & "|"
                End If
            Next
            Q.settings.SaveSettings()
            UpdatePlotList()
        End If

        txtStartNonce.Text = CStr(GetStartNonce())
    End Sub

    Private Sub txtAccount_TextChanged(sender As Object, e As EventArgs) Handles txtAccount.TextChanged
        txtStartNonce.Text = CStr(GetStartNonce())
    End Sub

    Private Sub HSSize_Scroll(sender As Object, e As EventArgs) Handles HSSize.Scroll

    End Sub

    Private Sub HSSize_ValueChanged(sender As Object, e As EventArgs) Handles HSSize.ValueChanged
        Try
            Dim FreeSpace As Double = CDbl(Generic.GetDiskspace(txtPath.Text)) 'My.Computer.FileSystem.GetDriveInfo(txtPath.Text).TotalFreeSpace
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
            If IO.File.Exists(ofd.FileName) And Generic.IsValidPlottFilename(ofd.FileName) Then
                lstPlots.Items.Add(ofd.FileName)
                Q.settings.Plots &= ofd.FileName & "|"
                txtStartNonce.Text = CStr(GetStartNonce())
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
                    If Generic.IsValidPlottFilePath(file) Then
                        lstPlots.Items.Add(file)
                        Q.settings.Plots &= file & "|"
                    End If
                Next
                txtStartNonce.Text = CStr(GetStartNonce())
                Q.settings.SaveSettings()
            End If
        End If
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()

    End Sub
    Private Sub StartPlottingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartPlottingToolStripMenuItem.Click
        StartPlotting()
    End Sub

    Private Sub ResumePlottingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResumePlottingToolStripMenuItem.Click
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

        Dim FileParts() As String = Nothing
        Dim FilePath As String = ""
        Try

            Dim ofd As New OpenFileDialog
            If ofd.ShowDialog = DialogResult.OK Then
                If IO.File.Exists(ofd.FileName) Then
                    'now check the plotfile
                    If Not Generic.IsValidPlottFilePath(ofd.FileName) Then
                        MsgBox("Selected File is not a plotfile.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Error")
                        Exit Sub
                    End If
                    FileParts = Split(IO.Path.GetFileName(ofd.FileName), "_")
                    FilePath = IO.Path.GetDirectoryName(ofd.FileName)
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
            MsgBox("Error parsing current plotfile.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End Try
        If FilePath.Contains(" ") Then FilePath = Chr(34) & FilePath & Chr(34)

        Dim Arg As String = "-id " & FileParts(0) & " -sn " & FileParts(1) & " -n " & FileParts(2) & " -t " & nrThreads.Value.ToString & " -path " & FilePath & " -mem " & nrRam.Value.ToString & "G"

        Try
            Dim p As Process = New Process
            p.StartInfo.WorkingDirectory = QGlobal.AppDir & "Xplotter"

            p.StartInfo.Arguments = Arg
            p.StartInfo.UseShellExecute = True
            If QGlobal.CPUInstructions.AVX Then
                p.StartInfo.FileName = QGlobal.AppDir & "Xplotter\XPlotter_avx.exe"
            Else
                p.StartInfo.FileName = QGlobal.AppDir & "Xplotter\XPlotter_sse.exe"
            End If
            p.StartInfo.Verb = "runas"
            p.Start()
        Catch ex As Exception
            Generic.WriteDebug(ex)
            MsgBox("Failed to start Xplotter.")
        End Try


    End Sub

    Private Sub txtPath_TextChanged(sender As Object, e As EventArgs) Handles txtPath.TextChanged
        ' Only enable the size selector if there is text in the path box'
        HSSize.Enabled = txtPath.Text.Length > 0
    End Sub

    Private Sub lblSelectAll_Click(sender As Object, e As EventArgs) Handles lblSelectAll.Click
        If lstPlots.Items.Count <= 0 Then Exit Sub
        For i As Integer = 0 To lstPlots.Items.Count - 1
            Me.lstPlots.SetSelected(i, True)
        Next
    End Sub

    Private Sub lblDeselectAll_Click(sender As Object, e As EventArgs) Handles lblDeselectAll.Click
        If lstPlots.Items.Count <= 0 Then Exit Sub
        For i As Integer = 0 To lstPlots.Items.Count - 1
            Me.lstPlots.SetSelected(i, False)
        Next
    End Sub
    Private Sub UpdatePlotList()
        lstPlots.Items.Clear()
        If Q.settings.Plots <> "" Then
            Dim buffer() As String = Split(Q.settings.Plots, "|")
            For Each plot As String In buffer
                If plot.Length > 1 Then
                    lstPlots.Items.Add(plot)
                End If
            Next
        End If
    End Sub

End Class