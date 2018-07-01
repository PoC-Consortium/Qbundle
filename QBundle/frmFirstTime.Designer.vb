<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmFirstTime
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFirstTime))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PnlWiz2 = New System.Windows.Forms.Panel()
        Me.chkUpdates = New System.Windows.Forms.CheckBox()
        Me.pnlDb = New System.Windows.Forms.Panel()
        Me.pnlMariaSettings = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDbPass = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtDbUser = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtDbName = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtDbAddress = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblDbHeader = New System.Windows.Forms.Label()
        Me.lblDBstatus = New System.Windows.Forms.Label()
        Me.lblStatusInfo = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnDownload = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pnlJava = New System.Windows.Forms.Panel()
        Me.lblJavaStatus = New System.Windows.Forms.Label()
        Me.lblJavaHeader = New System.Windows.Forms.Label()
        Me.btnDone = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlWiz2.SuspendLayout()
        Me.pnlDb.SuspendLayout()
        Me.pnlMariaSettings.SuspendLayout()
        Me.pnlJava.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.PictureBox1.Location = New System.Drawing.Point(-1, -2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(239, 381)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label7.Font = New System.Drawing.Font("Rockwell", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(18, 276)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(78, 19)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Qbundle"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label8.Location = New System.Drawing.Point(19, 298)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(201, 39)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "We have now checked the environment." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "If anything is missing please use" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "download" &
    " missing components."
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label9.Font = New System.Drawing.Font("Rockwell", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(18, 19)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(182, 19)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Welcome to Burstcoin"
        '
        'PnlWiz2
        '
        Me.PnlWiz2.AutoScroll = True
        Me.PnlWiz2.Controls.Add(Me.chkUpdates)
        Me.PnlWiz2.Controls.Add(Me.pnlDb)
        Me.PnlWiz2.Controls.Add(Me.lblStatusInfo)
        Me.PnlWiz2.Controls.Add(Me.btnBack)
        Me.PnlWiz2.Controls.Add(Me.btnDownload)
        Me.PnlWiz2.Controls.Add(Me.Label12)
        Me.PnlWiz2.Controls.Add(Me.pnlJava)
        Me.PnlWiz2.Controls.Add(Me.btnDone)
        Me.PnlWiz2.Location = New System.Drawing.Point(238, -1)
        Me.PnlWiz2.Name = "PnlWiz2"
        Me.PnlWiz2.Size = New System.Drawing.Size(420, 380)
        Me.PnlWiz2.TabIndex = 12
        '
        'chkUpdates
        '
        Me.chkUpdates.AutoSize = True
        Me.chkUpdates.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkUpdates.Checked = True
        Me.chkUpdates.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUpdates.Location = New System.Drawing.Point(9, 340)
        Me.chkUpdates.Name = "chkUpdates"
        Me.chkUpdates.Size = New System.Drawing.Size(221, 30)
        Me.chkUpdates.TabIndex = 21
        Me.chkUpdates.Text = "Allow connection to remote resources for " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "verifications and updates."
        Me.chkUpdates.UseVisualStyleBackColor = True
        '
        'pnlDb
        '
        Me.pnlDb.BackColor = System.Drawing.Color.LightCoral
        Me.pnlDb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDb.Controls.Add(Me.pnlMariaSettings)
        Me.pnlDb.Controls.Add(Me.lblDbHeader)
        Me.pnlDb.Controls.Add(Me.lblDBstatus)
        Me.pnlDb.Cursor = System.Windows.Forms.Cursors.Default
        Me.pnlDb.Location = New System.Drawing.Point(9, 117)
        Me.pnlDb.Name = "pnlDb"
        Me.pnlDb.Size = New System.Drawing.Size(403, 180)
        Me.pnlDb.TabIndex = 15
        '
        'pnlMariaSettings
        '
        Me.pnlMariaSettings.BackColor = System.Drawing.Color.Transparent
        Me.pnlMariaSettings.Controls.Add(Me.Label5)
        Me.pnlMariaSettings.Controls.Add(Me.txtDbPass)
        Me.pnlMariaSettings.Controls.Add(Me.Label17)
        Me.pnlMariaSettings.Controls.Add(Me.txtDbUser)
        Me.pnlMariaSettings.Controls.Add(Me.Label16)
        Me.pnlMariaSettings.Controls.Add(Me.txtDbName)
        Me.pnlMariaSettings.Controls.Add(Me.Label15)
        Me.pnlMariaSettings.Controls.Add(Me.txtDbAddress)
        Me.pnlMariaSettings.Controls.Add(Me.Label13)
        Me.pnlMariaSettings.Location = New System.Drawing.Point(10, 46)
        Me.pnlMariaSettings.Name = "pnlMariaSettings"
        Me.pnlMariaSettings.Size = New System.Drawing.Size(387, 126)
        Me.pnlMariaSettings.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(99, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(258, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "note: Database must exist but schema will be created"
        '
        'txtDbPass
        '
        Me.txtDbPass.Location = New System.Drawing.Point(102, 67)
        Me.txtDbPass.Name = "txtDbPass"
        Me.txtDbPass.Size = New System.Drawing.Size(270, 20)
        Me.txtDbPass.TabIndex = 7
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(15, 70)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(56, 13)
        Me.Label17.TabIndex = 6
        Me.Label17.Text = "Password:"
        '
        'txtDbUser
        '
        Me.txtDbUser.Location = New System.Drawing.Point(102, 46)
        Me.txtDbUser.Name = "txtDbUser"
        Me.txtDbUser.Size = New System.Drawing.Size(270, 20)
        Me.txtDbUser.TabIndex = 5
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(15, 28)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(56, 13)
        Me.Label16.TabIndex = 4
        Me.Label16.Text = "Database:"
        '
        'txtDbName
        '
        Me.txtDbName.Location = New System.Drawing.Point(102, 25)
        Me.txtDbName.Name = "txtDbName"
        Me.txtDbName.Size = New System.Drawing.Size(270, 20)
        Me.txtDbName.TabIndex = 3
        Me.txtDbName.Text = "burstwallet"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(15, 49)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(58, 13)
        Me.Label15.TabIndex = 2
        Me.Label15.Text = "Username:"
        '
        'txtDbAddress
        '
        Me.txtDbAddress.Location = New System.Drawing.Point(102, 4)
        Me.txtDbAddress.Name = "txtDbAddress"
        Me.txtDbAddress.Size = New System.Drawing.Size(270, 20)
        Me.txtDbAddress.TabIndex = 1
        Me.txtDbAddress.Text = "127.0.0.1:3306"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(15, 7)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(81, 13)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Server address:"
        '
        'lblDbHeader
        '
        Me.lblDbHeader.AutoSize = True
        Me.lblDbHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDbHeader.Location = New System.Drawing.Point(7, 12)
        Me.lblDbHeader.Name = "lblDbHeader"
        Me.lblDbHeader.Size = New System.Drawing.Size(61, 13)
        Me.lblDbHeader.TabIndex = 2
        Me.lblDbHeader.Text = "Database"
        '
        'lblDBstatus
        '
        Me.lblDBstatus.AutoSize = True
        Me.lblDBstatus.Location = New System.Drawing.Point(25, 30)
        Me.lblDBstatus.Name = "lblDBstatus"
        Me.lblDBstatus.Size = New System.Drawing.Size(157, 13)
        Me.lblDBstatus.TabIndex = 4
        Me.lblDBstatus.Text = "MariaDB is not yet downloaded."
        '
        'lblStatusInfo
        '
        Me.lblStatusInfo.AutoSize = True
        Me.lblStatusInfo.Location = New System.Drawing.Point(6, 303)
        Me.lblStatusInfo.Name = "lblStatusInfo"
        Me.lblStatusInfo.Size = New System.Drawing.Size(116, 13)
        Me.lblStatusInfo.TabIndex = 20
        Me.lblStatusInfo.Text = "Extracting Java Portble"
        Me.lblStatusInfo.Visible = False
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(244, 340)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(81, 31)
        Me.btnBack.TabIndex = 17
        Me.btnBack.Text = "<< Back"
        Me.btnBack.UseVisualStyleBackColor = True
        Me.btnBack.Visible = False
        '
        'btnDownload
        '
        Me.btnDownload.Location = New System.Drawing.Point(244, 303)
        Me.btnDownload.Name = "btnDownload"
        Me.btnDownload.Size = New System.Drawing.Size(168, 31)
        Me.btnDownload.TabIndex = 16
        Me.btnDownload.Text = "Download missing components"
        Me.btnDownload.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Rockwell", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(4, 11)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(276, 27)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "Checking environment."
        '
        'pnlJava
        '
        Me.pnlJava.BackColor = System.Drawing.Color.PaleGreen
        Me.pnlJava.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlJava.Controls.Add(Me.lblJavaStatus)
        Me.pnlJava.Controls.Add(Me.lblJavaHeader)
        Me.pnlJava.Cursor = System.Windows.Forms.Cursors.Default
        Me.pnlJava.Location = New System.Drawing.Point(9, 46)
        Me.pnlJava.Name = "pnlJava"
        Me.pnlJava.Size = New System.Drawing.Size(403, 65)
        Me.pnlJava.TabIndex = 13
        '
        'lblJavaStatus
        '
        Me.lblJavaStatus.AutoSize = True
        Me.lblJavaStatus.Location = New System.Drawing.Point(25, 27)
        Me.lblJavaStatus.Name = "lblJavaStatus"
        Me.lblJavaStatus.Size = New System.Drawing.Size(199, 13)
        Me.lblJavaStatus.TabIndex = 3
        Me.lblJavaStatus.Text = "Java was found installed on your system."
        '
        'lblJavaHeader
        '
        Me.lblJavaHeader.AutoSize = True
        Me.lblJavaHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJavaHeader.Location = New System.Drawing.Point(7, 12)
        Me.lblJavaHeader.Name = "lblJavaHeader"
        Me.lblJavaHeader.Size = New System.Drawing.Size(34, 13)
        Me.lblJavaHeader.TabIndex = 2
        Me.lblJavaHeader.Text = "Java"
        '
        'btnDone
        '
        Me.btnDone.Location = New System.Drawing.Point(331, 340)
        Me.btnDone.Name = "btnDone"
        Me.btnDone.Size = New System.Drawing.Size(81, 31)
        Me.btnDone.TabIndex = 12
        Me.btnDone.Text = "Continue >>"
        Me.btnDone.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.PictureBox2.Image = Global.QB.My.Resources.Resources.gb
        Me.PictureBox2.Location = New System.Drawing.Point(30, 73)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(166, 166)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 13
        Me.PictureBox2.TabStop = False
        '
        'frmFirstTime
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(662, 392)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PnlWiz2)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmFirstTime"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "First Time"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlWiz2.ResumeLayout(False)
        Me.PnlWiz2.PerformLayout()
        Me.pnlDb.ResumeLayout(False)
        Me.pnlDb.PerformLayout()
        Me.pnlMariaSettings.ResumeLayout(False)
        Me.pnlMariaSettings.PerformLayout()
        Me.pnlJava.ResumeLayout(False)
        Me.pnlJava.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents PnlWiz2 As Panel
    Friend WithEvents btnBack As Button
    Friend WithEvents btnDownload As Button
    Friend WithEvents pnlDb As Panel
    Friend WithEvents lblDbHeader As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents pnlJava As Panel
    Friend WithEvents lblJavaStatus As Label
    Friend WithEvents lblJavaHeader As Label
    Friend WithEvents btnDone As Button
    Friend WithEvents lblDBstatus As Label
    Friend WithEvents lblStatusInfo As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents pnlMariaSettings As Panel
    Friend WithEvents txtDbPass As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents txtDbUser As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txtDbName As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents txtDbAddress As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents chkUpdates As CheckBox
End Class
