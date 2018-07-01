Public Class frmFirstTime

#Region " DB Selection "



#End Region

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

        Dim Ok As Boolean = True

        'Set Default settings
        btnDone.Enabled = True
        btnDownload.Enabled = False
        pnlDb.Height = pnlJava.Height
        pnlDb.Visible = True
        pnlMariaSettings.Visible = False

        'Set java panael
        If Q.App.isInstalled(QGlobal.AppNames.JavaInstalled) Then
            pnlJava.BackColor = Color.PaleGreen
            lblJavaStatus.Text = "Java was found installed."
        ElseIf Q.App.isInstalled(QGlobal.AppNames.JavaPortable) Then
            pnlJava.BackColor = Color.PaleGreen
            lblJavaStatus.Text = "Java was found in a portable version."
        Else
            pnlJava.BackColor = Color.LightCoral
            lblJavaStatus.Text = Q.App.getAppMessage(QGlobal.AppNames.JavaInstalled) & vbCrLf & "Use download components to download a portable version."
            'offer the download
            btnDone.Enabled = False
            btnDownload.Enabled = True
        End If

        'set DB panel


        pnlDb.BackColor = Color.PaleGreen
        lblDbHeader.Text = "H2"
        lblDBstatus.Text = "H2 embedded does not require aditional components."

        PnlWiz2.Visible = True

    End Sub
    Private Sub btnNext_Click(sender As Object, e As EventArgs)
        FirstNext()

    End Sub



    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Me.Hide()

        btnBack.Enabled = False
        btnDownload.Enabled = False
        btnDone.Enabled = False
        Q.App.SetLocalInfo()
        Q.App.SetRemoteInfo()
        If Not Q.AppManager.isJavaInstalled() Then
            Q.AppManager.InstallApp("JavaPortable")
            'now java is installed
            pnlJava.BackColor = Color.PaleGreen
            lblJavaStatus.Text = "Java was found in a portable version."
        End If

        Q.App.SetLocalInfo()

        Me.Show()
    End Sub
    Private Sub btnDone_Click(sender As Object, e As EventArgs) Handles btnDone.Click

        If Q.App.isInstalled(QGlobal.AppNames.JavaInstalled) Then
            Q.settings.JavaType = QGlobal.AppNames.JavaInstalled
        Else
            Q.settings.JavaType = QGlobal.AppNames.JavaPortable
        End If
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


        Dim CurVer As Integer = Reflection.Assembly.GetExecutingAssembly.GetName.Version.Major * 10
        CurVer += Reflection.Assembly.GetExecutingAssembly.GetName.Version.Minor
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