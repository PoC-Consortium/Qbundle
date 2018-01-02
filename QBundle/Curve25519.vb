' Ported from C# to VB by Quibus, 19/08/2017
' Requires C# Behaviour. Integer overflow detection must be disabled 

' Ported parts from Java to C# And refactored by Hans Wolff, 17/09/2013 

' Ported from C to Java by Dmitry Skiba [sahn0], 23/02/08.
' Original: http : //code.google.com/p/curve25519-java/
'

' Generic 64-bit integer implementation of Curve25519 ECDH
' Written by Matthijs van Duin, 200608242056
' Public domain.
'
' Based on work by Daniel J Bernstein, http://cr.yp.to/ecdh.html
'


Imports System
Imports System.Security.Cryptography


Public Class Curve25519
        Public Const KeySize As Integer = 32
        Shared ReadOnly Order() As Byte = {237, 211, 245, 92, 26, 99, 18, 88, 214, 156, 247, 162, 222, 249, 222, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16}



        Public Shared Sub ClampPrivateKeyInline(key As Byte())
            If key Is Nothing Then Throw New ArgumentNullException("key")
            If key.Length <> 32 Then Throw New ArgumentException([String].Format("key must be 32 bytes long (but was {0} bytes long)", key.Length))
        key(31) = key(31) And CByte(&H7F)
        key(31) = key(31) Or CByte(&H40)
        key(0) = key(0) And CByte(&HF8)
    End Sub

        Public Shared Function ClampPrivateKey(rawKey As Byte()) As Byte()
            If rawKey Is Nothing Then Throw New ArgumentNullException("rawKey")
            If rawKey.Length <> 32 Then Throw New ArgumentException([String].Format("rawKey must be 32 bytes long (but was {0} bytes long)", rawKey.Length), "rawKey")
            Dim res = New Byte(31) {}
            Array.Copy(rawKey, res, 32)
        res(31) = res(31) And CByte(&H7F)
        res(31) = res(31) Or CByte(&H40)
        res(0) = res(0) And CByte(&HF8)
        Return res
        End Function

        Public Shared Function CreateRandomPrivateKey() As Byte()
            Dim privateKey = New Byte(31) {}
            RNGCryptoServiceProvider.Create().GetBytes(privateKey)
            ClampPrivateKeyInline(privateKey)
            Return privateKey
        End Function


        Public Shared Sub KeyGenInline(publicKey As Byte(), signingKey As Byte(), privateKey As Byte())
            If publicKey Is Nothing Then Throw New ArgumentNullException("publicKey")
            If publicKey.Length <> 32 Then Throw New ArgumentException([String].Format("publicKey must be 32 bytes long (but was {0} bytes long)", publicKey.Length), "publicKey")
            If signingKey Is Nothing Then Throw New ArgumentNullException("signingKey")
            If signingKey.Length <> 32 Then Throw New ArgumentException([String].Format("signingKey must be 32 bytes long (but was {0} bytes long)", signingKey.Length), "signingKey")
            If privateKey Is Nothing Then Throw New ArgumentNullException("privateKey")
            If privateKey.Length <> 32 Then Throw New ArgumentException([String].Format("privateKey must be 32 bytes long (but was {0} bytes long)", privateKey.Length), "privateKey")

            RNGCryptoServiceProvider.Create().GetBytes(privateKey)
            ClampPrivateKeyInline(privateKey)
            Core(publicKey, signingKey, privateKey, Nothing)

        End Sub

        '        Public Shared Function GetPublicKey(privateKey As Byte()) As Byte()
        '        Dim publicKey = New Byte(31) {}
        '            Core(publicKey, Nothing, privateKey, Nothing)
        '        Return publicKey
        '        End Function

        Public Shared Function GetPublicKey(privateKey As Byte()) As Byte()
            Dim publicKey = New Byte(31) {}
            ClampPrivateKeyInline(privateKey)
            Core(publicKey, Nothing, privateKey, Nothing)
            Return publicKey
        End Function

    Public Shared Sub GetSignKeys(PublicKey As Byte(), s As Byte(), PrivateKey As Byte())
        ClampPrivateKeyInline(PrivateKey)
        Core(PublicKey, s, PrivateKey, Nothing)
    End Sub

    Public Shared Function GetSigningKey(privateKey As Byte()) As Byte()
        Dim signingKey = New Byte(31) {}
        Dim publicKey = New Byte(31) {}
        Core(publicKey, signingKey, privateKey, Nothing)
        Return signingKey
    End Function

    Public Shared Function GetSharedSecret(privateKey As Byte(), peerPublicKey As Byte()) As Byte()
            Dim sharedSecret = New Byte(31) {}
            Core(sharedSecret, Nothing, privateKey, peerPublicKey)
            Return sharedSecret
        End Function


        Private NotInheritable Class Long10
            Public Sub New()
            End Sub
            Public Sub New(n0__1 As Long, n1__2 As Long, n2__3 As Long, n3__4 As Long, n4__5 As Long, n5__6 As Long, n6__7 As Long, n7__8 As Long, n8__9 As Long, n9__10 As Long)
                N0 = n0__1
                N1 = n1__2
                N2 = n2__3
                N3 = n3__4
                N4 = n4__5
                N5 = n5__6
                N6 = n6__7
                N7 = n7__8
                N8 = n8__9
                N9 = n9__10
            End Sub
            Public N0 As Long, N1 As Long, N2 As Long, N3 As Long, N4 As Long, N5 As Long, N6 As Long, N7 As Long, N8 As Long, N9 As Long
        End Class


        Private Shared Sub Copy32(source As Byte(), destination As Byte())
            Array.Copy(source, 0, destination, 0, 32)
        End Sub

        Private Shared Function MultiplyArraySmall(p As Byte(), q As Byte(), m As Integer, x As Byte(), n As Integer, z As Integer) As Integer
            Dim v As Integer = 0
            For i As Integer = 0 To n - 1
                v += (q(i + m) And &HFF) + z * (x(i) And &HFF)
                p(i + m) = CByte(v)
                v >>= 8
            Next
            Return v
        End Function

        Private Shared Sub MultiplyArray32(p As Byte(), x As Byte(), y As Byte(), t As Integer, z As Integer)
            Const n As Integer = 31
            Dim w As Integer = 0
            Dim i As Integer = 0
            While i < t
                Dim zy As Integer = z * (y(i) And &HFF)
                w += MultiplyArraySmall(p, p, i, x, n, zy) + (p(i + n) And &HFF) + zy * (x(n) And &HFF)
                p(i + n) = CByte(w)
                w >>= 8
                i += 1
            End While
            p(i + n) = CByte(w + (p(i + n) And &HFF))
        End Sub

        Private Shared Sub DivMod(q As Byte(), r As Byte(), n As Integer, d As Byte(), t As Integer)
            Dim rn As Integer = 0
            Dim dt As Integer = ((d(t - 1) And &HFF) << 8)
            If t > 1 Then
                dt = dt Or (d(t - 2) And &HFF)
            End If
        While System.Math.Max(System.Threading.Interlocked.Decrement(n), n + 1) >= t
            Dim z As Integer = (rn << 16) Or ((r(n) And &HFF) << 8)
                If n > 0 Then
                    z = z Or (r(n - 1) And &HFF)
                End If
                z /= dt
                rn += MultiplyArraySmall(r, r, n - t + 1, d, t, -z)
                q(n - t + 1) = CByte((z + rn) And &HFF)
                ' rn is 0 or -1 (underflow) 
                MultiplyArraySmall(r, r, n - t + 1, d, t, -rn)
                rn = (r(n) And &HFF)
                r(n) = 0
            End While
            r(t - 1) = CByte(rn)
        End Sub


        Private Shared Function GetNumSize(num As Byte(), maxSize As Integer) As Integer
            Dim i As Integer = maxSize
            While i >= 0
                If num(i) = 0 Then
                    Return i + 1
                End If
                i += 1
            End While
            Return 0
        End Function

    Private Shared Function Egcd32(x As Byte(), y As Byte(), a As Byte(), b As Byte()) As Byte()
        Dim bn As Integer = 32
        Dim i As Integer
        For i = 0 To 31
            x(i) = y(i) = 0
        Next
        x(0) = 1
        Dim an As Integer = GetNumSize(a, 32)
        If an = 0 Then
            Return y
        End If
        ' division by zero 
        Dim temp = New Byte(31) {}
        While True
            Dim qn As Integer = bn - an + 1
            DivMod(temp, b, bn, a, an)
            bn = GetNumSize(b, bn)
            If bn = 0 Then
                Return x
            End If
            MultiplyArray32(y, x, temp, qn, -1)

            qn = an - bn + 1
            DivMod(temp, a, an, b, bn)
            an = GetNumSize(a, an)
            If an = 0 Then
                Return y
            End If
            MultiplyArray32(x, y, temp, qn, -1)
        End While
        'fix for empty return
        Return New Byte() {}
    End Function

    Private Const P25 As Integer = 33554431
        Private Const P26 As Integer = 67108863

        Private Shared Sub Unpack(x As Long10, m As Byte())
            x.N0 = ((m(0) And &HFF)) Or ((m(1) And &HFF)) << 8 Or (m(2) And &HFF) << 16 Or ((m(3) And &HFF) And 3) << 24
            x.N1 = ((m(3) And &HFF) And Not 3) >> 2 Or (m(4) And &HFF) << 6 Or (m(5) And &HFF) << 14 Or ((m(6) And &HFF) And 7) << 22
            x.N2 = ((m(6) And &HFF) And Not 7) >> 3 Or (m(7) And &HFF) << 5 Or (m(8) And &HFF) << 13 Or ((m(9) And &HFF) And 31) << 21
            x.N3 = ((m(9) And &HFF) And Not 31) >> 5 Or (m(10) And &HFF) << 3 Or (m(11) And &HFF) << 11 Or ((m(12) And &HFF) And 63) << 19
            x.N4 = ((m(12) And &HFF) And Not 63) >> 6 Or (m(13) And &HFF) << 2 Or (m(14) And &HFF) << 10 Or (m(15) And &HFF) << 18
            x.N5 = (m(16) And &HFF) Or (m(17) And &HFF) << 8 Or (m(18) And &HFF) << 16 Or ((m(19) And &HFF) And 1) << 24
            x.N6 = ((m(19) And &HFF) And Not 1) >> 1 Or (m(20) And &HFF) << 7 Or (m(21) And &HFF) << 15 Or ((m(22) And &HFF) And 7) << 23
            x.N7 = ((m(22) And &HFF) And Not 7) >> 3 Or (m(23) And &HFF) << 5 Or (m(24) And &HFF) << 13 Or ((m(25) And &HFF) And 15) << 21
            x.N8 = ((m(25) And &HFF) And Not 15) >> 4 Or (m(26) And &HFF) << 4 Or (m(27) And &HFF) << 12 Or ((m(28) And &HFF) And 63) << 20
            x.N9 = ((m(28) And &HFF) And Not 63) >> 6 Or (m(29) And &HFF) << 2 Or (m(30) And &HFF) << 10 Or (m(31) And &HFF) << 18
        End Sub


        Private Shared Function IsOverflow(x As Long10) As Boolean
            Return (((x.N0 > P26 - 19)) And ((x.N1 And x.N3 And x.N5 And x.N7 And x.N9) = P25) And ((x.N2 And x.N4 And x.N6 And x.N8) = P26)) OrElse (x.N9 > P25)
        End Function

        Private Shared Sub Pack(x As Long10, m As Byte())
            Dim ld As Integer = (If(IsOverflow(x), 1, 0)) - (If((x.N9 < 0), 1, 0))
            Dim ud As Integer = ld * -(P25 + 1)
            ld *= 19
            Dim t As Long = ld + x.N0 + (x.N1 << 26)
            m(0) = CByte(t)
            m(1) = CByte(t >> 8)
            m(2) = CByte(t >> 16)
            m(3) = CByte(t >> 24)
            t = (t >> 32) + (x.N2 << 19)
            m(4) = CByte(t)
            m(5) = CByte(t >> 8)
            m(6) = CByte(t >> 16)
            m(7) = CByte(t >> 24)
            t = (t >> 32) + (x.N3 << 13)
            m(8) = CByte(t)
            m(9) = CByte(t >> 8)
            m(10) = CByte(t >> 16)
            m(11) = CByte(t >> 24)
            t = (t >> 32) + (x.N4 << 6)
            m(12) = CByte(t)
            m(13) = CByte(t >> 8)
            m(14) = CByte(t >> 16)
            m(15) = CByte(t >> 24)
            t = (t >> 32) + x.N5 + (x.N6 << 25)
            m(16) = CByte(t)
            m(17) = CByte(t >> 8)
            m(18) = CByte(t >> 16)
            m(19) = CByte(t >> 24)
            t = (t >> 32) + (x.N7 << 19)
            m(20) = CByte(t)
            m(21) = CByte(t >> 8)
            m(22) = CByte(t >> 16)
            m(23) = CByte(t >> 24)
            t = (t >> 32) + (x.N8 << 12)
            m(24) = CByte(t)
            m(25) = CByte(t >> 8)
            m(26) = CByte(t >> 16)
            m(27) = CByte(t >> 24)
            t = (t >> 32) + ((x.N9 + ud) << 6)
            m(28) = CByte(t)
            m(29) = CByte(t >> 8)
            m(30) = CByte(t >> 16)
            m(31) = CByte(t >> 24)
        End Sub


        Private Shared Sub Copy(numOut As Long10, numIn As Long10)
            numOut.N0 = numIn.N0
            numOut.N1 = numIn.N1
            numOut.N2 = numIn.N2
            numOut.N3 = numIn.N3
            numOut.N4 = numIn.N4
            numOut.N5 = numIn.N5
            numOut.N6 = numIn.N6
            numOut.N7 = numIn.N7
            numOut.N8 = numIn.N8
            numOut.N9 = numIn.N9
        End Sub

        Private Shared Sub [Set](numOut As Long10, numIn As Integer)
            numOut.N0 = numIn
            numOut.N1 = 0
            numOut.N2 = 0
            numOut.N3 = 0
            numOut.N4 = 0
            numOut.N5 = 0
            numOut.N6 = 0
            numOut.N7 = 0
            numOut.N8 = 0
            numOut.N9 = 0
        End Sub

        Private Shared Sub Add(xy As Long10, x As Long10, y As Long10)
            xy.N0 = x.N0 + y.N0
            xy.N1 = x.N1 + y.N1
            xy.N2 = x.N2 + y.N2
            xy.N3 = x.N3 + y.N3
            xy.N4 = x.N4 + y.N4
            xy.N5 = x.N5 + y.N5
            xy.N6 = x.N6 + y.N6
            xy.N7 = x.N7 + y.N7
            xy.N8 = x.N8 + y.N8
            xy.N9 = x.N9 + y.N9
        End Sub

        Private Shared Sub [Sub](xy As Long10, x As Long10, y As Long10)
            xy.N0 = x.N0 - y.N0
            xy.N1 = x.N1 - y.N1
            xy.N2 = x.N2 - y.N2
            xy.N3 = x.N3 - y.N3
            xy.N4 = x.N4 - y.N4
            xy.N5 = x.N5 - y.N5
            xy.N6 = x.N6 - y.N6
            xy.N7 = x.N7 - y.N7
            xy.N8 = x.N8 - y.N8
            xy.N9 = x.N9 - y.N9
        End Sub

        Private Shared Sub MulSmall(xy As Long10, x As Long10, y As Long)
            Dim temp As Long = (x.N8 * y)
            xy.N8 = (temp And ((1 << 26) - 1))
            temp = (temp >> 26) + (x.N9 * y)
            xy.N9 = (temp And ((1 << 25) - 1))
            temp = 19 * (temp >> 25) + (x.N0 * y)
            xy.N0 = (temp And ((1 << 26) - 1))
            temp = (temp >> 26) + (x.N1 * y)
            xy.N1 = (temp And ((1 << 25) - 1))
            temp = (temp >> 25) + (x.N2 * y)
            xy.N2 = (temp And ((1 << 26) - 1))
            temp = (temp >> 26) + (x.N3 * y)
            xy.N3 = (temp And ((1 << 25) - 1))
            temp = (temp >> 25) + (x.N4 * y)
            xy.N4 = (temp And ((1 << 26) - 1))
            temp = (temp >> 26) + (x.N5 * y)
            xy.N5 = (temp And ((1 << 25) - 1))
            temp = (temp >> 25) + (x.N6 * y)
            xy.N6 = (temp And ((1 << 26) - 1))
            temp = (temp >> 26) + (x.N7 * y)
            xy.N7 = (temp And ((1 << 25) - 1))
            temp = (temp >> 25) + xy.N8
            xy.N8 = (temp And ((1 << 26) - 1))
            xy.N9 += (temp >> 26)
        End Sub


        Private Shared Sub Multiply(xy As Long10, x As Long10, y As Long10)

            Dim x0 As Long = x.N0, x1 As Long = x.N1, x2 As Long = x.N2, x3 As Long = x.N3, x4 As Long = x.N4, x5 As Long = x.N5,
                x6 As Long = x.N6, x7 As Long = x.N7, x8 As Long = x.N8, x9 As Long = x.N9
            Dim y0 As Long = y.N0, y1 As Long = y.N1, y2 As Long = y.N2, y3 As Long = y.N3, y4 As Long = y.N4, y5 As Long = y.N5,
                y6 As Long = y.N6, y7 As Long = y.N7, y8 As Long = y.N8, y9 As Long = y.N9
            Dim t As Long = (x0 * y8) + (x2 * y6) + (x4 * y4) + (x6 * y2) + (x8 * y0) + 2 * ((x1 * y7) + (x3 * y5) + (x5 * y3) + (x7 * y1)) + 38 * (x9 * y9)
            xy.N8 = (t And ((1 << 26) - 1))
            t = (t >> 26) + (x0 * y9) + (x1 * y8) + (x2 * y7) + (x3 * y6) + (x4 * y5) + (x5 * y4) + (x6 * y3) + (x7 * y2) + (x8 * y1) + (x9 * y0)
            xy.N9 = (t And ((1 << 25) - 1))
            t = (x0 * y0) + 19 * ((t >> 25) + (x2 * y8) + (x4 * y6) + (x6 * y4) + (x8 * y2)) + 38 * ((x1 * y9) + (x3 * y7) + (x5 * y5) + (x7 * y3) + (x9 * y1))
            xy.N0 = (t And ((1 << 26) - 1))
            t = (t >> 26) + (x0 * y1) + (x1 * y0) + 19 * ((x2 * y9) + (x3 * y8) + (x4 * y7) + (x5 * y6) + (x6 * y5) + (x7 * y4) + (x8 * y3) + (x9 * y2))
            xy.N1 = (t And ((1 << 25) - 1))
            t = (t >> 25) + (x0 * y2) + (x2 * y0) + 19 * ((x4 * y8) + (x6 * y6) + (x8 * y4)) + 2 * (x1 * y1) + 38 * ((x3 * y9) + (x5 * y7) + (x7 * y5) + (x9 * y3))
            xy.N2 = (t And ((1 << 26) - 1))
            t = (t >> 26) + (x0 * y3) + (x1 * y2) + (x2 * y1) + (x3 * y0) + 19 * ((x4 * y9) + (x5 * y8) + (x6 * y7) + (x7 * y6) + (x8 * y5) + (x9 * y4))
            xy.N3 = (t And ((1 << 25) - 1))
            t = (t >> 25) + (x0 * y4) + (x2 * y2) + (x4 * y0) + 19 * ((x6 * y8) + (x8 * y6)) + 2 * ((x1 * y3) + (x3 * y1)) + 38 * ((x5 * y9) + (x7 * y7) + (x9 * y5))
            xy.N4 = (t And ((1 << 26) - 1))
            t = (t >> 26) + (x0 * y5) + (x1 * y4) + (x2 * y3) + (x3 * y2) + (x4 * y1) + (x5 * y0) + 19 * ((x6 * y9) + (x7 * y8) + (x8 * y7) + (x9 * y6))
            xy.N5 = (t And ((1 << 25) - 1))
            t = (t >> 25) + (x0 * y6) + (x2 * y4) + (x4 * y2) + (x6 * y0) + 19 * (x8 * y8) + 2 * ((x1 * y5) + (x3 * y3) + (x5 * y1)) + 38 * ((x7 * y9) + (x9 * y7))
            xy.N6 = (t And ((1 << 26) - 1))
            t = (t >> 26) + (x0 * y7) + (x1 * y6) + (x2 * y5) + (x3 * y4) + (x4 * y3) + (x5 * y2) + (x6 * y1) + (x7 * y0) + 19 * ((x8 * y9) + (x9 * y8))
            xy.N7 = (t And ((1 << 25) - 1))
            t = (t >> 25) + xy.N8
            xy.N8 = (t And ((1 << 26) - 1))
            xy.N9 += (t >> 26)
        End Sub

        Private Shared Sub Square(xsqr As Long10, x As Long10)
            Dim x0 As Long = x.N0, x1 As Long = x.N1, x2 As Long = x.N2, x3 As Long = x.N3, x4 As Long = x.N4, x5 As Long = x.N5,
                x6 As Long = x.N6, x7 As Long = x.N7, x8 As Long = x.N8, x9 As Long = x.N9

            Dim t As Long = (x4 * x4) + 2 * ((x0 * x8) + (x2 * x6)) + 38 * (x9 * x9) + 4 * ((x1 * x7) + (x3 * x5))

            xsqr.N8 = (t And ((1 << 26) - 1))
            t = (t >> 26) + 2 * ((x0 * x9) + (x1 * x8) + (x2 * x7) + (x3 * x6) + (x4 * x5))
            xsqr.N9 = (t And ((1 << 25) - 1))
            t = 19 * (t >> 25) + (x0 * x0) + 38 * ((x2 * x8) + (x4 * x6) + (x5 * x5)) + 76 * ((x1 * x9) + (x3 * x7))
            xsqr.N0 = (t And ((1 << 26) - 1))
            t = (t >> 26) + 2 * (x0 * x1) + 38 * ((x2 * x9) + (x3 * x8) + (x4 * x7) + (x5 * x6))
            xsqr.N1 = (t And ((1 << 25) - 1))
            t = (t >> 25) + 19 * (x6 * x6) + 2 * ((x0 * x2) + (x1 * x1)) + 38 * (x4 * x8) + 76 * ((x3 * x9) + (x5 * x7))
            xsqr.N2 = (t And ((1 << 26) - 1))
            t = (t >> 26) + 2 * ((x0 * x3) + (x1 * x2)) + 38 * ((x4 * x9) + (x5 * x8) + (x6 * x7))
            xsqr.N3 = (t And ((1 << 25) - 1))
            t = (t >> 25) + (x2 * x2) + 2 * (x0 * x4) + 38 * ((x6 * x8) + (x7 * x7)) + 4 * (x1 * x3) + 76 * (x5 * x9)
            xsqr.N4 = (t And ((1 << 26) - 1))
            t = (t >> 26) + 2 * ((x0 * x5) + (x1 * x4) + (x2 * x3)) + 38 * ((x6 * x9) + (x7 * x8))
            xsqr.N5 = (t And ((1 << 25) - 1))
            t = (t >> 25) + 19 * (x8 * x8) + 2 * ((x0 * x6) + (x2 * x4) + (x3 * x3)) + 4 * (x1 * x5) + 76 * (x7 * x9)
            xsqr.N6 = (t And ((1 << 26) - 1))
            t = (t >> 26) + 2 * ((x0 * x7) + (x1 * x6) + (x2 * x5) + (x3 * x4)) + 38 * (x8 * x9)
            xsqr.N7 = (t And ((1 << 25) - 1))
            t = (t >> 25) + xsqr.N8
            xsqr.N8 = (t And ((1 << 26) - 1))
            xsqr.N9 += (t >> 26)
        End Sub


        Private Shared Sub Reciprocal(y As Long10, x As Long10, sqrtAssist As Boolean)
            Dim t0 As New Long10(), t1 As New Long10(), t2 As New Long10(), t3 As New Long10(), t4 As New Long10()
            Dim i As Integer

            Square(t1, x)

            Square(t2, t1)

            Square(t0, t2)

            Multiply(t2, t0, x)

            Multiply(t0, t2, t1)

            Square(t1, t0)

            Multiply(t3, t1, t2)
            Square(t1, t3)
            Square(t2, t1)
            Square(t1, t2)
            Square(t2, t1)
            Square(t1, t2)
            Multiply(t2, t1, t3)
            Square(t1, t2)
            Square(t3, t1)
            For i = 1 To 4
                Square(t1, t3)
                Square(t3, t1)
            Next
            Multiply(t1, t3, t2)
            Square(t3, t1)
            Square(t4, t3)
            For i = 1 To 9
                Square(t3, t4)
                Square(t4, t3)
            Next
            Multiply(t3, t4, t1)
            For i = 0 To 4
                Square(t1, t3)
                Square(t3, t1)
            Next
            Multiply(t1, t3, t2)
            Square(t2, t1)
            Square(t3, t2)
            For i = 1 To 24
                Square(t2, t3)
                Square(t3, t2)
            Next
            Multiply(t2, t3, t1)
            Square(t3, t2)
            Square(t4, t3)
            For i = 1 To 49
                Square(t3, t4)
                Square(t4, t3)
            Next
            Multiply(t3, t4, t2)

            For i = 0 To 24
                Square(t4, t3)
                Square(t3, t4)
            Next
            Multiply(t2, t3, t1)
            Square(t1, t2)
            Square(t2, t1)
            If sqrtAssist Then
                Multiply(y, x, t2)
            Else
                Square(t1, t2)
                Square(t2, t1)
                Square(t1, t2)
                Multiply(y, t1, t0)
            End If
        End Sub


        Private Shared Function IsNegative(x As Long10) As Integer
            Return CInt((If((IsOverflow(x) Or (x.N9 < 0)), 1, 0)) Xor (x.N0 And 1))
        End Function

        Private Shared Sub MontyPrepare(t1 As Long10, t2 As Long10, ax As Long10, az As Long10)
            Add(t1, ax, az)
            [Sub](t2, ax, az)
        End Sub

        Private Shared Sub MontyAdd(t1 As Long10, t2 As Long10, t3 As Long10, t4 As Long10, ax As Long10, az As Long10, dx As Long10)
            Multiply(ax, t2, t3)
            Multiply(az, t1, t4)
            Add(t1, ax, az)
            [Sub](t2, ax, az)
            Square(ax, t1)
            Square(t1, t2)
            Multiply(az, t1, dx)
        End Sub


        Private Shared Sub MontyDouble(t1 As Long10, t2 As Long10, t3 As Long10, t4 As Long10, bx As Long10, bz As Long10)
            Square(t1, t3)
            Square(t2, t4)
            Multiply(bx, t1, t2)
            [Sub](t2, t1, t2)
            MulSmall(bz, t2, 121665)
            Add(t1, t1, bz)
            Multiply(bz, t1, t2)
        End Sub


        Private Shared Sub CurveEquationInline(y2 As Long10, x As Long10, temp As Long10)
            Square(temp, x)
            MulSmall(y2, x, 486662)
            Add(temp, temp, y2)
            temp.N0 += 1
            Multiply(y2, temp, x)
        End Sub


        Private Shared Sub Core(publicKey As Byte(), signingKey As Byte(), privateKey As Byte(), peerPublicKey As Byte())
            If publicKey Is Nothing Then Throw New ArgumentNullException("publicKey")

            If publicKey.Length <> 32 Then Throw New ArgumentException([String].Format("publicKey must be 32 bytes long (but was {0} bytes long)", publicKey.Length), "publicKey")
            If signingKey IsNot Nothing AndAlso signingKey.Length <> 32 Then Throw New ArgumentException([String].Format("signingKey must be null or 32 bytes long (but was {0} bytes long)", signingKey.Length), "signingKey")
            If privateKey Is Nothing Then Throw New ArgumentNullException("privateKey")
            If privateKey.Length <> 32 Then Throw New ArgumentException([String].Format("privateKey must be 32 bytes long (but was {0} bytes long)", privateKey.Length), "privateKey")

            If peerPublicKey IsNot Nothing AndAlso peerPublicKey.Length <> 32 Then Throw New ArgumentException([String].Format("peerPublicKey must be null or 32 bytes long (but was {0} bytes long)", peerPublicKey.Length), "peerPublicKey")


            Dim dx As New Long10(), t1 As New Long10(), t2 As New Long10(), t3 As New Long10(), t4 As New Long10()
            Dim x As Long10() = {New Long10(), New Long10()}, z As Long10() = {New Long10(), New Long10()}



            If peerPublicKey IsNot Nothing Then
                Unpack(dx, peerPublicKey)
            Else
                [Set](dx, 9)
            End If



            [Set](x(0), 1)
            [Set](z(0), 0)



            Copy(x(1), dx)
            [Set](z(1), 1)

            Dim i As Integer = 32
            While System.Math.Max(System.Threading.Interlocked.Decrement(i), i + 1) <> 0
                Dim j As Integer = 8
                While System.Math.Max(System.Threading.Interlocked.Decrement(j), j + 1) <> 0

                    Dim bit1 As Integer = (privateKey(i) And &HFF) >> j And 1
                    Dim bit0 As Integer = Not (privateKey(i) And &HFF) >> j And 1
                    Dim ax As Long10 = x(bit0)
                    Dim az As Long10 = z(bit0)
                    Dim bx As Long10 = x(bit1)
                    Dim bz As Long10 = z(bit1)

                    MontyPrepare(t1, t2, ax, az)
                    MontyPrepare(t3, t4, bx, bz)
                    MontyAdd(t1, t2, t3, t4, ax, az,
                        dx)
                    MontyDouble(t1, t2, t3, t4, bx, bz)
                End While
            End While

            Reciprocal(t1, z(0), False)
            Multiply(dx, x(0), t1)
            Pack(dx, publicKey)



            If signingKey IsNot Nothing Then
                CurveEquationInline(t1, dx, t2)

                Reciprocal(t3, z(1), False)

                Multiply(t2, x(1), t3)

                Add(t2, t2, dx)

                t2.N0 += 9 + 486662

                dx.N0 -= 9

                Square(t3, dx)

                Multiply(dx, t2, t3)

                [Sub](dx, dx, t1)

                dx.N0 -= 39420360

                Multiply(t1, dx, BaseR2Y)

                If IsNegative(t1) <> 0 Then

                    Copy32(privateKey, signingKey)
                Else

                    MultiplyArraySmall(signingKey, OrderTimes8, 0, privateKey, 32, -1)
                End If


                Dim temp1 = New Byte(31) {}
                Dim temp2 = New Byte(63) {}
                Dim temp3 = New Byte(63) {}
                Copy32(Order, temp1)
                Copy32(Egcd32(temp2, temp3, signingKey, temp1), signingKey)
                If (signingKey(31) And &H80) <> 0 Then
                    MultiplyArraySmall(signingKey, signingKey, 0, Order, 32, 1)
                End If
            End If
        End Sub


        Shared ReadOnly OrderTimes8 As Byte() = {104, 159, 174, 231, 210, 24, 147, 192, 178, 230, 188, 23, 245, 206, 247, 166, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128}

        Shared ReadOnly BaseR2Y As New Long10(5744, 8160848, 4790893, 13779497, 35730846, 12541209, 49101323, 30047407, 40071253, 6226132)

    Public Shared Function sign(v() As Byte, h() As Byte, x() As Byte, s() As Byte) As Boolean
        ' v = (x - h) s  mod q
        Dim w, i As Integer
        Dim h1(31) As Byte
        Dim x1(31) As Byte
        Dim tmp1(63) As Byte
        Dim tmp2(63) As Byte

        ' Don't clobber the arguments, be nice!
        cpy32(h1, h)
        cpy32(x1, x)

        ' Reduce modulo group order
        Dim tmp3(31) As Byte
        DivMod(tmp3, h1, 32, Order, 32)
        DivMod(tmp3, x1, 32, Order, 32)

        ' v = x1 - h1
        ' If v is negative, add the group order to it to become positive.
        ' If v was already positive we don't have to worry about overflow
        ' when adding the order because v < ORDER and 2*ORDER < 2^256
        mula_small(v, x1, 0, h1, 32, -1)
        mula_small(v, v, 0, Order, 32, 1)

        ' tmp1 = (x-h)*s mod q
        mula32(tmp1, v, s, 32, 1)
        DivMod(tmp2, tmp1, 64, Order, 32)

        w = 0
        i = 0
        Do While i < 32
            v(i) = tmp1(i)
            w = w Or v(i)
            i += 1
        Loop
        Return w <> 0
    End Function

    Private Shared Function mula_small(p() As Byte, q() As Byte, m As Integer, x() As Byte, n As Integer, z As Integer) As Integer
        Dim v As Integer = 0
        For i As Integer = 0 To n - 1
            v += (q(i + m) And &HFF) + z * (x(i) And &HFF)
            p(i + m) = CByte(v)
            v >>= 8
        Next i
        Return v
    End Function
    Private Shared Sub cpy32(d() As Byte, s() As Byte)
        Dim i As Integer
        For i = 0 To 31
            d(i) = s(i)
        Next i
    End Sub
    Private Shared Function mula32(p() As Byte, x() As Byte, y() As Byte, t As Integer, z As Integer) As Integer
        Const n As Integer = 31
        Dim w As Integer = 0
        Dim i As Integer = 0
        Do While i < t
            Dim zy As Integer = z * (y(i) And &HFF)
            w += mula_small(p, p, i, x, n, zy) + (p(i + n) And &HFF) + zy * (x(n) And &HFF)
            p(i + n) = CByte(w)
            w >>= 8
            i += 1
        Loop
        p(i + n) = CByte(w + (p(i + n) And &HFF))
        Return w >> 8
    End Function
End Class

