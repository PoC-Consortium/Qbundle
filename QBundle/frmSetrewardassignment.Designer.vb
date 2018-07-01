<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSetrewardassignment
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetrewardassignment))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbWallet = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPool = New System.Windows.Forms.TextBox()
        Me.btnPool = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtAccount = New System.Windows.Forms.TextBox()
        Me.btnAccounts = New System.Windows.Forms.Button()
        Me.cmlAccounts = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmlPools = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFee = New System.Windows.Forms.MaskedTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtFee)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.btnSend)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.cmbWallet)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtPool)
        Me.GroupBox2.Controls.Add(Me.btnPool)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtAccount)
        Me.GroupBox2.Controls.Add(Me.btnAccounts)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(391, 316)
        Me.GroupBox2.TabIndex = 16
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Set reward assignment"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(11, 277)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(137, 30)
        Me.Button1.TabIndex = 34
        Me.Button1.Text = "Check reward recipient"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 216)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(368, 52)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = resources.GetString("Label5.Text")
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(248, 277)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(132, 30)
        Me.btnSend.TabIndex = 31
        Me.btnSend.Text = "Set Reward Recipient"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Select wallet to use:"
        '
        'cmbWallet
        '
        Me.cmbWallet.FormattingEnabled = True
        Me.cmbWallet.Items.AddRange(New Object() {"Local wallet", "Online wallet Cryptoguru", "Online wallet Burst-team", "Online wallet Burstnation"})
        Me.cmbWallet.Location = New System.Drawing.Point(15, 136)
        Me.cmbWallet.Name = "cmbWallet"
        Me.cmbWallet.Size = New System.Drawing.Size(321, 21)
        Me.cmbWallet.TabIndex = 29
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(304, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Pool account or your account if solo mining (Reward Recipient)"
        '
        'txtPool
        '
        Me.txtPool.Location = New System.Drawing.Point(15, 89)
        Me.txtPool.Name = "txtPool"
        Me.txtPool.Size = New System.Drawing.Size(288, 20)
        Me.txtPool.TabIndex = 27
        '
        'btnPool
        '
        Me.btnPool.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPool.Location = New System.Drawing.Point(306, 88)
        Me.btnPool.Name = "btnPool"
        Me.btnPool.Size = New System.Drawing.Size(30, 22)
        Me.btnPool.TabIndex = 28
        Me.btnPool.Text = "..."
        Me.btnPool.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "Your account:"
        '
        'txtAccount
        '
        Me.txtAccount.Location = New System.Drawing.Point(14, 37)
        Me.txtAccount.Name = "txtAccount"
        Me.txtAccount.Size = New System.Drawing.Size(288, 20)
        Me.txtAccount.TabIndex = 24
        '
        'btnAccounts
        '
        Me.btnAccounts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAccounts.Location = New System.Drawing.Point(305, 36)
        Me.btnAccounts.Name = "btnAccounts"
        Me.btnAccounts.Size = New System.Drawing.Size(30, 22)
        Me.btnAccounts.TabIndex = 25
        Me.btnAccounts.Text = "..."
        Me.btnAccounts.UseVisualStyleBackColor = True
        '
        'cmlAccounts
        '
        Me.cmlAccounts.Name = "cmlAccounts"
        Me.cmlAccounts.Size = New System.Drawing.Size(61, 4)
        '
        'cmlPools
        '
        Me.cmlPools.Name = "cmlPools"
        Me.cmlPools.Size = New System.Drawing.Size(61, 4)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 170)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "Fee: "
        '
        'txtFee
        '
        Me.txtFee.Location = New System.Drawing.Point(42, 167)
        Me.txtFee.Mask = "0.00000000"
        Me.txtFee.Name = "txtFee"
        Me.txtFee.Size = New System.Drawing.Size(64, 20)
        Me.txtFee.TabIndex = 36
        Me.txtFee.Text = "001000000"
        Me.txtFee.ValidatingType = GetType(Integer)
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(112, 170)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(138, 13)
        Me.Label6.TabIndex = 37
        Me.Label6.Text = "0.00735000 is minimum fee."
        '
        'frmSetrewardassignment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(413, 340)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmSetrewardassignment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Set Reward Recipient"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents btnSend As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbWallet As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtPool As TextBox
    Friend WithEvents btnPool As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents txtAccount As TextBox
    Friend WithEvents btnAccounts As Button
    Friend WithEvents cmlAccounts As ContextMenuStrip
    Friend WithEvents cmlPools As ContextMenuStrip
    Friend WithEvents Button1 As Button
    Friend WithEvents txtFee As MaskedTextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
End Class
