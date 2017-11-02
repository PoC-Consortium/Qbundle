Public Class frmChangeDatabase
    Private Running As Boolean
    Private WithEvents WaitTimer As Timer
    Private Delegate Sub DProcEvents(ByVal [AppId] As Integer, ByVal [Operation] As Integer, ByVal [data] As String)
    Private Delegate Sub DStarting(ByVal [AppId] As Integer)
    Private Delegate Sub DStoped(ByVal [AppId] As Integer)
    Private Delegate Sub DAborted(ByVal [AppId] As Integer, ByVal [data] As String)
    Private StartTime As Date
    Private SelDB As Integer
    Private OldDB As Integer
    Private OP As Integer '0=copy 1=Fresh
#Region " Setup and form Events "

    Private Sub frmChangeDatabase_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing
        Try
            If Running Then
                MsgBox("You must wait until the convertion is compleated.", MsgBoxStyle.OkOnly, "Close")
                e.Cancel = True
                Exit Sub
            End If
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try

    End Sub
    Private Sub frmChangeDatabase_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SelDB = QB.settings.DbType

        'check if mariadbp is installed
        Select Case SelDB
            Case DbType.H2
                rDB1.Enabled = False
                rDB1.Text = rDB1.Text & " (Currently used)"
            Case DbType.FireBird
                rDB2.Enabled = False
                rDB2.Text = rDB2.Text & " (Currently used)"
            Case DbType.pMariaDB
                rDB3.Enabled = False
                rDB3.Text = rDB3.Text & " (Currently used)"
            Case DbType.MariaDB
                rDB4.Enabled = False
                rDB4.Text = rDB4.Text & " (Currently used)"
                pnlMariaSettings.Enabled = True
        End Select
        If Not App.isInstalled(AppNames.MariaPortable) Then
            rDB3.Enabled = False
            rDB3.Text = rDB3.Text & " (Not installed)"
        End If
        lblCurDB.Text = App.GetDbNameFromType(QB.settings.DbType)
        If Not QB.settings.DbType = DbType.H2 Then
            setdb(DbType.H2)
        Else
            setdb(DbType.FireBird)
        End If

        OP = 0

        Me.Width = 501
        Me.Height = 436

    End Sub
    Private Sub setdb(ByVal id As Integer)
        rDB1.Checked = False
        rDB2.Checked = False
        rDB3.Checked = False
        rDB4.Checked = False
        pnlMariaSettings.Enabled = False
        SelDB = id
        Select Case SelDB
            Case DbType.H2
                rDB1.Checked = True
                lblFromTo.Text = App.GetDbNameFromType(QB.settings.DbType) & " to " & App.GetDbNameFromType(DbType.H2)
            Case DbType.FireBird
                rDB2.Checked = True
                lblFromTo.Text = App.GetDbNameFromType(QB.settings.DbType) & " to " & App.GetDbNameFromType(DbType.FireBird)
            Case DbType.pMariaDB
                rDB3.Checked = True
                lblFromTo.Text = App.GetDbNameFromType(QB.settings.DbType) & " to " & App.GetDbNameFromType(DbType.pMariaDB)
            Case DbType.MariaDB
                rDB4.Checked = True
                lblFromTo.Text = App.GetDbNameFromType(QB.settings.DbType) & " to " & App.GetDbNameFromType(DbType.MariaDB)
                pnlMariaSettings.Enabled = True
        End Select
    End Sub
    Private Sub rDB1_Click(sender As Object, e As EventArgs) Handles rDB1.Click
        setdb(DbType.H2)
    End Sub
    Private Sub rDB2_Click(sender As Object, e As EventArgs) Handles rDB2.Click
        setdb(DbType.FireBird)
    End Sub
    Private Sub rDB3_Click(sender As Object, e As EventArgs) Handles rDB3.Click
        setdb(DbType.pMariaDB)
    End Sub
    Private Sub rDB4_Click(sender As Object, e As EventArgs) Handles rDB4.Click
        setdb(DbType.MariaDB)
    End Sub
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        pnlWiz1.Hide()
        pnlWiz2.Show()
        pnlWiz2.Top = pnlWiz1.Top
        pnlWiz2.Left = pnlWiz1.Left

    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        pnlWiz2.Hide()
        pnlWiz1.Show()
    End Sub
    Private Sub rOP1_CheckedChanged(sender As Object, e As EventArgs) Handles rOP1.Click
        OP = 0
        btnStart.Text = "Start Copy"
    End Sub
    Private Sub rOP2_CheckedChanged(sender As Object, e As EventArgs) Handles rOP2.CheckedChanged
        OP = 1
        btnStart.Text = "Save and close"
    End Sub
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click

        If btnStart.Text = "Close" Then
            Me.Close()
            Exit Sub
        End If
        If OP = 0 Then

            OldDB = QB.settings.DbType
            If frmMain.Running Then
                If MsgBox("The wallet must be stopped to export the database." & vbCrLf & " Would you like to stop it now?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Stop wallet?") Then
                    lblStatus.Text = "Waiting for wallet to stop."
                    frmMain.StopWallet()
                    WaitTimer = New Timer
                    WaitTimer.Interval = 500
                    WaitTimer.Enabled = True
                    WaitTimer.Start()
                    Exit Sub
                End If
            End If
            AddHandler ProcHandler.Aborting, AddressOf Aborted
            AddHandler ProcHandler.Started, AddressOf Starting
            AddHandler ProcHandler.Stopped, AddressOf Stopped
            AddHandler ProcHandler.Update, AddressOf ProcEvents
            If SelDB = DbType.pMariaDB Or OldDB = DbType.pMariaDB Then
                StartMaria()
            Else
                StartExport()
            End If
        Else
            QB.settings.DbType = SelDB
            QB.settings.DbName = txtDbName.Text
            QB.settings.DbPass = txtDbPass.Text
            QB.settings.DbUser = txtDbUser.Text
            QB.settings.DbServer = txtDbAddress.Text
            QB.settings.SaveSettings()
            QB.Generic.WriteNRSConfig()
            frmMain.SetDbInfo()
            Me.Close()
        End If

    End Sub
#End Region


    Private Sub StartExport()
        btnBack.Enabled = False
        btnStart.Enabled = False
        rOP1.Enabled = False
        rOP2.Enabled = False


        StartTime = Now
        Dim Pset As New clsProcessHandler.pSettings
        Pset.AppId = AppNames.Export
        If QB.settings.JavaType = AppNames.JavaInstalled Then
            Pset.AppPath = "java"
        Else
            Pset.AppPath = BaseDir & "Java\bin\java.exe"
        End If
        Pset.Cores = QB.settings.Cpulimit
        Pset.Params = "-cp burst.jar;lib\*;conf brs.db.quicksync.CreateBinDump " & BaseDir & "Convertion.bbd"
        Pset.StartSignal = ""
        Pset.StartsignalMaxTime = 1
        Pset.WorkingDirectory = BaseDir
        ProcHandler.StartProcess(Pset)

        Running = True




    End Sub
    Private Sub WaitTimer_tick() Handles WaitTimer.Tick
        If frmMain.Running = False Then
            WaitTimer.Stop()
            WaitTimer.Enabled = False
            AddHandler ProcHandler.Aborting, AddressOf Aborted
            AddHandler ProcHandler.Started, AddressOf Starting
            AddHandler ProcHandler.Stopped, AddressOf Stopped
            AddHandler ProcHandler.Update, AddressOf ProcEvents
            If SelDB = DbType.pMariaDB Or OldDB = DbType.pMariaDB Then
                StartMaria()
            Else
                StartExport()
            End If
        End If
    End Sub
    Private Sub Starting(ByVal AppId As Integer)
        If Me.InvokeRequired Then
            Dim d As New DStarting(AddressOf Starting)
            Me.Invoke(d, New Object() {AppId})
            Return
        End If

        Select Case AppId
            Case AppNames.Export
                lblStatus.Text = "Starting to export"
                pb1.Value = 0
            Case AppNames.Import
                lblStatus.Text = "Starting to Import"
                pb1.Value = 0
        End Select

    End Sub
    Private Sub Stopped(ByVal AppId As Integer)
        If Me.InvokeRequired Then
            Dim d As New DStoped(AddressOf Stopped)
            Me.Invoke(d, New Object() {AppId})
            Return
        End If
        If AppId = AppNames.Export Then
            QB.settings.DbType = SelDB
            QB.settings.DbName = txtDbName.Text
            QB.settings.DbPass = txtDbPass.Text
            QB.settings.DbUser = txtDbUser.Text
            QB.settings.DbServer = txtDbAddress.Text
            QB.settings.SaveSettings()
            QB.Generic.WriteNRSConfig()
            StartImport()
        End If
        If AppId = AppNames.Import Then
            If SelDB = DbType.pMariaDB Or OldDB = DbType.pMariaDB Then
                StopMaria() 'even if we dont use it we call it
            Else
                Complete()
            End If
        End If
        If AppId = AppNames.MariaPortable Then
            Complete()
        End If
    End Sub
    Private Sub Complete()
        frmMain.SetDbInfo() 'update frmmain as well
        Dim ElapsedTime As TimeSpan = Now.Subtract(StartTime)
        lblStatus.Text = "Done, Conversion completed in " & ElapsedTime.Hours & ":" & ElapsedTime.Minutes & ":" & ElapsedTime.Seconds
        btnStart.Text = "Close"
        btnStart.Enabled = True
        pb1.Value = 100
        Running = False
        RemoveHandler ProcHandler.Aborting, AddressOf Aborted
        RemoveHandler ProcHandler.Started, AddressOf Starting
        RemoveHandler ProcHandler.Stopped, AddressOf Stopped
        RemoveHandler ProcHandler.Update, AddressOf ProcEvents
    End Sub
    Private Sub ProcEvents(ByVal AppId As Integer, ByVal Operation As Integer, ByVal data As String)
        If Me.InvokeRequired Then
            Dim d As New DProcEvents(AddressOf ProcEvents)
            Me.Invoke(d, New Object() {AppId, Operation, data})
            Return
        End If
        Dim darray() As String = Nothing
        Dim percent As Integer = 0
        If AppId = AppNames.Export Then
            Select Case Operation

                Case ProcOp.ConsoleOut And ProcOp.ConsoleErr
                    Try
                        darray = Split(data, " ")
                        If UBound(darray) = 8 Then
                            If darray(3) = "CreateBinDump" And darray(7) = "/" Then
                                If Not darray(6) = "0" And Not darray(8) = "0" Then
                                    percent = Math.Round(Val(darray(6)) / Val(darray(8)) * 100, 0)
                                Else
                                    percent = 100
                                End If
                                lblStatus.Text = "Exporting " & darray(5).Replace(":", "") & " " & darray(6) & " of " & darray(8)
                                pb1.Value = percent
                            End If
                        End If
                    Catch ex As Exception
                        lblStatus.Text = "Error parsing data. Job still continues."
                    End Try
                Case ProcOp.Err
                    Running = False
            End Select
        End If
        If AppId = AppNames.Import Then 'we need to filter messages
            Select Case Operation
                Case ProcOp.ConsoleOut And ProcOp.ConsoleErr
                    Try
                        darray = Split(data, " ")
                        If UBound(darray) = 8 Then
                            If darray(3) = "LoadBinDump" And darray(7) = "/" Then
                                If Not darray(6) = "0" And Not darray(8) = "0" Then
                                    percent = Math.Round(Val(darray(6)) / Val(darray(8)) * 100, 0)
                                Else
                                    percent = 100
                                End If
                                lblStatus.Text = "Importing " & darray(5).Replace(":", "") & " " & darray(6) & " of " & darray(8)
                                pb1.Value = percent
                            End If
                        End If
                        If UBound(darray) > 5 Then
                            If darray(5) = "Compacting" And darray(6) = "database" Then
                                lblStatus.Text = "Compacting database. Please wait."
                                pb1.Value = 0
                            End If
                        End If
                    Catch ex As Exception
                        lblStatus.Text = "Error parsing data. Job still continues."
                    End Try
                Case ProcOp.Err
                    Running = False
            End Select
        End If
        If AppId = AppNames.MariaPortable Then
            Select Case Operation
                Case ProcOp.FoundSignal
                    StartExport()
            End Select
        End If

    End Sub
    Private Sub Aborted(ByVal AppId As Integer, ByVal Data As String)
        If Me.InvokeRequired Then
            Dim d As New DAborted(AddressOf Aborted)
            Me.Invoke(d, New Object() {AppId, Data})
            Return
        End If

        If AppId = AppNames.Export Or AppId = AppNames.Import Then
            MsgBox(Data)
        End If

    End Sub
    Private Sub StartImport()
        Dim Pset As New clsProcessHandler.pSettings
        Pset.AppId = AppNames.Import
        If QB.settings.JavaType = AppNames.JavaInstalled Then
            Pset.AppPath = "java"
        Else
            Pset.AppPath = BaseDir & "Java\bin\java.exe"
        End If
        Pset.Cores = QB.settings.Cpulimit
        Pset.Params = "-cp burst.jar;lib\*;conf brs.db.quicksync.LoadBinDump " & BaseDir & "Convertion.bbd -y"
        Pset.StartSignal = ""
        Pset.StartsignalMaxTime = 1
        Pset.WorkingDirectory = BaseDir
        ProcHandler.StartProcess(Pset)
    End Sub

    Private Sub StartMaria()
        Try
            'we already have a handler
            If QB.Generic.SanityCheck Then
                lblStatus.Text = "Starting MariaDB"
                Dim pr As New clsProcessHandler.pSettings
                pr.AppId = AppNames.MariaPortable
                pr.AppPath = BaseDir & "MariaDb\bin\mysqld.exe"
                pr.Cores = 0
                pr.Params = "--console"
                pr.WorkingDirectory = BaseDir & "MariaDb\bin\"
                pr.StartSignal = "ready for connections"
                pr.StartsignalMaxTime = 60
                ProcHandler.StartProcess(pr)
            End If
        Catch ex As Exception
            MsgBox("Unable to start Maria Portable.")
        End Try

    End Sub
    Private Sub StopMaria()
        lblStatus.Text = "Stopping MariaDB"
        ProcHandler.StopProcess(AppNames.MariaPortable)
    End Sub

End Class