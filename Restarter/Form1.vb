Imports System.IO
Imports System.Threading

Public Class frmRestart
    Private Arg As String()
    Private FromFile As String
    Private ToFile As String
    Private Sub frmRestart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Arg = Environment.GetCommandLineArgs()

        If UBound(Arg) < 2 Then
            FromFile = "BWLUpdate"
            ToFile = "BurstWallet.exe"
        Else
            FromFile = Arg(1)
            ToFile = Arg(2)
        End If

        Me.Show()
        Me.Label1.Refresh()

        Dim Workdir As String = Application.StartupPath
        If Not Workdir.EndsWith("\") Then Workdir &= "\"



        Try
            If Not IO.File.Exists(Workdir & FromFile) Then
                MsgBox("No update file available")
                End
            End If

            If Not IO.File.Exists(Workdir & ToFile) Then
                MsgBox("Installation is not complete")
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
        Dim PName = ToFile.Replace(".exe", "")
        Do
            Try
                File.Delete(Workdir & ToFile) 'if we can delete file then we can continue
                File.Copy(Workdir & FromFile, Workdir & ToFile, True)
                Shell(Workdir & ToFile, AppWinStyle.NormalFocus)
                Thread.Sleep(100)
                File.Delete(Workdir & FromFile)
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
