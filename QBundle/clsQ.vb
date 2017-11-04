Friend Class Q
    Public Enum DbType As Integer
        H2 = 0
        FireBird = 1
        pMariaDB = 2
        MariaDB = 3
    End Enum
    Public Enum AppNames As Integer
        Launcher = 0
        BRS = 1
        JavaInstalled = 2
        JavaPortable = 3
        MariaPortable = 4 'Maria DB Portable
        Import = 5 'Export / import db
        Export = 6
        DownloadFile = 7 'Download whatever
    End Enum

    Public Enums As clsEnums

    Sub New()
        Enums = New clsEnums
    End Sub



End Class

Public Class clsEnums

End Class