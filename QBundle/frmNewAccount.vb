Imports System.Security.Cryptography

Public Class frmNewAccount

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        txtPass.Text = MakeSeed(12)
    End Sub


    Private Function MakeSeed(ByVal nrWords As Integer) As String
        Dim KeySeed As String = ""
        Dim RNGCSP As RNGCryptoServiceProvider = New RNGCryptoServiceProvider()
        Dim rngdata((nrWords * 2) - 1) As Byte
        RNGCSP.GetBytes(rngdata)
        For t As Integer = 0 To (nrWords * 2) - 1 Step 2
            KeySeed &= QGlobal.PassPhraseWords(((rngdata(t) * 256) + rngdata(t + 1)) Mod 1626) & " "
        Next
        If KeySeed.Length > 0 Then
            Return Mid(KeySeed, 1, KeySeed.Length - 1)
        End If
        Return ""
    End Function


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
            If MsgBox("Your Burst passphrase is considered as a weak passphrase. You should consider another one. " & vbCrLf & "Do you still want to save it?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Weak account") = MsgBoxResult.No Then
                Exit Sub
            End If
        End If
        If txtPin.Text.Length < 6 Then
            MsgBox("You need to enter a valid Pin.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "No valid pin.")
            Exit Sub
        End If
        Me.DialogResult = DialogResult.OK
    End Sub
End Class
