
Imports System.IO
Imports System.Numerics
Imports System.Security.Cryptography
Imports System.Xml.Serialization
<Serializable>
Public Class clsAccounts
    Public AccArray As New ArrayList()
    Public Structure Account
        Public AccountName As String
        Public BurstPassword As String
        Public PublicKey As String
        Public AccountID As String
        Public RSAddress As String
    End Structure

    Public Sub AddAccount(ByVal name As String, ByVal Passphrase As String, ByVal Pin As String)

        Dim AccountID As String
        Dim AccountAddress As String
        Dim KeySeed As String
        Dim PrivateKey As Byte()
        Dim PublicKey As Byte()
        Dim PublicKeyHash As Byte()

        KeySeed = Passphrase
        Dim cSHA256 As SHA256 = SHA256Managed.Create()

        'create private key
        PrivateKey = cSHA256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(KeySeed))

        'create public key
        PublicKey = Curve25519.GetPublicKey(PrivateKey)

        'create public key hash
        PublicKeyHash = cSHA256.ComputeHash(PublicKey)

        'create numeric account id
        Dim b = New Byte() {PublicKeyHash(0), PublicKeyHash(1), PublicKeyHash(2), PublicKeyHash(3), PublicKeyHash(4), PublicKeyHash(5), PublicKeyHash(6), PublicKeyHash(7)}
        If (b(b.Length - 1) And &H80) <> 0 Then
            Array.Resize(Of Byte)(b, b.Length + 1)
        End If
        Dim Bint As New BigInteger(b)
        'Account ID
        AccountID = Bint.ToString
        'Create RS-Address
        AccountAddress = ReedSolomon.encode(Bint.ToString)


        Dim Acc As New Account
        Acc.AccountName = name
        Acc.PublicKey = BytesToHexString(PublicKey)
        Acc.AccountID = AccountID
        Acc.RSAddress = "BURST-" & AccountAddress
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
    Private Function BytesToHexString(ByVal bArray As Byte()) As String
        Dim HexString As String = ""
        Dim buffer As String = ""
        For Each b As Byte In bArray
            buffer = Conversion.Hex(b)
            If buffer.Length = 1 Then buffer = "0" & buffer
            HexString &= buffer
        Next
        Return HexString
    End Function

    Public Function GetAccountID(ByVal Name As String) As String
        For Each acc As Account In AccArray
            If acc.AccountName = Name Then
                Return acc.AccountID
                Exit For
            End If
        Next
        Return ""
    End Function
    Public Function GetAccountRS(ByVal Name As String) As String
        For Each acc As Account In AccArray
            If acc.AccountName = Name Then
                Return acc.RSAddress
                Exit For
            End If
        Next
        Return ""
    End Function
    Public Function GetPublicKey(ByVal Name As String) As String
        For Each acc As Account In AccArray
            If acc.AccountName = Name Then
                Return acc.PublicKey
                Exit For
            End If
        Next
        Return ""
    End Function

    Public Function GetRSFromPassPhrase(ByVal Passphrase As String) As String
        Dim AccountID As String
        Dim AccountAddress As String
        Dim KeySeed As String
        Dim PrivateKey As Byte()
        Dim PublicKey As Byte()
        Dim PublicKeyHash As Byte()

        KeySeed = Passphrase
        Dim cSHA256 As SHA256 = SHA256Managed.Create()
        PrivateKey = cSHA256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(KeySeed))
        PublicKey = Curve25519.GetPublicKey(PrivateKey)
        PublicKeyHash = cSHA256.ComputeHash(PublicKey)
        Dim b = New Byte() {PublicKeyHash(0), PublicKeyHash(1), PublicKeyHash(2), PublicKeyHash(3), PublicKeyHash(4), PublicKeyHash(5), PublicKeyHash(6), PublicKeyHash(7)}
        If (b(b.Length - 1) And &H80) <> 0 Then
            Array.Resize(Of Byte)(b, b.Length + 1)
        End If
        Dim Bint As New BigInteger(b)
        AccountID = Bint.ToString
        AccountAddress = ReedSolomon.encode(Bint.ToString)

        Return AccountAddress
    End Function
    Public Function GetAccountIDFromPassPhrase(ByVal Passphrase As String) As String
        Dim AccountID As String
        Dim KeySeed As String
        Dim PrivateKey As Byte()
        Dim PublicKey As Byte()
        Dim PublicKeyHash As Byte()

        KeySeed = Passphrase
        Dim cSHA256 As SHA256 = SHA256Managed.Create()
        PrivateKey = cSHA256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(KeySeed))
        PublicKey = Curve25519.GetPublicKey(PrivateKey)
        PublicKeyHash = cSHA256.ComputeHash(PublicKey)
        Dim b = New Byte() {PublicKeyHash(0), PublicKeyHash(1), PublicKeyHash(2), PublicKeyHash(3), PublicKeyHash(4), PublicKeyHash(5), PublicKeyHash(6), PublicKeyHash(7)}
        If (b(b.Length - 1) And &H80) <> 0 Then
            Array.Resize(Of Byte)(b, b.Length + 1)
        End If
        Dim Bint As New BigInteger(b)
        AccountID = Bint.ToString
        Return AccountID
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
    Public Function ConvertIdToRS(ByVal id As String) As String
        Return ReedSolomon.encode(id)
    End Function
    Public Function ConvertRSToId(ByVal Address As String) As String
        If UCase(Address).StartsWith("BURST-") Then
            Address = Address.Substring(6) 'Remove BURST- before sending it for convertion.
        End If
        Return ReedSolomon.decode(Address)
    End Function
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

