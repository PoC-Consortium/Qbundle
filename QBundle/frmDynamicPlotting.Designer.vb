<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDynamicPlotting
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDynamicPlotting))
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.pnlOnOff = New System.Windows.Forms.Panel()
        Me.lblFreeSpace = New System.Windows.Forms.Label()
        Me.lblPlotSize = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnAccounts = New System.Windows.Forms.Button()
        Me.txtAccount = New System.Windows.Forms.TextBox()
        Me.chkHide = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.trFreeSpace = New System.Windows.Forms.TrackBar()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.HSSize = New System.Windows.Forms.TrackBar()
        Me.btnPath = New System.Windows.Forms.Button()
        Me.rEnable = New System.Windows.Forms.RadioButton()
        Me.rDisable = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.lstPlots = New System.Windows.Forms.ListBox()
        Me.cmlAccounts = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.GroupBox3.SuspendLayout()
        Me.pnlOnOff.SuspendLayout()
        CType(Me.trFreeSpace, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HSSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.pnlOnOff)
        Me.GroupBox3.Controls.Add(Me.rEnable)
        Me.GroupBox3.Controls.Add(Me.rDisable)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(359, 324)
        Me.GroupBox3.TabIndex = 34
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Settings"
        '
        'pnlOnOff
        '
        Me.pnlOnOff.Controls.Add(Me.lblFreeSpace)
        Me.pnlOnOff.Controls.Add(Me.lblPlotSize)
        Me.pnlOnOff.Controls.Add(Me.Label5)
        Me.pnlOnOff.Controls.Add(Me.btnAccounts)
        Me.pnlOnOff.Controls.Add(Me.txtAccount)
        Me.pnlOnOff.Controls.Add(Me.chkHide)
        Me.pnlOnOff.Controls.Add(Me.Label2)
        Me.pnlOnOff.Controls.Add(Me.trFreeSpace)
        Me.pnlOnOff.Controls.Add(Me.txtPath)
        Me.pnlOnOff.Controls.Add(Me.Label1)
        Me.pnlOnOff.Controls.Add(Me.Label3)
        Me.pnlOnOff.Controls.Add(Me.HSSize)
        Me.pnlOnOff.Controls.Add(Me.btnPath)
        Me.pnlOnOff.Enabled = False
        Me.pnlOnOff.Location = New System.Drawing.Point(10, 66)
        Me.pnlOnOff.Name = "pnlOnOff"
        Me.pnlOnOff.Size = New System.Drawing.Size(338, 248)
        Me.pnlOnOff.TabIndex = 33
        '
        'lblFreeSpace
        '
        Me.lblFreeSpace.AutoSize = True
        Me.lblFreeSpace.Location = New System.Drawing.Point(103, 157)
        Me.lblFreeSpace.Name = "lblFreeSpace"
        Me.lblFreeSpace.Size = New System.Drawing.Size(10, 13)
        Me.lblFreeSpace.TabIndex = 32
        Me.lblFreeSpace.Text = "-"
        '
        'lblPlotSize
        '
        Me.lblPlotSize.AutoSize = True
        Me.lblPlotSize.Location = New System.Drawing.Point(154, 94)
        Me.lblPlotSize.Name = "lblPlotSize"
        Me.lblPlotSize.Size = New System.Drawing.Size(36, 13)
        Me.lblPlotSize.TabIndex = 31
        Me.lblPlotSize.Text = "10GiB"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(169, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Choose where to store the plotfiles"
        '
        'btnAccounts
        '
        Me.btnAccounts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAccounts.Location = New System.Drawing.Point(298, 65)
        Me.btnAccounts.Name = "btnAccounts"
        Me.btnAccounts.Size = New System.Drawing.Size(30, 22)
        Me.btnAccounts.TabIndex = 22
        Me.btnAccounts.Text = "..."
        Me.btnAccounts.UseVisualStyleBackColor = True
        '
        'txtAccount
        '
        Me.txtAccount.Location = New System.Drawing.Point(7, 66)
        Me.txtAccount.Name = "txtAccount"
        Me.txtAccount.Size = New System.Drawing.Size(288, 20)
        Me.txtAccount.TabIndex = 9
        '
        'chkHide
        '
        Me.chkHide.AutoSize = True
        Me.chkHide.Checked = True
        Me.chkHide.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkHide.Location = New System.Drawing.Point(9, 222)
        Me.chkHide.Name = "chkHide"
        Me.chkHide.Size = New System.Drawing.Size(119, 17)
        Me.chkHide.TabIndex = 30
        Me.chkHide.Text = "Hide plotter window"
        Me.chkHide.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(153, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Choose the size of the plotfiles:"
        '
        'trFreeSpace
        '
        Me.trFreeSpace.Location = New System.Drawing.Point(7, 172)
        Me.trFreeSpace.Maximum = 50000
        Me.trFreeSpace.Minimum = 8
        Me.trFreeSpace.Name = "trFreeSpace"
        Me.trFreeSpace.Size = New System.Drawing.Size(321, 45)
        Me.trFreeSpace.SmallChange = 8
        Me.trFreeSpace.TabIndex = 29
        Me.trFreeSpace.TickFrequency = 8
        Me.trFreeSpace.TickStyle = System.Windows.Forms.TickStyle.Both
        Me.trFreeSpace.Value = 8
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(7, 22)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(288, 20)
        Me.txtPath.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 156)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Choose free space:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Numeric account nr"
        '
        'HSSize
        '
        Me.HSSize.Location = New System.Drawing.Point(7, 109)
        Me.HSSize.Maximum = 100
        Me.HSSize.Minimum = 1
        Me.HSSize.Name = "HSSize"
        Me.HSSize.Size = New System.Drawing.Size(321, 45)
        Me.HSSize.TabIndex = 27
        Me.HSSize.TickStyle = System.Windows.Forms.TickStyle.Both
        Me.HSSize.Value = 10
        '
        'btnPath
        '
        Me.btnPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPath.Location = New System.Drawing.Point(298, 21)
        Me.btnPath.Name = "btnPath"
        Me.btnPath.Size = New System.Drawing.Size(30, 22)
        Me.btnPath.TabIndex = 11
        Me.btnPath.Text = "..."
        Me.btnPath.UseVisualStyleBackColor = True
        '
        'rEnable
        '
        Me.rEnable.AutoSize = True
        Me.rEnable.Location = New System.Drawing.Point(17, 46)
        Me.rEnable.Name = "rEnable"
        Me.rEnable.Size = New System.Drawing.Size(137, 17)
        Me.rEnable.TabIndex = 32
        Me.rEnable.Text = "Enable dynamic plotting"
        Me.rEnable.UseVisualStyleBackColor = True
        '
        'rDisable
        '
        Me.rDisable.AutoSize = True
        Me.rDisable.Checked = True
        Me.rDisable.Location = New System.Drawing.Point(17, 23)
        Me.rDisable.Name = "rDisable"
        Me.rDisable.Size = New System.Drawing.Size(139, 17)
        Me.rDisable.TabIndex = 31
        Me.rDisable.TabStop = True
        Me.rDisable.Text = "Disable dynamic plotting"
        Me.rDisable.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(377, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(142, 20)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "Dynamic plotting"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(378, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(253, 117)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = resources.GetString("Label6.Text")
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(535, 307)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 29)
        Me.btnSave.TabIndex = 37
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnRemove)
        Me.GroupBox1.Controls.Add(Me.btnImport)
        Me.GroupBox1.Controls.Add(Me.lstPlots)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 342)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(623, 84)
        Me.GroupBox1.TabIndex = 38
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "My Plotfiles"
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(531, 48)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(88, 27)
        Me.btnRemove.TabIndex = 2
        Me.btnRemove.Text = "Remove plotfile"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(531, 19)
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
        Me.lstPlots.Size = New System.Drawing.Size(519, 56)
        Me.lstPlots.TabIndex = 0
        '
        'cmlAccounts
        '
        Me.cmlAccounts.Name = "cmlAccounts"
        Me.cmlAccounts.Size = New System.Drawing.Size(61, 4)
        Me.cmlAccounts.Text = "Choose Account"
        '
        'frmDynamicPlotting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(647, 438)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmDynamicPlotting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dynamic Plotting"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.pnlOnOff.ResumeLayout(False)
        Me.pnlOnOff.PerformLayout()
        CType(Me.trFreeSpace, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HSSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents chkHide As CheckBox
    Friend WithEvents trFreeSpace As TrackBar
    Friend WithEvents Label1 As Label
    Friend WithEvents HSSize As TrackBar
    Friend WithEvents Label5 As Label
    Friend WithEvents btnPath As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents txtPath As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtAccount As TextBox
    Friend WithEvents btnAccounts As Button
    Friend WithEvents pnlOnOff As Panel
    Friend WithEvents rEnable As RadioButton
    Friend WithEvents rDisable As RadioButton
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents btnSave As Button
    Friend WithEvents lblFreeSpace As Label
    Friend WithEvents lblPlotSize As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnRemove As Button
    Friend WithEvents btnImport As Button
    Friend WithEvents lstPlots As ListBox
    Friend WithEvents cmlAccounts As ContextMenuStrip
End Class
