Imports HashLib
Imports System.Numerics
Public Class clsNonceMaker
    Private HashSize As Integer
    Private ScoopSize As Integer
    Private SeedMaxSize As Integer
    Private NonceSize As Integer

    Sub New()
        HashSize = 32 'bytes in length
        ScoopSize = 64 'bytes in length
        SeedMaxSize = 4096 'bytes in length
        NonceSize = 4096 * 64 'bytes in length
    End Sub


    Public Function CreateNonce(ByVal AccountID As UInt64, ByVal NonceNr As UInt64) As Byte()

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
        Hash.Initialize()

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
        Return Nonce

    End Function





End Class
