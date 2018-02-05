Imports System.Net
Imports System.Net.NetworkInformation

Public Class frmSettings
    Private JavaType As Integer

#Region " Form events "
    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSettings()
    End Sub
    Private Sub TabControl1_DrawItem(ByVal sender As Object, ByVal e As DrawItemEventArgs) Handles TabControl1.DrawItem
        Dim g As Graphics = e.Graphics
        Dim _TB As Brush
        Dim _TP As TabPage = TabControl1.TabPages(e.Index)
        Dim _TA As Rectangle = TabControl1.GetTabRect(e.Index)
        If (e.State = DrawItemState.Selected) Then
            _TB = New SolidBrush(Color.Black)
            g.FillRectangle(Brushes.SkyBlue, e.Bounds)
        Else
            _TB = New SolidBrush(e.ForeColor)
            e.DrawBackground()
        End If
        Dim _TF As New Font("Arial", 12.0, FontStyle.Bold, GraphicsUnit.Pixel)
        Dim _strFlags As New StringFormat()
        _strFlags.Alignment = StringAlignment.Center
        _strFlags.LineAlignment = StringAlignment.Center
        g.DrawString(_TP.Text, _TF, _TB, _TA, New StringFormat(_strFlags))
    End Sub
#End Region

#Region " Clickable objects "
    Private Sub rJava0_Click(sender As Object, e As EventArgs) Handles rJava0.Click
        ChangeJavaType(QGlobal.AppNames.JavaInstalled)
    End Sub
    Private Sub rJava1_Click(sender As Object, e As EventArgs) Handles rJava1.Click
        ChangeJavaType(QGlobal.AppNames.JavaPortable)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SaveSettings()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SaveSettings()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        SaveSettings()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim buffer() As String = Split(txtAddAllow.Text, "/")

        Dim ip As IPAddress = Nothing

        Select Case UBound(buffer)
            Case 0
                If Not IPAddress.TryParse(buffer(0), ip) Then
                    MsgBox("You have not entered a valid IP.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Wrong format")
                    Exit Sub
                End If

                lstConnectFrom.Items.Add(buffer(0))
            Case 1
                If Not IPAddress.TryParse(buffer(0), ip) Then
                    MsgBox("You have not entered a valid cidr network.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Wrong format")
                    Exit Sub
                End If
                If buffer(0).Contains(":") Then 'ipv6
                    If Val(buffer(1)) > 128 Or Val(buffer(1)) < 0 Then
                        MsgBox("You have not entered a valid ipv6 cidr network.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Wrong format")
                        Exit Sub
                    End If

                Else 'ipv4
                    If Val(buffer(1)) > 32 Or Val(buffer(1)) < 0 Then
                        MsgBox("You have not entered a valid  ipv4 cidr network.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Wrong format")
                        Exit Sub
                    End If
                End If
                lstConnectFrom.Items.Add(buffer(0) & "/" & buffer(1))
            Case Else
                MsgBox("You have not entered a valid IP or network.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Wrong format")
        End Select
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If lstConnectFrom.SelectedIndex = -1 Then Exit Sub
        lstConnectFrom.Items.RemoveAt(lstConnectFrom.SelectedIndex)
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        SaveSettings()
    End Sub
#End Region

    Private Sub SaveSettings()

        Dim buffer As String = ""

        'generic
        Q.settings.CheckForUpdates = chkCheckForUpdates.Checked
        Q.settings.AlwaysAdmin = chkAlwaysAdmin.Checked
        Q.settings.WalletException = chkWalletException.Checked
        Q.settings.UseOnlineWallet = chkOnlineWallets.Checked
        Q.settings.NTPCheck = chkNTP.Checked
        Q.settings.MinToTray = chkMinToTray.Checked
        Q.settings.GetCoinMarket = chkCoinmarket.Checked
        If cmbCurrency.SelectedIndex > -1 Then
            Q.settings.Currency = cmbCurrency.Items.Item(cmbCurrency.SelectedIndex).ToString
        End If

        'nrs
        Q.settings.AutoStart = chkAutoStart.Checked
        Q.settings.AutoIp = chkAutoIP.Checked
        Q.settings.DynPlatform = chkDynPlatform.Checked
        'nrs advanced
        Q.settings.Cpulimit = CInt(nrCores.Value)
        Q.settings.useOpenCL = chkOpenCL.Checked
        'nrs net
        If cmbListen.SelectedIndex = 0 Then
            Q.settings.ListenIf = "0.0.0.0" & ";" & CStr(nrListenPort.Value)
        Else
            Q.settings.ListenIf = cmbListen.Items.Item(cmbListen.SelectedIndex).ToString & ";" & CStr(nrListenPort.Value)
        End If
        If cmbPeerIP.SelectedIndex = 0 Then
            Q.settings.ListenPeer = "0.0.0.0" & ";" & CStr(nrPeerPort.Value)
        Else
            Q.settings.ListenPeer = cmbPeerIP.Items.Item(cmbPeerIP.SelectedIndex).ToString & ";" & CStr(nrPeerPort.Value)
        End If
        For x As Integer = 0 To lstConnectFrom.Items.Count - 1
            buffer &= lstConnectFrom.Items.Item(x).ToString & ";"
        Next

        Q.settings.Broadcast = txtBroadcast.Text

        Q.settings.ConnectFrom = buffer
        Q.settings.DbServer = txtDbServer.Text

        Q.settings.DbUser = txtDbUser.Text
        Q.settings.DbPass = txtDbPass.Text

        Q.settings.JavaType = JavaType
        Q.settings.DebugMode = chkDebug.Checked
        Q.settings.SaveSettings()
        If Q.settings.DebugMode Then
            Generic.DebugMe = True
        Else
            Generic.DebugMe = False
        End If
        'ok lets fix firewall if its intended to be like that
        Me.Close()


    End Sub
    Private Sub LoadSettings()
        If Not Q.App.isInstalled(QGlobal.AppNames.JavaInstalled) Then rJava0.Enabled = False
        If Not Q.App.isInstalled(QGlobal.AppNames.JavaPortable) Then rJava1.Enabled = False

        pnlMaria.Enabled = True
        pnlDbSettings.Enabled = True
        lblDb.Text = Q.App.GetDbNameFromType(Q.settings.DbType)

        chkCheckForUpdates.Checked = Q.settings.CheckForUpdates
        chkAlwaysAdmin.Checked = Q.settings.AlwaysAdmin
        chkDebug.Checked = Q.settings.DebugMode
        chkWalletException.Checked = Q.settings.WalletException
        chkOnlineWallets.Checked = Q.settings.UseOnlineWallet
        chkNTP.Checked = Q.settings.NTPCheck
        chkMinToTray.Checked = Q.settings.MinToTray
        chkCoinmarket.Checked = Q.settings.GetCoinMarket
        chkAutoStart.Checked = Q.settings.AutoStart
        chkAutoIP.Checked = Q.settings.AutoIp
        If Q.settings.AutoIp Then
            txtBroadcast.Enabled = False
            txtBroadcast.Text = Generic.GetMyIp()
        Else
            txtBroadcast.Enabled = True
            txtBroadcast.Text = Q.settings.Broadcast
        End If
        For t As Integer = 0 To cmbCurrency.Items.Count - 1
            If cmbCurrency.Items.Item(t).ToString = Q.settings.Currency Then
                cmbCurrency.SelectedIndex = t
                Exit For
            End If
        Next

        chkDynPlatform.Checked = Q.settings.DynPlatform
        txtDbServer.Text = Q.settings.DbServer
        txtDbUser.Text = Q.settings.DbUser
        txtDbPass.Text = Q.settings.DbPass
        ChangeJavaType(Q.settings.JavaType)
        If Q.App.CheckOpenCL() Then
            chkOpenCL.Checked = Q.settings.useOpenCL
        Else
            chkOpenCL.Enabled = False
            chkOpenCL.Checked = False
        End If

        nrCores.Maximum = Environment.ProcessorCount
        nrCores.Value = Q.settings.Cpulimit
        lblMaxCores.Text = CStr(Environment.ProcessorCount) & " cores."
        Select Case Environment.ProcessorCount
            Case 1
                lblRecommendedCPU.Text = CStr(1) & " cores."
            Case 2
                lblRecommendedCPU.Text = CStr(1) & " cores."
            Case 4
                lblRecommendedCPU.Text = CStr(3) & " cores."
            Case Else
                lblRecommendedCPU.Text = CStr(Environment.ProcessorCount - 2) & " cores."
        End Select





        'SetNRSnet
        SetIf()
        SetAllowedIP()
    End Sub

    Private Sub nrCores_ValueChanged(sender As Object, e As EventArgs)
        If nrCores.Value > Environment.ProcessorCount Then nrCores.Value = Environment.ProcessorCount
        If nrCores.Value < 1 Then nrCores.Value = 1

    End Sub
    Private Sub ChangeJavaType(ByVal id As Integer)
        rJava0.Checked = False
        rJava1.Checked = False
        Select Case id
            Case QGlobal.AppNames.JavaInstalled
                rJava0.Checked = True
            Case QGlobal.AppNames.JavaPortable
                rJava1.Checked = True
            Case Else
                rJava1.Checked = True
                id = QGlobal.AppNames.JavaPortable
        End Select
        JavaType = id
    End Sub
    Private Sub SetIf()
        Dim adapters As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
        Dim adapter As NetworkInterface
        cmbListen.Items.Clear()
        cmbPeerIP.Items.Clear()
        cmbListen.Items.Add("All Interfaces")
        cmbPeerIP.Items.Add("All Interfaces")
        cmbListen.Items.Add("127.0.0.1")
        cmbPeerIP.Items.Add("127.0.0.1")

        For Each adapter In adapters
            Dim properties As IPInterfaceProperties = adapter.GetIPProperties()
            For Each address In properties.UnicastAddresses
                'If address.Address.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                If adapter.OperationalStatus = OperationalStatus.Up And adapter.NetworkInterfaceType = NetworkInterfaceType.Ethernet Then
                    If address.Address.ToString.Contains("%") Then
                        Dim buffer As String = address.Address.ToString.Remove(address.Address.ToString.IndexOf("%"))
                        cmbListen.Items.Add(buffer)
                        cmbPeerIP.Items.Add(buffer)
                    Else
                        cmbListen.Items.Add(address.Address.ToString)
                        cmbPeerIP.Items.Add(address.Address.ToString)
                    End If


                End If
                'End If
            Next
        Next adapter


        Dim S() As String = Nothing
        Try
            S = Split(Q.settings.ListenPeer, ";")
            nrPeerPort.Value = CDec(Val(S(1)))
            If S(0) = "0.0.0.0" Then
                cmbPeerIP.SelectedIndex = 0
            Else
                For t As Integer = 0 To cmbListen.Items.Count - 1
                    If cmbPeerIP.Items.Item(t).ToString = S(0) Then cmbListen.SelectedIndex = t
                Next
            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try


        Try
            S = Split(Q.settings.ListenIf, ";")
            nrListenPort.Value = CDec(Val(S(1)))
            If S(0) = "0.0.0.0" Then
                cmbListen.SelectedIndex = 0
            Else
                For t As Integer = 0 To cmbListen.Items.Count - 1
                    If cmbListen.Items.Item(t).ToString = S(0) Then cmbListen.SelectedIndex = t
                Next
            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try


    End Sub
    Private Sub SetAllowedIP()

        Dim s() As String = Split(Q.settings.ConnectFrom, ";")
        lstConnectFrom.Items.Clear()
        For Each netw As String In s
            If Trim(netw) <> "" Then
                lstConnectFrom.Items.Add(Trim(netw))
            End If
        Next

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If MsgBox("Are you sure you want to reset to default values?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Set default values") = MsgBoxResult.Yes Then
            txtDbServer.Text = QGlobal.Dbinfo(Q.settings.DbType).ConnString
            txtDbUser.Text = QGlobal.Dbinfo(Q.settings.DbType).Username
            txtDbPass.Text = QGlobal.Dbinfo(Q.settings.DbType).Pass
        End If
    End Sub

    Private Sub chkAutoIP_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoIP.Click
        If chkAutoIP.Checked Then
            txtBroadcast.Enabled = False
        Else
            txtBroadcast.Enabled = True
        End If
    End Sub
End Class