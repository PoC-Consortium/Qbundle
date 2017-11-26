Imports System.Threading
Imports System.IO.Pipes
Imports System.Security.Principal
Imports System.Text
Imports System.Management
'


Public Class clsProcessHandler
    Private Settings As clsSettings
    'NRS Vars
    Private nrs As Process
    Private Startsignalfound As Boolean 'use to know if nrs has started 
    Private ConsoleBuffer As String
    Private Isrunning As Boolean
    'PipeVars
    Dim pipeServer As NamedPipeServerStream
    Private buffer(1023) As Byte
    Private LastException As Date
    'Generics
    Private Enum CtrlTypes As UInteger
        CTRL_C_EVENT = 0
        CTRL_BREAK_EVENT
        CTRL_CLOSE_EVENT
        CTRL_LOGOFF_EVENT = 5
        CTRL_SHUTDOWN_EVENT
    End Enum
    Private Delegate Function ConsoleCtrlDelegate(CtrlType As CtrlTypes) As Boolean
    Private Declare Function AttachConsole Lib "kernel32" (dwProcessId As UInteger) As Boolean
    Private Declare Sub GenerateConsoleCtrlEvent Lib "kernel32" (ByVal dwCtrlEvent As Short, ByVal dwProcessGroupId As Short)
    Private Declare Function SetConsoleCtrlHandler Lib "kernel32" (Handler As ConsoleCtrlDelegate, Add As Boolean) As Boolean
    Private Declare Function FreeConsole Lib "kernel32" () As Boolean
    Private ShouldStop As Boolean
    Private BaseDir As String

#Region " Main Functions "
    Sub New()
        BaseDir = AppDomain.CurrentDomain.BaseDirectory
        If Not BaseDir.EndsWith("\") Then BaseDir &= "\"
        Settings = New clsSettings
        Settings.LoadSettings()
        LastException = Now
        Isrunning = False
        nrs = New Process
    End Sub
    Public Enum AppNames As Integer
        JavaInstalled = 2
        JavaPortable = 3
    End Enum
    Public Sub Start()
        If Not Isrunning Then
            Isrunning = True
            ShouldStop = False
            Dim trd As Thread
            trd = New Thread(AddressOf NRSMonitor)
            trd.IsBackground = True
            trd.Start()

        End If
    End Sub

    Public Sub Quit()
        ShouldStop = True
        ShutDown(15000, 10000)
    End Sub

#End Region


#Region " Thread NRS "

    Private Sub NRSMonitor()
        'v1 of this asumes that we are in the same dir as launcher and burst.jar
        StartNrs()
        Thread.Sleep(5000)
        Do
            If Not IsBurstRunning() Then
                If ShouldStop Then
                    Exit Do
                Else
                    StartNrs()
                End If
            End If
            If ShouldStop Then Exit Do
            Thread.Sleep(500) '2 times a second check is enough for now.
        Loop
    End Sub

    Private Sub StartNrs()


        nrs.StartInfo.WorkingDirectory = BaseDir
        nrs.StartInfo.Arguments = "-cp burst.jar;conf nxt.Nxt"
        nrs.StartInfo.UseShellExecute = False

        '   nrs.StartInfo.RedirectStandardError = True
        nrs.StartInfo.CreateNoWindow = True

        AddHandler nrs.ErrorDataReceived, AddressOf ErroutHandler
        If Settings.JavaType = AppNames.JavaPortable Then
            nrs.StartInfo.FileName = BaseDir & "Java\bin\java.exe"
        Else
            nrs.StartInfo.FileName = "java"
        End If
        Try
            nrs.Start()
            Dim cores As Integer = (2 ^ Settings.Cpulimit) - 1
            If cores <> 0 Then nrs.ProcessorAffinity = CType(cores, IntPtr)
            '  nrs.BeginErrorReadLine()
            System.IO.File.AppendAllText(BaseDir & "\debugservice.txt", nrs.Id.ToString & vbCrLf)
        Catch ex As Exception

            Exit Sub
        End Try

    End Sub

    Sub ErroutHandler(sender As Object, e As DataReceivedEventArgs)
        If Not String.IsNullOrEmpty(e.Data) Then
            If e.Data.Contains("Started API server at") Then Startsignalfound = True
            ConsoleBuffer &= e.Data & vbCrLf
            If Settings.WalletException And LastException.AddHours(1) < Now Then
                If e.Data.StartsWith("Exception in") Or e.Data.StartsWith("java.lang.RuntimeException") Then
                    LastException = Now
                    ShutDown(15000, 10000) 'nrs monitor will start it again.
                End If
            End If
        End If
    End Sub

    Private Sub ShutDown(ByVal SigIntSleep As Integer, ByVal SigKillSleep As Integer)
        Try
            AttachConsole(nrs.Id)
            SetConsoleCtrlHandler(New ConsoleCtrlDelegate(AddressOf OnExit), True)
            GenerateConsoleCtrlEvent(CtrlTypes.CTRL_C_EVENT, 0)
            nrs.WaitForExit(SigIntSleep) 'wait for exit before we release. if not we might get ourself terminated.
            If Not nrs.HasExited Then
                nrs.Kill()
                nrs.WaitForExit(SigKillSleep)
            End If
            FreeConsole()
        Catch ex As Exception

        End Try
        Try
            nrs.Kill() 'fixing no java cleanup.
        Catch ex As Exception

        End Try
    End Sub
    Private Function OnExit(CtrlType As CtrlTypes) As Boolean
        Return True
    End Function

    Friend Function IsBurstRunning() As Boolean

        Dim Ok As Boolean = True
        Dim searcher As ManagementObjectSearcher
        Dim cmdline As String = ""
        Dim Msg As String = ""
        Dim res As MsgBoxResult = Nothing
        'Check if Java is running another burst.jar
        searcher = New ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_Process WHERE Name='java.exe'")
        For Each p As ManagementObject In searcher.[Get]()
            cmdline = p("CommandLine")
            If cmdline.ToLower.Contains("burst.jar") Then
                Return True
            End If
        Next

        Return False
    End Function
#End Region

#Region " Pipe Server "

    Private Sub PipeServerStart()

        Try
            pipeServer = New NamedPipeServerStream("BurstPipe", PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous)
            pipeServer.BeginWaitForConnection(New AsyncCallback(AddressOf WaitForConnectionCallBack), pipeServer)
        Catch ex As Exception

        End Try

    End Sub


    Public Sub StopServer()
        Try
            If pipeServer.IsConnected Then pipeServer.Disconnect()
            pipeServer.Close()
            pipeServer.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Send(ByVal Msg As String)
        Try
            If pipeServer.IsConnected Then
                Dim sendbuffer() As Byte = Encoding.UTF8.GetBytes(Msg)
                pipeServer.Write(sendbuffer, 0, sendbuffer.Length)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub WaitForConnectionCallBack(iar As IAsyncResult)
        Try
            pipeServer.EndWaitForConnection(iar)
            pipeServer.BeginRead(buffer, 0, buffer.Length, AddressOf Incmessage, iar)
        Catch
        End Try
    End Sub

    Private Sub Incmessage(iar As IAsyncResult)
        Dim totalbytes As Integer = pipeServer.EndRead(iar)
        Dim stringData As String = ""
        Dim Senddata As String = ""

        Try
            stringData = Encoding.UTF8.GetString(buffer, 0, totalbytes)
            '   If stringData <> "" Then RaiseEvent PipeMessage(stringData)
        Catch ex As Exception
        End Try

        Try
            If pipeServer.IsConnected Then
                pipeServer.BeginRead(buffer, 0, buffer.Length, AddressOf Incmessage, pipeServer)
            Else 'if client disconnected
                If Not ShouldStop Then
                    pipeServer.Close()
                    pipeServer.Dispose()
                    pipeServer = Nothing
                    pipeServer = New NamedPipeServerStream("BurstPipe", PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous)
                    pipeServer.BeginWaitForConnection(New AsyncCallback(AddressOf WaitForConnectionCallBack), pipeServer)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub













#End Region
End Class
