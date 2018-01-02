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
        Generic.UpdateLocalWallet()
        For t As Integer = 0 To UBound(QGlobal.Wallets)
            cmbWallet.Items.Add(QGlobal.Wallets(t).Name)
        Next
        cmbWallet.SelectedIndex = 0


    End Sub
    Private Sub SelectAccountID(sender As Object, e As EventArgs)
        Dim mnuitm As ToolStripMenuItem = Nothing
        Try
            mnuitm = DirectCast(sender, ToolStripMenuItem)
        Catch ex As Exception
            If Generic.DebugMe Then Generic.WriteDebug(ex.StackTrace, ex.Message)
            Exit Sub
        End Try
        txtAccount.Text = Q.Accounts.GetAccountRS(mnuitm.Text)
    End Sub
    Private Sub SelectPoolID(sender As Object, e As EventArgs)
        Dim mnuitm As ToolStripMenuItem = Nothing
        Try
            mnuitm = DirectCast(sender, ToolStripMenuItem)
        Catch ex As Exception
            If Generic.DebugMe Then Generic.WriteDebug(ex.StackTrace, ex.Message)
            Exit Sub
        End Try
        If mnuitm.Text = "Solo mining" Then
            txtPool.Text = txtAccount.Text
        Else
            For x As Integer = 0 To UBound(QGlobal.Pools)
                If mnuitm.Text = QGlobal.Pools(x).Name Then
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
            If txtAccount.Text.Length > 0 And UCase(txtAccount.Text).StartsWith("BURST-") Then
                For Each account As QB.clsAccounts.Account In Q.Accounts.AccArray
                    If account.RSAddress = txtAccount.Text Then
                        Dim pin As String = InputBox("Enter pin for account " & account.AccountName & " (" & account.RSAddress & ")", "Enter Pin", "")
                        If pin.Length > 0 Then
                            Dim tmp As String = Q.Accounts.GetPassword(account.AccountName, pin)
                            If tmp.Length > 0 Then
                                Passphrase = tmp
                            Else
                                MsgBox("You entered the wrong pin.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong Pin")
                                Exit Sub
                            End If
                        Else
                            MsgBox("You entered the wrong pin.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong Pin")
                            Exit Sub
                        End If
                        Exit For
                    End If
                Next
                'If no account then ask for passphrase
                If Passphrase.Length = 0 Then
                    Dim tmp As String = InputBox("Enter Passphrase for account (" & txtAccount.Text & ")", "Enter Passphrase", "")
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
            Else
                MsgBox("You must enter your account.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong account.")
                Exit Sub
            End If
            'now get AccountID from AccountRS for pool if not defined yet

            Dim AccID As String = ""
            'if only numeric the ok
            If txtPool.Text.Length > 0 Then
                If IsNumeric(txtPool.Text) And txtPool.Text.Length < 21 Then
                    AccID = txtPool.Text
                ElseIf UCase(txtPool.Text).StartsWith("BURST-") And txtPool.Text.Length = 26 Then
                    AccID = Q.Accounts.ConvertRSToId(txtPool.Text)
                Else
                    MsgBox("You seem to have entered an invalid pool address.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong pool address.")
                    Exit Sub
                End If
            Else
                MsgBox("You need to enter a pool address.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong pool address.")
                Exit Sub
            End If


            Dim http As New clsHttp


            Dim postData As String = "recipient=" & http.URLEncode(AccID)
            postData &= "&secretPhrase=" & http.URLEncode(Passphrase)
            postData &= "&requestType=" & http.URLEncode("setRewardRecipient")

            postData &= "&deadline=" & http.URLEncode("1440")
            postData &= "&feeNQT=" & http.URLEncode("100000000")
            postData &= "&submit="
            Dim result As String = http.PostUrl(QGlobal.Wallets(cmbWallet.SelectedIndex).Address & "/burst", postData)
            If result.Length > 0 Then
                If result.Contains("error") Then
                    MsgBox("Rewardassignment did not succeed.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Error :(")
                Else
                    MsgBox("Rewardassignment has been set.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "All done.")
                End If
            Else
                    MsgBox("Wallet seem to be offline. Try another wallet.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "No connection")
            End If
        Catch ex As Exception
            If Generic.DebugMe Then Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txtAccount.Text.Length < 10 Then
            MsgBox("You must fill in your account to be able to check the reward recipient status")
            Exit Sub
        End If

        If cmbWallet.SelectedIndex = 0 Then
            If Not frmMain.Running Then
                MsgBox("Your local wallet is not running. Please select an online wallet", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Not running")
                Exit Sub
            ElseIf Not frmMain.FullySynced Then
                MsgBox("Your local wallet is running but not fully synced. Please select an online wallet", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Not synced")
                Exit Sub
            End If
        End If
        Try


            Dim http As New clsHttp
            Dim buffer() As String = Nothing
            Dim AccountID As String = ""
            Dim PoolRS As String = ""
            Dim result() As String = Split(Replace(http.GetUrl(QGlobal.Wallets(cmbWallet.SelectedIndex).Address & "/burst?requestType=getRewardRecipient&account=" & txtAccount.Text), Chr(34), ""), ",")
            If result(0).StartsWith("{rewardRecipient:") Then
                AccountID = Mid(result(0), 18)
            Else
                MsgBox("Unable to get reward recipient status with selected wallet.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "No response")
                Exit Sub
            End If


            For t As Integer = 0 To UBound(QGlobal.Pools)
                If QGlobal.Pools(t).BurstAddress = "BURST-" & Q.Accounts.ConvertIdToRS(AccountID) Then
                    Dim msg As String
                    msg = "Your current reward recipient is:" & vbCrLf
                    msg &= "Burst address: " & QGlobal.Pools(t).BurstAddress & vbCrLf
                    msg &= "Name: " & QGlobal.Pools(t).Name & vbCrLf
                    MsgBox(msg, MsgBoxStyle.Information, "Reward recipient")
                    Exit Sub
                End If
            Next

            result = Split(Replace(http.GetUrl(QGlobal.Wallets(cmbWallet.SelectedIndex).Address & "/burst?requestType=getAccount&account=" & AccountID), Chr(34), ""), ",")
            If UBound(result) > 0 Then
                For t As Integer = 0 To UBound(result)
                    If result(t).StartsWith("name:") Then
                        Dim msg As String
                        msg = "Your current reward recipient is:" & vbCrLf
                        msg &= "Burst address: BURST-" & Q.Accounts.ConvertIdToRS(AccountID) & vbCrLf
                        msg &= "Name: " & Mid(result(t), 6)
                        MsgBox(msg, MsgBoxStyle.Information, "Reward recipient")
                        Exit Sub
                    End If
                Next
            Else
                Dim msg As String
                msg = "Your current reward recipient is:" & vbCrLf
                msg &= "Burst address: " & Q.Accounts.ConvertIdToRS(AccountID) & vbCrLf
                MsgBox(msg, MsgBoxStyle.Information, "Reward recipient")
            End If

        Catch ex As Exception
            MsgBox("Unable to get reward recipient status with selected wallet.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "No response")
            If Generic.DebugMe Then Generic.WriteDebug(ex.StackTrace, ex.Message)
            Exit Sub

        End Try


    End Sub
End Class