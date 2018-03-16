Public Class frmImport
    Private Running As Boolean
    Private WithEvents WaitTimer As Timer
    Private Delegate Sub DProcEvents(ByVal [AppId] As Integer, ByVal [Operation] As Integer, ByVal [data] As String)
    Private Delegate Sub DStarting(ByVal [AppId] As Integer)
    Private Delegate Sub DStoped(ByVal [AppId] As Integer)
    Private Delegate Sub DAborted(ByVal [AppId] As Integer, ByVal [data] As String)
    Private Delegate Sub DDownloadDone(ByVal [AppId] As Integer)
    Private Delegate Sub DProgress(ByVal [JobType] As Integer, ByVal [AppId] As Integer, ByVal [Percernt] As Integer, ByVal [Speed] As Integer, ByVal [lRead] As Long, ByVal [lLength] As Long)
    Private Delegate Sub DDLAborted(ByVal [AppId] As Integer)
    Private RepoDBUrls() As String
    Private SelectedType As Integer
    Private StartTime As Date
    Private IsAborted As Boolean
    Private Sub frmImport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Running = False
        cmbRepo.Items.Clear()

        Select Case Q.settings.DbType
            Case QGlobal.DbType.H2
                ReDim RepoDBUrls(0)
                RepoDBUrls(0) = "http://package.cryptoguru.org/dumps/latest.bbd"
                cmbRepo.Items.Add("Cryptoguru repository")
                cmbRepo.SelectedIndex = 0
            Case Else
                ReDim RepoDBUrls(0)
                RepoDBUrls(0) = "http://package.cryptoguru.org/dumps/latest.bbd"
                cmbRepo.Items.Add("Cryptoguru repository")
                cmbRepo.SelectedIndex = 0
        End Select



    End Sub
    Private Sub frmImport_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Running Then
                MsgBox("You cannot close the import form while importing.", MsgBoxStyle.OkOnly, "Exit")
                e.Cancel = True
                Exit Sub
            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try

    End Sub
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        If btnStart.Text = "Close" Then
            Me.Close()
            Exit Sub
        End If

        If Not MsgBox("Warning!" & vbCrLf & vbCrLf & "All existing data in your database will be erased." & vbCrLf & "Do you want to continue?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNoCancel, "All existing data removed") = MsgBoxResult.Yes Then
            Exit Sub
        End If

        r1.Enabled = False
        cmbRepo.Enabled = False
        btnStart.Enabled = False

        StartTime = Now
        Running = True
        'if wallet is running shut it down
        If frmMain.Running Then
            If MsgBox("The wallet must be stopped to import the database." & vbCrLf & " Would you like to stop it now?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Stop wallet?") = MsgBoxResult.Yes Then
                lblStatus.Text = "Waiting for wallet to stop."
                frmMain.StopWallet()
                WaitTimer = New Timer
                WaitTimer.Interval = 500
                WaitTimer.Enabled = True
                WaitTimer.Start()
                Exit Sub
            End If
        End If
        AddHandler Q.ProcHandler.Aborting, AddressOf Aborted
        AddHandler Q.ProcHandler.Started, AddressOf Starting
        AddHandler Q.ProcHandler.Stopped, AddressOf Stopped
        AddHandler Q.ProcHandler.Update, AddressOf ProcEvents
        If Q.settings.DbType = QGlobal.DbType.pMariaDB Then
            StartMaria()
        Else
            StartImport()
        End If


    End Sub
    Sub StartImport()
        IsAborted = False
        Select Case Q.settings.DbType
            Case QGlobal.DbType.H2
                DownloadForH2(RepoDBUrls(cmbRepo.SelectedIndex))
            Case QGlobal.DbType.pMariaDB
                DownloadForMaria(RepoDBUrls(cmbRepo.SelectedIndex))
        End Select

    End Sub
    Private Sub ImportFromFile(ByVal FileName As String)

        'verify that h2 is gone if we are using h2
        If Q.settings.DbType = QGlobal.DbType.H2 Then
            Try
                If IO.File.Exists(QGlobal.BaseDir & "burst_db\burst.mv.db") Then
                    IO.File.Delete(QGlobal.BaseDir & "burst_db\burst.mv.db")
                End If
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
        End If

        If FileName.Contains(" ") Then FileName = Chr(34) & FileName & Chr(34)
        Dim Pset As New clsProcessHandler.pSettings
        Pset.AppId = QGlobal.AppNames.Import
        If Q.settings.JavaType = QGlobal.AppNames.JavaInstalled Then
            Pset.AppPath = "java"
        Else
            Pset.AppPath = QGlobal.AppDir & "Java\bin\java.exe"
        End If
        Pset.Cores = Q.settings.Cpulimit
        Pset.Params = QGlobal.WalletLaunchString.Import & FileName & " -y"
        Pset.StartSignal = ""
        Pset.StartsignalMaxTime = 1
        Pset.WorkingDirectory = QGlobal.AppDir
        Q.ProcHandler.StartProcess(Pset)


    End Sub
    Private Sub DownloadForH2(ByVal Url As String)
        Dim S As frmDownloadExtract
        S = New frmDownloadExtract
        S.Url = Url
        S.Unzip = True
        Me.Hide()
        If S.ShowDialog = DialogResult.OK Then
            Me.Show()
            Try
                If IO.File.Exists(QGlobal.BaseDir & IO.Path.GetFileName(Url)) Then
                    IO.File.Delete(QGlobal.BaseDir & IO.Path.GetFileName(Url)) 'not if not ziped
                End If
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
            Try
                If IO.File.Exists(QGlobal.BaseDir & "burst_db\burst.mv.db") Then
                    IO.File.Delete(QGlobal.BaseDir & "burst_db\burst.mv.db")
                End If
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
            lblStatus.Text = "Moving the file to correct location."
            Try
                IO.File.Move(QGlobal.BaseDir & "burst.mv.db", QGlobal.BaseDir & "burst_db\burst.mv.db")
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
            Complete()
            Exit Sub
        End If
        Try
            Me.Show()
            'we have aborted return to download again
            IsAborted = True
            If Q.settings.DbType = QGlobal.DbType.pMariaDB Then StopMaria()
            Running = False
            r1.Enabled = True
            cmbRepo.Enabled = True
            btnStart.Enabled = True
            lblStatus.Text = "Aborted"
        Catch ex As Exception

        End Try


    End Sub
    Private Sub DownloadForMaria(ByVal Url As String)
        Dim S As frmDownloadExtract
        S = New frmDownloadExtract
        S.Url = Url
        S.Unzip = True
        Me.Hide()
        If S.ShowDialog = DialogResult.OK Then
            Me.Show()
            Try
                If IO.File.Exists(QGlobal.BaseDir & IO.Path.GetFileName(Url)) Then
                    IO.File.Delete(QGlobal.BaseDir & IO.Path.GetFileName(Url)) 'not if not ziped
                End If
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
            Try
                If IO.File.Exists(QGlobal.BaseDir & "burst_db\burst.mv.db") Then
                    IO.File.Delete(QGlobal.BaseDir & "burst_db\burst.mv.db")
                End If
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
            lblStatus.Text = "Moving the file to correct location."
            Try
                IO.File.Move(QGlobal.BaseDir & "burst.mv.db", QGlobal.BaseDir & "burst_db\burst.mv.db")
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
            Complete()
            Exit Sub
        End If
        Try
            Me.Show()
            'we have aborted return to download again
            IsAborted = True
            If Q.settings.DbType = QGlobal.DbType.pMariaDB Then StopMaria()
            Running = False
            r1.Enabled = True
            cmbRepo.Enabled = True
            btnStart.Enabled = True
            lblStatus.Text = "Aborted"
        Catch ex As Exception

        End Try


    End Sub
    Private Sub ImportFromUrl(ByVal Url As String)
        Dim S As frmDownloadExtract
        S = New frmDownloadExtract
        S.Url = Url
        Me.Hide()

        If S.ShowDialog = DialogResult.OK Then
            Me.Show()
            ImportFromFile(QGlobal.AppDir & IO.Path.GetFileName(Url))
            Exit Sub
        End If
        Me.Show()
        'we have aborted return to download again
        IsAborted = True
        If Q.settings.DbType = QGlobal.DbType.pMariaDB Then StopMaria()

        Running = False
        r1.Enabled = True
        cmbRepo.Enabled = True
        btnStart.Enabled = True
        lblStatus.Text = "Aborted"
    End Sub


    Private Sub r1_Click(sender As Object, e As EventArgs) Handles r1.Click
        ' SetSelect(1)
    End Sub
    Private Sub r2_Click(sender As Object, e As EventArgs)
        '  SetSelect(2)
    End Sub
    Private Sub r3_Click(sender As Object, e As EventArgs)
        '  SetSelect(3)
    End Sub



#Region " Proc Events "
    Private Sub Starting(ByVal AppId As Integer)
        If Me.InvokeRequired Then
            Dim d As New DStarting(AddressOf Starting)
            Me.Invoke(d, New Object() {AppId})
            Return
        End If

        Select Case AppId
            Case QGlobal.AppNames.Import
                lblStatus.Text = "Starting to import."
                pb1.Value = 0
        End Select

    End Sub
    Private Sub Stopped(ByVal AppId As Integer)
        If Me.InvokeRequired Then
            Dim d As New DStoped(AddressOf Stopped)
            Me.Invoke(d, New Object() {AppId})
            Return
        End If

        If AppId = QGlobal.AppNames.Import Then
            If Q.settings.DbType = QGlobal.DbType.pMariaDB Then
                StopMaria()
            Else
                Complete()
            End If
        End If
        If AppId = QGlobal.AppNames.MariaPortable Then
            Complete()
        End If
    End Sub
    Private Sub Complete()
        Try

            RemoveHandler Q.ProcHandler.Aborting, AddressOf Aborted
            RemoveHandler Q.ProcHandler.Started, AddressOf Starting
            RemoveHandler Q.ProcHandler.Stopped, AddressOf Stopped
            RemoveHandler Q.ProcHandler.Update, AddressOf ProcEvents
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try

        If IsAborted = False Then
            Dim ElapsedTime As TimeSpan = Now.Subtract(StartTime)
            lblStatus.Text = "Done! Import completed in " & ElapsedTime.Hours & "h " & ElapsedTime.Minutes & "m " & ElapsedTime.Seconds & "s"
            '  SetSelect(SelectedType)
            btnStart.Text = "Close"
            btnStart.Enabled = True
            pb1.Value = 100
            Running = False
            If chkStartWallet.Checked = True Then
                Try
                    frmMain.StartWallet()
                Catch ex As Exception
                    Generic.WriteDebug(ex)
                End Try
            End If
        Else
            '    SetSelect(SelectedType)
            btnStart.Enabled = True
            pb1.Value = 100
            Running = False
        End If
    End Sub
    Private Sub ProcEvents(ByVal AppId As Integer, ByVal Operation As Integer, ByVal data As String)
        If Me.InvokeRequired Then
            Dim d As New DProcEvents(AddressOf ProcEvents)
            Me.Invoke(d, New Object() {AppId, Operation, data})
            Return
        End If
        'threadsafe here
        Dim darray() As String = Nothing
        Dim percent As Integer = 0
        If AppId = QGlobal.AppNames.Import Then 'we need to filter messages
            Select Case Operation
                Case QGlobal.ProcOp.Stopped
                Case QGlobal.ProcOp.FoundSignal
                Case QGlobal.ProcOp.Stopping
                Case QGlobal.ProcOp.ConsoleOut And QGlobal.ProcOp.ConsoleErr
                    ' txtDebug.AppendText(data & vbCrLf)
                    'collect all messages here
                    'the data we collect looks something like this
                    '[INFO] 2017-09-19 11:41:13 CreateBinDump - Block: 373000 / 404480
                    Try
                        darray = Split(data, " ")
                        'we should have ubound 8
                        If UBound(darray) = 8 Then
                            'correct length
                            If darray(3) = "LoadBinDump" And darray(7) = "/" Then
                                'we can now asume we have something to parse
                                'lets create percent
                                If Not darray(6) = "0" And Not darray(8) = "0" Then
                                    percent = CInt(Math.Round(Val(darray(6)) / Val(darray(8)) * 100, 0))
                                Else
                                    percent = 100
                                End If
                                lblStatus.Text = "Importing " & darray(5).Replace(":", "") & " " & darray(6) & " of " & darray(8)
                                pb1.Value = percent
                            End If
                            If darray(5) = "Dump" And darray(6) = "loaded" Then
                                'we are done

                                Dim ts As New TimeSpan(0, 0, CInt(Val(darray(8).Replace("seconds", ""))))

                                lblStatus.Text = "Done! Export completed in " & ts.Hours & "h " & ts.Minutes & "m " & ts.Seconds & "s"
                            End If
                            'Compacting database - this may take a while

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
                Case QGlobal.ProcOp.Err
                    Running = False
            End Select
        End If
        If AppId = QGlobal.AppNames.MariaPortable Then
            If Operation = QGlobal.ProcOp.FoundSignal Then
                StartImport()
            End If
        End If
    End Sub
    Private Sub Aborted(ByVal AppId As Integer, ByVal Data As String)
        If Me.InvokeRequired Then
            Dim d As New DAborted(AddressOf Aborted)
            Me.Invoke(d, New Object() {AppId, Data})
            Return
        End If

        If AppId = QGlobal.AppNames.Import Then
            MsgBox(Data)
        End If

    End Sub
#End Region


    Private Sub WaitTimer_tick() Handles WaitTimer.Tick
        If frmMain.Running = False Then
            WaitTimer.Stop()
            WaitTimer.Enabled = False
            AddHandler Q.ProcHandler.Aborting, AddressOf Aborted
            AddHandler Q.ProcHandler.Started, AddressOf Starting
            AddHandler Q.ProcHandler.Stopped, AddressOf Stopped
            AddHandler Q.ProcHandler.Update, AddressOf ProcEvents
            If Q.settings.DbType = QGlobal.DbType.pMariaDB Then
                StartMaria()
            Else
                StartImport()
            End If
        End If
    End Sub


    Private Sub StartMaria()
        Try
            If QB.Generic.SanityCheck Then
                lblStatus.Text = "Starting MariaDB"
                Dim pr As New clsProcessHandler.pSettings
                pr.AppId = QGlobal.AppNames.MariaPortable
                pr.AppPath = QGlobal.AppDir & "MariaDb\bin\mysqld.exe"
                pr.Cores = 0
                pr.Params = "--console"
                pr.WorkingDirectory = QGlobal.AppDir & "MariaDb\bin\"
                pr.StartSignal = "ready for connections"
                pr.StartsignalMaxTime = 60
                Q.ProcHandler.StartProcess(pr)
            Else

            End If
        Catch ex As Exception
            MsgBox("Unable to start Maria Portable.")
        End Try

    End Sub
    Private Sub StopMaria()
        lblStatus.Text = "Stopping MariaDB"
        Q.ProcHandler.StopProcess(QGlobal.AppNames.MariaPortable)
    End Sub



End Class