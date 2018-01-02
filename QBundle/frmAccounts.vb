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
            Q.Accounts.DeleteAccount(lstAccounts.Items.Item(lstAccounts.SelectedIndex).ToString)
            ReloadAccountList()
            frmMain.SetLoginMenu()
        End If
    End Sub

    Private Sub frmAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReloadAccountList()
        txtPassprase.PasswordChar = CChar("*")
        txtPrivateKey.PasswordChar = CChar("*")
    End Sub
    Private Sub ReloadAccountList()

        lstAccounts.Items.Clear()
        For Each account As QB.clsAccounts.Account In Q.Accounts.AccArray
            lstAccounts.Items.Add(account.AccountName)
        Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim newAcc As New frmNewAccount
        If newAcc.ShowDialog = DialogResult.OK Then
            Q.Accounts.AddAccount(newAcc.txtName.Text, newAcc.txtPass.Text, newAcc.txtPin.Text)
            lstAccounts.Items.Add(newAcc.txtName.Text)
            Q.Accounts.SaveAccounts()
            frmMain.SetLoginMenu()
            MsgBox("Please make sure that you write down the passphrase on paper or remember it. The passphrase is your only access to your account. If you loose it you will never be able to access your account again.", MsgBoxStyle.Information, "Save your passphrase")
        End If
    End Sub

    Private Sub revealPwd_Click(sender As Object, e As EventArgs) Handles revealPwd.Click
        If lstAccounts.SelectedIndex = -1 Then Exit Sub
        Dim AccName As String = lstAccounts.Items.Item(lstAccounts.SelectedIndex).ToString
        Dim pwdf As New frmInput
        pwdf.Text = "Enter your pin"
        pwdf.lblInfo.Text = "Enter the pin for the account " & AccName
        If pwdf.ShowDialog() = DialogResult.OK Then
            Dim pin As String = pwdf.txtPwd.Text
            If pin.Length > 5 Then
                Dim Pass As String = Q.Accounts.GetPassword(AccName, pin)
                If Pass.Length > 0 Then

                    txtPassprase.PasswordChar = CChar("")
                    txtPassprase.Text = Pass

                Else
                    MsgBox("You entered the wrong pin.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong pin")
                End If
            Else
                MsgBox("You entered the wrong pin.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong pin")
            End If
        End If
    End Sub

    Private Sub revealPk_Click(sender As Object, e As EventArgs) Handles revealPk.Click
        If lstAccounts.SelectedIndex = -1 Then Exit Sub
        Dim AccName As String = lstAccounts.Items.Item(lstAccounts.SelectedIndex).ToString
        Dim pwdf As New frmInput
        pwdf.Text = "Enter your pin"
        pwdf.lblInfo.Text = "Enter the pin for the account " & AccName
        If pwdf.ShowDialog() = DialogResult.OK Then
            Dim pin As String = pwdf.txtPwd.Text
            If pin.Length > 5 Then
                Dim PK As String = Q.Accounts.GetPrivateKey(AccName, pin)
                If PK.Length > 0 Then
                    txtPrivateKey.PasswordChar = CChar("")
                    txtPrivateKey.Text = PK
                Else
                    MsgBox("You entered the wrong pin.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong pin")
                End If
            Else
                MsgBox("You entered the wrong pin.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong pin")
            End If
        End If

    End Sub

    Private Sub lstAccounts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstAccounts.SelectedIndexChanged
        If lstAccounts.SelectedIndex = -1 Then Exit Sub
        Dim AccName As String = lstAccounts.Items.Item(lstAccounts.SelectedIndex).ToString
        lblName.Text = AccName
        txtRs.Text = Q.Accounts.GetAccountRS(AccName)
        txtNr.Text = Q.Accounts.GetAccountID(AccName)
        txtPublicKey.Text = Q.Accounts.GetPublicKey(AccName)
        txtPassprase.Text = "***********"
        txtPrivateKey.Text = "***********"
    End Sub
End Class