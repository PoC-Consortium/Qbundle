<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmImport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImport))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.txtFile = New System.Windows.Forms.TextBox()
        Me.txtUrl = New System.Windows.Forms.TextBox()
        Me.cmbRepo = New System.Windows.Forms.ComboBox()
        Me.r3 = New System.Windows.Forms.RadioButton()
        Me.r2 = New System.Windows.Forms.RadioButton()
        Me.r1 = New System.Windows.Forms.RadioButton()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pb1 = New System.Windows.Forms.ProgressBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.chkStartWallet = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Location = New System.Drawing.Point(8, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(472, 144)
        Me.Panel1.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(218, 119)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(238, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Note: This process can take a long time."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(29, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "3. Import from file." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(29, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(215, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "2. Download and import from a url you enter."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(29, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(285, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "1. Download and import from one of the built in repositories."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(193, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "This can be done in following scenarios"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(29, 31)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(289, 13)
        Me.Label14.TabIndex = 1
        Me.Label14.Text = "This will allow you to import a database from a former export."
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(8, 11)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(121, 16)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "Import database"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.chkStartWallet)
        Me.Panel2.Controls.Add(Me.btnBrowse)
        Me.Panel2.Controls.Add(Me.txtFile)
        Me.Panel2.Controls.Add(Me.txtUrl)
        Me.Panel2.Controls.Add(Me.cmbRepo)
        Me.Panel2.Controls.Add(Me.r3)
        Me.Panel2.Controls.Add(Me.r2)
        Me.Panel2.Controls.Add(Me.r1)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Location = New System.Drawing.Point(8, 158)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(472, 220)
        Me.Panel2.TabIndex = 12
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(417, 160)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(33, 22)
        Me.btnBrowse.TabIndex = 13
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'txtFile
        '
        Me.txtFile.Location = New System.Drawing.Point(50, 161)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.Size = New System.Drawing.Size(367, 20)
        Me.txtFile.TabIndex = 12
        '
        'txtUrl
        '
        Me.txtUrl.Location = New System.Drawing.Point(50, 112)
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.Size = New System.Drawing.Size(367, 20)
        Me.txtUrl.TabIndex = 11
        '
        'cmbRepo
        '
        Me.cmbRepo.FormattingEnabled = True
        Me.cmbRepo.Location = New System.Drawing.Point(50, 62)
        Me.cmbRepo.Name = "cmbRepo"
        Me.cmbRepo.Size = New System.Drawing.Size(367, 21)
        Me.cmbRepo.TabIndex = 10
        '
        'r3
        '
        Me.r3.AutoSize = True
        Me.r3.Location = New System.Drawing.Point(32, 138)
        Me.r3.Name = "r3"
        Me.r3.Size = New System.Drawing.Size(93, 17)
        Me.r3.TabIndex = 9
        Me.r3.Text = "Import from file"
        Me.r3.UseVisualStyleBackColor = True
        '
        'r2
        '
        Me.r2.AutoSize = True
        Me.r2.Location = New System.Drawing.Point(32, 89)
        Me.r2.Name = "r2"
        Me.r2.Size = New System.Drawing.Size(91, 17)
        Me.r2.TabIndex = 8
        Me.r2.Text = "Import from url"
        Me.r2.UseVisualStyleBackColor = True
        '
        'r1
        '
        Me.r1.AutoSize = True
        Me.r1.Checked = True
        Me.r1.Location = New System.Drawing.Point(32, 39)
        Me.r1.Name = "r1"
        Me.r1.Size = New System.Drawing.Size(125, 17)
        Me.r1.TabIndex = 7
        Me.r1.TabStop = True
        Me.r1.Text = "Import from repository"
        Me.r1.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 11)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 16)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Settings"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(43, 381)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(24, 13)
        Me.lblStatus.TabIndex = 18
        Me.lblStatus.Text = "Idle"
        '
        'pb1
        '
        Me.pb1.Location = New System.Drawing.Point(8, 397)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(368, 17)
        Me.pb1.TabIndex = 17
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 381)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Status:"
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(382, 385)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(98, 30)
        Me.btnStart.TabIndex = 15
        Me.btnStart.Text = "Start Import"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'chkStartWallet
        '
        Me.chkStartWallet.AutoSize = True
        Me.chkStartWallet.Checked = True
        Me.chkStartWallet.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkStartWallet.Location = New System.Drawing.Point(32, 191)
        Me.chkStartWallet.Name = "chkStartWallet"
        Me.chkStartWallet.Size = New System.Drawing.Size(178, 17)
        Me.chkStartWallet.TabIndex = 14
        Me.chkStartWallet.Text = "Start wallet when import is done."
        Me.chkStartWallet.UseVisualStyleBackColor = True
        '
        'frmImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(487, 422)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.pb1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmImport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import database"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txtFile As TextBox
    Friend WithEvents txtUrl As TextBox
    Friend WithEvents cmbRepo As ComboBox
    Friend WithEvents r3 As RadioButton
    Friend WithEvents r2 As RadioButton
    Friend WithEvents r1 As RadioButton
    Friend WithEvents Label12 As Label
    Friend WithEvents btnBrowse As Button
    Friend WithEvents lblStatus As Label
    Friend WithEvents pb1 As ProgressBar
    Friend WithEvents Label6 As Label
    Friend WithEvents btnStart As Button
    Friend WithEvents chkStartWallet As CheckBox
End Class
