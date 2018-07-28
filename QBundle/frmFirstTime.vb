Public Class frmFirstTime


    Private Sub frmFirstTime_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.AutoScaleMode = AutoScaleMode.None

        Me.Width = PnlWiz2.Left + PnlWiz2.Width + 25
        Me.Height = PnlWiz2.Top + PnlWiz2.Height + 39

        Me.AutoScaleMode = AutoScaleMode.Font

        If QB.Generic.DebugMe Then
            Me.Text = Me.Text & " (DebugMode)"
        End If

        FirstNext()


    End Sub
    Private Sub FirstNext()
        SetInfo()
        PnlWiz2.Visible = True
    End Sub

    Sub SetInfo()

        Dim ok As Boolean = True

        If Not Q.AppManager.isJavaInstalled Then
            pnlJava.BackColor = Color.LightCoral
            lblJavaStatus.Text = Q.AppManager.Messages
            ok = False
        Else
            pnlJava.BackColor = Color.PaleGreen
            lblJavaStatus.Text = "Java is found installed-"
        End If

        If Not Q.AppManager.IsAppInstalled("BRS") Then
            pnlBRS.BackColor = Color.LightCoral
            lblBRS.Text = "The core wallet is not installed."
            ok = False
        Else
            pnlBRS.BackColor = Color.PaleGreen
            lblBRS.Text = "The core wallet is not installed."
        End If
        If ok Then
            btnDownload.Enabled = False
            btnDone.Enabled = True
        Else
            btnDownload.Enabled = True
            btnDone.Enabled = False
        End If

    End Sub



    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Me.Hide()


        If Not Q.AppManager.isJavaInstalled() Then
            Q.AppManager.InstallApp("JavaPortable")
        End If
        If Not Q.AppManager.IsAppInstalled("BRS") Then
            Q.AppManager.InstallApp("BRS")
        End If
        SetInfo()
        Me.Show()
    End Sub
    Private Sub btnDone_Click(sender As Object, e As EventArgs) Handles btnDone.Click
        Q.settings.CheckForUpdates = chkUpdates.Checked
        Q.settings.AutoIp = chkUpdates.Checked
        Q.settings.UseOnlineWallet = chkUpdates.Checked
        Q.settings.GetCoinMarket = chkUpdates.Checked
        Q.settings.NTPCheck = chkUpdates.Checked
        Q.settings.DbName = QGlobal.Dbinfo(0).Name
        Q.settings.DbPass = QGlobal.Dbinfo(0).Pass
        Q.settings.DbUser = QGlobal.Dbinfo(0).Username
        Q.settings.DbServer = QGlobal.Dbinfo(0).ConnString
        Q.settings.FirstRun = False
        Dim CurVer As Integer = Reflection.Assembly.GetExecutingAssembly.GetName.Version.Major * 100
        CurVer += Reflection.Assembly.GetExecutingAssembly.GetName.Version.Minor * 10
        CurVer += Reflection.Assembly.GetExecutingAssembly.GetName.Version.Revision
        Q.settings.Upgradev = CurVer
        finalSteps()

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs)

        PnlWiz2.Visible = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        finalSteps()

    End Sub

    Private Sub finalSteps()
        Try
            Q.settings.QBMode = 0
            Q.settings.SaveSettings()
        Catch ex As Exception

        End Try
        QB.Generic.WriteWalletConfig()
        Me.DialogResult = DialogResult.No


    End Sub
End Class