Imports System.Numerics
Imports System.Security.Cryptography
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class frmVanity
    Private Delegate Sub DFound(ByVal [Address] As String, ByVal [Pass] As String)
    Private running As Boolean
    Private AddressToFind As String
    Private NrofChars As Integer
    Private Tested As Long
    Private LockObj As Object
    Private counter As Integer
    Private lastval As Integer
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Me.cmSave.Show(Me.btnSave, Me.btnSave.PointToClient(Cursor.Position))
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click

        If btnStart.Text = "Start" Then
            Tested = 0
            counter = 0
            lastval = 0
            AddressToFind = txtFind1.Text & "-" & txtFind2.Text & "-" & txtFind3.Text & "-" & txtFind4.Text
            AddressToFind = Replace(AddressToFind, "@", ".")
            NrofChars = nrPass.Value
            Dim trda As Thread
            running = True
            For x As Integer = 1 To nrThreads.Value
                trda = New Thread(AddressOf VanityGeneration)
                trda.Priority = ThreadPriority.BelowNormal
                ' trda.IsBackground = True
                trda.Start()
            Next
            trda = Nothing
            btnStart.Text = "Stop"
            tmr.Enabled = True
            Exit Sub

        End If
        If btnStart.Text = "Stop" Then
            running = False
            btnStart.Text = "Start"
            tmr.Enabled = False
        End If


    End Sub

    Private Sub frmVanity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        running = False
        LockObj = New Object
        Exit Sub
        nrThreads.Maximum = Environment.ProcessorCount

        Select Case Environment.ProcessorCount
            Case 1
                nrThreads.Value = 1
            Case 2
                nrThreads.Value = 1
            Case 4
                nrThreads.Value = 3
            Case Else
                nrThreads.Value = Environment.ProcessorCount - 1
        End Select
    End Sub
    Private Sub VanityGeneration()
        Dim AccountAddress As String
        Dim KeySeed As String = ""
        Dim PrivateKey As Byte()
        Dim PublicKey As Byte()
        Dim PublicKeyHash As Byte()
        Dim cSHA256 As SHA256
        Dim b As Byte()
        Dim x As Integer
        ' Dim Crv As New Curve

        Dim chars() As String = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
         "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "X",
         "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"}
        Dim TotalChars As Integer = UBound(chars)
        Do
            If running = False Then Exit Do
            KeySeed = ""
            For x = 1 To NrofChars
                Randomize()
                KeySeed &= chars(Int(Rnd() * TotalChars))
            Next
            cSHA256 = SHA256Managed.Create()
            PrivateKey = cSHA256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(KeySeed))
            PublicKey = Curve.GetPublicKey(PrivateKey)

            PublicKeyHash = cSHA256.ComputeHash(PublicKey)
            b = New Byte() {PublicKeyHash(0), PublicKeyHash(1), PublicKeyHash(2), PublicKeyHash(3), PublicKeyHash(4), PublicKeyHash(5), PublicKeyHash(6), PublicKeyHash(7)}
            If (b(b.Length - 1) And &H80) <> 0 Then Array.Resize(Of Byte)(b, b.Length + 1)
            AccountAddress = ReedSolomon.encode(New BigInteger(b))
            '    SyncLock LockObj
            Tested += 1
            '    End SyncLock
            If Regex.IsMatch(AccountAddress, AddressToFind) Then
                If ValidateAddress(AccountAddress) Then
                    Found(AccountAddress, KeySeed)
                    Exit Sub
                End If
            End If
        Loop

    End Sub
    Private Function ValidateAddress(ByVal Addr As String) As Boolean

        'if account returns it has a public key. we do not want to give password to existing address
        Dim http As New clsHttp
        Dim result As String = http.GetUrl("https://wallet.burst.cryptoguru.org:8125/burst?requestType=getAccount&account=BURST-" & Addr)
        If result.Contains("errorDescription") Then Return True

        Return False
    End Function

    Private Sub Found(ByVal Address As String, ByVal Pass As String)
        If Me.InvokeRequired Then
            Dim d As New DFound(AddressOf Found)
            Me.Invoke(d, New Object() {Address, Pass})
            Return
        End If
        running = False
        txtAddress.Text = "BURST-" & Address
        txtPass.Text = Pass
        tmr.Enabled = False

        btnStart.Text = "Start"
        btnSave.Enabled = True
    End Sub


    Private Sub tmr_Tick(sender As Object, e As EventArgs) Handles tmr.Tick
        SyncLock LockObj
            lblTested.Text = Tested
            counter += 1
            If counter = 10 Then
                lblTesting.Text = CStr(Tested - lastval) & " keys/sec"
                lastval = Tested
                counter = 0
            End If
        End SyncLock

    End Sub

    Private Sub txtFind1_TextChanged(sender As Object, e As EventArgs) Handles txtFind1.TextChanged
        Dim Index As Integer = txtFind1.SelectionStart
        Dim result As String = txtFind1.Text
        If result.Length < 4 Then
            Do
                result &= "@"
                If result.Length = 4 Then Exit Do
            Loop
        End If
        Dim txt() As Char = UCase(result).ToCharArray
        Dim chars As String = "ABCDEFGHJKLMNPQRSTUVWXYX23456789@"
        result = ""
        For t As Integer = 0 To 3
            If Not chars.Contains(txt(t)) Then
                txt(t) = "@"

            End If
            result &= txt(t)
        Next
        txtFind1.Text = result
        txtFind1.Select(Index, 0)
        If Index = 4 Then txtFind2.Focus()

    End Sub

    Private Sub txtFind2_TextChanged(sender As Object, e As EventArgs) Handles txtFind2.TextChanged
        Dim Index As Integer = txtFind2.SelectionStart
        Dim result As String = txtFind2.Text
        If result.Length < 4 Then
            Do
                result &= "@"
                If result.Length = 4 Then Exit Do
            Loop
        End If
        Dim txt() As Char = UCase(result).ToCharArray
        Dim chars As String = "ABCDEFGHJKLMNPQRSTUVWXYX23456789@"
        result = ""
        For t As Integer = 0 To 3
            If Not chars.Contains(txt(t)) Then
                txt(t) = "@"
            End If
            result &= txt(t)
        Next
        txtFind2.Text = result
        txtFind2.Select(Index, 0)
        If Index = 4 Then txtFind3.Focus()
        '  If Index = 0 Then txtFind1.Focus()
    End Sub
    Private Sub txtFind2_TextKeyDown(sender As Object, e As KeyEventArgs) Handles txtFind2.KeyDown
        If txtFind2.SelectionStart = 0 Then
            If e.KeyCode = Keys.Back Then
                txtFind1.Focus()
            End If
        End If
    End Sub

    Private Sub txtFind3_TextChanged(sender As Object, e As EventArgs) Handles txtFind3.TextChanged
        Dim Index As Integer = txtFind3.SelectionStart
        Dim result As String = txtFind3.Text
        If result.Length < 4 Then
            Do
                result &= "@"
                If result.Length = 4 Then Exit Do
            Loop
        End If
        Dim txt() As Char = UCase(result).ToCharArray
        Dim chars As String = "ABCDEFGHJKLMNPQRSTUVWXYX23456789@"
        result = ""
        For t As Integer = 0 To 3
            If Not chars.Contains(txt(t)) Then
                txt(t) = "@"

            End If
            result &= txt(t)
        Next
        txtFind3.Text = result
        txtFind3.Select(Index, 0)
        If Index = 4 Then txtFind4.Focus()
        '  If Index = 0 Then txtFind2.Focus()
    End Sub
    Private Sub txtFind3_TextKeyDown(sender As Object, e As KeyEventArgs) Handles txtFind3.KeyDown
        If txtFind3.SelectionStart = 0 Then
            If e.KeyCode = Keys.Back Then
                txtFind2.Focus()
            End If
        End If
    End Sub

    Private Sub txtFind4_TextChanged(sender As Object, e As EventArgs) Handles txtFind4.TextChanged
        Dim Index As Integer = txtFind4.SelectionStart
        Dim result As String = txtFind4.Text
        If result.Length < 5 Then
            Do
                result &= "@"
                If result.Length = 5 Then Exit Do
            Loop
        End If
        Dim txt() As Char = UCase(result).ToCharArray
        Dim chars As String = "ABCDEFGHJKLMNPQRSTUVWXYX23456789@"
        result = ""
        For t As Integer = 0 To 4
            If Not chars.Contains(txt(t)) Then
                txt(t) = "@"

            End If
            result &= txt(t)
        Next
        txtFind4.Text = result
        txtFind4.Select(Index, 0)
        '  If Index = 0 Then txtFind3.Focus()

    End Sub
    Private Sub txtFind4_TextKeyDown(sender As Object, e As KeyEventArgs) Handles txtFind4.KeyDown
        If txtFind4.SelectionStart = 0 Then
            If e.KeyCode = Keys.Back Then
                txtFind3.Focus()
            End If
        End If
    End Sub

    Private Sub SaveAsTextfileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsTextfileToolStripMenuItem.Click
        Dim sfd As New SaveFileDialog
        sfd.Filter = "Text Files (*.txt)|*.txt"
        If sfd.ShowDialog = DialogResult.OK Then
            Try
                Dim Buffer As String = "Address:" & vbCrLf
                Buffer &= txtAddress.Text & vbCrLf & vbCrLf
                Buffer &= "Passphrase:" & vbCrLf
                Buffer &= txtPass.Text
                IO.File.WriteAllText(sfd.FileName, Buffer)
            Catch ex As Exception

            End Try
            MsgBox("Account information is saved", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "File saved")
        End If




    End Sub

    Private Sub SaveToAccountManagerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToAccountManagerToolStripMenuItem.Click
        Dim newAcc As New frmNewAccount
        newAcc.txtPass.Text = txtPass.Text
        newAcc.btnNew.Enabled = False
        newAcc.txtPass.ReadOnly = True
        If newAcc.ShowDialog = DialogResult.OK Then
            Q.Accounts.AddAccount(newAcc.txtName.Text, newAcc.txtPass.Text, newAcc.txtPin.Text)
            Q.Accounts.SaveAccounts()
            frmMain.SetLoginMenu()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Tested = 0
        counter = 0
        lastval = 0
        AddressToFind = txtFind1.Text & "-" & txtFind2.Text & "-" & txtFind3.Text & "-" & txtFind4.Text
        AddressToFind = Replace(AddressToFind, "@", ".")
        NrofChars = nrPass.Value
        Dim trda As Thread
        trda = New Thread(AddressOf TestParalel)
        trda.Priority = ThreadPriority.BelowNormal
        trda.IsBackground = True

        trda.Start()
        running = True

        btnStart.Text = "Stop"
        tmr.Enabled = True




    End Sub

    Private Sub TestParalel()

        Parallel.For(0, Integer.MaxValue, Sub(i, loopState)

                                              Dim AccountAddress As String
                                              Dim KeySeed As String = ""
                                              Dim PrivateKey As Byte()
                                              Dim PublicKey As Byte()
                                              Dim PublicKeyHash As Byte()
                                              Dim cSHA256 As SHA256
                                              Dim b As Byte()
                                              Dim x As Integer


                                              Dim chars() As String = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
         "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "X",
         "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"}
                                              Dim TotalChars As Integer = UBound(chars)

                                              If running = False Then

                                                  loopState.[Stop]()
                                                  Exit Sub

                                              End If
                                              KeySeed = ""
                                              For x = 1 To NrofChars
                                                  Randomize()
                                                  KeySeed &= chars(Int(Rnd() * TotalChars))
                                              Next
                                              cSHA256 = SHA256Managed.Create()
                                              PrivateKey = cSHA256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(KeySeed))
                                              PublicKey = Curve.GetPublicKey(PrivateKey)

                                              PublicKeyHash = cSHA256.ComputeHash(PublicKey)
                                              b = New Byte() {PublicKeyHash(0), PublicKeyHash(1), PublicKeyHash(2), PublicKeyHash(3), PublicKeyHash(4), PublicKeyHash(5), PublicKeyHash(6), PublicKeyHash(7)}
                                              If (b(b.Length - 1) And &H80) <> 0 Then Array.Resize(Of Byte)(b, b.Length + 1)
                                              AccountAddress = ReedSolomon.encode(New BigInteger(b))
                                              '    SyncLock LockObj
                                              Tested += 1
                                              '    End SyncLock
                                              If Regex.IsMatch(AccountAddress, AddressToFind) Then
                                                  If ValidateAddress(AccountAddress) Then
                                                      Found(AccountAddress, KeySeed)
                                                      Exit Sub
                                                  End If
                                              End If


                                          End Sub)

    End Sub
End Class