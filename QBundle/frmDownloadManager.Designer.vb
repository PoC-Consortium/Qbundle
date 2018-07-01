<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDownloadManager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDownloadManager))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblRead = New System.Windows.Forms.Label()
        Me.lblSpeed = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Pb1 = New System.Windows.Forms.ProgressBar()
        Me.btnAbort = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblTime)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.lblProgress)
        Me.GroupBox1.Controls.Add(Me.lblRead)
        Me.GroupBox1.Controls.Add(Me.lblSpeed)
        Me.GroupBox1.Controls.Add(Me.lblStatus)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Pb1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(430, 133)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Information"
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.Location = New System.Drawing.Point(205, 85)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(31, 13)
        Me.lblTime.TabIndex = 14
        Me.lblTime.Text = "0:0:0"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(117, 85)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(50, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Time left:"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.QB.My.Resources.Resources.Download
        Me.PictureBox1.Location = New System.Drawing.Point(10, 24)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(101, 65)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'lblProgress
        '
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Location = New System.Drawing.Point(205, 68)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(21, 13)
        Me.lblProgress.TabIndex = 12
        Me.lblProgress.Text = "0%"
        '
        'lblRead
        '
        Me.lblRead.AutoSize = True
        Me.lblRead.Location = New System.Drawing.Point(205, 51)
        Me.lblRead.Name = "lblRead"
        Me.lblRead.Size = New System.Drawing.Size(58, 13)
        Me.lblRead.TabIndex = 11
        Me.lblRead.Text = "0 / 0 bytes"
        '
        'lblSpeed
        '
        Me.lblSpeed.AutoSize = True
        Me.lblSpeed.Location = New System.Drawing.Point(205, 33)
        Me.lblSpeed.Name = "lblSpeed"
        Me.lblSpeed.Size = New System.Drawing.Size(56, 13)
        Me.lblSpeed.TabIndex = 10
        Me.lblSpeed.Text = "0 KiB/Sec"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(205, 16)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(10, 13)
        Me.lblStatus.TabIndex = 9
        Me.lblStatus.Text = "-"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(117, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Progress:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(117, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Downloaded:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(117, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Download speed:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(117, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Status:"
        '
        'Pb1
        '
        Me.Pb1.Location = New System.Drawing.Point(10, 109)
        Me.Pb1.Name = "Pb1"
        Me.Pb1.Size = New System.Drawing.Size(414, 15)
        Me.Pb1.TabIndex = 4
        '
        'btnAbort
        '
        Me.btnAbort.Location = New System.Drawing.Point(188, 146)
        Me.btnAbort.Name = "btnAbort"
        Me.btnAbort.Size = New System.Drawing.Size(76, 26)
        Me.btnAbort.TabIndex = 1
        Me.btnAbort.Text = "Cancel"
        Me.btnAbort.UseVisualStyleBackColor = True
        '
        'frmDownloadExtract
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(454, 181)
        Me.Controls.Add(Me.btnAbort)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmDownloadExtract"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Download manager"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Pb1 As ProgressBar
    Friend WithEvents lblProgress As Label
    Friend WithEvents lblRead As Label
    Friend WithEvents lblSpeed As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnAbort As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lblTime As Label
    Friend WithEvents Label6 As Label
End Class
