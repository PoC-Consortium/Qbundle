Imports System.ServiceProcess



Public Class main
    Inherits ServiceBase
    Private ProcHandler As clsProcessHandler

#Region " Component Designer generated code "
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
    Shared Sub Main()

        Dim ServicesToRun() As ServiceBase
        ServicesToRun = New ServiceBase() {New main()}
        ServiceBase.Run(ServicesToRun)



    End Sub
    Private components As System.ComponentModel.Container
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        Me.ServiceName = "Burst Service"

    End Sub
#End Region

    Protected Overrides Sub OnStart(ByVal args() As String)

        ProcHandler = New clsProcessHandler

        ProcHandler.Start()

    End Sub

    Protected Overrides Sub OnStop()
        'send stop
        ProcHandler.Quit()
    End Sub

End Class
