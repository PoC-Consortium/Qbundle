Imports System.IO
Imports System.Xml.Serialization

Friend Class clsQ

    Public WithEvents ProcHandler As clsProcessHandler
    Public WithEvents App As clsApp
    Public settings As clsSettings
    Public Accounts As clsAccounts
    Public Service As clsServiceHandler
    Public AppManager As clsAppManager
    Public Sub New()
        QGlobal.Init()
        Me.App = New clsApp
        Me.AppManager = New clsAppManager
        Me.ProcHandler = New clsProcessHandler
        Me.settings = New clsSettings
        Me.settings.LoadSettings()
        Me.Accounts = New clsAccounts
        Me.Accounts.LoadAccounts()
        Me.Service = New clsServiceHandler



    End Sub


End Class
