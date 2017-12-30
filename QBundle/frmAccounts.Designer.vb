<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAccounts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAccounts))
        Me.lstAccounts = New System.Windows.Forms.ListBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnDel = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPublicKey = New System.Windows.Forms.TextBox()
        Me.txtNr = New System.Windows.Forms.TextBox()
        Me.txtRs = New System.Windows.Forms.TextBox()
        Me.txtPassprase = New System.Windows.Forms.TextBox()
        Me.txtPrivateKey = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.revealPwd = New System.Windows.Forms.PictureBox()
        Me.revealPk = New System.Windows.Forms.PictureBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.revealPwd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.revealPk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstAccounts
        '
        Me.lstAccounts.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstAccounts.FormattingEnabled = True
        Me.lstAccounts.ItemHeight = 18
        Me.lstAccounts.Location = New System.Drawing.Point(6, 19)
        Me.lstAccounts.Name = "lstAccounts"
        Me.lstAccounts.Size = New System.Drawing.Size(196, 166)
        Me.lstAccounts.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.btnDel)
        Me.GroupBox1.Controls.Add(Me.lstAccounts)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(209, 225)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Accounts"
        '
        'btnDel
        '
        Me.btnDel.Location = New System.Drawing.Point(107, 191)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(95, 26)
        Me.btnDel.TabIndex = 1
        Me.btnDel.Text = "Delete account"
        Me.btnDel.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(6, 191)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(95, 26)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "Add account"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.revealPk)
        Me.GroupBox2.Controls.Add(Me.revealPwd)
        Me.GroupBox2.Controls.Add(Me.lblName)
        Me.GroupBox2.Controls.Add(Me.txtPrivateKey)
        Me.GroupBox2.Controls.Add(Me.txtPassprase)
        Me.GroupBox2.Controls.Add(Me.txtRs)
        Me.GroupBox2.Controls.Add(Me.txtNr)
        Me.GroupBox2.Controls.Add(Me.txtPublicKey)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(239, 22)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(496, 224)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Account information"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 164)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Private Key:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Public Key:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(259, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Numeric Account nr:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 121)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Passphrase:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 41)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Burst RS address:"
        '
        'txtPublicKey
        '
        Me.txtPublicKey.Location = New System.Drawing.Point(15, 98)
        Me.txtPublicKey.Name = "txtPublicKey"
        Me.txtPublicKey.ReadOnly = True
        Me.txtPublicKey.Size = New System.Drawing.Size(467, 20)
        Me.txtPublicKey.TabIndex = 7
        '
        'txtNr
        '
        Me.txtNr.Location = New System.Drawing.Point(260, 57)
        Me.txtNr.Name = "txtNr"
        Me.txtNr.ReadOnly = True
        Me.txtNr.Size = New System.Drawing.Size(222, 20)
        Me.txtNr.TabIndex = 8
        '
        'txtRs
        '
        Me.txtRs.Location = New System.Drawing.Point(15, 57)
        Me.txtRs.Name = "txtRs"
        Me.txtRs.ReadOnly = True
        Me.txtRs.Size = New System.Drawing.Size(222, 20)
        Me.txtRs.TabIndex = 9
        '
        'txtPassprase
        '
        Me.txtPassprase.Location = New System.Drawing.Point(15, 138)
        Me.txtPassprase.Name = "txtPassprase"
        Me.txtPassprase.ReadOnly = True
        Me.txtPassprase.Size = New System.Drawing.Size(441, 20)
        Me.txtPassprase.TabIndex = 10
        '
        'txtPrivateKey
        '
        Me.txtPrivateKey.Location = New System.Drawing.Point(15, 180)
        Me.txtPrivateKey.Name = "txtPrivateKey"
        Me.txtPrivateKey.ReadOnly = True
        Me.txtPrivateKey.Size = New System.Drawing.Size(441, 20)
        Me.txtPrivateKey.TabIndex = 11
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(47, 22)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(39, 13)
        Me.lblName.TabIndex = 12
        Me.lblName.Text = "Label7"
        '
        'revealPwd
        '
        Me.revealPwd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.revealPwd.Image = CType(resources.GetObject("revealPwd.Image"), System.Drawing.Image)
        Me.revealPwd.Location = New System.Drawing.Point(462, 138)
        Me.revealPwd.Name = "revealPwd"
        Me.revealPwd.Size = New System.Drawing.Size(20, 20)
        Me.revealPwd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.revealPwd.TabIndex = 13
        Me.revealPwd.TabStop = False
        '
        'revealPk
        '
        Me.revealPk.Cursor = System.Windows.Forms.Cursors.Hand
        Me.revealPk.Image = CType(resources.GetObject("revealPk.Image"), System.Drawing.Image)
        Me.revealPk.Location = New System.Drawing.Point(462, 180)
        Me.revealPk.Name = "revealPk"
        Me.revealPk.Size = New System.Drawing.Size(20, 20)
        Me.revealPk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.revealPk.TabIndex = 14
        Me.revealPk.TabStop = False
        '
        'frmAccounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(769, 265)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmAccounts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Account Manager"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.revealPwd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.revealPk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lstAccounts As ListBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnDel As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents lblName As Label
    Friend WithEvents txtPrivateKey As TextBox
    Friend WithEvents txtPassprase As TextBox
    Friend WithEvents txtRs As TextBox
    Friend WithEvents txtNr As TextBox
    Friend WithEvents txtPublicKey As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents revealPwd As PictureBox
    Friend WithEvents revealPk As PictureBox
End Class
