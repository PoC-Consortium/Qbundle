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


    End Sub
End Class