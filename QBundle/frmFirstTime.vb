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
            Case DbType.H2
                r0.Checked = True
                P0.BackColor = SystemColors.GradientInactiveCaption
            Case DbType.FireBird
                r1.Checked = True
                P1.BackColor = SystemColors.GradientInactiveCaption
            Case DbType.pMariaDB
                r2.Checked = True
                P2.BackColor = SystemColors.GradientInactiveCaption
            Case DbType.MariaDB
                r3.Checked = True
                P3.BackColor = SystemColors.GradientInactiveCaption
        End Select
    End Sub

    'h2
    Private Sub lblH2Desc_Click(sender As Object, e As EventArgs) Handles lblH2Desc.Click
        ChangeButton(DbType.H2)
    End Sub
    Private Sub lblH2Header_Click(sender As Object, e As EventArgs) Handles lblH2Header.Click
        ChangeButton(DbType.H2)
    End Sub
    Private Sub r4_Click(sender As Object, e As EventArgs) Handles r0.Click
        ChangeButton(DbType.H2)
    End Sub
    Private Sub P0_Click(sender As Object, e As EventArgs) Handles P0.Click
        ChangeButton(DbType.H2)
    End Sub

    'FireBird
    Private Sub P1_Click(sender As Object, e As EventArgs) Handles P1.Click
        ChangeButton(DbType.FireBird)
    End Sub
    Private Sub r1_Click(sender As Object, e As EventArgs) Handles r1.Click
        ChangeButton(DbType.FireBird)
    End Sub
    Private Sub lblFireBirdHeader_Click(sender As Object, e As EventArgs) Handles lblFireBirdHeader.Click
        ChangeButton(DbType.FireBird)
    End Sub
    Private Sub lblFireBirdDesc_Click(sender As Object, e As EventArgs) Handles lblFireBirdDesc.Click
        ChangeButton(DbType.FireBird)
    End Sub

    'PMaria
    Private Sub P2_Click(sender As Object, e As EventArgs) Handles P2.Click
        ChangeButton(DbType.pMariaDB)
    End Sub
    Private Sub lblPMariaHeader_Click(sender As Object, e As EventArgs) Handles lblPMariaHeader.Click
        ChangeButton(DbType.pMariaDB)
    End Sub
    Private Sub lblPMariaDesc_Click(sender As Object, e As EventArgs) Handles lblPMariaDesc.Click
        ChangeButton(DbType.pMariaDB)
    End Sub
    Private Sub r2_click(sender As Object, e As EventArgs) Handles r2.Click
        ChangeButton(DbType.pMariaDB)
    End Sub

    'DB Own
    Private Sub P3_Click(sender As Object, e As EventArgs) Handles P3.Click
        ChangeButton(DbType.MariaDB)
    End Sub
    Private Sub lblOwnHeader_Click(sender As Object, e As EventArgs) Handles lblOwnHeader.Click
        ChangeButton(DbType.MariaDB)
    End Sub
    Private Sub lblOwnDesc_Click(sender As Object, e As EventArgs) Handles lblOwnDesc.Click
        ChangeButton(DbType.MariaDB)
    End Sub
    Private Sub r3_Click(sender As Object, e As EventArgs) Handles r3.Click
        ChangeButton(DbType.MariaDB)
    End Sub

#End Region

    Private Sub frmFirstTime_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = 674
        Me.Height = 418 ' 405
        If QB.Generic.DebugMe Then
            Me.Text = Me.Text & " (DebugMode)"
        End If
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
        If App.isInstalled(AppNames.JavaInstalled) Then
            pnlJava.BackColor = Color.PaleGreen
            lblJavaStatus.Text = "Java was found installed."
        ElseIf App.isInstalled(AppNames.JavaPortable) Then
            pnlJava.BackColor = Color.PaleGreen
            lblJavaStatus.Text = "Java was found in a portable version."
        Else
            pnlJava.BackColor = Color.LightCoral
            lblJavaStatus.Text = "Java is not found." & vbCrLf & "Use download components to download a portable version."
            'offer the download
            btnDone.Enabled = False
            btnDownload.Enabled = True
        End If

        'set DB panel

        Select Case SelectedDBType
            Case DbType.H2
                pnlDb.BackColor = Color.PaleGreen
                lblDbHeader.Text = "H2"
                lblDBstatus.Text = "H2 embedded does not require aditional components."
            Case DbType.FireBird
                pnlDb.BackColor = Color.PaleGreen
                lblDbHeader.Text = "Firebird"
                lblDBstatus.Text = "Firebird embedded does not require aditional components."
            Case DbType.pMariaDB
                If App.isInstalled(AppNames.MariaPortable) Then
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
            Case DbType.MariaDB 'we require settings
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

        Dim S As frmDownloadExtract
        Dim res As DialogResult
        btnBack.Enabled = False
        btnDownload.Enabled = False
        btnDone.Enabled = False
        App.SetLocalInfo()
        If Not App.isInstalled(AppNames.JavaInstalled) And Not App.isInstalled(AppNames.JavaPortable) Then
            S = New frmDownloadExtract
            S.Appid = AppNames.JavaPortable
            res = S.ShowDialog()
            If res = DialogResult.Abort Then
                lblStatusInfo.Text = "Error: Internet is unreachable or repository offline."
                lblStatusInfo.Visible = True
                btnDownload.Enabled = True
                btnBack.Enabled = True
                S = Nothing

                Exit Sub
            End If
            If res = DialogResult.Cancel Then
                lblStatusInfo.Text = "Error: You have aborted the download."
                lblStatusInfo.Visible = True
                btnDownload.Enabled = True
                btnBack.Enabled = True
                S = Nothing

                Exit Sub
            End If
            S = Nothing
            'now java is installed
            pnlJava.BackColor = Color.PaleGreen
            lblJavaStatus.Text = "Java was found in a portable version."
        End If
        App.SetLocalInfo()
        If SelectedDBType = DbType.pMariaDB And Not App.isInstalled(AppNames.MariaPortable) Then
            S = New frmDownloadExtract
            S.Appid = AppNames.MariaPortable
            S.DialogResult = Nothing
            res = S.ShowDialog()
            If res = DialogResult.Abort Then
                lblStatusInfo.Text = "Error: Internet is unreachable or repository offline."
                lblStatusInfo.Visible = True
                btnDownload.Enabled = True
                btnBack.Enabled = True
                S = Nothing

                Exit Sub
            End If
            If res = DialogResult.Cancel Then
                lblStatusInfo.Text = "Error: You have canceled the download."
                lblStatusInfo.Visible = True
                btnDownload.Enabled = True
                btnBack.Enabled = True
                S = Nothing

                Exit Sub
            End If
            S = Nothing
            pnlDb.BackColor = Color.PaleGreen
            lblDBstatus.Text = "MariaDB was found as a portable version."
        End If
        App.SetLocalInfo()
        If SelectedDBType = DbType.MariaDB And DbVerified Then
            btnDone.Enabled = True
        ElseIf SelectedDBType <> DbType.MariaDB Then
            btnDone.Enabled = True
        End If

    End Sub
    Private Sub btnDone_Click(sender As Object, e As EventArgs) Handles btnDone.Click
        QB.settings.DbType = SelectedDBType
        If App.isInstalled(AppNames.JavaInstalled) Then
            QB.settings.JavaType = AppNames.JavaInstalled
        Else
            QB.settings.JavaType = AppNames.JavaPortable
        End If
        QB.settings.CheckForUpdates = chkUpdates.Checked
        QB.settings.DbName = txtDbName.Text
        QB.settings.DbPass = txtDbPass.Text
        QB.settings.DbUser = txtDbUser.Text
        QB.settings.DbServer = txtDbAddress.Text
        QB.settings.FirstRun = False


        Dim CurVer As Integer = Reflection.Assembly.GetExecutingAssembly.GetName.Version.Major * 10
        CurVer += Reflection.Assembly.GetExecutingAssembly.GetName.Version.Minor
        QB.settings.Upgradev = CurVer


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
            If App.isInstalled(AppNames.JavaInstalled) Or App.isInstalled(AppNames.JavaPortable) Then
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
        QB.settings.SaveSettings()
        'writing brs.properties since we need it for tools.
        QB.Generic.WriteNRSConfig()
        If rYes.Checked Then
            frmImport.Show()

        End If

        Me.Close()
    End Sub
End Class