Public Class frmInput
    Private Sub revealPwd_Mdown(sender As Object, e As EventArgs) Handles revealPwd.MouseDown
        txtPwd.PasswordChar = ""
    End Sub
    Private Sub revealPwd_Mup(sender As Object, e As EventArgs) Handles revealPwd.MouseUp
        txtPwd.PasswordChar = "*"
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.No
    End Sub

    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles txtPwd.KeyUp
        If e.KeyCode = Keys.Enter Then Me.DialogResult = DialogResult.OK
    End Sub
End Class