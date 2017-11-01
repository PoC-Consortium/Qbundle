<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChangeDatabase
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChangeDatabase))
        Me.pnlttl = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblCurDB = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pnlMariaSettings = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDbPass = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtDbUser = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtDbName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDbAddress = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.rDB4 = New System.Windows.Forms.RadioButton()
        Me.rDB3 = New System.Windows.Forms.RadioButton()
        Me.rDB2 = New System.Windows.Forms.RadioButton()
        Me.rDB1 = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pb1 = New System.Windows.Forms.ProgressBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.rOP2 = New System.Windows.Forms.RadioButton()
        Me.rOP1 = New System.Windows.Forms.RadioButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblFromTo = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.pnlWiz1 = New System.Windows.Forms.Panel()
        Me.pnlWiz2 = New System.Windows.Forms.Panel()
        Me.pnlttl.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlMariaSettings.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlWiz1.SuspendLayout()
        Me.pnlWiz2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlttl
        '
        Me.pnlttl.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.pnlttl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlttl.Controls.Add(Me.Label12)
        Me.pnlttl.Controls.Add(Me.lblCurDB)
        Me.pnlttl.Controls.Add(Me.Label14)
        Me.pnlttl.Controls.Add(Me.Label15)
        Me.pnlttl.Location = New System.Drawing.Point(5, 7)
        Me.pnlttl.Name = "pnlttl"
        Me.pnlttl.Size = New System.Drawing.Size(472, 71)
        Me.pnlttl.TabIndex = 12
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(29, 47)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(253, 13)
        Me.Label12.TabIndex = 3
        Me.Label12.Text = "Please select the database you want to use instead."
        '
        'lblCurDB
        '
        Me.lblCurDB.AutoSize = True
        Me.lblCurDB.Location = New System.Drawing.Point(227, 32)
        Me.lblCurDB.Name = "lblCurDB"
        Me.lblCurDB.Size = New System.Drawing.Size(21, 13)
        Me.lblCurDB.TabIndex = 2
        Me.lblCurDB.Text = "H2"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(29, 31)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(203, 13)
        Me.Label14.TabIndex = 1
        Me.Label14.Text = "You are currently using the databasetype:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(8, 11)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(131, 16)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "Change database"
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(383, 275)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(84, 27)
        Me.btnNext.TabIndex = 13
        Me.btnNext.Text = "Next >>"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.pnlMariaSettings)
        Me.Panel2.Controls.Add(Me.btnNext)
        Me.Panel2.Controls.Add(Me.rDB4)
        Me.Panel2.Controls.Add(Me.rDB3)
        Me.Panel2.Controls.Add(Me.rDB2)
        Me.Panel2.Controls.Add(Me.rDB1)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Location = New System.Drawing.Point(5, 84)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(472, 307)
        Me.Panel2.TabIndex = 14
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
        Me.pnlMariaSettings.Controls.Add(Me.Label1)
        Me.pnlMariaSettings.Controls.Add(Me.txtDbAddress)
        Me.pnlMariaSettings.Controls.Add(Me.Label13)
        Me.pnlMariaSettings.Enabled = False
        Me.pnlMariaSettings.Location = New System.Drawing.Point(32, 133)
        Me.pnlMariaSettings.Name = "pnlMariaSettings"
        Me.pnlMariaSettings.Size = New System.Drawing.Size(387, 126)
        Me.pnlMariaSettings.TabIndex = 15
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(99, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(132, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "note: Database must exist."
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Username:"
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
        'rDB4
        '
        Me.rDB4.AutoSize = True
        Me.rDB4.Location = New System.Drawing.Point(32, 110)
        Me.rDB4.Name = "rDB4"
        Me.rDB4.Size = New System.Drawing.Size(129, 17)
        Me.rDB4.TabIndex = 4
        Me.rDB4.Text = "Own MariaDB / Mysql"
        Me.rDB4.UseVisualStyleBackColor = True
        '
        'rDB3
        '
        Me.rDB3.AutoSize = True
        Me.rDB3.Location = New System.Drawing.Point(32, 87)
        Me.rDB3.Name = "rDB3"
        Me.rDB3.Size = New System.Drawing.Size(108, 17)
        Me.rDB3.TabIndex = 3
        Me.rDB3.Text = "Portable MariaDB"
        Me.rDB3.UseVisualStyleBackColor = True
        '
        'rDB2
        '
        Me.rDB2.AutoSize = True
        Me.rDB2.Location = New System.Drawing.Point(32, 64)
        Me.rDB2.Name = "rDB2"
        Me.rDB2.Size = New System.Drawing.Size(59, 17)
        Me.rDB2.TabIndex = 2
        Me.rDB2.Text = "Firebird"
        Me.rDB2.UseVisualStyleBackColor = True
        '
        'rDB1
        '
        Me.rDB1.AutoSize = True
        Me.rDB1.Checked = True
        Me.rDB1.Location = New System.Drawing.Point(32, 41)
        Me.rDB1.Name = "rDB1"
        Me.rDB1.Size = New System.Drawing.Size(39, 17)
        Me.rDB1.TabIndex = 1
        Me.rDB1.TabStop = True
        Me.rDB1.Text = "H2"
        Me.rDB1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(153, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Select new database"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.btnBack)
        Me.Panel5.Controls.Add(Me.Label8)
        Me.Panel5.Controls.Add(Me.lblStatus)
        Me.Panel5.Controls.Add(Me.pb1)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Controls.Add(Me.btnStart)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.rOP2)
        Me.Panel5.Controls.Add(Me.rOP1)
        Me.Panel5.Controls.Add(Me.Label9)
        Me.Panel5.Location = New System.Drawing.Point(5, 84)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(472, 307)
        Me.Panel5.TabIndex = 15
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(7, 275)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(84, 27)
        Me.btnBack.TabIndex = 24
        Me.btnBack.Text = "<< Back"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(29, 33)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(210, 13)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Select the option you would like to perform."
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(40, 234)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(24, 13)
        Me.lblStatus.TabIndex = 22
        Me.lblStatus.Text = "Idle"
        '
        'pb1
        '
        Me.pb1.Location = New System.Drawing.Point(7, 250)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(453, 17)
        Me.pb1.TabIndex = 21
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(4, 234)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Status:"
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(370, 273)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(90, 27)
        Me.btnStart.TabIndex = 19
        Me.btnStart.Text = "Start copy"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(49, 138)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(245, 39)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "No data from the old database will be copied. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "If you do not have a database in " &
    "place already" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "you will have to sync the chain from the beginning."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(47, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(387, 26)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "This option will export all data from the old database and import it to the new o" &
    "ne." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "This process can take a long time but will be much faster than to start fre" &
    "sh."
        '
        'rOP2
        '
        Me.rOP2.AutoSize = True
        Me.rOP2.Location = New System.Drawing.Point(32, 120)
        Me.rOP2.Name = "rOP2"
        Me.rOP2.Size = New System.Drawing.Size(69, 17)
        Me.rOP2.TabIndex = 3
        Me.rOP2.Text = "No Copy."
        Me.rOP2.UseVisualStyleBackColor = True
        '
        'rOP1
        '
        Me.rOP1.AutoSize = True
        Me.rOP1.Checked = True
        Me.rOP1.Location = New System.Drawing.Point(32, 59)
        Me.rOP1.Name = "rOP1"
        Me.rOP1.Size = New System.Drawing.Size(73, 17)
        Me.rOP1.TabIndex = 2
        Me.rOP1.TabStop = True
        Me.rOP1.Text = "Copy data"
        Me.rOP1.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(8, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 16)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Options"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.lblFromTo)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Location = New System.Drawing.Point(5, 7)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(472, 71)
        Me.Panel3.TabIndex = 16
        '
        'lblFromTo
        '
        Me.lblFromTo.AutoSize = True
        Me.lblFromTo.Location = New System.Drawing.Point(245, 31)
        Me.lblFromTo.Name = "lblFromTo"
        Me.lblFromTo.Size = New System.Drawing.Size(70, 13)
        Me.lblFromTo.TabIndex = 2
        Me.lblFromTo.Text = "H2 to Firebird"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(29, 31)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(220, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "You have selected to change database from:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 11)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(77, 16)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Transition"
        '
        'pnlWiz1
        '
        Me.pnlWiz1.Controls.Add(Me.pnlttl)
        Me.pnlWiz1.Controls.Add(Me.Panel2)
        Me.pnlWiz1.Location = New System.Drawing.Point(1, -1)
        Me.pnlWiz1.Name = "pnlWiz1"
        Me.pnlWiz1.Size = New System.Drawing.Size(487, 397)
        Me.pnlWiz1.TabIndex = 17
        '
        'pnlWiz2
        '
        Me.pnlWiz2.Controls.Add(Me.Panel3)
        Me.pnlWiz2.Controls.Add(Me.Panel5)
        Me.pnlWiz2.Location = New System.Drawing.Point(494, -1)
        Me.pnlWiz2.Name = "pnlWiz2"
        Me.pnlWiz2.Size = New System.Drawing.Size(487, 397)
        Me.pnlWiz2.TabIndex = 18
        '
        'frmChangeDatabase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(983, 396)
        Me.Controls.Add(Me.pnlWiz2)
        Me.Controls.Add(Me.pnlWiz1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmChangeDatabase"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Database"
        Me.pnlttl.ResumeLayout(False)
        Me.pnlttl.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlMariaSettings.ResumeLayout(False)
        Me.pnlMariaSettings.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.pnlWiz1.ResumeLayout(False)
        Me.pnlWiz2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlttl As Panel
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents btnNext As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents rDB4 As RadioButton
    Friend WithEvents rDB3 As RadioButton
    Friend WithEvents rDB2 As RadioButton
    Friend WithEvents rDB1 As RadioButton
    Friend WithEvents Label2 As Label
    Friend WithEvents pnlMariaSettings As Panel
    Friend WithEvents txtDbPass As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents txtDbUser As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txtDbName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtDbAddress As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents rOP2 As RadioButton
    Friend WithEvents rOP1 As RadioButton
    Friend WithEvents Label9 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents pb1 As ProgressBar
    Friend WithEvents Label6 As Label
    Friend WithEvents btnStart As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents lblCurDB As Label
    Friend WithEvents lblFromTo As Label
    Friend WithEvents btnBack As Button
    Friend WithEvents pnlWiz1 As Panel
    Friend WithEvents pnlWiz2 As Panel
End Class
