Imports System.IO.Pipes
Imports System.ServiceProcess
Imports System.Text
Imports System.Threading

Public Class clsServiceHandler
    Public Event MonitorEvents(ByVal Operation As Integer, ByVal Data As String) '0 service information '1 pipe information
    Private pipeClient As NamedPipeClientStream
    Private PipeBuffer() As Byte
    Private ShouldPipeRun As Boolean
#Region " Public Service Subs / Functions  "
    Public Function InstallService() As Boolean
        Try
            If QB.Generic.IsAdmin Then
                Dim Srv As String = BaseDir & "BurstService.exe"
                Configuration.Install.ManagedInstallerClass.InstallHelper(New String() {Srv})
                Return True
            Else
                Dim p As Process = New Process
                p.StartInfo.WorkingDirectory = BaseDir
                p.StartInfo.Arguments = "InstallService"
                p.StartInfo.UseShellExecute = True
                p.StartInfo.CreateNoWindow = True
                p.StartInfo.FileName = Application.ExecutablePath
                p.StartInfo.Verb = "runas"
                p.Start()
                Return True
            End If
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
        Return False

    End Function
    Public Function UninstallService() As Boolean
        Try
            If QB.Generic.IsAdmin Then
                Dim Srv As String = BaseDir & "BurstService.exe"
                Configuration.Install.ManagedInstallerClass.InstallHelper(New String() {"/u", Srv})
                Return True
            Else
                Dim p As Process = New Process
                p.StartInfo.WorkingDirectory = BaseDir
                p.StartInfo.Arguments = "InstallService"
                p.StartInfo.UseShellExecute = True
                p.StartInfo.CreateNoWindow = True
                p.StartInfo.FileName = Application.ExecutablePath
                p.StartInfo.Verb = "runas"
                p.Start()

                Return True
            End If
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
        Return False
    End Function
    Public Sub StartService()
        If Not IsServiceRunning() And IsInstalled() Then
            Dim service As ServiceController = New ServiceController("Burst Service")
            If service.Status.Equals(ServiceControllerStatus.Stopped) Or service.Status.Equals(ServiceControllerStatus.StopPending) Then
                service.Start()
            End If
        End If
    End Sub
    Public Sub StopService()
        If IsServiceRunning() And IsInstalled() Then
            Dim service As ServiceController = New ServiceController("Burst Service")
            If service.Status.Equals(ServiceControllerStatus.Running) Or service.Status.Equals(ServiceControllerStatus.StartPending) Then
                service.Stop()
            End If
        End If
    End Sub
    Public Function IsInstalled() As Boolean
        Dim service As ServiceController = New ServiceController("Burst Service")
        If service.Status = Nothing Then
            Return False
        End If
        Return True
    End Function
    Public Function IsServiceRunning() As Boolean
        Dim service As ServiceController = New ServiceController("Burst Service")
        If service.Status.Equals(ServiceControllerStatus.Running) Or service.Status.Equals(ServiceControllerStatus.StartPending) Then
            Return True
        End If
        Return False
    End Function
#End Region

#Region " Public Wallet Sub / Functions "


    Public Function IsConnected() As Boolean
        Return True
    End Function
    Public Sub StartWallet()

    End Sub
    Public Sub StopWallet()

    End Sub
    Public Sub GetConsoleLogs()

    End Sub
    Public Sub SendCommands(ByVal data As String)
        Dim sendbuffer() As Byte = Encoding.UTF8.GetBytes(data)
        pipeClient.Write(sendbuffer, 0, sendbuffer.Length)
    End Sub

#End Region

#Region " Pipe Client "

    Public Sub RunPipeClient()
        ShouldPipeRun = True
        Dim trda As Thread
        trda = New Thread(AddressOf StopPipeClient)
        trda.IsBackground = True
        trda.Start()
        trda = Nothing
    End Sub
    Private Sub StopPipeClient()
        ShouldPipeRun = False
        If pipeClient.IsConnected Then
            pipeClient.Close()
            pipeClient.Dispose()
        End If
    End Sub
    Private Sub PipeMonitor()
        Do
            If Not pipeClient.IsConnected Then
                StartPipeClient()
            End If
            Threading.Thread.Sleep(1000)
            If ShouldPipeRun = False Then Exit Do
        Loop
    End Sub

    Private Sub StartPipeClient()
        Try
            If ShouldPipeRun = True Then
                pipeClient = New NamedPipeClientStream(".", "BurstPipe", PipeDirection.InOut, PipeOptions.Asynchronous)
                pipeClient.Connect(5000) '5000ms is huge!
                pipeClient.BeginRead(PipeBuffer, 0, PipeBuffer.Length, AddressOf IncPipeMessage, pipeClient)
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub IncPipeMessage(iar As IAsyncResult)
        Try
            Dim totalbytes As Integer = pipeClient.EndRead(iar) 'stop this asyncread and start new later
            Dim stringData As String = Encoding.UTF8.GetString(PipeBuffer, 0, totalbytes)
            RaiseEvent MonitorEvents(1, stringData)
            pipeClient.BeginRead(PipeBuffer, 0, PipeBuffer.Length, AddressOf IncPipeMessage, iar)
        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class
