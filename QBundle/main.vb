Module main


    '############################
    ' Enums
    '############################


    'Used for Appnames to AppId for diffrent process and repo managements



    Public Enum States
        Stopped = 0
        Running = 1
        Abort = 2
    End Enum

    'Process operation signals
    Public Enum ProcOp As Integer
        Running = 1
        FoundSignal = 2
        Stopping = 3
        Stopped = 4
        Err = 5
        ConsoleErr = 6
        ConsoleOut = 7
    End Enum
    '############################
    'Const
    '############################
    '############################
    'Global Vars
    '############################
    Public BaseDir As String

    '############################
    'Classes
    '############################
    Public WithEvents App As clsApp
    Public WithEvents ProcHandler As clsProcessHandler
    Public settings As clsSettings
    Public Accounts As clsAccounts

End Module
