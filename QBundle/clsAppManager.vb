Imports System.IO
Imports System.Management
Imports System.Xml.Serialization

Public Class clsAppManager
    Public AppXML As String
    Public ActiveRepo As String
    Public AppStore As UpdateObject
    Friend Messages As String
    Sub New()
        AppXML = "QInfo.xml"
        AppStore = New UpdateObject
        LoadAppStore()
    End Sub
    Friend Sub LoadAppStore()
        Try
            Dim data As String
            data = File.ReadAllText(QGlobal.BaseDir & AppXML)
            Dim x As New XmlSerializer(GetType(UpdateObject))
            Dim Reader As TextReader = New StringReader(data)
            AppStore = DirectCast(x.Deserialize(Reader), UpdateObject)
            Reader.Close()
            Reader.Dispose()
            x = Nothing
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try

    End Sub
    Friend Sub UpdateAppStoreInformation()
        'load previous data
        Dim data As String = ""
        If IsNothing(AppStore.Repositories) Or AppStore.Repositories(0) = "" Then
            ReDim AppStore.Repositories(0)
            AppStore.Repositories(0) = "http://files.getburst.net/" 'fallback / rescue
        End If
        If Q.settings.CheckForUpdates Then
            For i = 0 To UBound(AppStore.Repositories)
                Dim Http As New clsHttp
                data = Http.GetUrl(AppStore.Repositories(i) & AppXML)
                Http = Nothing
                If data.Length <> 0 Then Exit For
            Next
        End If
        'save info if sucessfull. if not load previous config
        If data.Length > 0 Then
            Try
                IO.File.WriteAllText(QGlobal.BaseDir & AppXML, data)
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
            LoadAppStore()
        End If
    End Sub

    Friend Function IsAppInstalled(ByVal AppName As String) As Boolean
        If AppName = "Qbundle" Then Return True
        For t As Integer = 0 To UBound(AppStore.Apps)
            If AppStore.Apps(t).Name = AppName Then Return IO.File.Exists(QGlobal.BaseDir & AppStore.Apps(t).VersionPath)
        Next

        Return False


    End Function
    Friend Function GetInstalledVersion(ByVal AppName As String, Optional ByVal UseFilter As Boolean = False) As String
        Dim Version As String = ""
        If AppName = "Qbundle" Then Return GetQbundleVersion()
        Try
            For t As Integer = 0 To UBound(AppStore.Apps)
                If AppStore.Apps(t).Name = AppName Then
                    If File.Exists(QGlobal.BaseDir & AppStore.Apps(t).VersionPath) Then
                        Version = File.ReadAllText(QGlobal.BaseDir & AppStore.Apps(t).VersionPath)
                        Exit For
                    End If
                End If
            Next
            If UseFilter Then Version = FilterVersionNr(Version)
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try

        Return Version
    End Function
    Friend Function GetAppStoreVersion(ByVal AppName As String, Optional ByVal UseFilter As Boolean = False) As String
        Dim Version As String = ""
        Try
            For t As Integer = 0 To UBound(AppStore.Apps)
                If AppStore.Apps(t).Name = AppName Then
                    Version = AppStore.Apps(t).RemoteVersion
                    Exit For
                End If
            Next
            If UseFilter Then Version = FilterVersionNr(Version)
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        Return Version
    End Function
    Friend Function DoesAppNeedUpdate(ByVal AppName As String) As Boolean

        Try
            For t As Integer = 0 To UBound(AppStore.Apps)
                If AppStore.Apps(t).Name = AppName Then
                    Return CheckVersion(GetInstalledVersion(AppName), GetAppStoreVersion(AppName), True)
                End If
            Next
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        Return False
    End Function
    Friend Function CheckVersion(ByVal OldVersion As String, ByVal NewVersion As String, ByVal OnlyNew As Boolean) As Boolean

        OldVersion = FilterVersionNr(OldVersion)
        NewVersion = FilterVersionNr(NewVersion)

        OldVersion = OldVersion.Replace("_", ".")
        Dim mver() As String = Split(OldVersion, ".")

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
    Friend Function IsAppRunning(ByVal AppName As String) As Boolean
        Dim ProcessName As String = ""
        For t As Integer = 0 To UBound(AppStore.Apps)
            If AppStore.Apps(t).Name = AppName Then
                ProcessName = AppStore.Apps(t).ProcessName
            End If
        Next
        If ProcessName = "" Then Return False
        Try
            Dim searcher As ManagementObjectSearcher
            searcher = New ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_Process WHERE Name='" & ProcessName & "'")
            For Each p As ManagementObject In searcher.[Get]()
                ' cmdline = p("CommandLine")

                Return True
            Next
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        Return False
    End Function
    Friend Function GetQbundleVersion() As String
        Try
            Dim Major As String = CStr(Reflection.Assembly.GetExecutingAssembly.GetName.Version.Major)
            Dim Minor As String = CStr(Reflection.Assembly.GetExecutingAssembly.GetName.Version.Minor)
            Dim Revision As String = CStr(Reflection.Assembly.GetExecutingAssembly.GetName.Version.Revision)
            Return Major & "." & Minor & "." & Revision
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        Return "1.0"
    End Function

    Friend Function InstallApp(ByVal AppName As String, Optional ForceReinstall As Boolean = False)
        For t As Integer = 0 To UBound(AppStore.Apps)
            If AppStore.Apps(t).Name = AppName Then
                Dim s As New frmDownloadManager
                s.DownloadName = AppStore.Apps(t).DisplayName
                If ForceReinstall Then
                    s.Url = AppStore.Repositories(0) & AppStore.Apps(t).FullUrl
                Else
                    If IsAppInstalled(AppName) Then
                        s.Url = AppStore.Repositories(0) & AppStore.Apps(t).UpgradeUrl
                    Else
                        s.Url = AppStore.Repositories(0) & AppStore.Apps(t).FullUrl
                    End If
                End If
                s.Unzip = True

                Dim res As DialogResult
                res = s.ShowDialog
                If res = DialogResult.Cancel Then
                    Return False
                ElseIf res = DialogResult.Abort Then
                    MsgBox("Something went wrong. Internet connection might have been lost.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
                    Return False
                End If
                Return True
            End If
        Next

        Return False
    End Function

    Friend Function isJavaInstalled() As Boolean

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
                        Messages = "Java is found installed on your system."
                        Return True
                    Else
                        Messages = "Installed java version is outdated."
                    End If
                End If
                If LCase(result).Contains("does not support") Then
                    Messages = "32bit java was found but 64bit is required."
                End If
            Else
                Messages = "Java is not found installed on your system."
            End If
            p.Dispose()
            p.Dispose()
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try

        Return IsAppInstalled("JavaPortable")


    End Function

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

    <Serializable>
    Public Class UpdateObject
        Public Structure AppObject
            Public Name As String
            Public UpgradeUrl As String
            Public FullUrl As String
            Public RemoteVersion As String
            Public UpdateInfo As String
            Public VersionPath As String
            Public ProcessName As String
            Public ExePath As String
            Public DisplayName As String
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
