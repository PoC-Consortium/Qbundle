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
                frmMain.StopWallet()
            Else
                wStep = 1
                WasRunning = False
            End If
            nrBlocks.Enabled = False
            btnStart.Enabled = False

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
                wStep = 2
                lblInfo.Text = "Starting wallet in debug mode"
                frmMain.StartWallet(True)
            Case 3 'we have wallet in debug mode
                'now trying to popoff
                Dim s() As String = Split(Q.settings.ListenIf, ";")
                Dim url As String = Nothing
                If s(0) = "0.0.0.0" Then
                    url = "http://127.0.0.1:" & s(1)
                Else
                    url = "http://" & s(0) & ":" & s(1)
                End If

                lblInfo.Refresh()
                Dim http As New clsHttp
                lblInfo.Text = "Clearing unconfirmed transactions."
                Dim result As String = http.GetUrl(url & "/burst?requestType=clearUnconfirmedTransactions")
                lblInfo.Text = "Popping off blocks"
                result = http.GetUrl(url & "/burst?requestType=popOff&height=&numBlocks=" & nrBlocks.Value.ToString)

                wStep = 4
                lblInfo.Text = "Stopping wallet."
                frmMain.StopWallet()
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