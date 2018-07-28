<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmUpdate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdate))
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.lw1 = New System.Windows.Forms.ListView()
        Me.upd = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Application = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.InstalledVersion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.AvailableVersion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Information = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(319, 303)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(107, 28)
        Me.btnUpdate.TabIndex = 3
        Me.btnUpdate.Text = "Install / Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'lw1
        '
        Me.lw1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.upd, Me.Application, Me.InstalledVersion, Me.AvailableVersion, Me.Information})
        Me.lw1.GridLines = True
        Me.lw1.Location = New System.Drawing.Point(8, 11)
        Me.lw1.Name = "lw1"
        Me.lw1.Size = New System.Drawing.Size(724, 287)
        Me.lw1.TabIndex = 4
        Me.lw1.UseCompatibleStateImageBehavior = False
        Me.lw1.View = System.Windows.Forms.View.Details
        '
        'upd
        '
        Me.upd.Text = ""
        Me.upd.Width = 20
        '
        'Application
        '
        Me.Application.Text = "Application"
        Me.Application.Width = 204
        '
        'InstalledVersion
        '
        Me.InstalledVersion.Text = "Installed Version"
        Me.InstalledVersion.Width = 169
        '
        'AvailableVersion
        '
        Me.AvailableVersion.Text = "Available Version"
        Me.AvailableVersion.Width = 132
        '
        'Information
        '
        Me.Information.Text = "Information"
        Me.Information.Width = 164
        '
        'frmUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(744, 335)
        Me.Controls.Add(Me.lw1)
        Me.Controls.Add(Me.btnUpdate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmUpdate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Application manager"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnUpdate As Button
    Friend WithEvents lw1 As ListView
    Friend WithEvents Application As ColumnHeader
    Friend WithEvents InstalledVersion As ColumnHeader
    Friend WithEvents AvailableVersion As ColumnHeader
    Friend WithEvents Information As ColumnHeader
    Friend WithEvents upd As ColumnHeader
End Class
