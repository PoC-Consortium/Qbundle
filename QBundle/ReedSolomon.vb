'   Ported and modified from Java to VB.net by Quibus, 20/08/2017
'
'    Reed Solomon Encoding And Decoding For Nxt
'
'    Version: 1.0, license: Public Domain, coder : NxtChg(admin@nxtchg.com)
'    Java Version : ChuckOne(ChuckOne@mail.de).
'
Imports System.Numerics
Public Class ReedSolomon

    Private Shared ReadOnly initial_codeword() As Integer = New Integer() {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    Private Shared ReadOnly gexp() As Integer = New Integer() {1, 2, 4, 8, 16, 5, 10, 20, 13, 26, 17, 7, 14, 28, 29, 31, 27, 19, 3, 6, 12, 24, 21, 15, 30, 25, 23, 11, 22, 9, 18, 1}
    Private Shared ReadOnly glog() As Integer = New Integer() {0, 0, 1, 18, 2, 5, 19, 11, 3, 29, 6, 27, 20, 8, 12, 23, 4, 10, 30, 17, 7, 22, 28, 26, 21, 25, 9, 16, 13, 14, 24, 15}
    Private Shared ReadOnly codeword_map() As Integer = New Integer() {3, 2, 1, 0, 7, 6, 5, 4, 13, 14, 15, 16, 12, 8, 9, 10, 11}
    Private Shared ReadOnly alphabet As String = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ"

    Private Shared ReadOnly base_32_length As Integer = 13
    Private Shared ReadOnly base_10_length As Integer = 19

    Public Shared Function encode(plain As ULong) As String

        Dim plain_string As String = CStr(plain)
        Dim length As Integer = plain_string.Length
        Dim plain_string_10(base_10_length) As Integer
        For i As Integer = 0 To length - 1
            plain_string_10(i) = Int32.Parse(plain_string.Substring(i, 1))
        Next

        Dim codeword_length As Integer = 0
        Dim codeword(UBound(initial_codeword)) As Integer

        Dim new_length As Integer
        Dim digit_32 As Integer
        Do
            new_length = 0
            digit_32 = 0
            For i As Integer = 0 To length - 1

                digit_32 = digit_32 * 10 + plain_string_10(i)
                If digit_32 >= 32 Then
                    plain_string_10(new_length) = digit_32 >> 5
                    digit_32 = digit_32 And 31
                    new_length += 1
                ElseIf new_length > 0 Then
                    plain_string_10(new_length) = 0
                    new_length += 1
                End If

            Next
            length = new_length
            codeword(codeword_length) = digit_32
            codeword_length += 1
        Loop Until length <= 0

        Dim p = New Integer() {0, 0, 0, 0}
        Dim fb As Integer = 0
        For i As Integer = base_32_length - 1 To 0 Step -1
            fb = codeword(i) Xor p(3)
            p(3) = p(2) Xor gmult(30, fb)
            p(2) = p(1) Xor gmult(6, fb)
            p(1) = p(0) Xor gmult(9, fb)
            p(0) = gmult(17, fb)
        Next

        Array.Copy(p, 0, codeword, base_32_length, initial_codeword.length - base_32_length)


        Dim cypher_string_builder As String = ""
        Dim codework_index As Integer = 0
        Dim alphabet_index As Integer = 0
        For i As Integer = 0 To 16
            codework_index = codeword_map(i)
            alphabet_index = codeword(codework_index)
            cypher_string_builder &= alphabet.Substring(alphabet_index, 1)

            If (i And 3) = 3 And (i < 13) Then
                cypher_string_builder &= "-"
            End If

        Next

        Return cypher_string_builder
    End Function

    Public Shared Function decode(cypher_string As String) As ULong

        cypher_string = cypher_string.Replace("-", "") 'lets remove the - in the address

        Dim codeword(UBound(initial_codeword)) As Integer
        Array.Copy(initial_codeword, 0, codeword, 0, initial_codeword.length)
        Dim codeword_length As Integer = 0


        Dim position_in_alphabet As Integer
        Dim codework_index As Integer
        For i = 0 To cypher_string.Length - 1
            position_in_alphabet = alphabet.IndexOf(cypher_string.Substring(i, 1))
            If position_in_alphabet <= -1 Or position_in_alphabet > alphabet.Length Then Return 0
            If codeword_length > 16 Then Return 0
            codework_index = codeword_map(codeword_length)
            codeword(codework_index) = position_in_alphabet
            codeword_length += 1
        Next

        If Not is_codeword_valid(codeword) Then Return 0
        Dim length As Integer = base_32_length

        Dim cypher_string_32(length - 1) As Integer

        For i As Integer = 0 To length - 1
            cypher_string_32(i) = codeword(length - i - 1)
        Next


        Dim plain_string_builder As String = "" ' New System.Text.StringBuilder()

        Dim new_length As Integer = 0
        Dim digit_10 As Integer = 0
        Do
            new_length = 0
            digit_10 = 0

            For i As Integer = 0 To length - 1
                digit_10 = digit_10 * 32 + cypher_string_32(i)
                If digit_10 >= 10 Then
                    cypher_string_32(new_length) = digit_10 \ 10
                    digit_10 = digit_10 Mod 10
                    new_length += 1
                ElseIf new_length > 0 Then
                    cypher_string_32(new_length) = 0
                    new_length += 1
                End If

            Next
            length = new_length
            plain_string_builder = (ChrW(digit_10 + AscW("0"c))) & plain_string_builder


        Loop While length > 0

        Dim bigInt As New BigInteger
        bigInt = BigInteger.Parse(plain_string_builder)
        Dim retval As ULong = CULng(bigInt)

        Return retval
    End Function




    Private Shared Function gmult(a As Integer, b As Integer) As Integer
        If a = 0 Or b = 0 Then Return 0
        Dim idx As Integer = (glog(a) + glog(b)) Mod 31
        Return gexp(idx)
    End Function
    Private Shared Function is_codeword_valid(codeword() As Integer) As Boolean
        Dim sum As Integer = 0
        Dim t As Integer = 0
        Dim pos As Integer = 0
        For i As Integer = 1 To 4
            t = 0
            For j As Integer = 0 To 30
                If j > 12 And j < 27 Then
                    Continue For
                End If

                pos = j
                If j > 26 Then pos -= 14
                t = t Xor gmult(codeword(pos), gexp((i * j) Mod 31))
            Next
            sum = sum Or t
        Next
        If sum = 0 Then
            Return True
        Else
            Return False
        End If


    End Function







End Class
