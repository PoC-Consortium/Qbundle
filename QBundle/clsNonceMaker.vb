Imports HashLib
Imports System.Numerics
Public Class clsNonceMaker
    Private HashSize As Integer
    Private ScoopSize As Integer
    Private SeedMaxSize As Integer
    Private NonceSize As Integer
    Private Poc1() As Byte
    Private Poc2() As Byte

    Sub New()
        HashSize = 32 'bytes in length
        ScoopSize = 64 'bytes in length
        SeedMaxSize = 4096 'bytes in length
        NonceSize = 4096 * 64 'bytes in length
        ReDim Poc1(NonceSize - 1)
        ReDim Poc2(NonceSize - 1)
    End Sub


    Public Sub CreateNonce(ByVal AccountID As UInt64, ByVal NonceNr As UInt64)

        Dim bBuffer() As Byte 'bytebuffer
        Dim Nonce(NonceSize + 16) As Byte 'our nonce +intial seed
        Dim FinalHash() As Byte 'Final Hash for xor

        bBuffer = BitConverter.GetBytes(AccountID) 'convert it to bytes
        Array.Reverse(bBuffer) 'reverse the byte order to LSB
        Array.Copy(bBuffer, 0, Nonce, NonceSize, bBuffer.Length) 'place accountid at correct pos

        bBuffer = BitConverter.GetBytes(NonceNr) 'convert it to bytes
        Array.Reverse(bBuffer) 'reverse the byte order to LSB
        Array.Copy(bBuffer, 0, Nonce, NonceSize + 8, bBuffer.Length)  'place noncenr at correct pos


        'initiate the shabal crypto
        Dim Hash As IHash = HashFactory.Crypto.SHA3.CreateShabal256
        Hash.Initialize() 'init algo

        'Lets Create the Nonce
        Dim SeedLength As Integer
        '   Dim Hashr As HashResult
        For t As Integer = NonceSize To 32 Step -HashSize
            SeedLength = NonceSize + 16 - t
            If SeedLength > SeedMaxSize Then SeedLength = SeedMaxSize
            Hash.TransformBytes(Nonce, t, SeedLength)
            bBuffer = Hash.TransformFinal.GetBytes
            'bBuffer = Hashr.GetBytes
            Array.Copy(bBuffer, 0, Nonce, t - HashSize, bBuffer.Length)
        Next

        'Lets Create Final Hash
        Hash.TransformBytes(Nonce, 0, NonceSize + 16)
        FinalHash = Hash.TransformFinal.GetBytes

        'Lets Xor The Nonce
        Dim H As Integer = 1
        For T As Integer = 0 To NonceSize
            Nonce(T) = Nonce(T) Xor FinalHash(T Mod HashSize)
        Next

        'make Poc1 nonce
        Array.Copy(Nonce, 0, Poc1, 0, NonceSize - 1)

        'make Poc2 nonce
        Dim revPos As Integer = NonceSize - HashSize
        For pos As Integer = 0 To NonceSize / 2 Step 64
            Array.Copy(Nonce, pos, Poc2, pos, HashSize) 'Copy first hash in low scoop 
            Array.Copy(Nonce, revPos, Poc2, pos + HashSize, HashSize) 'Copy first hash in low scoop 
            revPos -= ScoopSize
        Next

    End Sub

    Public Function GetPoc1() As Byte()
        Return Poc1
    End Function

    Public Function GetPoc2() As Byte()
        Return Poc2
    End Function

    Public Function getPoCVersion(ByVal scoop0() As Byte) As Integer
        'pass atleast the first scoopt of the nonce to this function to get version.

        If Not IsNothing(scoop0) Then ' make sure we passed info
            If UBound(scoop0) >= HashSize - 1 Then ' make sure we passed enough info
                Dim buffer(HashSize - 1) As Byte

                'testpoc1
                Dim test As Boolean = True
                For t As Integer = 0 To 63
                    If Not Poc1(t) = scoop0(t) Then
                        test = False
                        Exit For
                    End If
                Next
                If test Then Return 1 'if test is not false then this is a poc1

                test = True
                For t As Integer = 0 To 63
                    If Not Poc2(t) = scoop0(t) Then
                        test = False
                        Exit For
                    End If
                Next
                If test Then Return 2 'if test is not false then this is a poc2
            End If
        End If


        Return 0 'marks invalid 

    End Function



End Class
