Imports System.IO
Imports System.Xml.Serialization

Friend Class clsQ

    Public WithEvents ProcHandler As clsProcessHandler
    Public WithEvents App As clsApp
    Public settings As clsSettings
    Public Accounts As clsAccounts
    Public Sub New()
        QGlobal.BaseDir = AppDomain.CurrentDomain.BaseDirectory
        If Not QGlobal.BaseDir.EndsWith("\") Then QGlobal.BaseDir &= "\"

        Me.App = New clsApp
        Me.ProcHandler = New clsProcessHandler

        Me.settings = New clsSettings
        Me.settings.LoadSettings()
        Me.Accounts = New clsAccounts
        Me.Accounts.LoadAccounts()

    End Sub


End Class
