
Imports System.IO
Imports System.IO.Compression
Imports System.Net
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
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
        Try
            readStream.Close()
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
        Try
            receiveStream.Close()
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
        Try
            WebResponse.Close()
            WebResponse.Dispose()
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try
        Try
            Http = Nothing
        Catch ex As Exception
            If QB.Generic.DebugMe Then QB.Generic.WriteDebug(ex.StackTrace, ex.Message)
        End Try

        Return stbuffer
    End Function


End Class
