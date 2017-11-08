Public Class frmSetrewardassignment
    Private Sub btnAccounts_Click(sender As Object, e As EventArgs) Handles btnAccounts.Click
        Try
            Me.cmlAccounts.Show(Me.btnAccounts, Me.btnAccounts.PointToClient(Cursor.Position))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmSetrewardassignment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmlAccounts.Items.Clear()
        Dim mnuitm As ToolStripMenuItem
        For Each account As QB.clsAccounts.Account In Q.Accounts.AccArray
            mnuitm = New ToolStripMenuItem
            mnuitm.Name = account.AccountName
            mnuitm.Text = account.AccountName
            AddHandler(mnuitm.Click), AddressOf SelectAccountID
            cmlAccounts.Items.Add(mnuitm)
        Next

        cmlPools.Items.Clear()
        mnuitm = New ToolStripMenuItem
        mnuitm.Name = "Solo mining"
        mnuitm.Text = "Solo mining"
        AddHandler(mnuitm.Click), AddressOf SelectPoolID
        cmlPools.Items.Add(mnuitm)

        For x As Integer = 0 To UBound(QGlobal.Pools)
            mnuitm = New ToolStripMenuItem
            mnuitm.Name = QGlobal.Pools(x).Name
            mnuitm.Text = QGlobal.Pools(x).Name
            AddHandler(mnuitm.Click), AddressOf SelectPoolID
            cmlPools.Items.Add(mnuitm)
        Next

    End Sub
    Private Sub SelectAccountID(sender As Object, e As EventArgs)
        txtAccount.Text = Q.Accounts.GetAccountRS(sender.text)
    End Sub
    Private Sub SelectPoolID(sender As Object, e As EventArgs)
        If sender.text = "Solo mining" Then
            txtPool.Text = txtAccount.Text
        Else
            For x As Integer = 0 To UBound(QGlobal.Pools)
                If sender.text = QGlobal.Pools(x).Name Then
                    txtPool.Text = QGlobal.Pools(x).BurstAddress
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub btnPool_Click(sender As Object, e As EventArgs) Handles btnPool.Click
        Try
            Me.cmlPools.Show(Me.btnPool, Me.btnPool.PointToClient(Cursor.Position))
        Catch ex As Exception

        End Try
    End Sub




End Class