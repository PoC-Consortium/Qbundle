<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMiner
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMiner))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.lstPlots = New System.Windows.Forms.ListBox()
        Me.btnPool = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtDeadLine = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkUseBoost = New System.Windows.Forms.CheckBox()
        Me.chkShowWinner = New System.Windows.Forms.CheckBox()
        Me.chkUseHDD = New System.Windows.Forms.CheckBox()
        Me.nrInfoPort = New System.Windows.Forms.NumericUpDown()
        Me.nrUpdatePort = New System.Windows.Forms.NumericUpDown()
        Me.nrMiningPort = New System.Windows.Forms.NumericUpDown()
        Me.txtInfoServer = New System.Windows.Forms.TextBox()
        Me.txtUpdateServer = New System.Windows.Forms.TextBox()
        Me.txtMiningServer = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.cmlServers = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.nrInfoPort, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nrUpdatePort, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nrMiningPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnRemove)
        Me.GroupBox1.Controls.Add(Me.btnImport)
        Me.GroupBox1.Controls.Add(Me.lstPlots)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 297)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(615, 84)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "My Plotfiles"
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(511, 48)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(88, 27)
        Me.btnRemove.TabIndex = 2
        Me.btnRemove.Text = "Remove plotfile"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(511, 19)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(88, 27)
        Me.btnImport.TabIndex = 1
        Me.btnImport.Text = "Import plotfile"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'lstPlots
        '
        Me.lstPlots.FormattingEnabled = True
        Me.lstPlots.Location = New System.Drawing.Point(6, 19)
        Me.lstPlots.Name = "lstPlots"
        Me.lstPlots.Size = New System.Drawing.Size(499, 56)
        Me.lstPlots.TabIndex = 0
        '
        'btnPool
        '
        Me.btnPool.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPool.Location = New System.Drawing.Point(108, 75)
        Me.btnPool.Name = "btnPool"
        Me.btnPool.Size = New System.Drawing.Size(134, 22)
        Me.btnPool.TabIndex = 31
        Me.btnPool.Text = "Select predfined server"
        Me.btnPool.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.txtDeadLine)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.chkUseBoost)
        Me.GroupBox2.Controls.Add(Me.chkShowWinner)
        Me.GroupBox2.Controls.Add(Me.chkUseHDD)
        Me.GroupBox2.Controls.Add(Me.nrInfoPort)
        Me.GroupBox2.Controls.Add(Me.nrUpdatePort)
        Me.GroupBox2.Controls.Add(Me.nrMiningPort)
        Me.GroupBox2.Controls.Add(Me.txtInfoServer)
        Me.GroupBox2.Controls.Add(Me.txtUpdateServer)
        Me.GroupBox2.Controls.Add(Me.txtMiningServer)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.RadioButton2)
        Me.GroupBox2.Controls.Add(Me.RadioButton1)
        Me.GroupBox2.Controls.Add(Me.btnPool)
        Me.GroupBox2.Location = New System.Drawing.Point(18, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(404, 280)
        Me.GroupBox2.TabIndex = 32
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Miner settings"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(274, 243)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(114, 29)
        Me.Button1.TabIndex = 51
        Me.Button1.Text = "Start Mining"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtDeadLine
        '
        Me.txtDeadLine.Location = New System.Drawing.Point(108, 170)
        Me.txtDeadLine.Name = "txtDeadLine"
        Me.txtDeadLine.Size = New System.Drawing.Size(183, 20)
        Me.txtDeadLine.TabIndex = 50
        Me.txtDeadLine.Text = "80000000"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(29, 173)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 13)
        Me.Label7.TabIndex = 49
        Me.Label7.Text = "Deadline limit:"
        '
        'chkUseBoost
        '
        Me.chkUseBoost.AutoSize = True
        Me.chkUseBoost.Checked = True
        Me.chkUseBoost.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUseBoost.Location = New System.Drawing.Point(32, 255)
        Me.chkUseBoost.Name = "chkUseBoost"
        Me.chkUseBoost.Size = New System.Drawing.Size(113, 17)
        Me.chkUseBoost.TabIndex = 48
        Me.chkUseBoost.Text = "Use multithreading"
        Me.chkUseBoost.UseVisualStyleBackColor = True
        '
        'chkShowWinner
        '
        Me.chkShowWinner.AutoSize = True
        Me.chkShowWinner.Location = New System.Drawing.Point(32, 232)
        Me.chkShowWinner.Name = "chkShowWinner"
        Me.chkShowWinner.Size = New System.Drawing.Size(141, 17)
        Me.chkShowWinner.TabIndex = 47
        Me.chkShowWinner.Text = "Show winner information"
        Me.chkShowWinner.UseVisualStyleBackColor = True
        '
        'chkUseHDD
        '
        Me.chkUseHDD.AutoSize = True
        Me.chkUseHDD.Checked = True
        Me.chkUseHDD.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUseHDD.Location = New System.Drawing.Point(32, 209)
        Me.chkUseHDD.Name = "chkUseHDD"
        Me.chkUseHDD.Size = New System.Drawing.Size(113, 17)
        Me.chkUseHDD.TabIndex = 46
        Me.chkUseHDD.Text = "Use HDD wakeup"
        Me.chkUseHDD.UseVisualStyleBackColor = True
        '
        'nrInfoPort
        '
        Me.nrInfoPort.Location = New System.Drawing.Point(332, 149)
        Me.nrInfoPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nrInfoPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nrInfoPort.Name = "nrInfoPort"
        Me.nrInfoPort.Size = New System.Drawing.Size(56, 20)
        Me.nrInfoPort.TabIndex = 45
        Me.nrInfoPort.Value = New Decimal(New Integer() {8124, 0, 0, 0})
        '
        'nrUpdatePort
        '
        Me.nrUpdatePort.Location = New System.Drawing.Point(332, 127)
        Me.nrUpdatePort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nrUpdatePort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nrUpdatePort.Name = "nrUpdatePort"
        Me.nrUpdatePort.Size = New System.Drawing.Size(56, 20)
        Me.nrUpdatePort.TabIndex = 44
        Me.nrUpdatePort.Value = New Decimal(New Integer() {8124, 0, 0, 0})
        '
        'nrMiningPort
        '
        Me.nrMiningPort.Location = New System.Drawing.Point(332, 105)
        Me.nrMiningPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nrMiningPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nrMiningPort.Name = "nrMiningPort"
        Me.nrMiningPort.Size = New System.Drawing.Size(56, 20)
        Me.nrMiningPort.TabIndex = 43
        Me.nrMiningPort.Value = New Decimal(New Integer() {8124, 0, 0, 0})
        '
        'txtInfoServer
        '
        Me.txtInfoServer.Location = New System.Drawing.Point(108, 148)
        Me.txtInfoServer.Name = "txtInfoServer"
        Me.txtInfoServer.Size = New System.Drawing.Size(183, 20)
        Me.txtInfoServer.TabIndex = 42
        '
        'txtUpdateServer
        '
        Me.txtUpdateServer.Location = New System.Drawing.Point(108, 126)
        Me.txtUpdateServer.Name = "txtUpdateServer"
        Me.txtUpdateServer.Size = New System.Drawing.Size(183, 20)
        Me.txtUpdateServer.TabIndex = 41
        '
        'txtMiningServer
        '
        Me.txtMiningServer.Location = New System.Drawing.Point(108, 104)
        Me.txtMiningServer.Name = "txtMiningServer"
        Me.txtMiningServer.Size = New System.Drawing.Size(183, 20)
        Me.txtMiningServer.TabIndex = 40
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(29, 151)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 13)
        Me.Label6.TabIndex = 39
        Me.Label6.Text = "Info server:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(29, 129)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "Update server:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(297, 129)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 13)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Port:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(297, 151)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "Port:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(297, 107)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Port:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 107)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "Mining server:"
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Checked = True
        Me.RadioButton2.Location = New System.Drawing.Point(32, 52)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(79, 17)
        Me.RadioButton2.TabIndex = 33
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Pool mining"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(32, 29)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(79, 17)
        Me.RadioButton1.TabIndex = 32
        Me.RadioButton1.Text = "Solo mining"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'cmlServers
        '
        Me.cmlServers.Name = "cmlServers"
        Me.cmlServers.Size = New System.Drawing.Size(61, 4)
        '
        'frmMiner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(642, 389)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmMiner"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Blago Miner"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.nrInfoPort, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nrUpdatePort, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nrMiningPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnRemove As Button
    Friend WithEvents btnImport As Button
    Friend WithEvents lstPlots As ListBox
    Friend WithEvents btnPool As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents txtDeadLine As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents chkUseBoost As CheckBox
    Friend WithEvents chkShowWinner As CheckBox
    Friend WithEvents chkUseHDD As CheckBox
    Friend WithEvents nrInfoPort As NumericUpDown
    Friend WithEvents nrUpdatePort As NumericUpDown
    Friend WithEvents nrMiningPort As NumericUpDown
    Friend WithEvents txtInfoServer As TextBox
    Friend WithEvents txtUpdateServer As TextBox
    Friend WithEvents txtMiningServer As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents cmlServers As ContextMenuStrip
End Class
