
Imports System.IO
Imports System.Xml.Serialization
<Serializable>
Public Class clsAccounts
    Public AccArray As New ArrayList()
    Public Structure Account
        Public AccountName As String
        Public BurstPassword As String
        Public PinCode As String
    End Structure

    Public Sub AddAccount(ByVal name As String, ByVal Passphrase As String, ByVal Pin As String)
        Dim Acc As New Account
        Acc.AccountName = name
        Acc.BurstPassword = Passphrase
        Acc.PinCode = Pin
        AccArray.Add(Acc)
    End Sub
    Public Sub DeleteAccount(ByVal Name As String)
        For Each acc As Account In AccArray
            If acc.AccountName = Name Then
                AccArray.Remove(acc)
                Exit For
            End If
        Next
        SaveAccounts()
    End Sub

    Public Sub SaveAccounts()
        Dim x As New XmlSerializer(GetType(ArrayList), New Type() {GetType(Account)})
        Dim writer As TextWriter = New StreamWriter(QGlobal.BaseDir & "Acconts.xml")
        x.Serialize(writer, AccArray)
        writer.Close()
        writer.Dispose()
        x = Nothing
    End Sub

    Public Sub LoadAccounts()
        Try
            Dim x As New XmlSerializer(GetType(ArrayList), New Type() {GetType(Account)})
            Dim Reader As TextReader = New StreamReader(QGlobal.BaseDir & "Acconts.xml")
            AccArray = DirectCast(x.Deserialize(Reader), ArrayList)
            Reader.Close()
            Reader.Dispose()
            x = Nothing
        Catch ex As Exception

        End Try
    End Sub



End Class

