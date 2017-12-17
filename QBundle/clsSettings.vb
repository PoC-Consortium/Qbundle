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
    Private _AutoStart As Boolean
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
    Private _Debug As Boolean
    Private _UseOnlineWallet As Boolean
    Private _NTPCheck As Boolean
    Private _MinToTray As Boolean
    Private _CoinMarket As Boolean
    'Plotting And Mining
    Private _Plots As String
    'dynamic plotting
    Private _DynPlotEnabled As Boolean
    Private _DynPlotPath As String
    Private _DynPlotAcc As String
    Private _DynPlotSize As Integer
    Private _DynPlotFree As Integer
    Private _DynPlotHide As Boolean

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
    Public Property AutoStart() As String
        Get
            Return _AutoStart
        End Get
        Set(ByVal value As String)
            _AutoStart = value
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
    Public Property DebugMode() As Boolean
        Get
            Return _Debug
        End Get
        Set(ByVal value As Boolean)
            _Debug = value
        End Set
    End Property
    Public Property UseOnlineWallet() As Boolean
        Get
            Return _UseOnlineWallet
        End Get
        Set(ByVal value As Boolean)
            _UseOnlineWallet = value
        End Set
    End Property
    Public Property NTPCheck() As Boolean
        Get
            Return _NTPCheck
        End Get
        Set(ByVal value As Boolean)
            _NTPCheck = value
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
    'dynamic plotting
    Public Property DynPlotEnabled() As Boolean
        Get
            Return _DynPlotEnabled
        End Get
        Set(ByVal value As Boolean)
            _DynPlotEnabled = value
        End Set
    End Property
    Public Property DynPlotPath() As String
        Get
            Return _DynPlotPath
        End Get
        Set(ByVal value As String)
            _DynPlotPath = value
        End Set
    End Property
    Public Property DynPlotAcc() As String
        Get
            Return _DynPlotAcc
        End Get
        Set(ByVal value As String)
            _DynPlotAcc = value
        End Set
    End Property
    Public Property DynPlotSize() As Integer
        Get
            Return _DynPlotSize
        End Get
        Set(ByVal value As Integer)
            _DynPlotSize = value
        End Set
    End Property
    Public Property DynPlotFree() As Integer
        Get
            Return _DynPlotFree
        End Get
        Set(ByVal value As Integer)
            _DynPlotFree = value
        End Set
    End Property
    Public Property DynPlotHide() As Boolean
        Get
            Return _DynPlotHide
        End Get
        Set(ByVal value As Boolean)
            _DynPlotHide = value
        End Set
    End Property
    Public Property MinToTray() As Boolean
        Get
            Return _MinToTray
        End Get
        Set(ByVal value As Boolean)
            _MinToTray = value
        End Set
    End Property
    Public Property GetCoinMarket() As Boolean
        Get
            Return _CoinMarket
        End Get
        Set(ByVal value As Boolean)
            _CoinMarket = value
        End Set
    End Property

    Friend Sub New()
        _autoip = False
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

        _JavaType = QGlobal.AppNames.JavaInstalled

        _FirstRun = True
        _CheckForUpdates = False
        _Upgradev = 16
        _AlwaysAdmin = False
        _Repo = "http://files.getburst.net;http://files2.getburst.net"
        _QBMode = 1 '0 = AIO 1 = Launcher
        _Debug = False
        _UseOnlineWallet = False
        _NTPCheck = False
        _MinToTray = False
        _CoinMarket = False
        _AutoStart = True

        _DynPlotEnabled = False
        _DynPlotPath = ""
        _DynPlotAcc = ""
        _DynPlotSize = 10
        _DynPlotFree = 10
        _DynPlotHide = True


    End Sub

    Friend Sub LoadSettings()
        Try
            If IO.File.Exists(QGlobal.AppDir & "\BWL.ini") Then
                Dim lines() As String = IO.File.ReadAllLines(QGlobal.AppDir & "\BWL.ini")
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
        Try
            If _Debug = True Then Generic.DebugMe = True
        Catch ex As Exception

        End Try
    End Sub
    Friend Sub SaveSettings()
        Try
            Dim Sdata As String = ""
            Dim RP() As Reflection.PropertyInfo = Me.GetType().GetProperties()
            For Each prop In RP
                Sdata &= prop.Name & "=" & prop.GetValue(Me) & vbCrLf
            Next
            IO.File.WriteAllText(QGlobal.SettingsDir & "\BWL.ini", Sdata)
        Catch ex As Exception

        End Try
    End Sub
End Class
