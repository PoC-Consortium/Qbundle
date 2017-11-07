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
End Class