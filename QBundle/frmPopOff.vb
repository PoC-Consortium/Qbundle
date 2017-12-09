Public Class frmPopOff
    Private WasRunning As Boolean = False
    Private wStep As Integer = 0
    Private WithEvents WaitTimer As Timer
    Private Delegate Sub DProcEvents(ByVal [AppId] As Integer, ByVal [Operation] As Integer, ByVal [data] As String)
    Private Delegate Sub DStarting(ByVal [AppId] As Integer)
    Private Delegate Sub DStoped(ByVal [AppId] As Integer)
    Private Delegate Sub DAborted(ByVal [AppId] As Integer, ByVal [data] As String)


    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        If MsgBox("Are you really sure you want to rollback chain?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Rollback chain") = MsgBoxResult.Yes Then

            AddHandler Q.ProcHandler.Update, AddressOf ProcEvents
            wStep = 0
            lblInfo.Text = "Stopping wallet."

            If frmMain.Running = True Then
                WasRunning = True
            Else
                wStep = 1
            End If
            nrBlocks.Enabled = False
            btnStart.Enabled = False

            frmMain.StopWallet()
            WaitTimer = New Timer
            WaitTimer.Interval = 500
            WaitTimer.Enabled = True
            WaitTimer.Start()
        End If
    End Sub


    Private Sub WaitTimer_tick() Handles WaitTimer.Tick


        Select Case wStep
            Case 0
                If frmMain.Running = False Then wStep = 1
            Case 1
                lblInfo.Text = "Starting wallet in debug mode"
                frmMain.StartWallet(True)
                wStep = 2
            Case 3 'we have wallet in debug mode
                'now trying to popoff

                lblInfo.Refresh()
                Dim http As New clsHttp
                lblInfo.Text = "Clearing unconfirmed transactions."
                Dim result As String = http.GetUrl("http://localhost:8125/burst?requestType=clearUnconfirmedTransactions")
                lblInfo.Text = "Popping off blocks"
                result = http.GetUrl("http://localhost:8125/burst?requestType=popOff&height=&numBlocks=" & nrBlocks.Value.ToString)
                lblInfo.Text = "Stopping wallet."
                frmMain.StopWallet()
                wStep = 4
            Case 4
                If frmMain.Running = False Then
                    If WasRunning Then
                        frmMain.StartWallet()
                    End If
                    lblInfo.Text = "Done!"
                    RemoveHandler Q.ProcHandler.Update, AddressOf ProcEvents
                    WaitTimer.Stop()
                    WaitTimer.Enabled = False
                    nrBlocks.Enabled = True
                    btnStart.Enabled = True

                End If
        End Select
    End Sub

    Private Sub Complete()
        RemoveHandler Q.ProcHandler.Update, AddressOf ProcEvents
    End Sub
    Private Sub ProcEvents(ByVal AppId As Integer, ByVal Operation As Integer, ByVal data As String)
        If Me.InvokeRequired Then
            Dim d As New DProcEvents(AddressOf ProcEvents)
            Me.Invoke(d, New Object() {AppId, Operation, data})
            Return
        End If
        If AppId = QGlobal.AppNames.BRS Then
            Select Case Operation
                Case QGlobal.ProcOp.FoundSignal
                    wStep = 3
            End Select
        End If


    End Sub





    Private Sub PopOffSome()





    End Sub

End Class