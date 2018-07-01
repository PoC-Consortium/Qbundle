<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmFirstTime
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFirstTime))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PnlWiz2 = New System.Windows.Forms.Panel()
        Me.chkUpdates = New System.Windows.Forms.CheckBox()
        Me.btnDownload = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pnlJava = New System.Windows.Forms.Panel()
        Me.lblJavaStatus = New System.Windows.Forms.Label()
        Me.lblJavaHeader = New System.Windows.Forms.Label()
        Me.btnDone = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.pnlBRS = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblBRS = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlWiz2.SuspendLayout()
        Me.pnlJava.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBRS.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.PictureBox1.Location = New System.Drawing.Point(-1, -2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(239, 304)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label7.Font = New System.Drawing.Font("Rockwell", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(18, 227)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(78, 19)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Qbundle"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label8.Location = New System.Drawing.Point(19, 249)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(201, 39)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "We have now checked the environment." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "If anything is missing please use" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "download" &
    " missing components."
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label9.Font = New System.Drawing.Font("Rockwell", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(24, 19)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(182, 19)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Welcome to Burstcoin"
        '
        'PnlWiz2
        '
        Me.PnlWiz2.AutoScroll = True
        Me.PnlWiz2.Controls.Add(Me.pnlBRS)
        Me.PnlWiz2.Controls.Add(Me.chkUpdates)
        Me.PnlWiz2.Controls.Add(Me.btnDownload)
        Me.PnlWiz2.Controls.Add(Me.Label12)
        Me.PnlWiz2.Controls.Add(Me.pnlJava)
        Me.PnlWiz2.Controls.Add(Me.btnDone)
        Me.PnlWiz2.Location = New System.Drawing.Point(238, -1)
        Me.PnlWiz2.Name = "PnlWiz2"
        Me.PnlWiz2.Size = New System.Drawing.Size(420, 303)
        Me.PnlWiz2.TabIndex = 12
        '
        'chkUpdates
        '
        Me.chkUpdates.AutoSize = True
        Me.chkUpdates.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkUpdates.Checked = True
        Me.chkUpdates.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUpdates.Location = New System.Drawing.Point(9, 257)
        Me.chkUpdates.Name = "chkUpdates"
        Me.chkUpdates.Size = New System.Drawing.Size(221, 30)
        Me.chkUpdates.TabIndex = 21
        Me.chkUpdates.Text = "Allow connection to remote resources for " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "verifications and updates."
        Me.chkUpdates.UseVisualStyleBackColor = True
        '
        'btnDownload
        '
        Me.btnDownload.Location = New System.Drawing.Point(244, 220)
        Me.btnDownload.Name = "btnDownload"
        Me.btnDownload.Size = New System.Drawing.Size(168, 31)
        Me.btnDownload.TabIndex = 16
        Me.btnDownload.Text = "Download missing components"
        Me.btnDownload.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Rockwell", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(4, 11)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(276, 27)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "Checking environment."
        '
        'pnlJava
        '
        Me.pnlJava.BackColor = System.Drawing.Color.PaleGreen
        Me.pnlJava.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlJava.Controls.Add(Me.lblJavaStatus)
        Me.pnlJava.Controls.Add(Me.lblJavaHeader)
        Me.pnlJava.Cursor = System.Windows.Forms.Cursors.Default
        Me.pnlJava.Location = New System.Drawing.Point(9, 46)
        Me.pnlJava.Name = "pnlJava"
        Me.pnlJava.Size = New System.Drawing.Size(403, 65)
        Me.pnlJava.TabIndex = 13
        '
        'lblJavaStatus
        '
        Me.lblJavaStatus.AutoSize = True
        Me.lblJavaStatus.Location = New System.Drawing.Point(25, 27)
        Me.lblJavaStatus.Name = "lblJavaStatus"
        Me.lblJavaStatus.Size = New System.Drawing.Size(199, 13)
        Me.lblJavaStatus.TabIndex = 3
        Me.lblJavaStatus.Text = "Java was found installed on your system."
        '
        'lblJavaHeader
        '
        Me.lblJavaHeader.AutoSize = True
        Me.lblJavaHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJavaHeader.Location = New System.Drawing.Point(7, 12)
        Me.lblJavaHeader.Name = "lblJavaHeader"
        Me.lblJavaHeader.Size = New System.Drawing.Size(34, 13)
        Me.lblJavaHeader.TabIndex = 2
        Me.lblJavaHeader.Text = "Java"
        '
        'btnDone
        '
        Me.btnDone.Location = New System.Drawing.Point(331, 257)
        Me.btnDone.Name = "btnDone"
        Me.btnDone.Size = New System.Drawing.Size(81, 31)
        Me.btnDone.TabIndex = 12
        Me.btnDone.Text = "Continue >>"
        Me.btnDone.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.PictureBox2.Image = Global.QB.My.Resources.Resources.gb
        Me.PictureBox2.Location = New System.Drawing.Point(30, 47)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(166, 166)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 13
        Me.PictureBox2.TabStop = False
        '
        'pnlBRS
        '
        Me.pnlBRS.BackColor = System.Drawing.Color.PaleGreen
        Me.pnlBRS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlBRS.Controls.Add(Me.Label1)
        Me.pnlBRS.Controls.Add(Me.lblBRS)
        Me.pnlBRS.Cursor = System.Windows.Forms.Cursors.Default
        Me.pnlBRS.Location = New System.Drawing.Point(9, 113)
        Me.pnlBRS.Name = "pnlBRS"
        Me.pnlBRS.Size = New System.Drawing.Size(403, 66)
        Me.pnlBRS.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "BRS - Core wallet"
        '
        'lblBRS
        '
        Me.lblBRS.AutoSize = True
        Me.lblBRS.Location = New System.Drawing.Point(25, 30)
        Me.lblBRS.Name = "lblBRS"
        Me.lblBRS.Size = New System.Drawing.Size(148, 13)
        Me.lblBRS.TabIndex = 4
        Me.lblBRS.Text = "Core Wallet is not yet installed"
        '
        'frmFirstTime
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(700, 338)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PnlWiz2)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmFirstTime"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Checking Environment"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlWiz2.ResumeLayout(False)
        Me.PnlWiz2.PerformLayout()
        Me.pnlJava.ResumeLayout(False)
        Me.pnlJava.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBRS.ResumeLayout(False)
        Me.pnlBRS.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents PnlWiz2 As Panel
    Friend WithEvents btnDownload As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents pnlJava As Panel
    Friend WithEvents lblJavaStatus As Label
    Friend WithEvents lblJavaHeader As Label
    Friend WithEvents btnDone As Button
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents chkUpdates As CheckBox
    Friend WithEvents pnlBRS As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents lblBRS As Label
End Class
