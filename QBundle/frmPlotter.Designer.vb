<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPlotter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPlotter))
        Me.btnStartPotting = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.txtAccount = New System.Windows.Forms.TextBox()
        Me.btnPath = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblDeselectAll = New System.Windows.Forms.Label()
        Me.lblSelectAll = New System.Windows.Forms.Label()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.lstPlots = New System.Windows.Forms.ListBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtStartNonce = New System.Windows.Forms.TextBox()
        Me.nrRam = New System.Windows.Forms.NumericUpDown()
        Me.lblTotalNonces = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.nrThreads = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.HSSize = New System.Windows.Forms.TrackBar()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkAddtoPlottfiles = New System.Windows.Forms.CheckBox()
        Me.btnAccounts = New System.Windows.Forms.Button()
        Me.lblSize = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cmlAccounts = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmImport = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ImportFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblcputype = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartPlottingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResumePlottingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.radPoC1 = New System.Windows.Forms.RadioButton()
        Me.radPoC2 = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.nrRam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nrThreads, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.HSSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmImport.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnStartPotting
        '
        Me.btnStartPotting.Location = New System.Drawing.Point(402, 378)
        Me.btnStartPotting.Name = "btnStartPotting"
        Me.btnStartPotting.Size = New System.Drawing.Size(119, 34)
        Me.btnStartPotting.TabIndex = 0
        Me.btnStartPotting.Text = "Start Plotting"
        Me.btnStartPotting.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(126, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Numeric account number"
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(17, 36)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(288, 20)
        Me.txtPath.TabIndex = 8
        '
        'txtAccount
        '
        Me.txtAccount.Location = New System.Drawing.Point(17, 80)
        Me.txtAccount.Name = "txtAccount"
        Me.txtAccount.Size = New System.Drawing.Size(288, 20)
        Me.txtAccount.TabIndex = 9
        '
        'btnPath
        '
        Me.btnPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPath.Location = New System.Drawing.Point(308, 35)
        Me.btnPath.Name = "btnPath"
        Me.btnPath.Size = New System.Drawing.Size(30, 22)
        Me.btnPath.TabIndex = 11
        Me.btnPath.Text = "..."
        Me.btnPath.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(164, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Choose where to store the plotfile"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.lblDeselectAll)
        Me.GroupBox1.Controls.Add(Me.lblSelectAll)
        Me.GroupBox1.Controls.Add(Me.btnRemove)
        Me.GroupBox1.Controls.Add(Me.btnImport)
        Me.GroupBox1.Controls.Add(Me.lstPlots)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 413)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(615, 100)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "My Plotfiles"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(264, 78)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(237, 13)
        Me.Label21.TabIndex = 8
        Me.Label21.Text = "Hold Ctrl or Shift to select/deselect multiple plots."
        '
        'lblDeselectAll
        '
        Me.lblDeselectAll.AutoSize = True
        Me.lblDeselectAll.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblDeselectAll.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblDeselectAll.Location = New System.Drawing.Point(60, 78)
        Me.lblDeselectAll.Name = "lblDeselectAll"
        Me.lblDeselectAll.Size = New System.Drawing.Size(62, 13)
        Me.lblDeselectAll.TabIndex = 4
        Me.lblDeselectAll.Text = "Deselect all"
        '
        'lblSelectAll
        '
        Me.lblSelectAll.AutoSize = True
        Me.lblSelectAll.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblSelectAll.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblSelectAll.Location = New System.Drawing.Point(3, 78)
        Me.lblSelectAll.Name = "lblSelectAll"
        Me.lblSelectAll.Size = New System.Drawing.Size(50, 13)
        Me.lblSelectAll.TabIndex = 3
        Me.lblSelectAll.Text = "Select all"
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
        Me.lstPlots.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstPlots.Size = New System.Drawing.Size(495, 56)
        Me.lstPlots.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox4)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 37)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(380, 375)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Create new plotfile"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.radPoC2)
        Me.GroupBox4.Controls.Add(Me.radPoC1)
        Me.GroupBox4.Controls.Add(Me.Label22)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.txtStartNonce)
        Me.GroupBox4.Controls.Add(Me.nrRam)
        Me.GroupBox4.Controls.Add(Me.lblTotalNonces)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Controls.Add(Me.nrThreads)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Location = New System.Drawing.Point(13, 227)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(355, 143)
        Me.GroupBox4.TabIndex = 34
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Advanced Settings"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Start nonce:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 60)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 13)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Total nonces:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(219, 88)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(22, 13)
        Me.Label17.TabIndex = 32
        Me.Label17.Text = "GB"
        '
        'txtStartNonce
        '
        Me.txtStartNonce.Location = New System.Drawing.Point(17, 35)
        Me.txtStartNonce.Name = "txtStartNonce"
        Me.txtStartNonce.Size = New System.Drawing.Size(319, 20)
        Me.txtStartNonce.TabIndex = 25
        Me.txtStartNonce.Text = "0"
        '
        'nrRam
        '
        Me.nrRam.Location = New System.Drawing.Point(167, 84)
        Me.nrRam.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nrRam.Name = "nrRam"
        Me.nrRam.Size = New System.Drawing.Size(48, 20)
        Me.nrRam.TabIndex = 31
        Me.nrRam.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'lblTotalNonces
        '
        Me.lblTotalNonces.AutoSize = True
        Me.lblTotalNonces.Location = New System.Drawing.Point(87, 60)
        Me.lblTotalNonces.Name = "lblTotalNonces"
        Me.lblTotalNonces.Size = New System.Drawing.Size(10, 13)
        Me.lblTotalNonces.TabIndex = 27
        Me.lblTotalNonces.Text = "-"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(123, 87)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(44, 13)
        Me.Label16.TabIndex = 30
        Me.Label16.Text = "Memory"
        '
        'nrThreads
        '
        Me.nrThreads.Location = New System.Drawing.Point(61, 84)
        Me.nrThreads.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nrThreads.Name = "nrThreads"
        Me.nrThreads.Size = New System.Drawing.Size(48, 20)
        Me.nrThreads.TabIndex = 28
        Me.nrThreads.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 87)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Threads"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.HSSize)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.btnPath)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.txtPath)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.txtAccount)
        Me.GroupBox3.Controls.Add(Me.chkAddtoPlottfiles)
        Me.GroupBox3.Controls.Add(Me.btnAccounts)
        Me.GroupBox3.Controls.Add(Me.lblSize)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 17)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(355, 205)
        Me.GroupBox3.TabIndex = 33
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Basic Settings"
        '
        'HSSize
        '
        Me.HSSize.Enabled = False
        Me.HSSize.Location = New System.Drawing.Point(17, 127)
        Me.HSSize.Maximum = 50000
        Me.HSSize.Minimum = 8
        Me.HSSize.Name = "HSSize"
        Me.HSSize.Size = New System.Drawing.Size(321, 45)
        Me.HSSize.SmallChange = 8
        Me.HSSize.TabIndex = 27
        Me.HSSize.TickFrequency = 8
        Me.HSSize.TickStyle = System.Windows.Forms.TickStyle.Both
        Me.HSSize.Value = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 111)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(148, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Choose the size of the plotfile:"
        '
        'chkAddtoPlottfiles
        '
        Me.chkAddtoPlottfiles.AutoSize = True
        Me.chkAddtoPlottfiles.Checked = True
        Me.chkAddtoPlottfiles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAddtoPlottfiles.Location = New System.Drawing.Point(17, 178)
        Me.chkAddtoPlottfiles.Name = "chkAddtoPlottfiles"
        Me.chkAddtoPlottfiles.Size = New System.Drawing.Size(195, 17)
        Me.chkAddtoPlottfiles.TabIndex = 26
        Me.chkAddtoPlottfiles.Text = "Add to my plotfiles at ""Start plotting"""
        Me.chkAddtoPlottfiles.UseVisualStyleBackColor = True
        '
        'btnAccounts
        '
        Me.btnAccounts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAccounts.Location = New System.Drawing.Point(308, 79)
        Me.btnAccounts.Name = "btnAccounts"
        Me.btnAccounts.Size = New System.Drawing.Size(30, 22)
        Me.btnAccounts.TabIndex = 22
        Me.btnAccounts.Text = "..."
        Me.btnAccounts.UseVisualStyleBackColor = True
        '
        'lblSize
        '
        Me.lblSize.AutoSize = True
        Me.lblSize.Location = New System.Drawing.Point(162, 111)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(10, 13)
        Me.lblSize.TabIndex = 24
        Me.lblSize.Text = "-"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(399, 113)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Step 2."
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(399, 129)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(188, 26)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Select the location where you want to " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "store the plotfile."
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(399, 161)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Step 3."
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(399, 176)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(222, 26)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Enter the numeric account number or " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "select an account from the account manager." &
    ""
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(399, 208)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(48, 13)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "Step 4."
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(399, 224)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(145, 26)
        Me.Label12.TabIndex = 20
        Me.Label12.Text = "Select the size of the plotfile. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Click start plotting"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(399, 272)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(228, 39)
        Me.Label13.TabIndex = 22
        Me.Label13.Text = "Do not change these values unless you are " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "absolutley sure of what you are doing" &
    "." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Qbundle automatically calculates these values."
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(399, 256)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(114, 13)
        Me.Label14.TabIndex = 21
        Me.Label14.Text = "Advanced Settings"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(396, 41)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(57, 20)
        Me.Label15.TabIndex = 23
        Me.Label15.Text = "Guide"
        '
        'cmlAccounts
        '
        Me.cmlAccounts.Name = "cmlAccounts"
        Me.cmlAccounts.Size = New System.Drawing.Size(61, 4)
        Me.cmlAccounts.Text = "Choose Account"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(398, 82)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(214, 26)
        Me.Label18.TabIndex = 25
        Me.Label18.Text = "If you have plotfiles already make sure you " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "add them to ""My plotfiles"" in the b" &
    "ottom first."
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(398, 66)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(48, 13)
        Me.Label19.TabIndex = 24
        Me.Label19.Text = "Step 1."
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
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(527, 397)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(69, 12)
        Me.Label20.TabIndex = 26
        Me.Label20.Text = "CPU capability:"
        '
        'lblcputype
        '
        Me.lblcputype.AutoSize = True
        Me.lblcputype.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcputype.Location = New System.Drawing.Point(594, 397)
        Me.lblcputype.Name = "lblcputype"
        Me.lblcputype.Size = New System.Drawing.Size(30, 12)
        Me.lblcputype.TabIndex = 27
        Me.lblcputype.Text = "AVX2"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ActionToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(643, 24)
        Me.MenuStrip1.TabIndex = 28
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CloseToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.CloseToolStripMenuItem.Text = "Close"
        '
        'ActionToolStripMenuItem
        '
        Me.ActionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StartPlottingToolStripMenuItem, Me.ResumePlottingToolStripMenuItem})
        Me.ActionToolStripMenuItem.Name = "ActionToolStripMenuItem"
        Me.ActionToolStripMenuItem.Size = New System.Drawing.Size(54, 20)
        Me.ActionToolStripMenuItem.Text = "Action"
        '
        'StartPlottingToolStripMenuItem
        '
        Me.StartPlottingToolStripMenuItem.Name = "StartPlottingToolStripMenuItem"
        Me.StartPlottingToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.StartPlottingToolStripMenuItem.Text = "Start Plotting"
        '
        'ResumePlottingToolStripMenuItem
        '
        Me.ResumePlottingToolStripMenuItem.Name = "ResumePlottingToolStripMenuItem"
        Me.ResumePlottingToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.ResumePlottingToolStripMenuItem.Text = "Resume plotting"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(16, 116)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(51, 13)
        Me.Label22.TabIndex = 33
        Me.Label22.Text = "Plot type:"
        '
        'radPoC1
        '
        Me.radPoC1.AutoSize = True
        Me.radPoC1.Location = New System.Drawing.Point(69, 115)
        Me.radPoC1.Name = "radPoC1"
        Me.radPoC1.Size = New System.Drawing.Size(51, 17)
        Me.radPoC1.TabIndex = 34
        Me.radPoC1.Text = "PoC1"
        Me.radPoC1.UseVisualStyleBackColor = True
        '
        'radPoC2
        '
        Me.radPoC2.AutoSize = True
        Me.radPoC2.Checked = True
        Me.radPoC2.Location = New System.Drawing.Point(125, 115)
        Me.radPoC2.Name = "radPoC2"
        Me.radPoC2.Size = New System.Drawing.Size(51, 17)
        Me.radPoC2.TabIndex = 35
        Me.radPoC2.TabStop = True
        Me.radPoC2.Text = "PoC2"
        Me.radPoC2.UseVisualStyleBackColor = True
        '
        'frmPlotter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(643, 525)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.lblcputype)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.btnStartPotting)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "frmPlotter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Plotter (Xplotter by Blago) modified by JohnnyFFM in 2018"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.nrRam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nrThreads, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.HSSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmImport.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnStartPotting As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents txtPath As TextBox
    Friend WithEvents txtAccount As TextBox
    Friend WithEvents btnPath As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lstPlots As ListBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnAccounts As Button
    Friend WithEvents btnRemove As Button
    Friend WithEvents btnImport As Button
    Friend WithEvents chkAddtoPlottfiles As CheckBox
    Friend WithEvents txtStartNonce As TextBox
    Friend WithEvents lblSize As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents lblTotalNonces As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents nrThreads As NumericUpDown
    Friend WithEvents cmlAccounts As ContextMenuStrip
    Friend WithEvents Label17 As Label
    Friend WithEvents nrRam As NumericUpDown
    Friend WithEvents Label16 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents HSSize As TrackBar
    Friend WithEvents cmImport As ContextMenuStrip
    Friend WithEvents ImportFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImportFolderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label20 As Label
    Friend WithEvents lblcputype As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ActionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StartPlottingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ResumePlottingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents lblDeselectAll As Label
    Friend WithEvents lblSelectAll As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents radPoC2 As RadioButton
    Friend WithEvents radPoC1 As RadioButton
    Friend WithEvents Label22 As Label
End Class
