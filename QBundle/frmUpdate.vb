Public Class frmUpdate

    Dim WalletWasRunning As Boolean
    Private WithEvents tmr As New Timer
    Private Sub frmUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lw1.CheckBoxes = True
        Q.AppManager.UpdateAppStoreInformation()
        UpdateLW()

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        WalletWasRunning = frmMain.Running

        'Check if lw1.Items(1).checked
        If lw1.Items(1).Checked Then
            'must stop brs wallet if it is running
        End If


        If WalletWasRunning Then
            frmMain.StopWallet()
            tmr.Interval = 1000
            tmr.Enabled = True
        Else
            ProcessDownload()
        End If

    End Sub

    Sub ProcessDownload()


        Dim item As ListViewItem
        For t As Integer = lw1.Items.Count - 1 To 0 Step -1
            item = lw1.Items(t)
            If item.Checked = True Then
                Q.AppManager.InstallApp(item.Tag)
            End If
        Next

        If lw1.Items(0).Checked Then

            'we need to restart Qbundle
        End If


        If WalletWasRunning Then
            frmMain.StartWallet()
        End If

        UpdateLW() 'show new versions


    End Sub



    Public Sub tmr_tick() Handles tmr.Tick
        If frmMain.Running = False Then
            tmr.Stop()
            tmr.Enabled = False
        End If
    End Sub

    Private Function UpdateLW() As Boolean

        Dim StrApp As String() = [Enum].GetNames(GetType(QGlobal.AppNames)) 'only used to count
        Dim L(2) As String
        Dim AnyUpdates As Boolean = False
        lw1.Items.Clear()
        Dim item1 As ListViewItem
        Dim AppName, DisplayName, LocalVer, RemoteVer As String ', AppInfo 
        Dim IsInstalled, IsRunning, NeedUpdate As Boolean

        For t As Integer = 0 To UBound(Q.AppManager.AppStore.Apps)
            AppName = Q.AppManager.AppStore.Apps(t).Name
            DisplayName = Q.AppManager.AppStore.Apps(t).DisplayName
            IsInstalled = Q.AppManager.IsAppInstalled(AppName)
            RemoteVer = Q.AppManager.GetAppStoreVersion(AppName)
            IsRunning = Q.AppManager.IsAppRunning(AppName)
            If IsInstalled Then
                LocalVer = Q.AppManager.GetInstalledVersion(AppName)
                NeedUpdate = Q.AppManager.DoesAppNeedUpdate(AppName)
            Else
                LocalVer = "-"
                NeedUpdate = False 'updated since it is not installed
            End If


            item1 = New ListViewItem("", 0)
            item1.UseItemStyleForSubItems = False

            item1.SubItems.Add(DisplayName)

            item1.SubItems.Add(LocalVer)
            item1.SubItems.Add(RemoteVer)

            item1.Tag = AppName
            If IsRunning Then
                item1.SubItems.Add("Is running")
            Else
                item1.SubItems.Add("-")
            End If
            If NeedUpdate Then
                item1.Checked = True
                item1.SubItems(2).ForeColor = Color.DarkRed
            Else
                item1.Checked = False
                item1.SubItems(2).ForeColor = Color.DarkGreen
            End If

            If IsRunning Then
                item1.Checked = False
                item1.SubItems(0).BackColor = Color.Gray
                item1.SubItems(1).BackColor = Color.Gray
                item1.SubItems(2).BackColor = Color.Gray
                item1.SubItems(3).BackColor = Color.Gray
                item1.SubItems(4).BackColor = Color.Gray
            End If
            lw1.Items.Add(item1)
        Next
        Return True
    End Function

    Private Sub lw1_ItemCheck1(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckedEventArgs) Handles lw1.ItemChecked
        If (e.Item.Checked = True) Then
            If e.Item.SubItems(4).Text = "Is running" Then
                MsgBox("The application you have choosen to update is running. You must close the application before any updates can be made.")
                e.Item.Checked = False
                lw1.Refresh()
            End If
        End If
    End Sub

    Private Sub lw1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lw1.SelectedIndexChanged

    End Sub
End Class