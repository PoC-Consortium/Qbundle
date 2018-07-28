
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Text

Public Class clsHttp

    Private Http As HttpWebRequest
    Private WebResponse As HttpWebResponse
    Private receiveStream As Stream
    Private readStream As StreamReader
    Private Mystate As Boolean = True
    Private stbuffer As String
    Friend Errmsg As String = ""
    Public Function GetUrl(ByVal url As String) As String
        stbuffer = ""
        Errmsg = ""
        Try
            Http = CType(WebRequest.Create(url), HttpWebRequest)
            Http.ReadWriteTimeout = 60 * 1000 'give it a minute
            Http.KeepAlive = False
            Http.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate")
            WebResponse = CType(Http.GetResponse(), HttpWebResponse)
            receiveStream = WebResponse.GetResponseStream()
            If WebResponse.ContentEncoding.ToLower().Contains("gzip") Then receiveStream = New GZipStream(receiveStream, CompressionMode.Decompress)
            If WebResponse.ContentEncoding.ToLower().Contains("deflate") Then receiveStream = New DeflateStream(receiveStream, CompressionMode.Decompress)
            readStream = New StreamReader(receiveStream, Text.Encoding.UTF8)

            stbuffer = readStream.ReadToEnd()
        Catch ex As Exception
            Mystate = False 'we shall return false after we have tried to clean up
            Generic.WriteDebug(ex)
        End Try
        Try
            readStream.Close()
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        Try
            receiveStream.Close()
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        Try
            WebResponse.Close()
            WebResponse.Dispose()
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        Try
            Http = Nothing
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try

        Return stbuffer
    End Function
    Public Function PostUrl(ByVal url As String, ByVal postData As String) As String
        stbuffer = ""
        Errmsg = ""
        Dim byteArray As Byte() = Nothing
        Dim dataStream As Stream = Nothing
        Try
            Http = CType(WebRequest.Create(url), HttpWebRequest)
            Http.ReadWriteTimeout = 60 * 1000 'give it a minute
            Http.KeepAlive = False
            Http.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate")
            Http.AllowAutoRedirect = True
            Http.ReadWriteTimeout = 600000
            Http.Method = "POST"
            Http.ContentType = "application/x-www-form-urlencoded"
            byteArray = Encoding.UTF8.GetBytes(postData)
            Http.ContentLength = byteArray.Length
            dataStream = Http.GetRequestStream() 'opening stream and posting data.
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Flush()
            dataStream.Close()
            dataStream.Dispose()
            byteArray = Nothing
            dataStream = Nothing
            WebResponse = CType(Http.GetResponse(), HttpWebResponse)
            receiveStream = WebResponse.GetResponseStream()
            If WebResponse.ContentEncoding.ToLower().Contains("gzip") Then receiveStream = New GZipStream(receiveStream, CompressionMode.Decompress)
            If WebResponse.ContentEncoding.ToLower().Contains("deflate") Then receiveStream = New DeflateStream(receiveStream, CompressionMode.Decompress)
            readStream = New StreamReader(receiveStream, Text.Encoding.UTF8)
            stbuffer = readStream.ReadToEnd()
        Catch ex As Exception
            Mystate = False 'we shall return false after we have tried to clean up
            Generic.WriteDebug(ex)
        End Try
        Try
            readStream.Close()
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        Try
            receiveStream.Close()
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        Try
            WebResponse.Close()
            WebResponse.Dispose()
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        Try
            Http = Nothing
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try

        Return stbuffer
    End Function
    Public Function URLEncode(ByVal StringToEncode As String) As String
        Dim TempAns As String = ""
        Try
            Dim CurChr As Integer = 0
            CurChr = 1
            Do Until CurChr - 1 = Len(StringToEncode)
                Select Case Asc(Mid(StringToEncode, CurChr, 1))
                    Case 48 To 57, 65 To 90, 97 To 122
                        TempAns = TempAns & Mid(StringToEncode, CurChr, 1)
                    Case 32
                        TempAns = TempAns & "%" & Hex(32)
                    Case Else
                        Dim a As String = Mid(StringToEncode, CurChr, 1)
                        Dim b As String = CStr(Asc(a))
                        Dim c As String = Hex(b)
                        If Len(c) = 1 Then c = "0" & c
                        TempAns = TempAns & "%" & c
                End Select
                CurChr = CurChr + 1
            Loop
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        Return TempAns
    End Function
End Class
