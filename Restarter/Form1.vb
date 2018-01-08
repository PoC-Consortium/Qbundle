Imports System.IO
Imports System.Threading

Public Class frmRestart
    Private Arg As String()
    Private Sub frmRestart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Arg = Environment.GetCommandLineArgs()

        If UBound(Arg) < 2 Then
            MsgBox("Argument Missing")
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
                MsgBox("Argument Missing")
                End
            End If

            If Not IO.File.Exists(Workdir & Arg(2)) Then
                MsgBox("Argument Missing")
                End
            End If
        Catch ex As Exception

        End Try
        Try
            Dim trda As Thread
            trda = New Thread(AddressOf RestartThread)
            trda.IsBackground = True
            trda.Start()
        Catch ex As Exception
            MsgBox("Failed to Start restarting thread.")
            End
        End Try




    End Sub

    Private Sub RestartThread()
        Dim Workdir As String = Application.StartupPath
        If Not Workdir.EndsWith("\") Then Workdir &= "\"
        Dim i As Integer = 0
        Dim p() As Process
        Dim PName = Arg(2).Replace(".exe", "")
        Do
            Try
                File.Delete(Workdir & Arg(2)) 'if we can delete file then we can continue
                File.Copy(Workdir & Arg(1), Workdir & Arg(2), True)
                Shell(Workdir & Arg(2), AppWinStyle.NormalFocus)
                Thread.Sleep(100)
                File.Delete(Workdir & Arg(1))
                Exit Do
            Catch ex As Exception

            End Try
            Thread.Sleep(500)
            If i < 4 Then i += 1
            If i > 4 Then
                Try
                    p = Process.GetProcessesByName(PName)
                    If p.Count > 0 Then
                        p(0).Kill()
                    End If
                Catch ex As Exception

                End Try
            End If
        Loop
        End


    End Sub


End Class
