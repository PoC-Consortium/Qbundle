Public Class frmSetrewardassignment
    Private Sub btnAccounts_Click(sender As Object, e As EventArgs) Handles btnAccounts.Click
        Try
            Me.cmlAccounts.Show(Me.btnAccounts, Me.btnAccounts.PointToClient(Cursor.Position))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmSetrewardassignment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmlAccounts.Items.Clear()
        Dim mnuitm As ToolStripMenuItem
        For Each account As QB.clsAccounts.Account In Q.Accounts.AccArray
            mnuitm = New ToolStripMenuItem
            mnuitm.Name = account.AccountName
            mnuitm.Text = account.AccountName
            AddHandler(mnuitm.Click), AddressOf SelectAccountID
            cmlAccounts.Items.Add(mnuitm)
        Next

        cmlPools.Items.Clear()
        mnuitm = New ToolStripMenuItem
        mnuitm.Name = "Solo mining"
        mnuitm.Text = "Solo mining"
        AddHandler(mnuitm.Click), AddressOf SelectPoolID
        cmlPools.Items.Add(mnuitm)

        For x As Integer = 0 To UBound(QGlobal.Pools)
            mnuitm = New ToolStripMenuItem
            mnuitm.Name = QGlobal.Pools(x).Name
            mnuitm.Text = QGlobal.Pools(x).Name
            AddHandler(mnuitm.Click), AddressOf SelectPoolID
            cmlPools.Items.Add(mnuitm)
        Next

        cmbWallet.Items.Clear()
        For t As Integer = 0 To UBound(QGlobal.Wallets)
            cmbWallet.Items.Add(QGlobal.Wallets(t).Name)
        Next
        cmbWallet.SelectedIndex = 0


    End Sub
    Private Sub SelectAccountID(sender As Object, e As EventArgs)
        txtAccount.Text = Q.Accounts.GetAccountRS(sender.text)
    End Sub
    Private Sub SelectPoolID(sender As Object, e As EventArgs)
        If sender.text = "Solo mining" Then
            txtPool.Text = txtAccount.Text
        Else
            For x As Integer = 0 To UBound(QGlobal.Pools)
                If sender.text = QGlobal.Pools(x).Name Then
                    txtPool.Text = QGlobal.Pools(x).BurstAddress
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub btnPool_Click(sender As Object, e As EventArgs) Handles btnPool.Click
        Try
            Me.cmlPools.Show(Me.btnPool, Me.btnPool.PointToClient(Cursor.Position))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        Try

            Dim Passphrase As String = ""
            'check first for account and ask for pin
            For Each account As QB.clsAccounts.Account In Q.Accounts.AccArray
                If account.RSAddress = txtAccount.Text Then
                    Dim tmp As String = InputBox("Enter pin for account " & account.AccountName & " (" & account.RSAddress & ")", "Enter Pin", "")
                    If tmp.Length > 0 Then
                        Passphrase = tmp
                    Else
                        MsgBox("You entered the wrong pin.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong Pin")
                        Exit Sub
                    End If
                    Exit For
                End If
            Next
            'If no account then ask for passphrase
            If Passphrase.Length = 0 Then
                Dim tmp As String = InputBox("Enter Passphrase for account (" & txtAccount.Text & ")", "Enter Pin", "")
                If tmp.Length > 0 Then
                    If UCase(txtAccount.Text) = UCase("BURST-" & Q.Accounts.GetRSFromPassPhrase(tmp)) Then
                        Passphrase = tmp
                    Else
                        MsgBox("You entered the wrong passphrase.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong passphrase")
                        Exit Sub
                    End If
                Else
                    MsgBox("You entered the wrong passphrase.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong passphrase")
                    Exit Sub
                End If
            End If

            'else ask for passphrase

            Dim http As New clsHttp
            Dim postData As String = "recipient=" & Generic.URLEncode(txtPool.Text)
            postData &= "&secretPhrase=" & Generic.URLEncode(Passphrase)
            postData &= "&requestType=" & Generic.URLEncode("setRewardRecipient")

            postData &= "&deadline=" & Generic.URLEncode("1440")
            postData &= "&feeNQT=" & Generic.URLEncode("100000000")
            postData &= "&submit="
            Dim result As String = http.PostUrl(QGlobal.Wallets(cmbWallet.SelectedIndex).Address & "/burst", postData)
            If result.Length > 0 Then
                'we need to check response to know what to check for
            Else
                MsgBox("Wallet seem to be offline. Try another wallet.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "No connection")
            End If


        Catch ex As Exception
            If Generic.DebugMe Then Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
    End Sub
End Class