Imports System.Runtime.InteropServices
Imports System.Threading
Public Class clsProcessHandler
    Public Event Update(ByVal Pid As Integer, ByVal Status As Integer, ByVal Data As String)
    Public Event Stopped(ByVal Pid As Integer)
    Public Event Started(ByVal Pid As Integer)
    Public Event Aborting(ByVal Pid As Integer, ByVal Data As String)

    Private P() As clsProcessWorker
    ' Generic Processhandler class
    '
    Sub New()
        ReDim P(UBound([Enum].GetNames(GetType(AppNames))))
    End Sub

    Public Sub StartProcess(ByVal Pcls As pSettings)
        P(Pcls.AppId) = New clsProcessWorker
        P(Pcls.AppId).Appid = Pcls.AppId
        P(Pcls.AppId).AppToStart = Pcls.AppPath
        P(Pcls.AppId).Arguments = Pcls.Params
        P(Pcls.AppId).WorkingDirectory = Pcls.WorkingDirectory
        P(Pcls.AppId).StartSignal = Pcls.StartSignal
        P(Pcls.AppId).Cores = (2 ^ Pcls.Cores) - 1 'processaffinity is set bitwise 
        P(Pcls.AppId).SSMTEnd = Pcls.StartsignalMaxTime
        AddHandler P(Pcls.AppId).UpdateConsole, AddressOf ProcUpdate

        Dim trda As Thread
        trda = New Thread(AddressOf P(Pcls.AppId).Work)
        trda.IsBackground = True
        trda.Start()
        trda = Nothing


    End Sub
    Friend Sub ReStartProcess(ByVal AppId As Object)
        Dim trda As Thread
        trda = New Thread(AddressOf RestartWorker)
        trda.IsBackground = True
        trda.Start(AppId)
        trda = Nothing
    End Sub
    Private Sub RestartWorker(ByVal AppId As Object)
        If Not IsNothing(P(AppId)) Then
            If P(AppId).IsRunning Then
                P(AppId).StopProc()
                Do
                    If P(AppId).State = ProcOp.Stopped Then Exit Do
                    Thread.Sleep(500)
                Loop
            End If
            'we have stopped
            Dim trda As Thread
            trda = New Thread(AddressOf P(AppId).Work)
            trda.IsBackground = True
            trda.Start()
            trda = Nothing
        End If
    End Sub


    Public Sub StartProcessSquence(ByVal Pcls() As pSettings)
        'this is needed if working with mariadb portable. maybe for otherthings later aswell.
        Dim trda As Thread
        trda = New Thread(AddressOf StartPS)
        trda.IsBackground = True
        trda.Start(Pcls)
        trda = Nothing

    End Sub
    Private Sub StartPS(ByVal pcls() As Object)
        Dim ps() As pSettings = CType(pcls, pSettings())

        For Each Proc As pSettings In ps
            P(Proc.AppId) = New clsProcessWorker
            P(Proc.AppId).Appid = Proc.AppId
            P(Proc.AppId).AppToStart = Proc.AppPath
            P(Proc.AppId).Arguments = Proc.Params
            P(Proc.AppId).WorkingDirectory = Proc.WorkingDirectory
            P(Proc.AppId).StartSignal = Proc.StartSignal
            P(Proc.AppId).Cores = (2 ^ Proc.Cores) - 1 'processaffinity is set bitwise 
            P(Proc.AppId).SSMTEnd = Proc.StartsignalMaxTime
            AddHandler P(Proc.AppId).UpdateConsole, AddressOf ProcUpdate
            Dim trda As Thread
            trda = New Thread(AddressOf P(Proc.AppId).Work)
            trda.IsBackground = True
            trda.Start()
            trda = Nothing
            Do
                Thread.Sleep(1000)
                If P(Proc.AppId).IsRunning Then Exit Do
                If P(Proc.AppId).State <> ProcOp.Running Then Exit Do
            Loop
            If P(Proc.AppId).State <> ProcOp.Running Then Exit For 'abort
        Next
    End Sub


    Public Sub StopProcess(ByVal Appid As Integer)
        Try
            If Not IsNothing(P(Appid)) Then
                P(Appid).StopProc()
            End If
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
    End Sub
    Public Sub StopProcessSquence(ByVal Appid() As Object)
        Dim trda As Thread
        trda = New Thread(AddressOf StopPS)
        trda.IsBackground = True
        trda.Start(Appid)
        trda = Nothing
    End Sub
    Private Sub StopPS(ByVal pId() As Object)

        For Each Appid As Integer In pId
            P(Appid).StopProc()
            Do
                Thread.Sleep(1000)
                If P(Appid).IsRunning = False Then Exit Do
            Loop
        Next

    End Sub

    Private Sub ProcUpdate(ByVal AppId As Integer, ByVal Operation As Integer, ByVal Data As String)
        'Streamlineing threads output
        Select Case Operation
            Case ProcOp.Running
                RaiseEvent Started(AppId)
            Case ProcOp.FoundSignal
                RaiseEvent Update(AppId, Operation, Data)
            Case ProcOp.Stopping
                RaiseEvent Update(AppId, Operation, Data)
            Case ProcOp.Stopped
                RaiseEvent Stopped(AppId)
            Case ProcOp.Err
                RaiseEvent Aborting(AppId, Data)
            Case ProcOp.ConsoleOut
                RaiseEvent Update(AppId, Operation, Data)
            Case ProcOp.ConsoleErr
                RaiseEvent Update(AppId, Operation, Data)
        End Select

    End Sub


    Public Class pSettings
        Public AppId As Integer = 0
        Public AppPath As String = ""
        Public Params As String = ""
        Public Cores As Integer = 0
        Public StartSignal As String = ""
        Public WorkingDirectory As String = ""
        Public StartsignalMaxTime As Integer = 300 '5 minutes
    End Class

    Private Class clsProcessWorker
        Public Event UpdateConsole(ByVal AppId As Integer, ByVal Operation As Integer, ByVal Data As String)

        Private p As Process
        Private OutputBuffer As String
        Private ErrorBuffer As String
        Private _state As Integer
        Public AppToStart As String
        Public WorkingDirectory As String
        Public Arguments As String
        Public Appid As Integer
        Public StartSignal As String
        Public FoundStartSignal As Boolean
        Public Cores As Integer
        Public SSMTEnd As Integer
        Private Abort As Boolean

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

        Private Sub ShutDown(ByVal SigIntSleep As Integer, ByVal SigKillSleep As Integer)
            Try
                AttachConsole(p.Id)
                SetConsoleCtrlHandler(New ConsoleCtrlDelegate(AddressOf OnExit), True)
                GenerateConsoleCtrlEvent(CtrlTypes.CTRL_C_EVENT, 0)
                p.WaitForExit(SigIntSleep) 'wait for exit before we release. if not we might get ourself terminated.
                If Not p.HasExited Then
                    p.Kill()
                    p.WaitForExit(SigKillSleep)
                End If
                FreeConsole()
            Catch ex As Exception
                If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
            End Try
        End Sub
        Private Function OnExit(CtrlType As CtrlTypes)
            Return True
        End Function

        Public Sub Work()
            _state = ProcOp.Running
            FoundStartSignal = False
            Abort = False
            p = New Process
            p.StartInfo.WorkingDirectory = WorkingDirectory
            p.StartInfo.Arguments = Arguments
            p.StartInfo.UseShellExecute = False
            '    p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.RedirectStandardError = True
            p.StartInfo.CreateNoWindow = True
            '    AddHandler p.OutputDataReceived, AddressOf OutputHandler
            AddHandler p.ErrorDataReceived, AddressOf ErroutHandler
            p.StartInfo.FileName = AppToStart

            Try

                p.Start()
                If Cores <> 0 Then p.ProcessorAffinity = CType(Cores, IntPtr)
                p.BeginErrorReadLine()

                '     p.BeginOutputReadLine()
            Catch ex As Exception
                'if we have error here exit right away.
                RaiseEvent UpdateConsole(Appid, ProcOp.Err, ex.Message)
                RaiseEvent UpdateConsole(Appid, ProcOp.Stopped, "")
                Abort = True
                Exit Sub
            End Try

            RaiseEvent UpdateConsole(Appid, ProcOp.Running, "")

            'if no startsignal then just asume we did
            If StartSignal = "" Then FoundStartSignal = True
            Dim SSMTEndTime As Date = Now.AddSeconds(SSMTEnd)
            Do 'just wait and see if we have exit.
                If FoundStartSignal = False And Now > SSMTEndTime Then
                    RaiseEvent UpdateConsole(Appid, ProcOp.Err, "Process did not completely start in reasonable time. Shutting down.")
                    Exit Do
                End If
                Thread.Sleep(500)
                If p.HasExited Then Exit Do
                If Abort Then Exit Do
            Loop

            If Not p.HasExited Then
                RaiseEvent UpdateConsole(Appid, ProcOp.Stopping, "")
                ShutDown(25000, 10000) '25 sec and 10 sec
            End If
            _state = ProcOp.Stopped
            RaiseEvent UpdateConsole(Appid, ProcOp.Stopped, "")
        End Sub
        Public Function IsRunning() As Boolean
            Try
                If p.HasExited Then

                    Return False
                End If
            Catch ex As Exception
                If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
            End Try
            If FoundStartSignal Then Return True
            Return False

        End Function
        Public Function State()
            Return _state
        End Function
        Public Sub StopProc()
            Abort = True
        End Sub

        Public Function KillMe() As Boolean
            Try
                p.Kill()
                Return True
            Catch ex As Exception
                If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
            End Try
            Return False
        End Function
        Public Sub Cleanup()
            Try
                p = Nothing
                AppToStart = Nothing
                WorkingDirectory = Nothing
                Arguments = Nothing
                OutputBuffer = Nothing
                ErrorBuffer = Nothing

            Catch ex As Exception

            End Try
        End Sub
        Sub OutputHandler(sender As Object, e As DataReceivedEventArgs)
            If Not String.IsNullOrEmpty(e.Data) Then
                If FoundStartSignal = False Then
                    If e.Data.Contains(StartSignal) Then
                        FoundStartSignal = True
                        RaiseEvent UpdateConsole(Appid, ProcOp.FoundSignal, "")
                    End If

                End If

                RaiseEvent UpdateConsole(Appid, ProcOp.ConsoleOut, e.Data)
            End If
        End Sub
        Sub ErroutHandler(sender As Object, e As DataReceivedEventArgs)

            If Not String.IsNullOrEmpty(e.Data) Then
                If FoundStartSignal = False Then
                    If e.Data.Contains(StartSignal) Then
                        FoundStartSignal = True
                        RaiseEvent UpdateConsole(Appid, ProcOp.FoundSignal, "")
                    End If
                End If

                RaiseEvent UpdateConsole(Appid, ProcOp.ConsoleErr, e.Data)
            End If
        End Sub
    End Class









End Class
