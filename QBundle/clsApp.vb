Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Threading

Public Class clsApp
    Public Event DownloadDone(ByVal AppId As Integer)
    Public Event Progress(ByVal JobType As Integer, ByVal AppId As Integer, ByVal Percernt As Integer, ByVal Speed As Integer, ByVal lRead As Long, ByVal lLength As Long)
    Public Event Aborted(ByVal AppId As Integer)
    Public Event UpdateAvailable()
    Private Structure StrucApps
        Dim LocalFound As Boolean
        Dim RemoteUrl As String
        Dim RemoteVersion As String
        Dim LocalVersion As String
        Dim ExtractToDir As String
        Dim Updated As Boolean
    End Structure
    Private _Apps() As StrucApps
    Private _Aborted As Boolean
    Private _Repositories() As String
    Private _UpdateNotifyState As Integer
    Public ErrMSg As String
    Public Sub New()

        'our appstore
        Dim l As String() = [Enum].GetNames(GetType(QGlobal.AppNames))
        ReDim _Apps(UBound(l))
        l = Nothing
        For i As Integer = 0 To UBound(_Apps)
            _Apps(i).LocalFound = False
            _Apps(i).LocalVersion = ""
            _Apps(i).RemoteVersion = ""
            _Apps(i).RemoteUrl = ""
            _Apps(i).ExtractToDir = ""
            _Apps(i).Updated = False
        Next

        'repositories to download from
        ReDim _Repositories(1)
        _Repositories(0) = "http://files.getburst.net/"
        _Repositories(1) = "http://files.getburst.net/"



    End Sub

#Region " Detection "
    Public Sub SetLocalInfo()
        'Set Launcher version
        Launcher()
        'check nrs
        Nrs()
        'check installed java
        JavaInstalled()
        'check portable java
        JavaPortable()
        'check portable maria
        MariaDB()

        XPlotter()

    End Sub
    Public Function SetRemoteInfo() As Boolean
        Dim data As String = ""
        Dim AllOk As Boolean = False
        For i = 0 To UBound(_Repositories)
            Dim Http As New clsHttp
            data = Http.GetUrl(_Repositories(i) & "QInfo")
            If Http.Errmsg <> "" Then ErrMSg = Http.Errmsg
            Http = Nothing
            If data.Length <> 0 Then Exit For
        Next

        'ok we have the data to parse
        'now we need to translate the names into correct integer values corresponding to Appname enum
        Dim AppId As Integer
        If data.Length <> 0 Then
            Dim Lines() As String = Split(data, vbCrLf)
            For Each Line In Lines
                If Trim(Line) <> "" Then
                    Try
                        Dim Cell() As String = Split(Line, "|")
                        AppId = [Enum].Parse(GetType(QGlobal.AppNames), Cell(0)) 'converting name to appid 'if not exist it will move on
                        _Apps(AppId).RemoteVersion = Cell(1)
                        _Apps(AppId).ExtractToDir = Cell(2)
                        _Apps(AppId).RemoteUrl = Cell(3)
                        _Apps(AppId).Updated = False 'reset updates
                        AllOk = True
                    Catch ex As Exception
                        If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
                    End Try
                End If
            Next
        End If

        Return AllOk
    End Function
    Public Function isInstalled(ByVal id As Integer) As Boolean
        Return _Apps(id).LocalFound
    End Function
    Private Sub Launcher()
        Try
            Dim Major As String = CStr(Reflection.Assembly.GetExecutingAssembly.GetName.Version.Major)
            Dim Minor As String = CStr(Reflection.Assembly.GetExecutingAssembly.GetName.Version.Minor)
            _Apps(QGlobal.AppNames.Launcher).LocalFound = True
            _Apps(QGlobal.AppNames.Launcher).LocalVersion = Major & "." & Minor
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
    End Sub
    Private Sub Nrs()

        Try
            If File.Exists(QGlobal.BaseDir & "burst.jar") Then 'check if burst jar is here then we have nrs?
                _Apps(QGlobal.AppNames.BRS).LocalFound = True
                'try to set version since we have burst.jar
                If File.Exists(QGlobal.BaseDir & "conf\version") Then
                    Dim Version As String = File.ReadAllText(QGlobal.BaseDir & "conf\version")
                    _Apps(QGlobal.AppNames.BRS).LocalVersion = Version
                Else
                    'asume version 1.3.4cg
                    _Apps(QGlobal.AppNames.BRS).LocalVersion = "1.3.6cg"
                End If
            End If
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try



    End Sub
    Private Sub JavaInstalled()
        Dim JavaFound As Boolean = False
        Try
            Dim p As New Process
            Dim result As String = ""
            p.StartInfo.RedirectStandardError = True
            p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.UseShellExecute = False
            p.StartInfo.CreateNoWindow = True
            p.StartInfo.FileName = "java"
            p.StartInfo.Arguments = "-d64 -showversion"
            p.Start()
            p.WaitForExit()
            result = LCase(p.StandardError.ReadLine())
            If result <> "" Then
                If LCase(result).Contains("java version") Then
                    result = result.Replace("java version", "")
                    result = result.Replace(" ", "")
                    result = Trim(result.Replace(Chr(34), ""))
                    JavaFound = CheckVersion("1.8", result, False)
                End If
            End If
            p.Dispose()
            p.Dispose()
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
        _Apps(QGlobal.AppNames.JavaInstalled).LocalFound = JavaFound
    End Sub
    Private Sub JavaPortable()
        Try
            If File.Exists(QGlobal.BaseDir & "Java\bin\java.exe") Then
                _Apps(QGlobal.AppNames.JavaPortable).LocalFound = True
                'try find Javaversion
                If File.Exists(QGlobal.BaseDir & "Java\release") Then
                    Dim Lines() As String = File.ReadAllLines(QGlobal.BaseDir & "Java\release")
                    _Apps(QGlobal.AppNames.JavaPortable).LocalVersion = Lines(0)
                Else
                    'asume 1.8.0_131
                    _Apps(QGlobal.AppNames.JavaPortable).LocalVersion = "1.8.0_131"
                End If
            End If
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
    End Sub
    Private Sub MariaDB()
        Try
            If File.Exists(QGlobal.BaseDir & "MariaDb\bin\mysqld.exe") Then
                _Apps(QGlobal.AppNames.MariaPortable).LocalFound = True
                'try find MariaVersion
                If File.Exists(QGlobal.BaseDir & "MariaDb\release") Then
                    Dim version As String = File.ReadAllText(QGlobal.BaseDir & "MariaDb\release")
                    _Apps(QGlobal.AppNames.MariaPortable).LocalVersion = version
                Else
                    'asume 5.5.29
                    _Apps(QGlobal.AppNames.MariaPortable).LocalVersion = "5.5.29"
                End If

            End If
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
    End Sub
    Private Sub Xplotter()
        Try
            If File.Exists(QGlobal.BaseDir & "Xplotter\XPlotter_avx.exe") Then
                _Apps(QGlobal.AppNames.Xplotter).LocalFound = True
                'try find MariaVersion
                If File.Exists(QGlobal.BaseDir & "Xplotter\release") Then
                    Dim version As String = File.ReadAllText(QGlobal.BaseDir & "Xplotter\release")
                    _Apps(QGlobal.AppNames.Xplotter).LocalVersion = version
                Else
                    'asume 1.0
                    _Apps(QGlobal.AppNames.Xplotter).LocalVersion = "1.0"
                End If

            End If
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
    End Sub
#End Region

#Region " Download and unpack "
    Public Sub AbortDownload()
        _Aborted = True
    End Sub
    Public Sub DownloadApp(ByVal Appid As Integer)
        'ok we have an integer
        _Aborted = False
        Dim trda As Thread
        trda = New Thread(AddressOf DownloadUnpack)
        trda.IsBackground = True
        trda.Start(Appid)
        trda = Nothing
    End Sub
    Public Sub DownloadFile(ByVal Url As String)
        _Aborted = False
        _Apps(QGlobal.AppNames.DownloadFile).RemoteUrl = Url
        Dim trda As Thread
        trda = New Thread(AddressOf DownloadOnly)
        trda.IsBackground = True
        trda.Start(QGlobal.AppNames.DownloadFile)
        trda = Nothing
    End Sub
    Private Sub DownloadOnly(ByVal obj As Object)

        Dim appid As Integer = CType(obj, Integer)
        If Not Download(appid, False) Then 'ok lets start download
            RaiseEvent Aborted(appid)
            Exit Sub
        End If
        If Not _Aborted Then RaiseEvent DownloadDone(appid)
    End Sub

    Private Sub DownloadUnpack(ByVal obj As Object)
        Dim appid As Integer = CType(obj, Integer)
        'we are now in threaded environment
        'if we do not have remoteinfo lets get it.
        If _Apps(QGlobal.AppNames.BRS).RemoteUrl = "" Then
            If Not SetRemoteInfo() Then
                RaiseEvent Aborted(appid)
                Exit Sub
            End If
        End If

        If Not Download(appid) Then 'ok lets start download
            RaiseEvent Aborted(appid)
            Exit Sub
        End If
        If _Aborted Then Exit Sub
        If Not Extract(appid) Then 'ok lets start download
            RaiseEvent Aborted(appid)
            Exit Sub
        End If
        DeleteFile(appid)
        If _Aborted Then Exit Sub
        _Apps(appid).Updated = True
        RaiseEvent DownloadDone(appid)
    End Sub

    Private Function Download(ByVal AppId As Integer, Optional ByVal FromRepos As Boolean = True) As Boolean

        Dim DLOk As Integer = False
        Dim filename As String = QGlobal.BaseDir & Path.GetFileName(_Apps(AppId).RemoteUrl)
        Dim File As FileStream = Nothing
        For x = 0 To UBound(_Repositories) 'try next repo if fail.
            Try
                Dim bBuffer(262143) As Byte '256k chunks downloadbuffer
                Dim TotalRead As Long = 0
                Dim iBytesRead As Integer = 0
                Dim ContentLength As Long = 0
                Dim percent As Integer = 0
                Dim url As String = ""
                If FromRepos Then
                    url = _Repositories(x) & _Apps(AppId).RemoteUrl
                Else
                    url = _Apps(AppId).RemoteUrl
                End If
                Dim http As HttpWebRequest = WebRequest.Create(url)
                Dim WebResponse As HttpWebResponse = http.GetResponse
                ContentLength = WebResponse.ContentLength
                Dim sChunks As Stream = WebResponse.GetResponseStream
                File = New FileStream(filename, FileMode.Create, FileAccess.Write)
                TotalRead = 0
                Dim SW As Stopwatch = Stopwatch.StartNew
                Dim speed As Integer = 0
                Do
                    If _Aborted Then
                        RaiseEvent Aborted(AppId)
                        Exit Do
                    End If
                    iBytesRead = sChunks.Read(bBuffer, 0, 262144)
                    If iBytesRead = 0 Then Exit Do
                    TotalRead += iBytesRead
                    File.Write(bBuffer, 0, iBytesRead)
                    If SW.ElapsedMilliseconds > 0 Then speed = CInt(TotalRead / SW.ElapsedMilliseconds)
                    percent = Math.Round((TotalRead / ContentLength) * 100, 0)
                    RaiseEvent Progress(0, AppId, percent, speed, TotalRead, ContentLength)
                Loop
                File.Flush()
                sChunks.Close()
                DLOk = True
            Catch ex As Exception
                If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
            End Try
            Try
                File.Close()
            Catch ex As Exception
                If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
            End Try
            'we need to cleanup
            If DLOk Then Exit For
        Next
        Return DLOk
    End Function
    Private Function Extract(ByVal AppId As Integer) As Boolean
        Dim AllOk As Boolean = False
        Try
            Dim filename As String = QGlobal.BaseDir & Path.GetFileName(_Apps(AppId).RemoteUrl)
            Dim target As String = QGlobal.BaseDir & _Apps(AppId).ExtractToDir
            Dim Archive As ZipArchive = ZipFile.OpenRead(filename)
            Dim totalfiles As Integer = Archive.Entries.Count
            Dim counter As Integer = 0
            Dim percent As Integer = 0
            For Each entry As ZipArchiveEntry In Archive.Entries
                If _Aborted Then
                    RaiseEvent Aborted(AppId)
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
                percent = Math.Round((counter / totalfiles) * 100, 0)
                RaiseEvent Progress(1, AppId, percent, 0, 0, 0)
            Next
            AllOk = True
            Archive.Dispose()
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try

        Return AllOk


    End Function
    Private Sub DeleteFile(ByVal appid As Integer)
        Try
            Dim filename As String = QGlobal.BaseDir & Path.GetFileName(_Apps(appid).RemoteUrl)
            If File.Exists(filename) Then
                File.Delete(filename)
            End If
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
    End Sub
#End Region

#Region " Updates "
    Public Sub StartUpdateNotifications()

        If Not _UpdateNotifyState = QGlobal.States.Stopped Then
            Exit Sub
        End If
        _UpdateNotifyState = QGlobal.States.Running

        Dim trda As Thread
        trda = New Thread(AddressOf UpdateNotifyTimer)
        trda.IsBackground = True
        trda.Start()
        trda = Nothing
    End Sub
    Public Sub StopUpdateNotifications()
        _UpdateNotifyState = QGlobal.States.Abort

    End Sub
    Private Sub UpdateNotifyTimer()
        Dim Nextcheck As New Date
        Dim Data As String = ""
        Do
            If SetRemoteInfo() Then
                For l = 0 To UBound(_Apps)
                    If _Apps(l).LocalFound And _Apps(l).RemoteVersion <> "" Then 'is it installed and do we have a repo for it?
                        If CheckVersion(_Apps(l).LocalVersion, _Apps(l).RemoteVersion, True) Then
                            RaiseEvent UpdateAvailable()
                            Exit For
                        End If
                    End If
                Next
            End If
            Nextcheck = Now.AddDays(1) '24 hours check
            Do 'sleepthread
                Thread.Sleep(600000) 'sleep for 10 minutes
                If Now >= Nextcheck Then Exit Do
                If _UpdateNotifyState = QGlobal.States.Abort Then Exit Do
            Loop
            If _UpdateNotifyState = QGlobal.States.Abort Then Exit Do
        Loop
        _UpdateNotifyState = QGlobal.States.Stopped
    End Sub
#End Region

#Region " Misc Functions "
    Private Function FilterVersionNr(ByVal data As String) As String
        Dim acceptedChars() As Char = "01234567890._".ToCharArray
        data = (From ch As Char In data Select ch Where acceptedChars.Contains(ch)).ToArray
        'remove all preceeding or trailing _ or .
        Do
            If data.StartsWith("_") Or data.StartsWith(".") Then
                data = data.Substring(1)
            Else
                Exit Do
            End If
        Loop
        Do
            If data.EndsWith("_") Or data.EndsWith(".") Then
                data = data.Substring(0, Len(data) - 1)
            Else
                Exit Do
            End If
        Loop
        Return data
    End Function
    Public Function CheckVersion(ByVal MinVersion As String, ByVal NewVersion As String, ByVal OnlyNew As Boolean) As Boolean

        MinVersion = FilterVersionNr(MinVersion)
        NewVersion = FilterVersionNr(NewVersion)

        MinVersion = MinVersion.Replace("_", ".")
        Dim mver() As String = Split(MinVersion, ".")

        NewVersion = NewVersion.Replace("_", ".")
        Dim nver() As String = Split(NewVersion, ".")

        If nver.Length <> mver.Length Then
            If nver.Length > mver.Length Then
                ReDim Preserve mver(UBound(nver))
            Else
                ReDim Preserve nver(UBound(mver))
            End If
        End If

        Dim vheight As Integer = 1 '0=lower version '1 same version '2 newer version
        For t As Integer = 0 To UBound(mver)
            If Val(nver(t)) > Val(mver(t)) Then
                vheight = 2
                Exit For
            ElseIf nver(t) < mver(t) Then
                vheight = 0
                Exit For
            End If
        Next
        Dim result As Boolean = False
        If OnlyNew Then
            If vheight = 2 Then result = True
        Else
            If vheight <> 0 Then result = True
        End If

        Return result
    End Function
    Public Function GetAppNameFromId(ByVal AppId As Integer) As String
        'Used when AppId needs resolves to human readable names
        Select Case AppId
            Case QGlobal.AppNames.BRS
                Return "BRS"
            Case QGlobal.AppNames.JavaInstalled
                Return "Java"
            Case QGlobal.AppNames.JavaPortable
                Return "Portable Java"
            Case QGlobal.AppNames.MariaPortable
                Return "Portable MariaDB"
            Case QGlobal.AppNames.Launcher
                Return "Qbundle"
            Case QGlobal.AppNames.Xplotter
                Return "Xplotter"
        End Select
        Return ""
    End Function
    Public Function GetDbNameFromType(ByVal Dtype As Integer) As String

        Select Case Dtype
            Case QGlobal.DbType.H2
                Return "H2"
            Case QGlobal.DbType.FireBird
                Return "FireBird"
            Case QGlobal.DbType.MariaDB
                Return "MariaDB"
            Case QGlobal.DbType.pMariaDB
                Return "MariaDB"
        End Select
        Return ""
    End Function
    Public Function GetLocalVersion(ByVal appid As Integer, Optional ByVal UseFilter As Boolean = True) As String
        If UseFilter Then Return FilterVersionNr(_Apps(appid).LocalVersion)
        Return _Apps(appid).LocalVersion
    End Function
    Public Function GetRemoteVersion(ByVal appid As Integer, Optional ByVal UseFilter As Boolean = True) As String
        If UseFilter Then Return FilterVersionNr(_Apps(appid).RemoteVersion)
        Return _Apps(appid).RemoteVersion
    End Function
    Public Function ShouldUpdate(ByVal AppId As Integer) As Boolean
        If _Apps(AppId).LocalFound Then 'we have it installed?
            If _Apps(AppId).RemoteVersion <> "" Then 'this means it has a repository entry
                Return CheckVersion(_Apps(AppId).LocalVersion, _Apps(AppId).RemoteVersion, True)
            End If
        End If
        Return False
    End Function
    Public Function HasRepository(ByVal AppId As Integer) As Boolean
        If _Apps(AppId).RemoteVersion <> "" Then Return True
        Return False
    End Function
    Public Function isUpdated(ByVal AppId As Integer) As Boolean
        Return _Apps(AppId).Updated
    End Function
    Public Function GetRemoteUrl(ByVal AppId As Integer) As String
        Return _Apps(AppId).RemoteUrl
    End Function



    Public Function CheckOpenCL()
        Try
            If IO.File.Exists(Environment.SystemDirectory & "\OpenCL.dll") Then
                Return True
            End If
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
        Return False
    End Function
#End Region

End Class
