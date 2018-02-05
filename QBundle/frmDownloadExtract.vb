Public Class frmDownloadExtract

    Friend OverideFilename As String
    Friend Appid As Integer
    Friend Upgrade As Boolean = False
    Friend Url As String
    Friend Unzip As Boolean = False
    Private Delegate Sub DProgress(ByVal [Job] As Integer, ByVal [AppId] As Integer, ByVal [percent] As Integer, ByVal [Speed] As Integer, ByVal [lRead] As Long, ByVal [lLength] As Long)
    Private Delegate Sub DDone()
    Private Delegate Sub DAborting()

    Private DownloadName As String 'set depending on dl type
    Private Result As DialogResult = Nothing
    Private TimeElapsed As New Stopwatch
    Private Sub frmDownloadExtract_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        AddHandler Q.App.Progress, AddressOf Progress
        AddHandler Q.App.DownloadDone, AddressOf Done
        AddHandler Q.App.Aborted, AddressOf Aborting
        lblSpeed.Text = "0 KB/sec"
        lblRead.Text = "0 / 0 bytes"
        lblProgress.Text = "0%"
        TimeElapsed.Start()
        Pb1.Value = 0

        If Url <> "" Then
            If Unzip Then
                Q.App.DownloadUnzip(Url)
            Else
                Q.App.DownloadFile(Url)
            End If
            DownloadName = IO.Path.GetFileName(Url) 'just download
        Else
            If Upgrade Then
                Q.App.UpgradeApp(Appid) 'download and extract
            Else
                Q.App.InstallApp(Appid) 'download and extract
            End If

            DownloadName = Q.App.GetAppNameFromId(Appid)
        End If

        If OverideFilename <> "" Then
            DownloadName = OverideFilename
        End If

    End Sub
    Public Sub Done()
        If Me.InvokeRequired Then
            Dim d As New DDone(AddressOf Done)
            Me.Invoke(d, New Object() {})
            Return
        End If

        lblProgress.Text = "100%"
        Pb1.Value = 100
        Try
            RemoveHandler Q.App.Progress, AddressOf Progress
            RemoveHandler Q.App.DownloadDone, AddressOf Done
            RemoveHandler Q.App.Aborted, AddressOf Aborting
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try

        'we are done so close
        If Result = Nothing Then
            Me.DialogResult = DialogResult.OK
        Else
            Me.DialogResult = Result
        End If

        Me.Close()
    End Sub

    Private Sub Aborting()
        If Me.InvokeRequired Then
            Dim d As New DAborting(AddressOf Aborting)
            Me.Invoke(d, New Object() {})
            Return
        End If
        If Result = Nothing Then Result = DialogResult.Abort
        Me.DialogResult = Result
        Me.Close()
    End Sub

    Private Sub Progress(ByVal Job As Integer, ByVal AppId As Integer, percent As Integer, ByVal Speed As Integer, ByVal lRead As Long, ByVal lLength As Long)
        If Me.InvokeRequired Then
            Dim d As New DProgress(AddressOf Progress)
            Me.Invoke(d, New Object() {Job, AppId, percent, Speed, lRead, lLength})
            Return
        End If
        Dim TimeLeft As TimeSpan
        Select Case Job
            Case 0
                Try
                    TimeLeft = TimeSpan.FromMilliseconds((lLength - lRead) * TimeElapsed.ElapsedMilliseconds / lRead)
                Catch ex As Exception
                End Try

                lblStatus.Text = "Downloading " & DownloadName
                If Speed > 1024 Then
                    lblSpeed.Text = CStr(Math.Round(Speed / 1024, 2)) & " MiB / sec"
                Else
                    lblSpeed.Text = CStr(Speed) & " KiB / sec"
                End If

                lblRead.Text = CStr(lRead) & " / " & CStr(lLength) & " bytes"
                Try
                    lblTime.Text = TimeLeft.Hours & "h " & TimeLeft.Minutes & "m " & TimeLeft.Seconds & "s"
                Catch ex As Exception
                End Try
            Case 1
                lblStatus.Text = "Extracting: " & DownloadName
                lblSpeed.Visible = False
                lblRead.Visible = False
        End Select
        lblProgress.Text = CStr(percent) & "%"
        Pb1.Value = percent
    End Sub

    Private Sub btnAbort_Click(sender As Object, e As EventArgs) Handles btnAbort.Click
        Me.DialogResult = DialogResult.Cancel
        Q.App.AbortDownload()
    End Sub
End Class