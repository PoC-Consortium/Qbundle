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
            Q.Accounts.DeleteAccount(lstAccounts.Items.Item(lstAccounts.SelectedIndex))
            ReloadAccountList()
            frmMain.SetLoginMenu()
        End If
    End Sub

    Private Sub frmAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReloadAccountList()
    End Sub
    Private Sub ReloadAccountList()

        lstAccounts.Items.Clear()
        For Each account As QB.clsAccounts.Account In Q.Accounts.AccArray
            lstAccounts.Items.Add(account.AccountName)
        Next

    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        For Each account As QB.clsAccounts.Account In Q.Accounts.AccArray
            If account.AccountName = txtName.Text Then
                MsgBox("You already have an account with that name.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Duplicate account name")
                Exit Sub
            End If
        Next
        If txtName.Text.Length < 1 Then
            MsgBox("You need to enter a name for the account.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "No name")
            Exit Sub
        End If
        If txtPass.Text.Length < 1 Then
            MsgBox("You need to enter a Burst passphrase for the account.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "No Passphrase")
            Exit Sub
        End If
        If txtPass.Text.Length < 35 Then
            If MsgBox("Your Burst passphrase is considered as a weak passphrase. You should concider another one. " & vbCrLf & "Do you still want to save it?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Weak account") = MsgBoxResult.No Then
                Exit Sub
            End If
        End If
        If txtPin.Text.Length < 6 Then
            MsgBox("You need to enter a valid Pin.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "No valid pin.")
            Exit Sub
        End If
        Q.Accounts.AddAccount(txtName.Text, txtPass.Text, txtPin.Text)
        lstAccounts.Items.Add(txtName.Text)
        Q.Accounts.SaveAccounts()
        frmMain.SetLoginMenu()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Dim KeySeed As String = ""
        Dim curwords As String = ""

        Randomize()
        Dim value As Integer = 0
        KeySeed = QGlobal.PassPhraseWords(CInt(Int(1626 * Rnd()))) & " "
        KeySeed &= QGlobal.PassPhraseWords(CInt(Int(1626 * Rnd()))) & " "
        KeySeed &= QGlobal.PassPhraseWords(CInt(Int(1626 * Rnd()))) & " "
        KeySeed &= QGlobal.PassPhraseWords(CInt(Int(1626 * Rnd()))) & " "
        KeySeed &= QGlobal.PassPhraseWords(CInt(Int(1626 * Rnd()))) & " "
        KeySeed &= QGlobal.PassPhraseWords(CInt(Int(1626 * Rnd()))) & " "
        KeySeed &= QGlobal.PassPhraseWords(CInt(Int(1626 * Rnd()))) & " "
        KeySeed &= QGlobal.PassPhraseWords(CInt(Int(1626 * Rnd()))) & " "
        KeySeed &= QGlobal.PassPhraseWords(CInt(Int(1626 * Rnd()))) & " "
        KeySeed &= QGlobal.PassPhraseWords(CInt(Int(1626 * Rnd()))) & " "
        KeySeed &= QGlobal.PassPhraseWords(CInt(Int(1626 * Rnd()))) & " "
        KeySeed &= QGlobal.PassPhraseWords(CInt(Int(1626 * Rnd())))

        txtPass.Text = KeySeed



    End Sub
End Class