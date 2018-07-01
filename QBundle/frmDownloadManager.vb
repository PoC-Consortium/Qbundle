Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Threading

Public Class frmDownloadManager
    Private _Aborted As Boolean
    Friend DownloadName As String = ""
    Friend Url As String = ""
    Friend Unzip As Boolean = False

    Private Delegate Sub DProgress(ByVal [Job] As Integer, ByVal [AppId] As Integer, ByVal [percent] As Integer, ByVal [Speed] As Integer, ByVal [lRead] As Long, ByVal [lLength] As Long)
    Private Delegate Sub DDone()
    Private Delegate Sub DAborting()

    Private Result As DialogResult = Nothing
    Private TimeElapsed As New Stopwatch

    Private Sub frmDownloadManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblSpeed.Text = "0 KB/sec"
        lblRead.Text = "0 / 0 bytes"
        lblProgress.Text = "0%"
        TimeElapsed.Start()
        Pb1.Value = 0
        DownloadFile()

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
        _Aborted = True

        Me.DialogResult = DialogResult.Cancel

    End Sub

    Public Sub DownloadFile()
        _Aborted = False
        Dim trda As Thread
        trda = New Thread(AddressOf Download)
        trda.IsBackground = True
        trda.Start()
        trda = Nothing
    End Sub
    Private Function Download() As Boolean

        Dim DLOk As Boolean = False
        Dim filename As String = QGlobal.AppDir & Path.GetFileName(Url)
        Dim File As FileStream = Nothing

        Try
            Dim bBuffer(262143) As Byte '256k chunks downloadbuffer
            Dim TotalRead As Long = 0
            Dim iBytesRead As Integer = 0
            Dim ContentLength As Long = 0
            Dim percent As Integer = 0

            Dim http As WebRequest = WebRequest.Create(Url)
            Dim WebResponse As WebResponse = http.GetResponse
            ContentLength = WebResponse.ContentLength
            Dim sChunks As Stream = WebResponse.GetResponseStream
            File = New FileStream(filename, FileMode.Create, FileAccess.Write)
            TotalRead = 0
            Dim SW As Stopwatch = Stopwatch.StartNew
            Dim speed As Integer = 0
            Do
                If _Aborted Then Exit Do 'we will return false 
                iBytesRead = sChunks.Read(bBuffer, 0, 262144)
                If iBytesRead = 0 Then Exit Do
                TotalRead += iBytesRead
                File.Write(bBuffer, 0, iBytesRead)
                If SW.ElapsedMilliseconds > 0 Then speed = CInt(TotalRead / SW.ElapsedMilliseconds)
                percent = CInt(Math.Round((TotalRead / ContentLength) * 100, 0))
                Progress(0, 0, percent, speed, TotalRead, ContentLength)
            Loop
            File.Flush()
            sChunks.Close()
            DLOk = True
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        Try
            File.Close()
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        If Unzip Then
            Extract()
            DeleteFile()
        End If
        Done()
    End Function
    Private Function Extract() As Boolean
        Dim AllOk As Boolean = False
        Try
            Dim filename As String = QGlobal.AppDir & Path.GetFileName(Url)
            Dim target As String = QGlobal.AppDir
            Dim Archive As ZipArchive = ZipFile.OpenRead(filename)
            Dim totalfiles As Integer = Archive.Entries.Count
            Dim counter As Integer = 0
            Dim percent As Integer = 0


            For Each entry As ZipArchiveEntry In Archive.Entries
                If _Aborted Then
                    Aborting()
                    Exit For
                End If

                If entry.FullName.EndsWith("/") Then
                    If Not Directory.Exists(Path.Combine(target, entry.FullName)) Then
                        Directory.CreateDirectory(Path.Combine(target, entry.FullName))
                    End If
                Else
                    entry.ExtractToFile(Path.Combine(target, entry.FullName), True)
                End If

                counter += 1
                percent = CInt(Math.Round((counter / totalfiles) * 100, 0))
                Progress(1, 0, percent, 0, 0, 0)
            Next
            AllOk = True
            Archive.Dispose()
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try

        Return AllOk


    End Function

    Private Sub DeleteFile()
        Try
            Dim filename As String = QGlobal.AppDir & Path.GetFileName(Url)
            If File.Exists(filename) Then
                File.Delete(filename)
            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
    End Sub
End Class