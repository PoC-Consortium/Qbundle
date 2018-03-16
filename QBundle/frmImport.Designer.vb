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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkStartWallet = New System.Windows.Forms.CheckBox()
        Me.cmbRepo = New System.Windows.Forms.ComboBox()
        Me.r1 = New System.Windows.Forms.RadioButton()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pb1 = New System.Windows.Forms.ProgressBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Location = New System.Drawing.Point(8, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(472, 99)
        Me.Panel1.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(29, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 13)
        Me.Label2.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(403, 26)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "This feature will download a verified chain from one of the built in repositories" &
    "." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "It will allow you do get started much quicker than downloading and verify fro" &
    "m peers."
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(8, 11)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(120, 16)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "Bootstrap chain."
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.chkStartWallet)
        Me.Panel2.Controls.Add(Me.cmbRepo)
        Me.Panel2.Controls.Add(Me.r1)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Location = New System.Drawing.Point(8, 112)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(472, 123)
        Me.Panel2.TabIndex = 12
        '
        'chkStartWallet
        '
        Me.chkStartWallet.AutoSize = True
        Me.chkStartWallet.Checked = True
        Me.chkStartWallet.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkStartWallet.Location = New System.Drawing.Point(23, 89)
        Me.chkStartWallet.Name = "chkStartWallet"
        Me.chkStartWallet.Size = New System.Drawing.Size(178, 17)
        Me.chkStartWallet.TabIndex = 14
        Me.chkStartWallet.Text = "Start wallet when import is done."
        Me.chkStartWallet.UseVisualStyleBackColor = True
        '
        'cmbRepo
        '
        Me.cmbRepo.FormattingEnabled = True
        Me.cmbRepo.Location = New System.Drawing.Point(50, 62)
        Me.cmbRepo.Name = "cmbRepo"
        Me.cmbRepo.Size = New System.Drawing.Size(367, 21)
        Me.cmbRepo.TabIndex = 10
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
        Me.lblStatus.Location = New System.Drawing.Point(43, 238)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(24, 13)
        Me.lblStatus.TabIndex = 18
        Me.lblStatus.Text = "Idle"
        '
        'pb1
        '
        Me.pb1.Location = New System.Drawing.Point(8, 254)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(368, 17)
        Me.pb1.TabIndex = 17
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 238)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Status:"
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(382, 242)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(98, 30)
        Me.btnStart.TabIndex = 15
        Me.btnStart.Text = "Start Import"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(218, 73)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(238, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Note: This process can take a long time."
        '
        'frmImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(487, 281)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.pb1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmImport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import database"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents cmbRepo As ComboBox
    Friend WithEvents r1 As RadioButton
    Friend WithEvents Label12 As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents pb1 As ProgressBar
    Friend WithEvents Label6 As Label
    Friend WithEvents btnStart As Button
    Friend WithEvents chkStartWallet As CheckBox
    Friend WithEvents Label5 As Label
End Class
