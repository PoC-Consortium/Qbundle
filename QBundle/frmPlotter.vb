Imports System.ComponentModel

Public Class frmPlotter
    Private Sub btnPath_Click(sender As Object, e As EventArgs) Handles btnPath.Click
        Dim FD As New FolderBrowserDialog
        If FD.ShowDialog() = DialogResult.OK Then
            txtPath.Text = FD.SelectedPath
            Dim FreeSpace As Double = My.Computer.FileSystem.GetDriveInfo(txtPath.Text).TotalFreeSpace
            Dim nonces As Integer = Math.Floor(FreeSpace / 1024 / 256)
            HSSize.Maximum = CInt(nonces)
            HSSize.Minimum = 6
            HSSize.Value = CInt(nonces)
            lblTotalNonces.Text = nonces.ToString

            lblSize.Text = GetReadableSpace(FreeSpace) & " / " & GetReadableSpace(FreeSpace)


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


        cmlAccounts.Items.Clear()
        Dim mnuitm As ToolStripMenuItem
        For Each account As QB.clsAccounts.Account In Q.Accounts.AccArray
            mnuitm = New ToolStripMenuItem
            mnuitm.Name = account.AccountName
            mnuitm.Text = account.AccountName
            AddHandler(mnuitm.Click), AddressOf SelectAccountID
            cmlAccounts.Items.Add(mnuitm)
        Next


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
                res = s.ShowDialog
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
            'ok Xplotter is now installed

            'XPlotter_avx.exe -id 17930413153828766298 -sn 603000000 -n 800000 -t 6 -path H:\plots -mem 6G

            'check path
            Dim Path As String = txtPath.Text

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

            '14063911016565811119_40957504_40957504_40957504

            'check accountnr
            'Hur skall detta kollas?



            'check nonce overlap.
            If checkPlotOverLapp() = False Then
                MsgBox("You are trying to write a plot that overlap with an existing one. Please change your Startnonce.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Free space changed.")
                Exit Sub
            End If


            '




        End If
    End Sub

    Private Function checkPlotOverLapp() As Boolean

        Dim Plotfiles() As String
        Dim AccountID As String = txtAccount.Text
        Dim StartNonce As Double = CDbl(txtStartNonce.Text)
        Dim EndNonce As Double = StartNonce + HSSize.Value - 1
        Dim PStartNonce As Double = 0
        Dim PEndNonce As Double = 0

        '
        ' SCENARIOS TO COVER!
        '

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
                Next
            End If
        Catch ex As Exception
            Return False
        End Try



        Return True
    End Function

    Private Sub StartXplotter()




    End Sub

    Private Sub btnAccounts_Click(sender As Object, e As EventArgs) Handles btnAccounts.Click

        Try
            Me.cmlAccounts.Show(Me.btnAccounts, Me.btnAccounts.PointToClient(Cursor.Position))
        Catch ex As Exception

        End Try


    End Sub
End Class