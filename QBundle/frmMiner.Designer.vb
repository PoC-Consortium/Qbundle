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
        Me.grpMiner = New System.Windows.Forms.GroupBox()
        Me.pnlPool = New System.Windows.Forms.Panel()
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
        Me.btnStartMine = New System.Windows.Forms.Button()
        Me.txtDeadLine = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkUseBoost = New System.Windows.Forms.CheckBox()
        Me.chkShowWinner = New System.Windows.Forms.CheckBox()
        Me.chkUseHDD = New System.Windows.Forms.CheckBox()
        Me.rbPool = New System.Windows.Forms.RadioButton()
        Me.rbSolo = New System.Windows.Forms.RadioButton()
        Me.cmlServers = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblReward = New System.Windows.Forms.Label()
        Me.cmImport = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ImportFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmrRemovePass = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.grpMiner.SuspendLayout()
        Me.pnlPool.SuspendLayout()
        CType(Me.nrInfoPort, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nrUpdatePort, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nrMiningPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmImport.SuspendLayout()
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
        Me.btnRemove.Location = New System.Drawing.Point(511, 49)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(88, 27)
        Me.btnRemove.TabIndex = 2
        Me.btnRemove.Text = "Remove plotfile"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(511, 18)
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
        Me.btnPool.Location = New System.Drawing.Point(94, 1)
        Me.btnPool.Name = "btnPool"
        Me.btnPool.Size = New System.Drawing.Size(134, 22)
        Me.btnPool.TabIndex = 31
        Me.btnPool.Text = "Select predfined pool"
        Me.btnPool.UseVisualStyleBackColor = True
        '
        'grpMiner
        '
        Me.grpMiner.Controls.Add(Me.pnlPool)
        Me.grpMiner.Controls.Add(Me.btnStartMine)
        Me.grpMiner.Controls.Add(Me.txtDeadLine)
        Me.grpMiner.Controls.Add(Me.Label7)
        Me.grpMiner.Controls.Add(Me.chkUseBoost)
        Me.grpMiner.Controls.Add(Me.chkShowWinner)
        Me.grpMiner.Controls.Add(Me.chkUseHDD)
        Me.grpMiner.Controls.Add(Me.rbPool)
        Me.grpMiner.Controls.Add(Me.rbSolo)
        Me.grpMiner.Location = New System.Drawing.Point(18, 12)
        Me.grpMiner.Name = "grpMiner"
        Me.grpMiner.Size = New System.Drawing.Size(404, 280)
        Me.grpMiner.TabIndex = 32
        Me.grpMiner.TabStop = False
        Me.grpMiner.Text = "Miner settings"
        '
        'pnlPool
        '
        Me.pnlPool.Controls.Add(Me.nrInfoPort)
        Me.pnlPool.Controls.Add(Me.nrUpdatePort)
        Me.pnlPool.Controls.Add(Me.nrMiningPort)
        Me.pnlPool.Controls.Add(Me.txtInfoServer)
        Me.pnlPool.Controls.Add(Me.txtUpdateServer)
        Me.pnlPool.Controls.Add(Me.txtMiningServer)
        Me.pnlPool.Controls.Add(Me.Label6)
        Me.pnlPool.Controls.Add(Me.Label5)
        Me.pnlPool.Controls.Add(Me.Label4)
        Me.pnlPool.Controls.Add(Me.Label3)
        Me.pnlPool.Controls.Add(Me.Label2)
        Me.pnlPool.Controls.Add(Me.Label1)
        Me.pnlPool.Controls.Add(Me.btnPool)
        Me.pnlPool.Location = New System.Drawing.Point(6, 68)
        Me.pnlPool.Name = "pnlPool"
        Me.pnlPool.Size = New System.Drawing.Size(389, 96)
        Me.pnlPool.TabIndex = 52
        '
        'nrInfoPort
        '
        Me.nrInfoPort.Location = New System.Drawing.Point(318, 75)
        Me.nrInfoPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nrInfoPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nrInfoPort.Name = "nrInfoPort"
        Me.nrInfoPort.Size = New System.Drawing.Size(56, 20)
        Me.nrInfoPort.TabIndex = 45
        Me.nrInfoPort.Value = New Decimal(New Integer() {8124, 0, 0, 0})
        '
        'nrUpdatePort
        '
        Me.nrUpdatePort.Location = New System.Drawing.Point(318, 53)
        Me.nrUpdatePort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nrUpdatePort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nrUpdatePort.Name = "nrUpdatePort"
        Me.nrUpdatePort.Size = New System.Drawing.Size(56, 20)
        Me.nrUpdatePort.TabIndex = 44
        Me.nrUpdatePort.Value = New Decimal(New Integer() {8124, 0, 0, 0})
        '
        'nrMiningPort
        '
        Me.nrMiningPort.Location = New System.Drawing.Point(318, 31)
        Me.nrMiningPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nrMiningPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nrMiningPort.Name = "nrMiningPort"
        Me.nrMiningPort.Size = New System.Drawing.Size(56, 20)
        Me.nrMiningPort.TabIndex = 43
        Me.nrMiningPort.Value = New Decimal(New Integer() {8124, 0, 0, 0})
        '
        'txtInfoServer
        '
        Me.txtInfoServer.Location = New System.Drawing.Point(94, 74)
        Me.txtInfoServer.Name = "txtInfoServer"
        Me.txtInfoServer.Size = New System.Drawing.Size(183, 20)
        Me.txtInfoServer.TabIndex = 42
        '
        'txtUpdateServer
        '
        Me.txtUpdateServer.Location = New System.Drawing.Point(94, 52)
        Me.txtUpdateServer.Name = "txtUpdateServer"
        Me.txtUpdateServer.Size = New System.Drawing.Size(183, 20)
        Me.txtUpdateServer.TabIndex = 41
        '
        'txtMiningServer
        '
        Me.txtMiningServer.Location = New System.Drawing.Point(94, 30)
        Me.txtMiningServer.Name = "txtMiningServer"
        Me.txtMiningServer.Size = New System.Drawing.Size(183, 20)
        Me.txtMiningServer.TabIndex = 40
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 77)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 13)
        Me.Label6.TabIndex = 39
        Me.Label6.Text = "Info server:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "Update server:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(283, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 13)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Port:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(283, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "Port:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(283, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Port:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "Mining server:"
        '
        'btnStartMine
        '
        Me.btnStartMine.Location = New System.Drawing.Point(266, 237)
        Me.btnStartMine.Name = "btnStartMine"
        Me.btnStartMine.Size = New System.Drawing.Size(114, 29)
        Me.btnStartMine.TabIndex = 51
        Me.btnStartMine.Text = "Start Mining"
        Me.btnStartMine.UseVisualStyleBackColor = True
        '
        'txtDeadLine
        '
        Me.txtDeadLine.Location = New System.Drawing.Point(100, 164)
        Me.txtDeadLine.Name = "txtDeadLine"
        Me.txtDeadLine.Size = New System.Drawing.Size(183, 20)
        Me.txtDeadLine.TabIndex = 50
        Me.txtDeadLine.Text = "80000000"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(21, 167)
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
        Me.chkUseBoost.Location = New System.Drawing.Point(24, 249)
        Me.chkUseBoost.Name = "chkUseBoost"
        Me.chkUseBoost.Size = New System.Drawing.Size(113, 17)
        Me.chkUseBoost.TabIndex = 48
        Me.chkUseBoost.Text = "Use multithreading"
        Me.chkUseBoost.UseVisualStyleBackColor = True
        '
        'chkShowWinner
        '
        Me.chkShowWinner.AutoSize = True
        Me.chkShowWinner.Location = New System.Drawing.Point(24, 226)
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
        Me.chkUseHDD.Location = New System.Drawing.Point(24, 203)
        Me.chkUseHDD.Name = "chkUseHDD"
        Me.chkUseHDD.Size = New System.Drawing.Size(113, 17)
        Me.chkUseHDD.TabIndex = 46
        Me.chkUseHDD.Text = "Use HDD wakeup"
        Me.chkUseHDD.UseVisualStyleBackColor = True
        '
        'rbPool
        '
        Me.rbPool.AutoSize = True
        Me.rbPool.Checked = True
        Me.rbPool.Location = New System.Drawing.Point(24, 46)
        Me.rbPool.Name = "rbPool"
        Me.rbPool.Size = New System.Drawing.Size(79, 17)
        Me.rbPool.TabIndex = 33
        Me.rbPool.TabStop = True
        Me.rbPool.Text = "Pool mining"
        Me.rbPool.UseVisualStyleBackColor = True
        '
        'rbSolo
        '
        Me.rbSolo.AutoSize = True
        Me.rbSolo.Location = New System.Drawing.Point(24, 23)
        Me.rbSolo.Name = "rbSolo"
        Me.rbSolo.Size = New System.Drawing.Size(79, 17)
        Me.rbSolo.TabIndex = 32
        Me.rbSolo.Text = "Solo mining"
        Me.rbSolo.UseVisualStyleBackColor = True
        '
        'cmlServers
        '
        Me.cmlServers.Name = "cmlServers"
        Me.cmlServers.Size = New System.Drawing.Size(61, 4)
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(432, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(97, 18)
        Me.Label8.TabIndex = 33
        Me.Label8.Text = "Solo mining"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(432, 137)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(97, 18)
        Me.Label9.TabIndex = 34
        Me.Label9.Text = "Pool mining"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(432, 34)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(203, 91)
        Me.Label10.TabIndex = 35
        Me.Label10.Text = resources.GetString("Label10.Text")
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(432, 157)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(192, 52)
        Me.Label11.TabIndex = 36
        Me.Label11.Text = "Make sure you have set correct server " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "information or use a predefined pool." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Al" &
    "so, be sure to have set the correct" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Rewardrecipient for your account."
        '
        'lblReward
        '
        Me.lblReward.AutoSize = True
        Me.lblReward.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReward.Location = New System.Drawing.Point(430, 255)
        Me.lblReward.Name = "lblReward"
        Me.lblReward.Size = New System.Drawing.Size(130, 36)
        Me.lblReward.TabIndex = 37
        Me.lblReward.Text = "Your reward recipient is set to: " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Account.-" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Name: -" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'cmImport
        '
        Me.cmImport.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportFileToolStripMenuItem, Me.ImportFolderToolStripMenuItem})
        Me.cmImport.Name = "cmImport"
        Me.cmImport.Size = New System.Drawing.Size(147, 48)
        '
        'ImportFileToolStripMenuItem
        '
        Me.ImportFileToolStripMenuItem.Name = "ImportFileToolStripMenuItem"
        Me.ImportFileToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.ImportFileToolStripMenuItem.Text = "Import file"
        '
        'ImportFolderToolStripMenuItem
        '
        Me.ImportFolderToolStripMenuItem.Name = "ImportFolderToolStripMenuItem"
        Me.ImportFolderToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.ImportFolderToolStripMenuItem.Text = "Import Folder"
        '
        'tmrRemovePass
        '
        Me.tmrRemovePass.Interval = 3000
        '
        'frmMiner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(652, 393)
        Me.Controls.Add(Me.lblReward)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.grpMiner)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmMiner"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Miner (Blago CPU Miner)"
        Me.GroupBox1.ResumeLayout(False)
        Me.grpMiner.ResumeLayout(False)
        Me.grpMiner.PerformLayout()
        Me.pnlPool.ResumeLayout(False)
        Me.pnlPool.PerformLayout()
        CType(Me.nrInfoPort, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nrUpdatePort, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nrMiningPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmImport.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnRemove As Button
    Friend WithEvents btnImport As Button
    Friend WithEvents lstPlots As ListBox
    Friend WithEvents btnPool As Button
    Friend WithEvents grpMiner As GroupBox
    Friend WithEvents rbPool As RadioButton
    Friend WithEvents rbSolo As RadioButton
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
    Friend WithEvents btnStartMine As Button
    Friend WithEvents cmlServers As ContextMenuStrip
    Friend WithEvents pnlPool As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents lblReward As Label
    Friend WithEvents cmImport As ContextMenuStrip
    Friend WithEvents ImportFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImportFolderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tmrRemovePass As Timer
End Class
