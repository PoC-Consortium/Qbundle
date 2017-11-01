Imports System.IO

Public Class frmRestart
    Private Sub frmRestart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Arg As String() = Environment.GetCommandLineArgs()
        If UBound(Arg) < 2 Then
            End
        End If
        Me.Show()
        Me.Label1.Refresh()
        Dim Workdir As String = Application.StartupPath
        If Not Workdir.EndsWith("\") Then Workdir &= "\"
        'arg1 Fromfile
        'arg2 Tofile

        Try
            If Not IO.File.Exists(Workdir & Arg(1)) Then
                End
            End If

            If Not IO.File.Exists(Workdir & Arg(2)) Then
                End
            End If

        Catch ex As Exception

        End Try


        Do

            Try
                File.Delete(Workdir & Arg(2)) 'if we can delete file then we can continue
                File.Copy(Workdir & Arg(1), Workdir & Arg(2), True)
                Shell(Workdir & Arg(2), AppWinStyle.NormalFocus)
                Threading.Thread.Sleep(100)
                File.Delete(Workdir & Arg(1))
                Exit Do
            Catch ex As Exception

            End Try
            Threading.Thread.Sleep(500)
        Loop


        End

    End Sub


End Class
