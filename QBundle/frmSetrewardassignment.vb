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

        For x As Integer = 0 To UBound(Q.App.DynamicInfo.Pools)
            mnuitm = New ToolStripMenuItem
            mnuitm.Name = Q.App.DynamicInfo.Pools(x).Name
            mnuitm.Text = Q.App.DynamicInfo.Pools(x).Name
            AddHandler(mnuitm.Click), AddressOf SelectPoolID
            cmlPools.Items.Add(mnuitm)
        Next

        cmbWallet.Items.Clear()
        Generic.UpdateLocalWallet()
        For t As Integer = 0 To UBound(Q.App.DynamicInfo.Wallets)
            cmbWallet.Items.Add(Q.App.DynamicInfo.Wallets(t).Name)
        Next
        cmbWallet.SelectedIndex = 0


    End Sub
    Private Sub SelectAccountID(sender As Object, e As EventArgs)
        Dim mnuitm As ToolStripMenuItem = Nothing
        Try
            mnuitm = DirectCast(sender, ToolStripMenuItem)
        Catch ex As Exception
            Generic.WriteDebug(ex)
            Exit Sub
        End Try
        txtAccount.Text = Q.Accounts.GetAccountRS(mnuitm.Text)
    End Sub
    Private Sub SelectPoolID(sender As Object, e As EventArgs)
        Dim mnuitm As ToolStripMenuItem = Nothing
        Try
            mnuitm = DirectCast(sender, ToolStripMenuItem)
        Catch ex As Exception
            Generic.WriteDebug(ex)
            Exit Sub
        End Try
        If mnuitm.Text = "Solo mining" Then
            txtPool.Text = txtAccount.Text
        Else
            For x As Integer = 0 To UBound(Q.App.DynamicInfo.Pools)
                If mnuitm.Text = Q.App.DynamicInfo.Pools(x).Name Then
                    txtPool.Text = Q.App.DynamicInfo.Pools(x).BurstAddress
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
        Dim pwdf As New frmInput

        If Double.Parse(txtFee.Text) < 0.00735 Then
            MsgBox("You must set atleast 0.00735 as fee.")
            Exit Sub
        End If

        Dim fee As String = txtFee.Text.Replace(".", "").Replace(",", "").TrimStart("0").Trim

        Try

            Dim Passphrase As String = ""
            'check first for account and ask for pin
            If txtAccount.Text.Length > 0 And UCase(txtAccount.Text).StartsWith("BURST-") Then
                For Each account As QB.clsAccounts.Account In Q.Accounts.AccArray
                    If account.RSAddress = txtAccount.Text Then

                        pwdf.Text = "Enter your pin"
                        pwdf.lblInfo.Text = "Enter pin for account " & account.AccountName & " (" & account.RSAddress & ")"
                        If pwdf.ShowDialog() = DialogResult.OK Then
                            Dim pin As String = pwdf.txtPwd.Text
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
                    End If
                Next
                'If no account then ask for passphrase
                If Passphrase.Length = 0 Then
                    pwdf.Text = "Enter your pin"
                    pwdf.lblInfo.Text = "Enter Passphrase for account (" & txtAccount.Text & ")"
                    If pwdf.ShowDialog() = DialogResult.OK Then
                        Dim tmp As String = pwdf.txtPwd.Text
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
            postData &= "&feeNQT=" & http.URLEncode(fee)


            Dim result As String = http.PostUrl(Q.App.DynamicInfo.Wallets(cmbWallet.SelectedIndex).Address & "/burst", postData)
            If result.Length > 0 Then
                If result.Contains("error") Then
                    MsgBox("Rewardassignment did not succeed. Make sure the wallet you are using works and that you atleast have the fee amount in your account.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Error :(")
                Else
                    MsgBox("Rewardassignment has been set.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "All done.")
                End If
            Else
                MsgBox("Wallet seem to be offline. Try another wallet.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "No connection")
            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
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
            Dim result() As String = Split(Replace(http.GetUrl(Q.App.DynamicInfo.Wallets(cmbWallet.SelectedIndex).Address & "/burst?requestType=getRewardRecipient&account=" & txtAccount.Text), Chr(34), ""), ",")
            If result(0).StartsWith("{rewardRecipient:") Then
                AccountID = Mid(result(0), 18)
            Else
                MsgBox("Unable to get reward recipient status with selected wallet.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "No response")
                Exit Sub
            End If

            Dim msg As String
            For t As Integer = 0 To UBound(Q.App.DynamicInfo.Pools)
                If Q.App.DynamicInfo.Pools(t).BurstAddress = "BURST-" & Q.Accounts.ConvertIdToRS(AccountID) Then
                    msg = "Your current reward recipient is:" & vbCrLf
                    msg &= "Burst address: " & Q.App.DynamicInfo.Pools(t).BurstAddress & vbCrLf
                    msg &= "Name: " & Q.App.DynamicInfo.Pools(t).Name & vbCrLf
                    MsgBox(msg, MsgBoxStyle.Information, "Reward recipient")
                    Exit Sub
                End If
            Next

            result = Split(Replace(http.GetUrl(Q.App.DynamicInfo.Wallets(cmbWallet.SelectedIndex).Address & "/burst?requestType=getAccount&account=" & AccountID), Chr(34), ""), ",")
            If UBound(result) > 0 Then
                For t As Integer = 0 To UBound(result)
                    If result(t).StartsWith("name:") Then
                        msg = "Your current reward recipient is:" & vbCrLf
                        msg &= "Burst address: BURST-" & Q.Accounts.ConvertIdToRS(AccountID) & vbCrLf
                        msg &= "Name: " & Mid(result(t), 6)
                        MsgBox(msg, MsgBoxStyle.Information, "Reward recipient")
                        Exit Sub
                    End If
                Next
            End If

            msg = "Your current reward recipient is:" & vbCrLf
            msg &= "Burst address: BURST-" & Q.Accounts.ConvertIdToRS(AccountID) & vbCrLf
            MsgBox(msg, MsgBoxStyle.Information, "Reward recipient")

        Catch ex As Exception
            MsgBox("Unable to get reward recipient status with selected wallet.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "No response")
            Generic.WriteDebug(ex)
            Exit Sub

        End Try


    End Sub


End Class