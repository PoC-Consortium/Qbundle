<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmVanity
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVanity))
        Me.txtFind1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.nrThreads = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.nrPass = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtFind4 = New System.Windows.Forms.TextBox()
        Me.txtFind3 = New System.Windows.Forms.TextBox()
        Me.txtFind2 = New System.Windows.Forms.TextBox()
        Me.lblTesting = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblTested = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtPass = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.tmr = New System.Windows.Forms.Timer(Me.components)
        Me.cmSave = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SaveAsTextfileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToAccountManagerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.nrThreads, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nrPass, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.cmSave.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtFind1
        '
        Me.txtFind1.Location = New System.Drawing.Point(64, 26)
        Me.txtFind1.Name = "txtFind1"
        Me.txtFind1.Size = New System.Drawing.Size(51, 20)
        Me.txtFind1.TabIndex = 0
        Me.txtFind1.Text = "@@@@"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "BURST-"
        '
        'nrThreads
        '
        Me.nrThreads.Location = New System.Drawing.Point(64, 68)
        Me.nrThreads.Maximum = New Decimal(New Integer() {96, 0, 0, 0})
        Me.nrThreads.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nrThreads.Name = "nrThreads"
        Me.nrThreads.Size = New System.Drawing.Size(57, 20)
        Me.nrThreads.TabIndex = 2
        Me.nrThreads.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Threads:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(372, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(195, 130)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = resources.GetString("Label3.Text")
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(223, 107)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(77, 23)
        Me.btnStart.TabIndex = 5
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'nrPass
        '
        Me.nrPass.Location = New System.Drawing.Point(224, 69)
        Me.nrPass.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nrPass.Minimum = New Decimal(New Integer() {64, 0, 0, 0})
        Me.nrPass.Name = "nrPass"
        Me.nrPass.Size = New System.Drawing.Size(57, 20)
        Me.nrPass.TabIndex = 6
        Me.nrPass.Value = New Decimal(New Integer() {64, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(127, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Passprase length:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtFind4)
        Me.GroupBox1.Controls.Add(Me.txtFind3)
        Me.GroupBox1.Controls.Add(Me.txtFind2)
        Me.GroupBox1.Controls.Add(Me.lblTesting)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.lblTested)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.btnStart)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtFind1)
        Me.GroupBox1.Controls.Add(Me.nrPass)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.nrThreads)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(336, 144)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Generate address"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(241, 30)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(10, 13)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "-"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(179, 30)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(10, 13)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "-"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(116, 29)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(10, 13)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "-"
        '
        'txtFind4
        '
        Me.txtFind4.Location = New System.Drawing.Point(251, 26)
        Me.txtFind4.Name = "txtFind4"
        Me.txtFind4.Size = New System.Drawing.Size(63, 20)
        Me.txtFind4.TabIndex = 14
        Me.txtFind4.Text = "@@@@@"
        '
        'txtFind3
        '
        Me.txtFind3.Location = New System.Drawing.Point(189, 26)
        Me.txtFind3.Name = "txtFind3"
        Me.txtFind3.Size = New System.Drawing.Size(51, 20)
        Me.txtFind3.TabIndex = 13
        Me.txtFind3.Text = "@@@@"
        '
        'txtFind2
        '
        Me.txtFind2.Location = New System.Drawing.Point(127, 26)
        Me.txtFind2.Name = "txtFind2"
        Me.txtFind2.Size = New System.Drawing.Size(51, 20)
        Me.txtFind2.TabIndex = 12
        Me.txtFind2.Text = "@@@@"
        '
        'lblTesting
        '
        Me.lblTesting.AutoSize = True
        Me.lblTesting.Location = New System.Drawing.Point(88, 117)
        Me.lblTesting.Name = "lblTesting"
        Me.lblTesting.Size = New System.Drawing.Size(60, 13)
        Me.lblTesting.TabIndex = 11
        Me.lblTesting.Text = "0 keys/sec"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(37, 115)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(45, 13)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "Testing:"
        '
        'lblTested
        '
        Me.lblTested.AutoSize = True
        Me.lblTested.Location = New System.Drawing.Point(88, 102)
        Me.lblTested.Name = "lblTested"
        Me.lblTested.Size = New System.Drawing.Size(13, 13)
        Me.lblTested.TabIndex = 9
        Me.lblTested.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 102)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Total tested:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtPass)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtAddress)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.btnSave)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 160)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(623, 116)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Result"
        '
        'txtPass
        '
        Me.txtPass.Location = New System.Drawing.Point(100, 52)
        Me.txtPass.Name = "txtPass"
        Me.txtPass.ReadOnly = True
        Me.txtPass.Size = New System.Drawing.Size(493, 20)
        Me.txtPass.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(23, 55)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Passphrase:"
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(100, 26)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ReadOnly = True
        Me.txtAddress.Size = New System.Drawing.Size(236, 20)
        Me.txtAddress.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(23, 29)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Address:"
        '
        'btnSave
        '
        Me.btnSave.Enabled = False
        Me.btnSave.Location = New System.Drawing.Point(515, 78)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(78, 25)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'tmr
        '
        '
        'cmSave
        '
        Me.cmSave.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveAsTextfileToolStripMenuItem, Me.SaveToAccountManagerToolStripMenuItem})
        Me.cmSave.Name = "cmSave"
        Me.cmSave.Size = New System.Drawing.Size(207, 48)
        '
        'SaveAsTextfileToolStripMenuItem
        '
        Me.SaveAsTextfileToolStripMenuItem.Name = "SaveAsTextfileToolStripMenuItem"
        Me.SaveAsTextfileToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.SaveAsTextfileToolStripMenuItem.Text = "Save as textfile"
        '
        'SaveToAccountManagerToolStripMenuItem
        '
        Me.SaveToAccountManagerToolStripMenuItem.Name = "SaveToAccountManagerToolStripMenuItem"
        Me.SaveToAccountManagerToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.SaveToAccountManagerToolStripMenuItem.Text = "Add to account manager"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(563, 83)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(56, 15)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmVanity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(647, 288)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmVanity"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Vanity address generator"
        CType(Me.nrThreads, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nrPass, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.cmSave.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtFind1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents nrThreads As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnStart As Button
    Friend WithEvents nrPass As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtPass As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents btnSave As Button
    Friend WithEvents lblTested As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents tmr As Timer
    Friend WithEvents lblTesting As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents txtFind4 As TextBox
    Friend WithEvents txtFind3 As TextBox
    Friend WithEvents txtFind2 As TextBox
    Friend WithEvents cmSave As ContextMenuStrip
    Friend WithEvents SaveAsTextfileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToAccountManagerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Button1 As Button
End Class
