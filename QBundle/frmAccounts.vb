Public Class frmAccounts
    Private L As New Collection
    Private Structure Mjau
        Dim Name As String
        Dim Burst As String
        Dim Pin As String
    End Structure

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        If lstAccounts.SelectedIndex = -1 Then Exit Sub

        If MsgBox("Are you sure you want to delete account " & lstAccounts.Items.Item(lstAccounts.SelectedIndex).ToString & "?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Delete account") = MsgBoxResult.Yes Then

        End If
    End Sub

    Private Sub frmAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'loadaccounts


    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim S As New Mjau
        S.Name = txtName.Text
        S.Burst = txtPass.Text
        S.Pin = txtPin.Text
        L.Add(S)

        Dim F As Integer = 1
    End Sub
End Class