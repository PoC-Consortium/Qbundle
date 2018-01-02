Public Class frmDynamicPlotting
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        'we do not care about settings if disabled
        If rDisable.Checked = True Then
            Q.settings.DynPlotEnabled = False
            Q.settings.SaveSettings()
            Me.Close()
        End If

        'we are enabled. lets make checks
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
        End If

        Q.settings.DynPlotEnabled = True

        If rEnable.Checked = True Then

        Else
            Q.settings.DynPlotEnabled = False
        End If
        Q.settings.DynPlotPath = txtPath.Text
        Q.settings.DynPlotAcc = txtAccount.Text
        Q.settings.DynPlotSize = HSSize.Value
        Q.settings.DynPlotFree = trFreeSpace.Value
        Q.settings.DynPlotHide = chkHide.Checked
        Q.settings.SaveSettings()

        Me.Close()
    End Sub

    Private Sub frmDynamicPlotting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Q.settings.DynPlotEnabled = True Then
            rEnable.Checked = True
            rDisable.Checked = False
        End If
        txtPath.Text = Q.settings.DynPlotPath
        txtAccount.Text = Q.settings.DynPlotAcc
        HSSize.Value = Q.settings.DynPlotSize
        trFreeSpace.Value = Q.settings.DynPlotFree
        chkHide.Checked = Q.settings.DynPlotHide

        Dim TotalSpace As Long
        If Q.settings.DynPlotEnabled Then
            Try
                TotalSpace = My.Computer.FileSystem.GetDriveInfo(Q.settings.DynPlotPath).TotalSize  'bytes
                TotalSpace = CLng(Math.Floor(TotalSpace / 1024 / 1024 / 1024)) 'GiB
                trFreeSpace.Minimum = 1
                trFreeSpace.Maximum = CInt(TotalSpace)
            Catch ex As Exception

            End Try
        End If
        txtPath.Text = Q.settings.DynPlotPath
        txtAccount.Text = Q.settings.DynPlotAcc
        HSSize.Value = Q.settings.DynPlotSize
        trFreeSpace.Value = Q.settings.DynPlotFree
        chkHide.Checked = Q.settings.DynPlotHide
        lblPlotSize.Text = HSSize.Value.ToString & "GiB"
        lblFreeSpace.Text = CStr(trFreeSpace.Value) & "GiB (" & Math.Floor((trFreeSpace.Value / TotalSpace) * 100).ToString & "%)"
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


    End Sub
    Private Sub SelectAccountID(sender As Object, e As EventArgs)
        Dim mnuitm As ToolStripMenuItem = Nothing
        Try
            mnuitm = DirectCast(sender, ToolStripMenuItem)
        Catch ex As Exception
            If Generic.DebugMe Then Generic.WriteDebug(ex.StackTrace, ex.Message)
            Exit Sub
        End Try
        txtAccount.Text = Q.Accounts.GetAccountID(mnuitm.Text)

    End Sub
    Private Sub rDisable_CheckedChanged(sender As Object, e As EventArgs) Handles rDisable.Click
        pnlOnOff.Enabled = False

    End Sub
    Private Sub rEnable_CheckedChanged(sender As Object, e As EventArgs) Handles rEnable.CheckedChanged
        pnlOnOff.Enabled = True
    End Sub
    Private Sub btnPath_Click(sender As Object, e As EventArgs) Handles btnPath.Click
        Dim FD As New FolderBrowserDialog
        If FD.ShowDialog() = DialogResult.OK Then
            txtPath.Text = FD.SelectedPath
            If IO.Path.GetPathRoot(FD.SelectedPath) = FD.SelectedPath Then
                MsgBox("Xplotter does not allow to plot directly to root path of a drive. Create a directory and put your plots in there.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong path")
            End If
            Dim TotalSpace As Long = My.Computer.FileSystem.GetDriveInfo(txtPath.Text).TotalSize  'bytes
            TotalSpace = CLng(Math.Floor(TotalSpace / 1024 / 1024 / 1024)) 'GiB
            trFreeSpace.Minimum = 1
            trFreeSpace.Maximum = CInt(TotalSpace)
            trFreeSpace.Value = CInt(TotalSpace / 10) 'set 10% as start value
            lblFreeSpace.Text = CStr(TotalSpace / 10) & "GiB (10%)"

        End If
    End Sub
    Private Sub HSSize_Scroll(sender As Object, e As EventArgs) Handles HSSize.ValueChanged
        lblPlotSize.Text = HSSize.Value.ToString & "GiB"
    End Sub
    Private Sub trFreeSpace_Scroll(sender As Object, e As EventArgs) Handles trFreeSpace.ValueChanged
        Try
            Dim TotalSpace As Long = My.Computer.FileSystem.GetDriveInfo(txtPath.Text).TotalSize  'bytes
            TotalSpace = CLng(Math.Floor(TotalSpace / 1024 / 1024 / 1024)) 'GiB
            lblFreeSpace.Text = CStr(trFreeSpace.Value) & "GiB (" & Math.Floor((trFreeSpace.Value / TotalSpace) * 100).ToString & "%)"
        Catch ex As Exception

        End Try
    End Sub

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
                Q.settings.Plots &= lstPlots.Items.Item(t).ToString & "|"
            Next
            Q.settings.SaveSettings()

        End If
    End Sub

    Private Sub ImportFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportFileToolStripMenuItem.Click
        Dim ofd As New OpenFileDialog
        If ofd.ShowDialog = DialogResult.OK Then
            If IO.File.Exists(ofd.FileName) Then
                lstPlots.Items.Add(ofd.FileName)
                Q.settings.Plots &= ofd.FileName & "|"
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

                Q.settings.SaveSettings()
            End If
        End If
    End Sub
End Class