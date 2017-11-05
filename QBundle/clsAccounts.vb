
Imports System.IO
Imports System.Security.Cryptography
Imports System.Xml.Serialization
<Serializable>
Public Class clsAccounts
    Public AccArray As New ArrayList()
    Public Structure Account
        Public AccountName As String
        Public BurstPassword As String
    End Structure

    Public Sub AddAccount(ByVal name As String, ByVal Passphrase As String, ByVal Pin As String)
        Dim Acc As New Account
        Acc.AccountName = name
        Acc.BurstPassword = Enc(Passphrase, Pin)
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
    Public Function GetPassword(ByVal Name As String, ByVal Pin As String) As String
        For Each acc As Account In AccArray
            If acc.AccountName = Name Then
                Return Dec(acc.BurstPassword, Pin)
                Exit For
            End If
        Next
        Return ""
    End Function



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


    Public Function Enc(ByVal input As String, ByVal Keyval As String) As String
        Dim PreKeySeed As String = "9Q&Eag8Lq=+d*Jb6?+E?CNqRY82pFYGJ"
        If Keyval.Length < 32 Then Keyval = PreKeySeed.Substring(0, PreKeySeed.Length - Keyval.Length) & Keyval
        If Keyval.Length > 32 Then Keyval = Keyval.Substring(0, 32)
        Dim sToEncrypt As String = "BURST" & input
        Dim myRijndael As New RijndaelManaged
        myRijndael.Padding = PaddingMode.Zeros
        myRijndael.Mode = CipherMode.CBC
        myRijndael.KeySize = 256
        myRijndael.BlockSize = 256
        Dim encrypted() As Byte
        Dim toEncrypt() As Byte
        Dim IV() As Byte = System.Text.Encoding.UTF8.GetBytes("Wm8@(8#i|6f!JxY@%Hh!Rs]3af)qY=!t")
        Dim key() As Byte = System.Text.Encoding.UTF8.GetBytes(Keyval)
        Dim encryptor As ICryptoTransform = myRijndael.CreateEncryptor(key, IV)
        Dim msEncrypt As New MemoryStream()
        Dim csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
        toEncrypt = System.Text.Encoding.ASCII.GetBytes(sToEncrypt)
        csEncrypt.Write(toEncrypt, 0, toEncrypt.Length)
        csEncrypt.FlushFinalBlock()
        encrypted = msEncrypt.ToArray()
        Return Convert.ToBase64String(encrypted)
    End Function
    Public Function Dec(ByVal input As String, ByVal Keyval As String) As String
        Dim PreKeySeed As String = "9Q&Eag8Lq=+d*Jb6?+E?CNqRY82pFYGJ"
        If Keyval.Length < 32 Then Keyval = PreKeySeed.Substring(0, PreKeySeed.Length - Keyval.Length) & Keyval
        If Keyval.Length > 32 Then Keyval = Keyval.Substring(0, 32)
        Dim myRijndael As New RijndaelManaged
        Dim encrypted() As Byte
        Dim toEncrypt() As Byte = Convert.FromBase64String(input)
        myRijndael.Padding = PaddingMode.Zeros
        myRijndael.Mode = CipherMode.CBC
        myRijndael.KeySize = 256
        myRijndael.BlockSize = 256
        Dim IV() As Byte = System.Text.Encoding.UTF8.GetBytes("Wm8@(8#i|6f!JxY@%Hh!Rs]3af)qY=!t")
        Dim key() As Byte = System.Text.Encoding.UTF8.GetBytes(Keyval)
        Dim DEcryptor As ICryptoTransform = myRijndael.CreateDecryptor(key, IV)
        Dim msDecrypt As New MemoryStream()
        Dim csEncrypt As New CryptoStream(msDecrypt, DEcryptor, CryptoStreamMode.Write)
        csEncrypt.Write(toEncrypt, 0, toEncrypt.Length)
        csEncrypt.FlushFinalBlock()
        encrypted = msDecrypt.ToArray()
        'check to see if it is a burst pass
        Dim RetVal As String = Trim(Replace(System.Text.Encoding.UTF8.GetString(encrypted), vbNullChar, Nothing))
        If RetVal.Substring(0, 5) = "BURST" Then
            RetVal = RetVal.Substring(5)
        Else
            RetVal = ""
        End If
        Return RetVal
    End Function
End Class

