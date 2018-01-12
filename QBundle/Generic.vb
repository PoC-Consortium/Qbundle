Imports System.Management
Imports System.Net
Imports System.Net.Sockets
Imports System.Text.RegularExpressions
Imports Microsoft.Win32

Friend Class Generic
    Public Shared DebugMe As Boolean
    Friend Shared Sub CheckUpgrade()
        Dim CurVer As Integer = Reflection.Assembly.GetExecutingAssembly.GetName.Version.Major * 10
        CurVer += Reflection.Assembly.GetExecutingAssembly.GetName.Version.Minor
        Dim OldVer As Integer = Q.settings.Upgradev
        If CurVer <= OldVer Then Exit Sub
        Do
            Select Case OldVer
                Case 11 'upgrade from 11 to 12
                    'check for old settings file. if there is one make the settings.
                    'if there is we use maria and local java
                    Try
                        'only execute this if there is a settings.ini.
                        If IO.File.Exists(QGlobal.BaseDir & "\Settings.ini") Then
                            Dim lines() As String = IO.File.ReadAllLines(QGlobal.BaseDir & "\Settings.ini")
                            Q.settings.FirstRun = False
                            'CheckForUpdates, True, 3
                            Dim cell() As String
                            For Each line As String In lines
                                If line <> "" Then
                                    cell = Split(line, ",")
                                    Select Case cell(0)
                                        Case "CheckForUpdates"
                                            If cell(1) = "True" Then
                                                Q.settings.CheckForUpdates = True
                                            Else
                                                Q.settings.CheckForUpdates = False
                                            End If
                                    End Select
                                    Q.settings.DbName = "burstwallet"
                                    Q.settings.DbUser = "burstwallet"
                                    Q.settings.DbPass = "burstwallet"
                                    Q.settings.DbServer = "localhost:3306"
                                    Q.settings.DbType = QGlobal.DbType.pMariaDB
                                    Q.settings.JavaType = QGlobal.AppNames.JavaPortable
                                End If
                            Next
                            IO.File.Delete(QGlobal.BaseDir & "\Settings.ini")
                            Q.settings.SaveSettings()
                        End If



                    Catch ex As Exception
                        If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
                    End Try

                Case 12 'from 12-13
                    Q.settings.UseOnlineWallet = Q.settings.CheckForUpdates
                Case 13 ' from 13-14
                    Q.settings.GetCoinMarket = Q.settings.CheckForUpdates
                    Q.settings.NTPCheck = Q.settings.CheckForUpdates
                Case 14 ' from 14-15
                Case 15 ' from 15-16
                Case 16 ' from 16-17
                    Try
                        If IO.File.Exists(QGlobal.BaseDir & "\Acconts.xml") Then
                            IO.File.Move(QGlobal.BaseDir & "\Acconts.xml", QGlobal.BaseDir & "\Accounts.xml")
                            Q.Accounts.LoadAccounts()
                        End If
                    Catch ex As Exception
                        If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
                    End Try

                    Select Case Q.settings.DbType
                        Case QGlobal.DbType.FireBird
                            Q.settings.DbServer = "jdbc:firebirdsql:embedded:./burst_db/burst.firebird.db"
                            Q.settings.DbUser = "sa"
                            Q.settings.DbPass = "sa"
                        Case QGlobal.DbType.pMariaDB
                            Q.settings.DbServer = "jdbc:mariadb://localhost:3306/burstwallet"
                            Q.settings.DbUser = "burstwallet"
                            Q.settings.DbPass = "burstwallet"
                        Case QGlobal.DbType.MariaDB
                            Q.settings.DbServer = "jdbc:mariadb://" & Q.settings.DbServer & "/" & Q.settings.DbName
                        Case QGlobal.DbType.H2
                            Q.settings.DbServer = "jdbc:h2:./burst_db/burst;DB_CLOSE_ON_EXIT=False"
                            Q.settings.DbUser = "sa"
                            Q.settings.DbPass = "sa"
                    End Select
                    Q.settings.SaveSettings()
                Case 17 '  from 17-18
                    'must check settings
                    Select Case Q.settings.DbType
                        Case QGlobal.DbType.FireBird
                            Q.settings.DbServer = "jdbc:firebirdsql:embedded:./burst_db/burst.firebird.db"
                            Q.settings.DbUser = "sa"
                            Q.settings.DbPass = "sa"
                        Case QGlobal.DbType.pMariaDB
                            Q.settings.DbServer = "jdbc:mariadb://localhost:3306/burstwallet"
                            Q.settings.DbUser = "burstwallet"
                            Q.settings.DbPass = "burstwallet"
                        Case QGlobal.DbType.H2
                            Q.settings.DbServer = "jdbc:h2:./burst_db/burst;DB_CLOSE_ON_EXIT=False"
                            Q.settings.DbUser = "sa"
                            Q.settings.DbPass = "sa"
                    End Select
                    Q.settings.SaveSettings()
                Case 18 '18 -19
                    Q.settings.Currency = "USD"
                    Q.settings.SaveSettings()
            End Select
            OldVer += 1
            If CurVer = OldVer Then Exit Do
        Loop

        Q.settings.Upgradev = CurVer
        Q.settings.SaveSettings()

    End Sub
    Friend Shared Sub WriteBRSConfig(Optional ByVal WriteDebug As Boolean = False)
        Dim Data As String = ""
        Dim Buffer() As String = Nothing
        'writing brs.properties

        'Peer settings
        Data &= "#Peer network" & vbCrLf
        Buffer = Split(Q.settings.ListenPeer, ";")
        Data &= "brs.peerServerPort = " & Buffer(1) & vbCrLf
        Data &= "brs.peerServerHost = " & Buffer(0) & vbCrLf & vbCrLf

        'API settings
        Data &= "#API network" & vbCrLf
        Buffer = Split(Q.settings.ListenIf, ";")
        Data &= "brs.apiServerPort = " & Buffer(1) & vbCrLf
        Data &= "brs.apiServerHost = " & Buffer(0) & vbCrLf
        If Q.settings.ConnectFrom.Contains("0.0.0.0") Then
            Data &= "brs.allowedBotHosts = *" & vbCrLf & vbCrLf
        Else
            Data &= "brs.allowedBotHosts = " & Q.settings.ConnectFrom & vbCrLf & vbCrLf
        End If


        'autoip
        If Q.settings.AutoIp Then
            Dim ip As String = GetMyIp()
            If ip <> "" Then
                Data &= "#Auto IP set" & vbCrLf
                Data &= "brs.myAddress = " & ip & vbCrLf & vbCrLf
            End If
        End If

        'Dyn platform
        If Q.settings.DynPlatform Then
            Data &= "#Dynamic platform" & vbCrLf
            Data &= "brs.myPlatform = Q-" & Q.App.GetDbNameFromType(Q.settings.DbType) & vbCrLf & vbCrLf
        End If

        Select Case Q.settings.DbType
            Case QGlobal.DbType.FireBird
                Try
                    If Not IO.Directory.Exists(QGlobal.AppDir & "burst_db") Then
                        IO.Directory.CreateDirectory(QGlobal.AppDir & "burst_db")
                    End If
                Catch ex As Exception
                    If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
                End Try
                Data &= "#Using Firebird" & vbCrLf
                Data &= "brs.dbUrl = jdbc:firebirdsql:embedded:./burst_db/burst.firebird.db" & vbCrLf
                Data &= "brs.dbUsername = sa" & vbCrLf
                Data &= "brs.dbPassword = sa" & vbCrLf & vbCrLf
            Case QGlobal.DbType.pMariaDB
                Data &= "#Using MariaDb Portable" & vbCrLf
                Data &= "brs.dbUrl = jdbc:mariadb://localhost:3306/burstwallet" & vbCrLf
                Data &= "brs.dbUsername = burstwallet" & vbCrLf
                Data &= "brs.dbPassword = burstwallet" & vbCrLf & vbCrLf
            Case QGlobal.DbType.MariaDB
                Data &= "#Using installed MariaDb" & vbCrLf
                Data &= "brs.dbUrl=jdbc:mariadb://" & Q.settings.DbServer & "/" & Q.settings.DbName & vbCrLf
                Data &= "brs.dbUsername = " & Q.settings.DbUser & vbCrLf
                Data &= "brs.dbPassword = " & Q.settings.DbPass & vbCrLf & vbCrLf
            Case QGlobal.DbType.H2
                Data &= "#Using H2" & vbCrLf
                Data &= "brs.dbMaximumPoolSize = 30" & vbCrLf
                Data &= "brs.dbUrl=jdbc:h2:./burst_db/burst;DB_CLOSE_ON_EXIT=False" & vbCrLf
                Data &= "brs.dbUsername = sa" & vbCrLf
                Data &= "brs.dbPassword = sa" & vbCrLf & vbCrLf
        End Select

        Data &= "brs.dbUrl = " & Q.settings.DbServer & vbCrLf
        Data &= "brs.dbUsername = " & Q.settings.DbUser & vbCrLf
        Data &= "brs.dbPassword = " & Q.settings.DbPass & vbCrLf & vbCrLf



        If Q.settings.useOpenCL Then
            Data &= "#CPU Offload" & vbCrLf
            Data &= "brs.oclAuto = true" & vbCrLf
            Data &= "brs.oclHashesPerEnqueue=100" & vbCrLf
            Data &= "brs.oclVerify = true" & vbCrLf & vbCrLf

        End If
        If WriteDebug Then
            Data &= "#Debug mode" & vbCrLf
            Data &= "brs.disablePeerConnectingThread = true" & vbCrLf
            Data &= "brs.enableTransactionRebroadcasting=false" & vbCrLf
            Data &= "brs.disableGetMorePeersThread = true " & vbCrLf
            Data &= "brs.disableProcessTransactionsThread = true" & vbCrLf
            Data &= "brs.disableRemoveUnconfirmedTransactionsThread = true" & vbCrLf
            Data &= "brs.disableRebroadcastTransactionsThread = true" & vbCrLf
            Data &= "brs.apiServerEnforcePOST = false" & vbCrLf
            Data &= "brs.enableDebugAPI = true" & vbCrLf & vbCrLf
        End If
        Try
            IO.File.WriteAllText(QGlobal.AppDir & "conf\brs.properties", Data)
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try



    End Sub
    Friend Shared Sub WriteWalletConfig(Optional ByVal WriteDebug As Boolean = False)
        WriteNRSConfig(WriteDebug)
        ' WriteBRSConfig(WriteDebug)
    End Sub
    Friend Shared Sub WriteNRSConfig(Optional ByVal WriteDebug As Boolean = False)
        Dim Data As String = ""
        Dim Buffer() As String = Nothing
        'writing brs.properties

        'Peer settings
        Data &= "#Peer network" & vbCrLf
        Buffer = Split(Q.settings.ListenPeer, ";")
        Data &= "nxt.peerServerPort = " & Buffer(1) & vbCrLf
        Data &= "nxt.peerServerHost = " & Buffer(0) & vbCrLf & vbCrLf

        'API settings
        Data &= "#API network" & vbCrLf
        Buffer = Split(Q.settings.ListenIf, ";")
        Data &= "nxt.apiServerPort = " & Buffer(1) & vbCrLf
        Data &= "nxt.apiServerHost = " & Buffer(0) & vbCrLf
        If Q.settings.ConnectFrom.Contains("0.0.0.0") Then
            Data &= "nxt.allowedBotHosts = *" & vbCrLf & vbCrLf
        Else
            Data &= "nxt.allowedBotHosts = " & Q.settings.ConnectFrom & vbCrLf & vbCrLf
        End If

        'autoip
        If Q.settings.AutoIp Then
            Dim ip As String = GetMyIp()
            If ip <> "" Then
                Data &= "#Auto IP set" & vbCrLf
                Data &= "nxt.myAddress = " & ip & vbCrLf & vbCrLf
            End If
        Else
            If Q.settings.Broadcast.Length > 0 Then
                Data &= "#Manual broadcast" & vbCrLf
                Data &= "nxt.myAddress = " & Q.settings.Broadcast & vbCrLf & vbCrLf
            End If
        End If

        'Dyn platform
        If Q.settings.DynPlatform Then
            Data &= "#Dynamic platform" & vbCrLf
            Data &= "nxt.myPlatform = Q-" & Q.App.GetDbNameFromType(Q.settings.DbType) & vbCrLf & vbCrLf
        End If

        Select Case Q.settings.DbType
            Case QGlobal.DbType.FireBird
                Try
                    If Not IO.Directory.Exists(QGlobal.AppDir & "burst_db") Then
                        IO.Directory.CreateDirectory(QGlobal.AppDir & "burst_db")
                    End If
                Catch ex As Exception
                    If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
                End Try
                Data &= "#Using Firebird" & vbCrLf

            Case QGlobal.DbType.pMariaDB
                Data &= "#Using MariaDb Portable" & vbCrLf
            Case QGlobal.DbType.MariaDB
                Data &= "#Using installed MariaDb" & vbCrLf
            Case QGlobal.DbType.H2
                Data &= "#Using H2" & vbCrLf
                Data &= "nxt.dbMaximumPoolSize = 30" & vbCrLf
        End Select

        Data &= "nxt.dbUrl = " & Q.settings.DbServer & vbCrLf
        Data &= "nxt.dbUsername = " & Q.settings.DbUser & vbCrLf
        Data &= "nxt.dbPassword = " & Q.settings.DbPass & vbCrLf & vbCrLf



        If Q.settings.useOpenCL Then
            Data &= "#CPU Offload" & vbCrLf
            Data &= "burst.oclAuto = true" & vbCrLf
            Data &= "burst.oclHashesPerEnqueue=100" & vbCrLf
            Data &= "burst.oclVerify = true" & vbCrLf & vbCrLf
        End If

        If WriteDebug Then
            Data &= "#Debug mode" & vbCrLf
            Data &= "nxt.disablePeerConnectingThread = true" & vbCrLf
            Data &= "nxt.enableTransactionRebroadcasting=false" & vbCrLf
            Data &= "nxt.disableGetMorePeersThread = true " & vbCrLf
            Data &= "nxt.disableProcessTransactionsThread = true" & vbCrLf
            Data &= "nxt.disableRemoveUnconfirmedTransactionsThread = true" & vbCrLf
            Data &= "nxt.disableRebroadcastTransactionsThread = true" & vbCrLf
            Data &= "nxt.apiServerEnforcePOST = false" & vbCrLf
            Data &= "nxt.enableDebugAPI = true" & vbCrLf & vbCrLf
        End If
        Try
            IO.File.WriteAllText(QGlobal.AppDir & "conf\nxt.properties", Data)
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try



    End Sub
    Friend Shared Function CalculateBytes(ByVal value As Long, ByVal decimals As Integer, Optional ByVal startat As Integer = 0) As String
        Dim t As Integer
        Dim res As Double = CDbl(value)
        For t = startat To 10
            If res > 1024 Then
                res /= 1024
            Else
                Exit For
            End If
        Next
        If startat = 11 Then
            Return Math.Round(res, decimals).ToString("0.00")
        End If
        If t = 0 Then
            Return Math.Round(res, decimals).ToString("0.00") & "bytes"
        End If
        If t = 1 Then
            Return Math.Round(res, decimals).ToString("0.00") & "KiB"
        End If
        If t = 2 Then
            Return Math.Round(res, decimals).ToString("0.00") & "MiB"
        End If
        If t = 3 Then
            Return Math.Round(res, decimals).ToString("0.00") & "GiB"
        End If
        If t = 4 Then
            Return Math.Round(res, decimals).ToString("0.00") & "TiB"
        End If
        Return "??"

    End Function
    Friend Shared Function IsAdmin() As Boolean
        Try
            If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Friend Shared Sub SetFirewallFromSettings()

        Dim s() As String
        Dim buffer As String
        If IsAdmin() Then
            s = Split(Q.settings.ListenPeer, ";")
            If s(0) = "0.0.0.0" Then s(0) = "*"
            If Not SetFirewall("Burst Peers", s(1), s(0), "") Then
                MsgBox("Failed to apply firewall rules.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Firewall")
                End
            End If
            s = Split(Q.settings.ListenIf, ";")
            If s(0) = "0.0.0.0" Then s(0) = "*"
            buffer = Trim(Q.settings.ConnectFrom)
            If buffer <> "" Then
                buffer = buffer.Replace(";", ",")
                buffer = buffer.Replace(" ", "")
                If buffer.EndsWith(",") Then buffer = buffer.Remove(buffer.Length - 1)
            End If
            If Not SetFirewall("Burst Api", s(1), s(0), buffer) Then
                MsgBox("Failed to apply firewall rules.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Firewall")
                End
            End If
            MsgBox("Windows firewall rules sucessfully applied.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Firewall")
        Else
            'start it as admin
            Try
                Dim p As Process = New Process
                p.StartInfo.WorkingDirectory = QGlobal.BaseDir
                p.StartInfo.Arguments = "ADDFW"
                p.StartInfo.UseShellExecute = True
                p.StartInfo.FileName = Application.ExecutablePath
                p.StartInfo.Verb = "runas"
                p.Start()
            Catch ex As Exception
                MsgBox("Failed to apply firewall rules.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Firewall")
            End Try
        End If


    End Sub

    Private Shared Function SetFirewall(ByVal fwName As String, ByVal ports As String, LocalNet As String, RemoteNet As String) As Boolean
        Try
            'first we try to remove old rule if any
            Const NET_FW_IP_PROTOCOL_TCP = 6
            Const NET_FW_RULE_DIR_IN = 1
            Const NET_FW_ACTION_ALLOW = 1
            Dim fwPolicy2 As Object = CreateObject("HNetCfg.FwPolicy2")
            Dim RulesObject As Object = fwPolicy2.Rules
            'remove old if exists
            RulesObject.Remove(fwName)
            'add new settings
            Dim CurrentProfiles As Object = fwPolicy2.CurrentProfileTypes
            Dim NewRule As Object = CreateObject("HNetCfg.FWRule")
            NewRule.Name = fwName
            NewRule.Description = "Allows incoming traffic to " & fwName
            NewRule.Protocol = NET_FW_IP_PROTOCOL_TCP
            NewRule.LocalPorts = ports
            NewRule.Direction = NET_FW_RULE_DIR_IN
            NewRule.Enabled = True
            NewRule.LocalAddresses = LocalNet
            NewRule.RemoteAddresses = RemoteNet '"127.0.0.1/255.255.255.255,192.168.0.0/255.255.255.0,192.168.1.0/255.255.0.0"
            NewRule.Profiles = 7 'CurrentProfiles
            NewRule.Action = NET_FW_ACTION_ALLOW
            RulesObject.Add(NewRule)
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function
    Friend Shared Sub CheckCommandArgs()
        '0 = appname
        '1 = Type to do

        Dim clArgs() As String = Environment.GetCommandLineArgs()
        If UBound(clArgs) > 0 Then
            Select Case clArgs(1)
                Case "ADDFW"
                    Try
                        QB.Generic.SetFirewallFromSettings()
                    Catch ex As Exception
                        MsgBox("Failed to apply firewall rules. Maybe you run another firewall on your computer?", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Firewall")
                    End Try
                    End
                Case "InstallService"
                    If Q.Service.InstallService() Then
                        MsgBox("Sucessfully installed burstwallet as a service.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Service")
                    Else
                        MsgBox("Unable to install burstwallet as a service.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Service")
                    End If
                    End

                Case "UnInstallService"
                    If Q.Service.UninstallService Then
                        MsgBox("Sucessfully removed burstwallet from services.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Service")
                    Else
                        MsgBox("Unable to remove burstwallet from services.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Service")
                    End If
                    End

                Case "StartService"
                    Q.Service.StartService()
                    End
                Case "StopService"
                    Q.Service.StopService()
                    End
                Case "Debug"
                    QB.Generic.DebugMe = True
                Case "BetaUpdate"
                    Q.App.UpdateInfo = "BetaUpdate"
            End Select
        End If
    End Sub
    Friend Shared Function SanityCheck() As Boolean

        Dim Ok As Boolean = True

        Dim cmdline As String = ""
        Dim Msg As String = ""
        Dim res As MsgBoxResult = Nothing
        'Check if Java is running another burst.jar
        Try
            Dim searcher As ManagementObjectSearcher
            searcher = New ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_Process WHERE Name='java.exe'")
            For Each p As ManagementObject In searcher.[Get]()
                cmdline = p("CommandLine").ToString
                If cmdline.ToLower.Contains("burst.jar") Then
                    Msg = "Qbundle has detected that another burst wallet is running." & vbCrLf
                    Msg &= "If the other wallet use the same setting as this one. it will not work." & vbCrLf
                    Msg &= "Would you like to stop the other wallet?" & vbCrLf & vbCrLf
                    Msg &= "Yes = Stop the other wallet and start this one." & vbCrLf
                    Msg &= "No = Start this wallet despite the other wallet." & vbCrLf
                    Msg &= "Cancel = Do not start this one." & vbCrLf
                    res = MsgBox(Msg, MsgBoxStyle.Information Or MsgBoxStyle.YesNoCancel, "Another wallet is running")
                    If res = MsgBoxResult.Yes Then
                        Dim proc As Process = Process.GetProcessById(Integer.Parse(p("ProcessId").ToString))
                        proc.Kill()
                        Threading.Thread.Sleep(1000)
                    ElseIf res = MsgBoxResult.No Then
                        'do nothing 
                    Else
                        Ok = False
                    End If
                End If
            Next

        Catch ex As Exception
            If Generic.DebugMe Then Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
        Try
            If Q.settings.DbType = QGlobal.DbType.pMariaDB And Ok = True Then
                cmdline = ""
                Msg = ""
                Dim searcher As ManagementObjectSearcher
                searcher = New ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_Process WHERE Name='mysqld.exe'")
                For Each p As ManagementObject In searcher.[Get]()
                    ' cmdline = p("CommandLine")
                    Msg = "Qbundle has detected that another Mysql/MariaDB is running." & vbCrLf
                    Msg &= "If the other database use the same setting as this one. it will not work." & vbCrLf
                    Msg &= "Would you like to stop the other database?" & vbCrLf & vbCrLf
                    Msg &= "Yes = Stop the other database and start this one." & vbCrLf
                    Msg &= "No = Start this database despite the other database." & vbCrLf
                    Msg &= "Cancel = Do not start this one." & vbCrLf
                    res = MsgBox(Msg, MsgBoxStyle.Information Or MsgBoxStyle.YesNoCancel, "Another database is running")
                    If res = MsgBoxResult.Yes Then
                        Dim proc As Process = Process.GetProcessById(Integer.Parse(p("ProcessId").ToString))
                        proc.Kill()
                        Threading.Thread.Sleep(1000)
                    ElseIf res = MsgBoxResult.No Then
                        'do nothing 
                    Else
                        Ok = False
                    End If
                Next
            End If
        Catch ex As Exception
            If Generic.DebugMe Then Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
        If Q.settings.NTPCheck Then
            Try
                Dim ntpTime As Date = GetNTPTime("time.windows.com")
                Dim OffSeconds As Integer = 0
                Dim localTimezoneNTPTime As Date = TimeZoneInfo.ConvertTime(ntpTime, TimeZoneInfo.Utc, TimeZoneInfo.Local)
                If Now > localTimezoneNTPTime Then
                    OffSeconds = CInt((Now - localTimezoneNTPTime).TotalSeconds)
                ElseIf Now < localTimezoneNTPTime Then
                    OffSeconds = CInt((localTimezoneNTPTime - Now).TotalSeconds)
                End If

                If OffSeconds > 15 Then
                    Msg = "Your computer clock is drifting." & vbCrLf
                    Msg &= "World time (UTC): " & ntpTime.ToString & vbCrLf
                    Msg &= "Your computer time: " & Now.ToString & vbCrLf & vbCrLf
                    Msg &= "Your computer time with current timezone setting " & vbCrLf
                    Msg &= "should be: " & localTimezoneNTPTime.ToString & vbCrLf & vbCrLf
                    Msg &= "Your time is currently off with " & OffSeconds.ToString & " Seconds" & vbCrLf
                    Msg &= "Burstwallet allows max 15 seconds drifting." & vbCrLf
                    MsgBox(Msg, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Change computer time")
                    Ok = False
                End If
            Catch ex As Exception
                If Generic.DebugMe Then Generic.WriteDebug(ex.StackTrace, ex.Message)
            End Try
        End If
        Return Ok
    End Function
    Friend Shared Function IsProcessRunning(ByVal Name As String) As Boolean
        Dim Ok As Boolean = True
        Dim searcher As ManagementObjectSearcher
        Dim RetVal As Boolean = False
        Dim prc As String = ""
        searcher = New ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_Process") ' WHERE Name='" & exeName & "'
        For Each p As ManagementObject In searcher.[Get]()
            prc = LCase(p("Name").ToString)
            If prc.Contains(LCase(Name)) Then RetVal = True
        Next
        Return RetVal
    End Function
    Friend Shared Sub KillAllProcessesWithName(ByVal Name As String)
        Dim Ok As Boolean = True
        Dim searcher As ManagementObjectSearcher
        Dim prc As String = ""
        searcher = New ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_Process") ' WHERE Name='" & exeName & "'
        For Each p As ManagementObject In searcher.[Get]()
            prc = LCase(p("Name").ToString)
            If prc.Contains(LCase(Name)) Then
                Dim proc As Process = Process.GetProcessById(Integer.Parse(p("ProcessId").ToString))
                proc.Kill()
                Threading.Thread.Sleep(100)
            End If


        Next
    End Sub
    Friend Shared Function GetMyIp() As String
        Try
            Dim WC As Net.WebClient = New Net.WebClient()
            Return WC.DownloadString("http://files.getburst.net/ip.php")
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
        Return ""
    End Function
    Friend Shared Function CheckWritePermission() As Boolean
        Try
            IO.File.WriteAllText(QGlobal.AppDir & "testfile", "test")
            IO.File.Delete(QGlobal.AppDir & "testfile")
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Friend Shared Sub WriteDebug(ByVal strace As String, ByVal msg As String)

        Try
            Dim strErr As String = "------------------------- " & Now.ToString & " --------------------------" & vbCrLf
            strErr &= "Message: " & msg & vbCrLf
            strErr &= "StackTrace:" & strace & vbCrLf
            IO.File.AppendAllText(QGlobal.LogDir & "\bwl_debug.txt", strErr)
        Catch ex As Exception
            MsgBox(msg)
        End Try


    End Sub
    Friend Shared Sub RestartAsAdmin()

        Try
            Dim p As Process = New Process
            p.StartInfo.WorkingDirectory = QGlobal.BaseDir
            p.StartInfo.UseShellExecute = True
            If QB.Generic.DebugMe Then p.StartInfo.Arguments = "Debug"
            p.StartInfo.FileName = Application.ExecutablePath
            p.StartInfo.Verb = "runas"
            p.Start()
        Catch ex As Exception
            MsgBox("Failed to start Qbundle as administrator.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Admin")
        End Try
    End Sub
    Public Shared Function GetLatencyMs(ByRef hostNameOrAddress As String) As Long
        Dim ping As New System.Net.NetworkInformation.Ping
        Return ping.Send(hostNameOrAddress, 300).RoundtripTime
    End Function
    Public Shared Sub UpdateLocalWallet()
        Dim s() As String = Split(Q.settings.ListenIf, ";")
        Dim url As String = Nothing
        If s(0) = "0.0.0.0" Then
            url = "http://127.0.0.1:" & s(1)
        Else
            url = "http://" & s(0) & ":" & s(1)
        End If
        QGlobal.Wallets(0).Address = url
    End Sub
    Friend Shared Function GetStartNonce(ByVal AccountID As String, ByVal Length As Double) As Double

        Dim Plotfiles() As String

        Dim StartNonce As Double = 0
        Dim EndNonce As Double = StartNonce + Length
        Dim HighestEndNonce As Double = 0
        Dim PEndNonce As Double = 0
        Try
            If Q.settings.Plots.Length > 0 Then
                Plotfiles = Split(Q.settings.Plots, "|")
                For Each Plot As String In Plotfiles
                    If Plot.Length > 1 Then
                        Dim N() As String = Split(IO.Path.GetFileName(Plot), "_")
                        If UBound(N) = 3 Then
                            If N(0) = Trim(AccountID) Then
                                PEndNonce = CDbl(N(1)) + CDbl(N(2))
                                If PEndNonce > HighestEndNonce Then HighestEndNonce = PEndNonce
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Return 0
        End Try

        Return HighestEndNonce
    End Function
    Public Shared Function GetNTPTime(ByVal ntpServer As String) As Date

        Dim socket = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
        Try

            Dim ntpData = New Byte(47) {}
            ntpData(0) = &H1B
            Dim addresses = Dns.GetHostEntry(ntpServer).AddressList
            Dim ipEndPoint = New IPEndPoint(addresses(0), 123)

            Socket.SendTimeout = 5000
            socket.ReceiveTimeout = 5000

            socket.Connect(ipEndPoint)
            socket.Send(ntpData)
            socket.Receive(ntpData)
            socket.Close()

            Dim intPart As ULong = CULng(ntpData(40)) << 24 Or CULng(ntpData(41)) << 16 Or CULng(ntpData(42)) << 8 Or CULng(ntpData(43))
            Dim fractPart As ULong = CULng(ntpData(44)) << 24 Or CULng(ntpData(45)) << 16 Or CULng(ntpData(46)) << 8 Or CULng(ntpData(47))

            Dim milliseconds = (intPart * 1000) + ((fractPart * 1000) / &H100000000L)
            Dim networkDateTime = (New DateTime(1900, 1, 1)).AddMilliseconds(CLng(milliseconds))

            Return networkDateTime

        Catch ex As Exception
            If Generic.DebugMe Then Generic.WriteDebug(ex.StackTrace, ex.Message)
        Finally
            socket.Dispose()
        End Try
        Return Now

    End Function

    Friend Shared Function PlotDriveTypeOk(ByVal path As String) As Boolean

        Dim TheDrive As System.IO.DriveInfo = New System.IO.DriveInfo(path)

        If TheDrive.DriveFormat = "NTFS" Then
            Return True
        End If


        Return False

    End Function

    Friend Shared Function CheckDotNet() As Boolean
        Const subkey As String = "SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\"
        Using ndpKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey)
            If ndpKey IsNot Nothing AndAlso ndpKey.GetValue("Release") IsNot Nothing Then
                If ((CInt(ndpKey.GetValue("Release")) >= 379893)) Then 'ver 4.5.2
                    Return True
                End If
            End If
        End Using
        Return False
    End Function

    Friend Shared Function IsValidPlottFilename(filename As String) As Boolean
        If IsNothing(filename) Then Return False
        Return Regex.IsMatch(filename, "\d+_\d+_\d+_\d+")
    End Function

    Friend Shared Function IsValidPlottFilePath(filepath As String) As Boolean
        If IsNothing(filepath) Then Return False
        Return IsValidPlottFilename(IO.Path.GetFileName(filepath))
    End Function


End Class
