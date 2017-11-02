Public Class frmUpdate


    Private WithEvents tmr As New Timer
    Private Sub frmUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not App.SetRemoteInfo() Then
            MsgBox("There was an error getting update info. Check internet connection and try again.")
            Me.Close()
        End If
        If CheckAndUpdateLW() Then
            btnUpdate.Enabled = True
        Else
            btnUpdate.Enabled = False
        End If

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        If frmMain.Running Then
            If MsgBox("Do you want to stop the wallet?" & vbCrLf & " It must be stopped before updating the components.", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Exit Sub
            End If
        End If
        If App.ShouldUpdate(AppNames.Launcher) Then
            If MsgBox("Burst wallet launcher will automatically restart after update." & vbCrLf & " Do you want to continue?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Exit Sub
            End If
        End If
        btnUpdate.Enabled = False
        If frmMain.Running Then
            lblStatus.Text = "Waiting for wallet to stop"
            If QB.settings.DbType = DbType.pMariaDB Then 'send startsequence
                Dim Pid(1) As Object
                Pid(0) = AppNames.BRS
                Pid(1) = AppNames.MariaPortable
                ProcHandler.StopProcessSquence(Pid)
            Else
                ProcHandler.StopProcess(AppNames.BRS)
            End If
            tmr.Interval = 500
            tmr.Start()
            tmr.Enabled = True
        Else
            DoUpdate()
        End If

    End Sub

    Public Sub tmr_tick() Handles tmr.Tick
        If frmMain.Running = False Then
            tmr.Stop()
            tmr.Enabled = False
            DoUpdate()
        End If
    End Sub

    Private Function CheckAndUpdateLW() As Boolean

        Dim StrApp As String() = [Enum].GetNames(GetType(AppNames)) 'only used to count
        Dim L(2) As String
        Dim AnyUpdates As Boolean = False
        Lw1.Items.Clear()
        For t As Integer = 0 To UBound(StrApp)
            If App.isInstalled(t) Then 'no reason to test non installed
                If App.HasRepository(t) Then 'Is it available at repo?
                    L(0) = App.GetAppNameFromId(t)
                    L(1) = App.GetLocalVersion(t)
                    L(2) = App.GetRemoteVersion(t)
                    Dim itm As New ListViewItem(L)

                    If App.ShouldUpdate(t) Then
                        itm.SubItems(1).ForeColor = Color.DarkRed
                        AnyUpdates = True
                    Else
                        itm.SubItems(1).ForeColor = Color.DarkGreen
                    End If

                    itm.UseItemStyleForSubItems = False
                    Lw1.Items.Add(itm)
                End If
            End If
        Next

        Return AnyUpdates


    End Function

    Private Sub DoUpdate()

        Dim S As frmDownloadExtract
        Dim AppCount As Integer = UBound([Enum].GetNames(GetType(AppNames)))
        CheckAndUpdateLW()
        Dim res As DialogResult
        For t As Integer = 0 To AppCount
            If App.ShouldUpdate(t) Then
                S = New frmDownloadExtract
                S.Appid = t
                res = S.ShowDialog
                If res = DialogResult.Cancel Then
                    btnUpdate.Enabled = True
                    Exit Sub
                ElseIf res = DialogResult.Abort Then
                    MsgBox("Something went wrong. Internet connection might have been lost.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
                    btnUpdate.Enabled = True
                    Exit Sub
                End If
                CheckAndUpdateLW()
            End If
        Next

        If App.isUpdated(AppNames.Launcher) Then
            'we should restart now since we have updates pending on ourselfs
            Dim wdir As String = Application.StartupPath
            If Not wdir.EndsWith("\") Then wdir &= "\"
            If IO.File.Exists(wdir & "Restarter.exe") Then
                Dim p As Process = New Process
                p.StartInfo.WorkingDirectory = wdir
                p.StartInfo.Arguments = "BWLUpdate" & " " & IO.Path.GetFileName(Application.ExecutablePath)
                p.StartInfo.UseShellExecute = True
                p.StartInfo.FileName = wdir & "Restarter.exe"
                p.Start()
                End
            End If
        End If
        frmMain.lblUpdates.Visible = False
        pb1.Visible = False
        lblStatus.Text = "Update complete."
    End Sub



End Class