Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Threading
Imports System.Xml.Serialization

Public Class clsApp
    Public Event DownloadDone()
    Public Event Progress(ByVal JobType As Integer, ByVal AppId As Integer, ByVal Percernt As Integer, ByVal Speed As Integer, ByVal lRead As Long, ByVal lLength As Long)
    Public Event Aborted()
    Public Event UpdateAvailable()
    Private Structure StrucApps
        Dim LocalFound As Boolean
        Dim UppgradeUrl As String
        Dim FullUrl As String
        Dim RemoteVersion As String
        Dim LocalVersion As String
        Dim ExtractToDir As String
        Dim Updated As Boolean
        Dim Message As String
    End Structure
    Private _Apps() As StrucApps
    Private _Aborted As Boolean

    Private _UpdateNotifyState As Integer
    Public ErrMSg As String
    Public UpdateInfo As String
    Public DynamicInfo As UpdateObject
    Public Sub New()

        'our appstore
        Dim l As String() = [Enum].GetNames(GetType(QGlobal.AppNames))
        ReDim _Apps(UBound(l))
        l = Nothing
        For i As Integer = 0 To UBound(_Apps)
            _Apps(i).LocalFound = False
            _Apps(i).LocalVersion = ""
            _Apps(i).RemoteVersion = ""
            _Apps(i).UppgradeUrl = ""
            _Apps(i).FullUrl = ""
            _Apps(i).ExtractToDir = ""
            _Apps(i).Updated = False
            _Apps(i).Message = ""
        Next
        UpdateInfo = "QInfo.xml"
        DynamicInfo = New UpdateObject

        'repositories to download from




    End Sub

#Region " Detection "

    Public Sub LoadDynamicInfo()
        'load previous data
        Dim data As String = ""
        If DynamicInfo.Repositories(0) = "" Then LoadUpdateInfoXML()
        If IsNothing(DynamicInfo.Repositories) Or DynamicInfo.Repositories(0) = "" Then
            ReDim DynamicInfo.Repositories(0)
            DynamicInfo.Repositories(0) = "http://files.getburst.net/" 'fallback / rescue
        End If

        If Q.settings.CheckForUpdates Then
            If Not IsNothing(DynamicInfo.Repositories) Then
                For i = 0 To UBound(DynamicInfo.Repositories)
                    Dim Http As New clsHttp
                    data = Http.GetUrl(DynamicInfo.Repositories(i) & UpdateInfo)
                    If Http.Errmsg <> "" Then ErrMSg = Http.Errmsg
                    Http = Nothing
                    If data.Length <> 0 Then Exit For
                Next
            End If
        End If

        'save info if sucessfull. if not load previous config
        If data.Length > 0 Then
            Try
                IO.File.WriteAllText(QGlobal.BaseDir & "UpdateInfo.xml", data)
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
            'xml deserialize
            Try
                Dim x As New XmlSerializer(GetType(UpdateObject))
                Dim Reader As TextReader = New StringReader(data)
                DynamicInfo = DirectCast(x.Deserialize(Reader), UpdateObject)
                Reader.Close()
                Reader.Dispose()
                x = Nothing
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
        End If
    End Sub
    Public Sub LoadUpdateInfoXML()

        Try
            Dim data As String
            data = File.ReadAllText(QGlobal.BaseDir & "UpdateInfo.xml")
            Dim x As New XmlSerializer(GetType(UpdateObject))
            Dim Reader As TextReader = New StringReader(data)
            DynamicInfo = DirectCast(x.Deserialize(Reader), UpdateObject)
            Reader.Close()
            Reader.Dispose()
            x = Nothing
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try

    End Sub


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

        Xplotter()
        BlagoMiner()
    End Sub
    Public Function SetRemoteInfo() As Boolean
        LoadDynamicInfo()

        Dim allok As Boolean = False
        Dim AppId As Integer
        'setting AppInfo
        For x As Integer = 0 To UBound(DynamicInfo.Apps)
            Try
                AppId = CInt([Enum].Parse(GetType(QGlobal.AppNames), DynamicInfo.Apps(x).Name)) 'converting name to appid 'if not exist it will move on
                _Apps(AppId).RemoteVersion = DynamicInfo.Apps(x).RemoteVersion
                _Apps(AppId).ExtractToDir = ""
                _Apps(AppId).UppgradeUrl = DynamicInfo.Apps(x).UpgradeUrl
                _Apps(AppId).FullUrl = DynamicInfo.Apps(x).FullUrl
                _Apps(AppId).Updated = False 'reset updates
                allok = True
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
        Next

        Return AllOk
    End Function
    Public Function isInstalled(ByVal id As Integer) As Boolean
        Return _Apps(id).LocalFound
    End Function
    Private Sub Launcher()
        Try
            Dim Major As String = CStr(Reflection.Assembly.GetExecutingAssembly.GetName.Version.Major)
            Dim Minor As String = CStr(Reflection.Assembly.GetExecutingAssembly.GetName.Version.Minor)
            Dim Revision As String = CStr(Reflection.Assembly.GetExecutingAssembly.GetName.Version.Revision)
            _Apps(QGlobal.AppNames.Launcher).LocalFound = True
            _Apps(QGlobal.AppNames.Launcher).LocalVersion = Major & "." & Minor & "." & Revision
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
    End Sub
    Private Sub Nrs()

        Try
            If File.Exists(QGlobal.AppDir & "burst.jar") Then 'check if burst jar is here then we have nrs?
                _Apps(QGlobal.AppNames.BRS).LocalFound = True
                'try to set version since we have burst.jar
                If File.Exists(QGlobal.AppDir & "conf\version") Then
                    Dim Version As String = File.ReadAllText(QGlobal.AppDir & "conf\version")
                    _Apps(QGlobal.AppNames.BRS).LocalVersion = Version
                Else
                    'asume version 1.3.4cg
                    _Apps(QGlobal.AppNames.BRS).LocalVersion = "1.3.6cg"
                End If
            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
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
            p.StartInfo.Arguments = "-d64 -version"
            p.Start()
            p.WaitForExit(1000)
            result = LCase(p.StandardError.ReadLine())

            If result <> "" Then
                If LCase(result).Contains("java version") Then
                    result = result.Replace("java version", "")
                    result = result.Replace(" ", "")
                    result = Trim(result.Replace(Chr(34), ""))
                    JavaFound = CheckVersion("1.8", result, False)
                    If JavaFound Then
                        _Apps(QGlobal.AppNames.JavaInstalled).Message = "Java is found installed on your system."
                    Else
                        _Apps(QGlobal.AppNames.JavaInstalled).Message = "Installed java version is outdated."
                    End If
                End If
                If LCase(result).Contains("does not support") Then
                    _Apps(QGlobal.AppNames.JavaInstalled).Message = "32bit java was found but 64bit is required."
                End If
            Else
                _Apps(QGlobal.AppNames.JavaInstalled).Message = "Java is not found installed on your system."
            End If
            p.Dispose()
            p.Dispose()
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        _Apps(QGlobal.AppNames.JavaInstalled).LocalFound = JavaFound
    End Sub
    Private Sub JavaPortable()
        Try
            If File.Exists(QGlobal.AppDir & "Java\bin\java.exe") Then
                _Apps(QGlobal.AppNames.JavaPortable).LocalFound = True
                'try find Javaversion
                If File.Exists(QGlobal.AppDir & "Java\release") Then
                    Dim Lines() As String = File.ReadAllLines(QGlobal.AppDir & "Java\release")
                    _Apps(QGlobal.AppNames.JavaPortable).LocalVersion = Lines(0)
                Else
                    'asume 1.8.0_131
                    _Apps(QGlobal.AppNames.JavaPortable).LocalVersion = "1.8.0_131"
                End If
            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
    End Sub
    Private Sub MariaDB()
        Try
            If File.Exists(QGlobal.AppDir & "MariaDb\bin\mysqld.exe") Then
                _Apps(QGlobal.AppNames.MariaPortable).LocalFound = True
                'try find MariaVersion
                If File.Exists(QGlobal.AppDir & "MariaDb\release") Then
                    Dim version As String = File.ReadAllText(QGlobal.AppDir & "MariaDb\release")
                    _Apps(QGlobal.AppNames.MariaPortable).LocalVersion = version
                Else
                    'asume 5.5.29
                    _Apps(QGlobal.AppNames.MariaPortable).LocalVersion = "5.5.29"
                End If

            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
    End Sub
    Private Sub Xplotter()
        Try
            If File.Exists(QGlobal.AppDir & "Xplotter\XPlotter_avx.exe") Then
                _Apps(QGlobal.AppNames.Xplotter).LocalFound = True
                'try find MariaVersion
                If File.Exists(QGlobal.AppDir & "Xplotter\release") Then
                    Dim version As String = File.ReadAllText(QGlobal.AppDir & "Xplotter\release")
                    _Apps(QGlobal.AppNames.Xplotter).LocalVersion = version
                Else
                    'asume 1.0
                    _Apps(QGlobal.AppNames.Xplotter).LocalVersion = "1.0"
                End If

            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
    End Sub
    Private Sub BlagoMiner()
        Try
            If File.Exists(QGlobal.AppDir & "BlagoMiner\BlagoMiner_AVX2.exe") Then
                _Apps(QGlobal.AppNames.BlagoMiner).LocalFound = True
                'try find MariaVersion
                If File.Exists(QGlobal.AppDir & "BlagoMiner\version") Then
                    Dim version As String = File.ReadAllText(QGlobal.AppDir & "BlagoMiner\version")
                    _Apps(QGlobal.AppNames.BlagoMiner).LocalVersion = version
                Else
                    'asume 1.0
                    _Apps(QGlobal.AppNames.BlagoMiner).LocalVersion = "1.170820"
                End If

            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
    End Sub
#End Region

#Region " Download and unpack "

    '#########
    ' Here he have some Private vars we pass on to threads.
    '########
    Private DoDownload As Boolean 'if we are going to download anything at all
    Private DoUnpack As Boolean 'Unpack after download
    Private DoCleanUp As Boolean 'Remove downloaded file 
    Private AppNr As Integer 'Get from Qinfo.xml 
    Private Url As String 'Use this for download (overide AppId)
    Private DoUpgrade As Boolean 'Download from upgrade Qinfo.xml

    Public Sub AbortDownload()
        _Aborted = True
    End Sub

    ' #######
    ' Public Functions
    ' #######

    Public Sub InstallApp(ByVal Appid As Integer)
        'ok we have an integer

        DoDownload = True
        DoUnpack = True
        DoCleanUp = True
        AppNr = Appid
        Url = ""
        DoUpgrade = False
        _Aborted = False

        Dim trda As Thread
        trda = New Thread(AddressOf ProcessApp)
        trda.IsBackground = True
        trda.Start()
        trda = Nothing
    End Sub
    Public Sub UpgradeApp(ByVal Appid As Integer)

        DoDownload = True
        DoUnpack = True
        DoCleanUp = True
        AppNr = Appid
        Url = ""
        DoUpgrade = True
        _Aborted = False


        Dim trda As Thread
        trda = New Thread(AddressOf ProcessApp)
        trda.IsBackground = True
        trda.Start()
        trda = Nothing
    End Sub

    Public Sub DownloadFile(ByVal inUrl As String)
        _Aborted = False
        '_Apps(QGlobal.AppNames.DownloadFile).RemoteUrl = Url


        DoDownload = True
        DoUnpack = False
        DoCleanUp = False
        AppNr = 0
        Url = inUrl
        DoUpgrade = False
        _Aborted = False

        Dim trda As Thread
        trda = New Thread(AddressOf ProcessApp)
        trda.IsBackground = True
        trda.Start()
        trda = Nothing
    End Sub
    Public Sub DownloadUnzip(ByVal inUrl As String)
        _Aborted = False
        DoDownload = True
        DoUnpack = True
        DoCleanUp = True
        AppNr = 0
        Url = inUrl
        DoUpgrade = False
        _Aborted = False
        Dim trda As Thread
        trda = New Thread(AddressOf ProcessApp)
        trda.IsBackground = True
        trda.Start()
        trda = Nothing
    End Sub

    ' ######
    ' Private Functions in thread
    ' ######

    Private Sub ProcessApp()
        Dim LastProcessOk As Boolean = False

        '## Download Processing
        If DoDownload Then
            If Url <> "" Then
                If Download() Then LastProcessOk = True
            Else
                For x = 0 To UBound(DynamicInfo.Repositories)
                    If DoUpgrade Then
                        Url = DynamicInfo.Repositories(x) & _Apps(AppNr).UppgradeUrl
                    Else
                        Url = DynamicInfo.Repositories(x) & _Apps(AppNr).FullUrl
                    End If
                    If Download() Then
                        LastProcessOk = True
                        Exit For
                    End If
                Next
            End If
        Else
            LastProcessOk = True
        End If


        '## Unpack Processing (only if no dl or dl was ok)
        If LastProcessOk And DoCleanUp Then
            RaiseEvent Progress(1, AppNr, 0, 0, 0, 0) 'signal extract
            LastProcessOk = False
            If Extract() Then LastProcessOk = True
        End If

        '## Cleanup Processing (we still want cleanup if something goes wrong with extract)
        If DoCleanUp Then
            DeleteFile()
        End If

        '## Now we exit with correct signaling
        If LastProcessOk Then
            'all done
            RaiseEvent DownloadDone()
        Else
            'aborted
            RaiseEvent Aborted()
        End If




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
                RaiseEvent Progress(0, 0, percent, speed, TotalRead, ContentLength)
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
        Return DLOk
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
                    RaiseEvent Aborted()
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
                RaiseEvent Progress(1, 0, percent, 0, 0, 0)
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
                Return "Wallet"
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
            Case QGlobal.AppNames.BlagoMiner
                Return "Blago Miner"
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
    Public Function GetFullUrl(ByVal AppId As Integer) As String
        Return _Apps(AppId).FullUrl
    End Function
    Public Function GetUppgradeUrl(ByVal AppId As Integer) As String
        Return _Apps(AppId).UppgradeUrl
    End Function
    Public Function CheckOpenCL() As Boolean
        Try
            If IO.File.Exists(Environment.SystemDirectory & "\OpenCL.dll") Then
                Return True
            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        Return False
    End Function
    Public Function getAppMessage(ByVal appid As Integer) As String
        Return _Apps(appid).Message
    End Function
#End Region


    <Serializable>
    Public Class UpdateObject
        Public Structure AppObject
            Public Name As String
            Public UpgradeUrl As String
            Public FullUrl As String
            Public RemoteVersion As String
            Public UpdateInfo As String
        End Structure

        Public Structure PoolObject
            Public Name As String
            Public Address As String
            Public Port As String
            Public BurstAddress As String
            Public DeadLine As String
        End Structure
        Public Structure WalletObject
            Public Name As String
            Public Address As String
        End Structure
        Public Apps() As AppObject
        Public Pools() As PoolObject
        Public Wallets() As WalletObject
        Public Repositories() As String

        Sub New()
            ReDim Apps(0)
            ReDim Pools(0)
            ReDim Wallets(0)
            ReDim Repositories(0)
            Wallets(0).Name = "Local wallet"

        End Sub
    End Class

End Class
