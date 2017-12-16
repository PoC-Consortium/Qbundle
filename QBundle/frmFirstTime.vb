Public Class frmFirstTime
    Private SelectedDBType As Integer = 0
    Private DbVerified As Boolean = False
    Private Chosen As Boolean = False

#Region " DB Selection "



    Private Sub ChangeButton(ByVal Dbver As Integer)
        If Chosen Then Exit Sub 
        'disable all
        r0.Checked = False
        r1.Checked = False
        r2.Checked = False
        r3.Checked = False

        P0.BackColor = Color.White
        P1.BackColor = Color.White
        P2.BackColor = Color.White
        P3.BackColor = Color.White
        SelectedDBType = Dbver
        Select Case Dbver
            Case QGlobal.DbType.H2
                r0.Checked = True
                P0.BackColor = SystemColors.GradientInactiveCaption
            Case QGlobal.DbType.FireBird
                r1.Checked = True
                P1.BackColor = SystemColors.GradientInactiveCaption
            Case QGlobal.DbType.pMariaDB
                r2.Checked = True
                P2.BackColor = SystemColors.GradientInactiveCaption
            Case QGlobal.DbType.MariaDB
                r3.Checked = True
                P3.BackColor = SystemColors.GradientInactiveCaption
        End Select
    End Sub

    'h2
    Private Sub lblH2Desc_Click(sender As Object, e As EventArgs) Handles lblH2Desc.Click
        ChangeButton(QGlobal.DbType.H2)
    End Sub
    Private Sub lblH2Header_Click(sender As Object, e As EventArgs) Handles lblH2Header.Click
        ChangeButton(QGlobal.DbType.H2)
    End Sub
    Private Sub r4_Click(sender As Object, e As EventArgs) Handles r0.Click
        ChangeButton(QGlobal.DbType.H2)
    End Sub
    Private Sub P0_Click(sender As Object, e As EventArgs) Handles P0.Click
        ChangeButton(QGlobal.DbType.H2)
    End Sub

    'FireBird
    Private Sub P1_Click(sender As Object, e As EventArgs) Handles P1.Click
        ChangeButton(QGlobal.DbType.FireBird)
    End Sub
    Private Sub r1_Click(sender As Object, e As EventArgs) Handles r1.Click
        ChangeButton(QGlobal.DbType.FireBird)
    End Sub
    Private Sub lblFireBirdHeader_Click(sender As Object, e As EventArgs) Handles lblFireBirdHeader.Click
        ChangeButton(QGlobal.DbType.FireBird)
    End Sub
    Private Sub lblFireBirdDesc_Click(sender As Object, e As EventArgs) Handles lblFireBirdDesc.Click
        ChangeButton(QGlobal.DbType.FireBird)
    End Sub

    'PMaria
    Private Sub P2_Click(sender As Object, e As EventArgs) Handles P2.Click
        ChangeButton(QGlobal.DbType.pMariaDB)
    End Sub
    Private Sub lblPMariaHeader_Click(sender As Object, e As EventArgs) Handles lblPMariaHeader.Click
        ChangeButton(QGlobal.DbType.pMariaDB)
    End Sub
    Private Sub lblPMariaDesc_Click(sender As Object, e As EventArgs) Handles lblPMariaDesc.Click
        ChangeButton(QGlobal.DbType.pMariaDB)
    End Sub
    Private Sub r2_click(sender As Object, e As EventArgs) Handles r2.Click
        ChangeButton(QGlobal.DbType.pMariaDB)
    End Sub

    'DB Own
    Private Sub P3_Click(sender As Object, e As EventArgs) Handles P3.Click
        ChangeButton(QGlobal.DbType.MariaDB)
    End Sub
    Private Sub lblOwnHeader_Click(sender As Object, e As EventArgs) Handles lblOwnHeader.Click
        ChangeButton(QGlobal.DbType.MariaDB)
    End Sub
    Private Sub lblOwnDesc_Click(sender As Object, e As EventArgs) Handles lblOwnDesc.Click
        ChangeButton(QGlobal.DbType.MariaDB)
    End Sub
    Private Sub r3_Click(sender As Object, e As EventArgs) Handles r3.Click
        ChangeButton(QGlobal.DbType.MariaDB)
    End Sub

#End Region

    Private Sub frmFirstTime_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = 674
        Me.Height = 418 ' 405
        If QB.Generic.DebugMe Then
            Me.Text = Me.Text & " (DebugMode)"
        End If
        ' Me.DialogResult = DialogResult.No

    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Chosen = True
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

        Select Case SelectedDBType
            Case QGlobal.DbType.H2
                pnlDb.BackColor = Color.PaleGreen
                lblDbHeader.Text = "H2"
                lblDBstatus.Text = "H2 embedded does not require aditional components."
            Case QGlobal.DbType.FireBird
                pnlDb.BackColor = Color.PaleGreen
                lblDbHeader.Text = "Firebird"
                lblDBstatus.Text = "Firebird embedded does not require aditional components."
            Case QGlobal.DbType.pMariaDB
                If Q.App.isInstalled(QGlobal.AppNames.MariaPortable) Then
                    pnlDb.BackColor = Color.PaleGreen
                    lblDbHeader.Text = "MariaDB"
                    lblDBstatus.Text = "MariaDB was found as a portable version."
                Else
                    pnlDb.BackColor = Color.LightCoral
                    lblDbHeader.Text = "MariaDB"
                    lblDBstatus.Text = "MariaDB was not found." & vbCrLf & "Use download components to download a portable version."
                    btnDone.Enabled = False
                    btnDownload.Enabled = True
                End If
            Case QGlobal.DbType.MariaDB 'we require settings
                btnDone.Enabled = False
                lblDbHeader.Text = "MariaDB / Mysql"
                lblDBstatus.Text = "Use settings below to configure the database settings."
                pnlDb.BackColor = Color.LightCoral
                pnlDb.Height = 180
                pnlMariaSettings.Visible = True
        End Select
        pnlWiz1.Visible = False
        PnlWiz2.Top = pnlWiz1.Top
        PnlWiz2.Left = pnlWiz1.Left
        PnlWiz2.Visible = True


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        PnlWiz2.Visible = False
        pnlWiz1.Visible = True
        Chosen = False
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Me.Hide()
        Dim S As frmDownloadExtract
        Dim res As DialogResult
        btnBack.Enabled = False
        btnDownload.Enabled = False
        btnDone.Enabled = False
        Q.App.SetLocalInfo()
        If Not Q.App.isInstalled(QGlobal.AppNames.JavaInstalled) And Not Q.App.isInstalled(QGlobal.AppNames.JavaPortable) Then
            S = New frmDownloadExtract
            S.Appid = QGlobal.AppNames.JavaPortable
            res = S.ShowDialog()
            If res = DialogResult.Abort Then
                lblStatusInfo.Text = "Error: Internet is unreachable or repository offline."
                lblStatusInfo.Visible = True
                btnDownload.Enabled = True
                btnBack.Enabled = True
                S = Nothing
                Me.Show()
                Exit Sub
            End If
            If res = DialogResult.Cancel Then
                lblStatusInfo.Text = "Error: You have aborted the download."
                lblStatusInfo.Visible = True
                btnDownload.Enabled = True
                btnBack.Enabled = True
                S = Nothing
                Me.Show()
                Exit Sub
            End If
            S = Nothing
            'now java is installed
            pnlJava.BackColor = Color.PaleGreen
            lblJavaStatus.Text = "Java was found in a portable version."
        End If
        Q.App.SetLocalInfo()
        If SelectedDBType = QGlobal.DbType.pMariaDB And Not Q.App.isInstalled(QGlobal.AppNames.MariaPortable) Then
            S = New frmDownloadExtract
            S.Appid = QGlobal.AppNames.MariaPortable
            S.DialogResult = Nothing
            res = S.ShowDialog()
            If res = DialogResult.Abort Then
                lblStatusInfo.Text = "Error: Internet is unreachable or repository offline."
                lblStatusInfo.Visible = True
                btnDownload.Enabled = True
                btnBack.Enabled = True
                S = Nothing
                Me.Show()
                Exit Sub
            End If
            If res = DialogResult.Cancel Then
                lblStatusInfo.Text = "Error: You have canceled the download."
                lblStatusInfo.Visible = True
                btnDownload.Enabled = True
                btnBack.Enabled = True
                S = Nothing
                Me.Show()
                Exit Sub
            End If
            S = Nothing
            pnlDb.BackColor = Color.PaleGreen
            lblDBstatus.Text = "MariaDB was found as a portable version."
        End If
        Q.App.SetLocalInfo()
        If SelectedDBType = QGlobal.DbType.MariaDB And DbVerified Then
            btnDone.Enabled = True
        ElseIf SelectedDBType <> QGlobal.DbType.MariaDB Then
            btnDone.Enabled = True
        End If
        Me.Show()
    End Sub
    Private Sub btnDone_Click(sender As Object, e As EventArgs) Handles btnDone.Click
        Q.settings.DbType = SelectedDBType
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

        Q.settings.DbName = txtDbName.Text
        Q.settings.DbPass = txtDbPass.Text
        Q.settings.DbUser = txtDbUser.Text
        Q.settings.DbServer = txtDbAddress.Text
        Q.settings.FirstRun = False


        Dim CurVer As Integer = Reflection.Assembly.GetExecutingAssembly.GetName.Version.Major * 10
        CurVer += Reflection.Assembly.GetExecutingAssembly.GetName.Version.Minor
        Q.settings.Upgradev = CurVer


        'temp untill repo in place
        '  BWL.settings.SaveSettings()
        '  BWL.Generic.WriteNRSConfig()
        '  Me.Close()

        'Temp unavail untill repo in place
        PnlWiz2.Visible = False
        PnlWiz3.Top = pnlWiz1.Top
        PnlWiz3.Left = pnlWiz1.Left
        PnlWiz3.Visible = True

    End Sub

    Private Sub txtDbAddress_TextChanged(sender As Object, e As EventArgs) Handles txtDbAddress.TextChanged
        CheckMariaSettings()
    End Sub

    Private Sub txtDbName_TextChanged(sender As Object, e As EventArgs) Handles txtDbName.TextChanged
        CheckMariaSettings()
    End Sub

    Private Sub txtDbUser_TextChanged(sender As Object, e As EventArgs) Handles txtDbUser.TextChanged
        CheckMariaSettings()
    End Sub

    Private Sub txtDbPass_TextChanged(sender As Object, e As EventArgs) Handles txtDbPass.TextChanged
        CheckMariaSettings()
    End Sub
    Private Sub CheckMariaSettings()
        Dim Ok As Boolean = True
        If Not txtDbAddress.Text <> "" Then Ok = False
        If Not txtDbName.Text <> "" Then Ok = False
        If Not txtDbUser.Text <> "" Then Ok = False
        If Not txtDbPass.Text <> "" Then Ok = False

        If Ok = False Then
            pnlDb.BackColor = Color.LightCoral
            DbVerified = False
        Else
            pnlDb.BackColor = Color.PaleGreen
            DbVerified = True
        End If
        'also check that we have downloaded all
        If DbVerified = True Then
            If Q.App.isInstalled(QGlobal.AppNames.JavaInstalled) Or Q.App.isInstalled(QGlobal.AppNames.JavaPortable) Then
                btnDone.Enabled = True
            End If
        Else
            btnDone.Enabled = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        PnlWiz3.Visible = False
        PnlWiz2.Visible = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Q.settings.QBMode = 0
        If Q.App.CheckOpenCL() Then Q.settings.useOpenCL = True
        Q.settings.SaveSettings()
        'writing brs.properties since we need it for tools.

        QB.Generic.WriteWalletConfig()
        If rYes.Checked Then
            Me.DialogResult = DialogResult.Yes
        Else
            Me.DialogResult = DialogResult.No
            'frmImport.Show()
        End If
        '  Me.Close()
    End Sub
End Class