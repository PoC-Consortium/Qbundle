Public Class frmMiner
    Private Sub frmMiner_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lstPlots.Items.Clear()
        If Q.settings.Plots <> "" Then
            Dim buffer() As String = Split(Q.settings.Plots, "|")
            For Each plot As String In buffer
                If plot.Length > 1 Then
                    lstPlots.Items.Add(plot)
                End If
            Next
        End If

        cmlServers.Items.Clear()
        Dim mnuitm As ToolStripMenuItem
        For t As Integer = 0 To UBound(QGlobal.Pools)
            mnuitm = New ToolStripMenuItem
            mnuitm.Name = QGlobal.Pools(t).Name
            mnuitm.Text = QGlobal.Pools(t).Name
            AddHandler(mnuitm.Click), AddressOf SelectPoolID
            cmlServers.Items.Add(mnuitm)

        Next

    End Sub
    Private Sub SelectPoolID(sender As Object, e As EventArgs)

        For x As Integer = 0 To UBound(QGlobal.Pools)
            If sender.text = QGlobal.Pools(x).Name Then
                txtMiningServer.Text = QGlobal.Pools(x).Address
                nrMiningPort.Value = Val(QGlobal.Pools(x).Port)
                txtUpdateServer.Text = QGlobal.Pools(x).Address
                nrUpdatePort.Value = Val(QGlobal.Pools(x).Port)
                txtInfoServer.Text = QGlobal.Pools(x).Address
                nrInfoPort.Value = Val(QGlobal.Pools(x).Port)
                txtDeadLine.Text = QGlobal.Pools(x).DeadLine
                Exit For
            End If
        Next

    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click

        Dim ofd As New OpenFileDialog
        If ofd.ShowDialog = DialogResult.OK Then
            If IO.File.Exists(ofd.FileName) Then
                lstPlots.Items.Add(ofd.FileName)
                Q.settings.Plots &= ofd.FileName & "|"
                Q.settings.SaveSettings()
            End If
        End If
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If lstPlots.SelectedIndex = -1 Then
            MsgBox("You need to select a plot to remove.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Nothing to remove")
            Exit Sub
        End If
        If MsgBox("Are you sure you want to remove selected plot?" & vbCrLf & "It will not be deleted from disk.", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Remove plotfile") = MsgBoxResult.Yes Then
            lstPlots.Items.RemoveAt(lstPlots.SelectedIndex)
            Q.settings.Plots = ""
            For t As Integer = 0 To lstPlots.Items.Count - 1
                Q.settings.Plots &= lstPlots.Items.Item(t) & "|"
            Next
            Q.settings.SaveSettings()

        End If
    End Sub

    Private Sub btnPool_Click(sender As Object, e As EventArgs) Handles btnPool.Click
        Try
            Me.cmlServers.Show(Me.btnPool, Me.btnPool.PointToClient(Cursor.Position))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub WriteConfig()

        Dim plots As String = ""

        For t As Integer = 0 To lstPlots.Items.Count - 1
            plots &= Chr(34) & IO.Path.GetDirectoryName(lstPlots.Items.Item(t)) & Chr(34) & ","
        Next
        plots = Replace(plots, "\", "\\")
        plots = plots.Substring(0, plots.Length - 1)

        Dim UseHdd As String = "false"
        Dim ShowWInner As String = "false"
        Dim UseBoost As String = "false"

        If chkUseHDD.Checked Then UseHdd = "true"
        If chkUseBoost.Checked Then UseBoost = "true"
        If chkShowWinner.Checked Then ShowWInner = "true"

        Dim cfg As String = ""
        cfg &= "{" & vbCrLf
        cfg &= "   " & Chr(34) & "Mode" & Chr(34) & " : " & Chr(34) & "pool" & Chr(34) & "," & vbCrLf
        cfg &= "   " & Chr(34) & "Server" & Chr(34) & " : " & Chr(34) & txtMiningServer.Text & Chr(34) & "," & vbCrLf
        cfg &= "   " & Chr(34) & "Port" & Chr(34) & ": " & nrMiningPort.Value.ToString & "," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "UpdaterAddr" & Chr(34) & " : " & Chr(34) & txtUpdateServer.Text & Chr(34) & "," & vbCrLf
        cfg &= "   " & Chr(34) & "UpdaterPort" & Chr(34) & ": " & Chr(34) & nrMiningPort.Value.ToString & Chr(34) & "," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "InfoAddr" & Chr(34) & " : " & Chr(34) & txtInfoServer.Text & Chr(34) & "," & vbCrLf
        cfg &= "   " & Chr(34) & "InfoPort" & Chr(34) & ": " & Chr(34) & nrInfoPort.Value.ToString & Chr(34) & "," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "EnableProxy" & Chr(34) & ": false," & vbCrLf
        cfg &= "   " & Chr(34) & "ProxyPort" & Chr(34) & ": 8126," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "Paths" & Chr(34) & ":[" & plots & "]," & vbCrLf
        cfg &= "   " & Chr(34) & "CacheSize" & Chr(34) & " : 10000," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "Debug" & Chr(34) & ": true," & vbCrLf
        cfg &= "   " & Chr(34) & "UseHDDWakeUp" & Chr(34) & ": " & UseHdd & "," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "TargetDeadline" & Chr(34) & ": " & txtDeadLine.Text & "," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "SendInterval" & Chr(34) & ": 100," & vbCrLf
        cfg &= "   " & Chr(34) & "UpdateInterval" & Chr(34) & ": 950," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "UseLog" & Chr(34) & " : true," & vbCrLf
        cfg &= "   " & Chr(34) & "ShowWinner" & Chr(34) & " : " & ShowWInner & "," & vbCrLf
        cfg &= "   " & Chr(34) & "UseBoost" & Chr(34) & " : " & UseBoost & "," & vbCrLf
        cfg &= "" & vbCrLf
        cfg &= "   " & Chr(34) & "WinSizeX" & Chr(34) & ": 76," & vbCrLf
        cfg &= "   " & Chr(34) & "WinSizeY" & Chr(34) & ": 60" & vbCrLf
        cfg &= "}" & vbCrLf

    End Sub


End Class