Friend Class clsSettings
    'NRS
    Private _autoip As Boolean
    Private _WalletException As Boolean
    Private _DynPlatform As Boolean
    Private _useOpenCL As Boolean
    Private _Cpulimit As Integer
    Private _ListenIf As String
    Private _ListenPeer As String
    Private _ConnectFrom As String
    'DB
    Private _DbType As Integer
    Private _DbServer As String
    Private _DbUser As String
    Private _DbName As String
    Private _DbPass As String

    'JAVA
    Private _JavaType As Integer

    'general
    Private _FirstRun As Boolean
    Private _CheckForUpdates As Boolean
    Private _Upgradev As Integer
    Private _AlwaysAdmin As Boolean
    Private _Repo As String
    Private _QBMode As Integer

    'Plotting And Mining
    Private _Plots As String

    'NRS
    Public Property AutoIp() As Boolean
        Get
            Return _autoip
        End Get
        Set(ByVal value As Boolean)
            _autoip = value
        End Set
    End Property
    Public Property WalletException() As Boolean
        Get
            Return _WalletException
        End Get
        Set(ByVal value As Boolean)
            _WalletException = value
        End Set
    End Property
    Public Property DynPlatform() As Boolean
        Get
            Return _DynPlatform
        End Get
        Set(ByVal value As Boolean)
            _DynPlatform = value
        End Set
    End Property
    Public Property useOpenCL() As Boolean
        Get
            Return _useOpenCL
        End Get
        Set(ByVal value As Boolean)
            _useOpenCL = value
        End Set
    End Property
    Public Property Cpulimit() As Integer
        Get
            Return _Cpulimit
        End Get
        Set(ByVal value As Integer)
            _Cpulimit = value
        End Set
    End Property
    Public Property ListenIf() As String
        Get
            Return _ListenIf
        End Get
        Set(ByVal value As String)
            _ListenIf = value
        End Set
    End Property
    Public Property ListenPeer() As String
        Get
            Return _ListenPeer
        End Get
        Set(ByVal value As String)
            _ListenPeer = value
        End Set
    End Property
    Public Property ConnectFrom() As String
        Get
            Return _ConnectFrom
        End Get
        Set(ByVal value As String)
            _ConnectFrom = value
        End Set
    End Property
    'DB
    Public Property DbType() As Integer
        Get
            Return _DbType
        End Get
        Set(ByVal value As Integer)
            _DbType = value
        End Set
    End Property
    Public Property DbServer() As String
        Get
            Return _DbServer
        End Get
        Set(ByVal value As String)
            _DbServer = value
        End Set
    End Property
    Public Property DbName() As String
        Get
            Return _DbName
        End Get
        Set(ByVal value As String)
            _DbName = value
        End Set
    End Property
    Public Property DbUser() As String
        Get
            Return _DbUser
        End Get
        Set(ByVal value As String)
            _DbUser = value
        End Set
    End Property
    Public Property DbPass() As String
        Get
            Return _DbPass
        End Get
        Set(ByVal value As String)
            _DbPass = value
        End Set
    End Property
    'JAVA
    Public Property JavaType() As Integer
        Get
            Return _JavaType
        End Get
        Set(ByVal value As Integer)
            _JavaType = value
        End Set
    End Property
    'General
    Public Property FirstRun() As Boolean
        Get
            Return _FirstRun
        End Get
        Set(ByVal value As Boolean)
            _FirstRun = value
        End Set
    End Property
    Public Property CheckForUpdates() As Boolean
        Get
            Return _CheckForUpdates
        End Get
        Set(ByVal value As Boolean)
            _CheckForUpdates = value
        End Set
    End Property
    Public Property Upgradev() As Integer
        Get
            Return _Upgradev
        End Get
        Set(ByVal value As Integer)
            _Upgradev = value
        End Set
    End Property
    Public Property AlwaysAdmin() As Boolean
        Get
            Return _AlwaysAdmin
        End Get
        Set(ByVal value As Boolean)
            _AlwaysAdmin = value
        End Set
    End Property
    Public Property Repo() As String
        Get
            Return _Repo
        End Get
        Set(ByVal value As String)
            _Repo = value
        End Set
    End Property
    Public Property QBMode() As Integer
        Get
            Return _QBMode
        End Get
        Set(ByVal value As Integer)
            _QBMode = value
        End Set
    End Property

    'Plotting And Mining
    Public Property Plots() As String
        Get
            Return _Plots
        End Get
        Set(ByVal value As String)
            _Plots = value
        End Set
    End Property


    Friend Sub New()
        _autoip = True
        _WalletException = True
        _DynPlatform = True
        _useOpenCL = False
        _Cpulimit = 0
        _ListenIf = "127.0.0.1;8125"
        _ListenPeer = "0.0.0.0;8123"
        _ConnectFrom = "127.0.0.1"

        _DbType = 0
        _DbServer = "127.0.0.1:3306"
        _DbName = "burstwallet"
        _DbUser = ""
        _DbPass = ""

        _JavaType = 2 ' QGlobal.AppNames.JavaInstalled

        _FirstRun = True
        _CheckForUpdates = True
        _Upgradev = 11
        _AlwaysAdmin = False
        _Repo = ""
        _QBMode = 1 '0 = AIO 1 = Launcher
    End Sub



    Friend Sub LoadSettings()
        Try
            Dim BaseDir As String = AppDomain.CurrentDomain.BaseDirectory
            If Not BaseDir.EndsWith("\") Then BaseDir &= "\"
            If IO.File.Exists(BaseDir & "\BWL.ini") Then
                Dim lines() As String = IO.File.ReadAllLines(BaseDir & "\BWL.ini")
                For Each line As String In lines 'lets populate
                    Try
                        Dim Cell() As String = Split(line, "=")
                        CallByName(Me, Cell(0), CallType.Set, Cell(1))
                    Catch ex As Exception
                    End Try
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class
