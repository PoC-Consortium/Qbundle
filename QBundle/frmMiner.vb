Public Class frmMiner
    Private Sub frmMiner_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdatePlotList()


        cmlServers.Items.Clear()
        Dim mnuitm As ToolStripMenuItem
        For t As Integer = 0 To UBound(Q.App.DynamicInfo.Pools)
            mnuitm = New ToolStripMenuItem
            mnuitm.Name = Q.App.DynamicInfo.Pools(t).Name
            mnuitm.Text = Q.App.DynamicInfo.Pools(t).Name
            AddHandler(mnuitm.Click), AddressOf SelectPoolID
            cmlServers.Items.Add(mnuitm)
        Next

        rbSolo.Enabled = True
        rbSolo.Checked = False
        rbPool.Checked = False

        rbSolo.Text = "Solo Mining"

        If Not frmMain.Running Then
            rbSolo.Enabled = False
            '   rbPool.Checked = True
            rbSolo.Text = "Solo Mining (Local wallet is not running)"
        ElseIf Not frmMain.fullysynced Then
            rbSolo.Enabled = False
            rbSolo.Text = "Solo Mining (Local wallet is running but not fully syncronized)"
        End If

        lblReward.Visible = False
        If lstPlots.Items.Count > 0 Then
            If frmMain.Running And frmMain.FullySynced Then
                CheckRewardAssignment(0)
            ElseIf Q.settings.UseOnlineWallet Then
                CheckRewardAssignment(1)
            End If
        End If

        If QGlobal.CPUInstructions.SSE Then lblcputype.Text = "SSE"
        If QGlobal.CPUInstructions.AVX Then lblcputype.Text = "AVX"
        If QGlobal.CPUInstructions.AVX2 Then lblcputype.Text = "AVX2"

        If Q.settings.Solomining Then
            rbSolo.Enabled = True
            rbSolo.Checked = True
            pnlPool.Enabled = False
        Else
            rbPool.Enabled = True
            rbPool.Checked = True
            pnlPool.Enabled = True
        End If

        txtMiningServer.Text = Q.settings.MiningServer
        txtUpdateServer.Text = Q.settings.UpdateServer
        txtInfoServer.Text = Q.settings.InfoServer
        nrMiningPort.Value = Q.settings.MiningServerPort
        nrUpdatePort.Value = Q.settings.UpdateServerPort
        nrInfoPort.Value = Q.settings.InfoServerPort
        txtDeadLine.Text = Q.settings.Deadline
        chkUseHDD.Checked = Q.settings.Hddwakeup
        chkShowWinner.Checked = Q.settings.ShowWinner
        chkUseBoost.Checked = Q.settings.UseMultithread

    End Sub



    Private Sub CheckRewardAssignment(ByVal WalletId As Integer)

        'get accountid from plotfile

        Try
            Dim PlotAccount As String = GetAccountIdFromPlot(lstPlots.Items.Item(0).ToString)

            Dim http As New clsHttp
            Dim buffer() As String = Nothing
            Dim AccountID As String = ""
            Dim PoolRS As String = ""
            Dim result() As String = Split(Replace(http.GetUrl(Q.App.DynamicInfo.Wallets(WalletId).Address & "/burst?requestType=getRewardRecipient&account=" & PlotAccount), Chr(34), ""), ",")
            If result(0).StartsWith("{rewardRecipient:") Then
                AccountID = Mid(result(0), 18)
            Else
                Exit Sub
            End If
            'check if it solomining first
            If AccountID = PlotAccount Then
                Dim msg As String
                msg = "Your reward recipient Is set to" & vbCrLf
                msg &= "Account: " & Q.Accounts.ConvertIdToRS(AccountID) & vbCrLf
                msg &= "Only solomining available"
                lblReward.Text = msg
                lblReward.Visible = True
                rbPool.Enabled = False
                If rbSolo.Enabled = False Then
                    btnStartMine.Enabled = False
                Else
                    rbSolo.Checked = True
                    rbPool.Checked = False
                End If
            End If


            For t As Integer = 0 To UBound(Q.App.DynamicInfo.Pools)
                If Q.App.DynamicInfo.Pools(t).BurstAddress = "BURST-" & Q.Accounts.ConvertIdToRS(AccountID) Then
                    Dim msg As String
                    msg = "Your reward recipient Is set to" & vbCrLf
                    msg &= "Account: " & Q.App.DynamicInfo.Pools(t).BurstAddress & vbCrLf
                    msg &= "Name: " & Q.App.DynamicInfo.Pools(t).Name
                    lblReward.Text = msg
                    lblReward.Visible = True
                    SelectPoolByAccountID(AccountID)
                    Exit Sub
                End If
            Next

            result = Split(Replace(http.GetUrl(Q.App.DynamicInfo.Wallets(WalletId).Address & "/burst?requestType=getAccount&account=" & AccountID), Chr(34), ""), ",")
            If UBound(result) > 0 Then
                For t As Integer = 0 To UBound(result)
                    If result(t).StartsWith("name:") Then
                        Dim msg As String
                        msg = "Your reward recipient Is set to" & vbCrLf
                        msg &= "Account: " & Q.Accounts.ConvertIdToRS(AccountID) & vbCrLf
                        msg &= "Name: " & Mid(result(t), 6)
                        lblReward.Text = msg
                        lblReward.Visible = True
                        Exit Sub
                    End If
                Next
            Else
                Dim msg As String
                msg = "Your reward recipient Is set to" & vbCrLf
                msg &= "Account: " & Q.Accounts.ConvertIdToRS(AccountID) & vbCrLf
                lblReward.Text = msg
                lblReward.Visible = True
            End If

        Catch ex As Exception
            '   MsgBox("Unable to get reward recipient status with selected wallet.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "No response")
            Generic.WriteDebug(ex)
            Exit Sub

        End Try


    End Sub

    Private Sub SelectPoolID(sender As Object, e As EventArgs)
        Dim mnuitm As ToolStripMenuItem = Nothing
        Try
            mnuitm = DirectCast(sender, ToolStripMenuItem)
        Catch ex As Exception
            Generic.WriteDebug(ex)
            Exit Sub
        End Try
        For x As Integer = 0 To UBound(Q.App.DynamicInfo.Pools)
            If mnuitm.Text = Q.App.DynamicInfo.Pools(x).Name Then
                txtMiningServer.Text = Q.App.DynamicInfo.Pools(x).Address
                nrMiningPort.Value = CDec(Val(Q.App.DynamicInfo.Pools(x).Port))
                txtUpdateServer.Text = Q.App.DynamicInfo.Pools(x).Address
                nrUpdatePort.Value = CDec(Val(Q.App.DynamicInfo.Pools(x).Port))
                txtInfoServer.Text = Q.App.DynamicInfo.Pools(x).Address
                nrInfoPort.Value = CDec(Val(Q.App.DynamicInfo.Pools(x).Port))
                txtDeadLine.Text = Q.App.DynamicInfo.Pools(x).DeadLine
                Exit For
            End If
        Next

    End Sub
    Private Sub SelectPoolByAccountID(ByVal AccountID As String)
        For x As Integer = 0 To UBound(Q.App.DynamicInfo.Pools)
            If AccountID = Q.Accounts.ConvertRSToId(Q.App.DynamicInfo.Pools(x).BurstAddress) Then
                txtMiningServer.Text = Q.App.DynamicInfo.Pools(x).Address
                nrMiningPort.Value = CDec(Val(Q.App.DynamicInfo.Pools(x).Port))
                txtUpdateServer.Text = Q.App.DynamicInfo.Pools(x).Address
                nrUpdatePort.Value = CDec(Val(Q.App.DynamicInfo.Pools(x).Port))
                txtInfoServer.Text = Q.App.DynamicInfo.Pools(x).Address
                nrInfoPort.Value = CDec(Val(Q.App.DynamicInfo.Pools(x).Port))
                txtDeadLine.Text = Q.App.DynamicInfo.Pools(x).DeadLine
                Exit For
            End If
        Next
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Me.cmImport.Show(Me.btnImport, Me.btnImport.PointToClient(Cursor.Position))



    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If lstPlots.SelectedIndex = -1 Then
            MsgBox("You need to select a plot to remove.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Nothing to remove")
            Exit Sub
        End If

        If MsgBox("Are you sure you want to remove selected plot(s)?" & vbCrLf & "It will not be deleted from disk.", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Remove plotfile") = MsgBoxResult.Yes Then
            Q.settings.Plots = ""
            For i As Integer = 0 To lstPlots.Items.Count - 1
                If Not lstPlots.GetSelected(i) = True Then
                    Q.settings.Plots &= lstPlots.Items.Item(i).ToString & "|"
                End If
            Next
            Q.settings.SaveSettings()
            UpdatePlotList()
        End If
    End Sub

    Private Sub btnPool_Click(sender As Object, e As EventArgs) Handles btnPool.Click
        Try
            Me.cmlServers.Show(Me.btnPool, Me.btnPool.PointToClient(Cursor.Position))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnStartMine_Click(sender As Object, e As EventArgs) Handles btnStartMine.Click

        'generic checks

        Dim PassPhrase As String = ""

        If lstPlots.Items.Count = 0 Then
            MsgBox("You need to add your plots to My plotfiles in the bottom before you can mine.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "No plotfiles")
            Exit Sub
        End If
        If Not IsNumeric(txtDeadLine.Text) Then
            MsgBox("Your deadline is not a numeric value.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "No deadline")
            Exit Sub
        End If

        If rbSolo.Checked Then
            'Do checks for solo
            If Not frmMain.Running Then
                MsgBox("Your local wallet is not running. Please start your local wallet and make sure it is synced.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "No wallet to mine against")
                Exit Sub
            End If

            Dim AccountId As String = GetAccountIdFromPlot(lstPlots.Items.Item(0).ToString)
            If lstPlots.Items.Count > 1 Then
                For t As Integer = 1 To lstPlots.Items.Count - 1
                    If Not AccountId = GetAccountIdFromPlot(lstPlots.Items(t).ToString) Then
                        MsgBox("When mining solo you cannot mine with different accounts at the same time. Please remove the plotfiles with account id that does not match the account you want to mine with.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Multiple accounts.")
                        Exit Sub
                    End If
                Next
            End If

            'check account and ask for pin or passphrase
            'check first for account and ask for pin

            For Each account As QB.clsAccounts.Account In Q.Accounts.AccArray
                If account.AccountID = AccountId Then
                    Dim pin As String = InputBox("Enter pin for account " & account.AccountName & " (" & account.RSAddress & ") used in plotfiles.", "Enter Pin", "")
                    If pin.Length > 0 Then
                        Dim tmp As String = Q.Accounts.GetPassword(account.AccountName, pin)
                        If tmp.Length > 0 Then
                            PassPhrase = tmp
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
            If PassPhrase.Length = 0 Then
                Dim tmp As String = InputBox("Enter passphrase for account BURST-" & Q.Accounts.ConvertIdToRS(AccountId) & " (" & AccountId & ") used in plotfiles.", "Enter Passphrase", "")
                If tmp.Length > 0 Then
                    If AccountId = Q.Accounts.GetAccountIDFromPassPhrase(tmp) Then
                        PassPhrase = tmp
                    Else
                        MsgBox("Passphrase does not match accountid in plotfiles.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong passphrase")
                        Exit Sub
                    End If
                Else
                    MsgBox("You entered the wrong passphrase.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong passphrase")
                    Exit Sub
                End If
            End If



        Else
            'we need to do additional checks when it comes to pool settings.
            If txtMiningServer.Text.Length < 4 Or Not txtMiningServer.Text.Contains(".") Then
                MsgBox("You need to enter a valid mining server address.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Mining server missing")
                Exit Sub
            End If
            If txtUpdateServer.Text.Length < 4 Or Not txtUpdateServer.Text.Contains(".") Then
                MsgBox("You need to enter a valid update server address.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Update server missing")
                Exit Sub
            End If
            If txtInfoServer.Text.Length < 4 Or Not txtInfoServer.Text.Contains(".") Then
                MsgBox("You need to enter a valid info server address.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Info server missing")
                Exit Sub
            End If

            If txtMiningServer.Text.Contains("/") Then
                MsgBox("Mining server shold not be defined as a url.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Mining server wrong")
                Exit Sub
            End If

            If txtUpdateServer.Text.Contains("/") Then
                MsgBox("Update server shold not be defined as a url.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Update server wrong")
                Exit Sub
            End If

            If txtInfoServer.Text.Contains("/") Then
                MsgBox("Info server shold not be defined as a url.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Info server wrong")
                Exit Sub
            End If

            'now we can only asume it is correct.

        End If
        Dim msg As String = ""
        msg &= "Blago Miner is not installed yet. Do you want to download and install it now?" & vbCrLf & vbCrLf
        msg &= "Please be advised that Miners can be detected as malware by antimalware software." & vbCrLf
        msg &= "If you have a antimalware software you might need to whitelist the miner." & vbCrLf

        Q.App.SetLocalInfo() 'do a precheck due to antivir removals
        If Not Q.App.isInstalled(QGlobal.AppNames.BlagoMiner) Then
            If MsgBox(msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Download Miner") = MsgBoxResult.Yes Then
                Dim s As frmDownloadExtract = New frmDownloadExtract
                s.Appid = QGlobal.AppNames.BlagoMiner
                Dim res As DialogResult
                Me.Hide()
                res = s.ShowDialog
                Me.Show()
                If res = DialogResult.Cancel Then
                    Exit Sub
                ElseIf res = DialogResult.Abort Then
                    MsgBox("Something went wrong. Internet connection might have been lost.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        End If
        Threading.Thread.Sleep(100)
        Q.App.SetLocalInfo() 'update to check that it really is installed.
        If Not Q.App.isInstalled(QGlobal.AppNames.BlagoMiner) Then
            MsgBox("Miner was downloaded but removed. Please check Antimalware software.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        'ok all seems fine. if solo then write password file
        If rbSolo.Checked Then
            If PassPhrase.Length > 0 Then
                System.IO.File.WriteAllText(QGlobal.AppDir & "\BlagoMiner\passphrases.txt", PassPhrase)
            End If
        Else
            'lets make sure pass is not there
            Try
                If IO.File.Exists(QGlobal.AppDir & "\BlagoMiner\passphrases.txt") Then
                    IO.File.Move(QGlobal.AppDir & "\BlagoMiner\passphrases.txt", QGlobal.AppDir & "\BlagoMiner\passphrases.txt.qbundle")
                End If
            Catch ex As Exception
                MsgBox("Unable to remove passphrases.txt. Refusing to start poolmining.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Stop.")
                Exit Sub
            End Try

        End If
            WriteConfig()

        If rbSolo.Checked Then
            Q.settings.Solomining = True
        Else
            Q.settings.Solomining = False
        End If
        Q.settings.MiningServer = txtMiningServer.Text
        Q.settings.UpdateServer = txtUpdateServer.Text
        Q.settings.InfoServer = txtInfoServer.Text
        Q.settings.MiningServerPort = nrMiningPort.Value
        Q.settings.UpdateServerPort = nrUpdatePort.Value
        Q.settings.InfoServerPort = nrInfoPort.Value
        Q.settings.Deadline = txtDeadLine.Text
        Q.settings.Hddwakeup = chkUseHDD.Checked
        Q.settings.ShowWinner = chkShowWinner.Checked
        Q.settings.UseMultithread = chkUseBoost.Checked
        Q.settings.SaveSettings()


        Try
            Dim p As Process = New Process
            p.StartInfo.WorkingDirectory = QGlobal.AppDir & "BlagoMiner"
            p.StartInfo.Arguments = ""
            p.StartInfo.UseShellExecute = True
            If QGlobal.CPUInstructions.AVX2 Then
                p.StartInfo.FileName = QGlobal.AppDir & "BlagoMiner\BlagoMiner_avx2.exe"
            ElseIf QGlobal.CPUInstructions.AVX Then
                p.StartInfo.FileName = QGlobal.AppDir & "BlagoMiner\BlagoMiner_avx.exe"
            ElseIf QGlobal.CPUInstructions.SSE Then
                p.StartInfo.FileName = QGlobal.AppDir & "BlagoMiner\BlagoMiner_sse.exe"
            End If
            p.StartInfo.Verb = "runas"
            p.Start()
        Catch ex As Exception
            MsgBox("Failed to start Blago miner. This is most likely because of antimalware software.")
        End Try
        If PassPhrase.Length > 0 Then
            tmrRemovePass.Enabled = True
        End If

    End Sub
    Private Function GetAccountIdFromPlot(ByVal Plotfile As String) As String
        Dim filename As String = IO.Path.GetFileName(Plotfile)
        Dim buffer() As String = Split(filename, "_")
        Return buffer(0)
    End Function
    Private Sub WriteConfig()
        Dim plots As String = ""
        Dim Buffer(lstPlots.Items.Count - 1) As String
        For t As Integer = 0 To lstPlots.Items.Count - 1
            If Buffer.Contains(IO.Path.GetDirectoryName(lstPlots.Items.Item(t).ToString)) Then
            Else
                Buffer(t) = IO.Path.GetDirectoryName(lstPlots.Items.Item(t).ToString)
                plots &= Chr(34) & IO.Path.GetDirectoryName(lstPlots.Items.Item(t).ToString) & Chr(34) & ","
            End If
        Next
        plots = Replace(plots, "\", "\\")
        plots = plots.Substring(0, plots.Length - 1)
        Dim Mode As String = ""
        If rbPool.Checked Then
            Mode = "pool"
        Else
            Mode = "solo"
        End If

        Dim UseHdd As String = "false"
        Dim ShowWInner As String = "false"
        Dim UseBoost As String = "false"

        If chkUseHDD.Checked Then UseHdd = "true"
        If chkUseBoost.Checked Then UseBoost = "true"
        If chkShowWinner.Checked Then ShowWInner = "true"

        Dim cfg As String = ""
        cfg &= "{" & vbCrLf
        cfg &= "   " & Chr(34) & "Mode" & Chr(34) & " : " & Chr(34) & Mode & Chr(34) & "," & vbCrLf
        cfg &= "   " & Chr(34) & "Server" & Chr(34) & " : " & Chr(34) & txtMiningServer.Text & Chr(34) & "," & vbCrLf
        cfg &= "   " & Chr(34) & "Port" & Chr(34) & ": " & nrMiningPort.Value.ToString & "," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "UpdaterAddr" & Chr(34) & " : " & Chr(34) & txtUpdateServer.Text & Chr(34) & "," & vbCrLf
        cfg &= "   " & Chr(34) & "UpdaterPort" & Chr(34) & ": " & Chr(34) & nrMiningPort.Value.ToString & Chr(34) & "," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "InfoAddr" & Chr(34) & " : " & Chr(34) & txtInfoServer.Text & Chr(34) & "," & vbCrLf
        cfg &= "   " & Chr(34) & "InfoPort" & Chr(34) & ": " & Chr(34) & nrInfoPort.Value.ToString & Chr(34) & "," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "EnableProxy" & Chr(34) & ": false," & vbCrLf
        cfg &= "   " & Chr(34) & "ProxyPort" & Chr(34) & ": 8126," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "Paths" & Chr(34) & ":[" & plots & "]," & vbCrLf
        cfg &= "   " & Chr(34) & "CacheSize" & Chr(34) & " : 10000," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "Debug" & Chr(34) & ": true," & vbCrLf
        cfg &= "   " & Chr(34) & "UseHDDWakeUp" & Chr(34) & ": " & UseHdd & "," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "TargetDeadline" & Chr(34) & ": " & txtDeadLine.Text & "," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "SendInterval" & Chr(34) & ": 100," & vbCrLf
        cfg &= "   " & Chr(34) & "UpdateInterval" & Chr(34) & ": 950," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "UseLog" & Chr(34) & " : true," & vbCrLf
        cfg &= "   " & Chr(34) & "ShowWinner" & Chr(34) & " : " & ShowWInner & "," & vbCrLf
        cfg &= "   " & Chr(34) & "UseBoost" & Chr(34) & " : " & UseBoost & "," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "WinSizeX" & Chr(34) & ": 76," & vbCrLf
        cfg &= "   " & Chr(34) & "WinSizeY" & Chr(34) & ": 60" & vbCrLf
        cfg &= "}" & vbCrLf

        System.IO.File.WriteAllText(QGlobal.AppDir & "BlagoMiner\miner.conf", cfg)

    End Sub

    Private Sub rbPool_CheckedChanged(sender As Object, e As EventArgs) Handles rbPool.Click

        pnlPool.Enabled = True

    End Sub

    Private Sub rbSolo_CheckedChanged(sender As Object, e As EventArgs) Handles rbSolo.Click
        pnlPool.Enabled = False

        Generic.UpdateLocalWallet()

        Dim buffer() As String = Split(Replace(Q.App.DynamicInfo.Wallets(0).Address, "http://", ""), ":")
        nrMiningPort.Value = CDec(Val(buffer(1)))
        nrUpdatePort.Value = CDec(Val(buffer(1)))
        nrInfoPort.Value = CDec(Val(buffer(1)))
        txtMiningServer.Text = buffer(0)
        txtUpdateServer.Text = buffer(0)
        txtInfoServer.Text = buffer(0)
        txtDeadLine.Text = "86400"


    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

    End Sub

    Private Sub ImportFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportFileToolStripMenuItem.Click
        Dim ofd As New OpenFileDialog
        If ofd.ShowDialog = DialogResult.OK Then
            If IO.File.Exists(ofd.FileName) Then
                lstPlots.Items.Add(ofd.FileName)
                Q.settings.Plots &= ofd.FileName & "|"
                Q.settings.SaveSettings()
                If lstPlots.Items.Count = 1 Then
                    If frmMain.Running And frmMain.FullySynced Then
                        CheckRewardAssignment(0)
                    ElseIf Q.settings.UseOnlineWallet Then
                        CheckRewardAssignment(1)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ImportFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportFolderToolStripMenuItem.Click
        Dim ofd As New FolderBrowserDialog
        If ofd.ShowDialog = DialogResult.OK Then
            If IO.Directory.Exists(ofd.SelectedPath) Then
                Dim fileEntries As String() = IO.Directory.GetFiles(ofd.SelectedPath)
                For Each file As String In fileEntries
                    lstPlots.Items.Add(file)
                    Q.settings.Plots &= file & "|"
                Next
                Q.settings.SaveSettings()
            End If
        End If
    End Sub

    Private Sub tmrRemovePass_Tick(sender As Object, e As EventArgs) Handles tmrRemovePass.Tick
        Try
            tmrRemovePass.Enabled = False
            System.IO.File.Delete(QGlobal.AppDir & "\BlagoMiner\passphrases.txt")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub lblSelectAll_Click(sender As Object, e As EventArgs) Handles lblSelectAll.Click
        If lstPlots.Items.Count <= 0 Then Exit Sub
        For i As Integer = 0 To lstPlots.Items.Count - 1
            Me.lstPlots.SetSelected(i, True)
        Next
    End Sub

    Private Sub lblDeselectAll_Click(sender As Object, e As EventArgs) Handles lblDeselectAll.Click
        If lstPlots.Items.Count <= 0 Then Exit Sub
        For i As Integer = 0 To lstPlots.Items.Count - 1
            Me.lstPlots.SetSelected(i, False)
        Next
    End Sub
    Private Sub UpdatePlotList()
        lstPlots.Items.Clear()
        If Q.settings.Plots <> "" Then
            Dim buffer() As String = Split(Q.settings.Plots, "|")
            For Each plot As String In buffer
                If plot.Length > 1 Then
                    lstPlots.Items.Add(plot)
                End If
            Next
        End If
    End Sub

    Private Sub rbSolo_CheckedChanged_1(sender As Object, e As EventArgs) Handles rbSolo.CheckedChanged
        '     setMiningType()

    End Sub

    Private Sub rbPool_CheckedChanged_1(sender As Object, e As EventArgs) Handles rbPool.CheckedChanged
        '   setMiningType()

    End Sub
End Class