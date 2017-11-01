Public Class frmDownloadExtract

    Friend OverideFilename As String
    Friend Appid As Integer
    Friend Url As String
    Private Delegate Sub DProgress(ByVal [Job] As Integer, ByVal [AppId] As Integer, ByVal [percent] As Integer, ByVal [Speed] As Integer, ByVal [lRead] As Long, ByVal [lLength] As Long)
    Private Delegate Sub DDone(ByVal [AppId] As Integer)
    Private Delegate Sub DAborting(ByVal [AppId] As Integer)

    Private DownloadName As String 'set depending on dl type
    Private Result As DialogResult = Nothing
    Private TimeElapsed As New Stopwatch
    Private Sub frmDownloadExtract_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        AddHandler App.Progress, AddressOf Progress
        AddHandler App.DownloadDone, AddressOf Done
        AddHandler App.Aborted, AddressOf Aborting
        lblSpeed.Text = "0 KB/sec"
        lblRead.Text = "0 / 0 bytes"
        lblProgress.Text = "0%"


        TimeElapsed.Start()

        Pb1.Value = 0
        If Url <> "" Then
            App.DownloadFile(Url)
            DownloadName = IO.Path.GetFileName(Url) 'just download
        Else
            App.DownloadApp(Appid) 'download and extract
            DownloadName = App.GetAppNameFromId(Appid)
        End If
        If OverideFilename <> "" Then
            DownloadName = OverideFilename
        End If
    End Sub
    Public Sub Done(ByVal AppId As Integer)
        If Me.InvokeRequired Then
            Dim d As New DDone(AddressOf Done)
            Me.Invoke(d, New Object() {AppId})
            Return
        End If

        lblProgress.Text = "100%"
        Pb1.Value = 100
        Try
            RemoveHandler App.Progress, AddressOf Progress
            RemoveHandler App.DownloadDone, AddressOf Done
            RemoveHandler App.Aborted, AddressOf Aborting
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try

        'we are done so close
        If Result = Nothing Then
            Me.DialogResult = DialogResult.OK
        Else
            Me.DialogResult = Result
        End If

        Me.Close()
    End Sub

    Private Sub Aborting(ByVal AppId As Integer)
        If Me.InvokeRequired Then
            Dim d As New DAborting(AddressOf Aborting)
            Me.Invoke(d, New Object() {AppId})
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
                lblSpeed.Text = CStr(Speed) & "KiB / sec"
                lblRead.Text = CStr(lRead) & " / " & CStr(lLength) & " bytes"
                Try
                    lblTime.Text = TimeLeft.Hours & ":" & TimeLeft.Minutes & ":" & TimeLeft.Seconds
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
        App.AbortDownload()
    End Sub
End Class