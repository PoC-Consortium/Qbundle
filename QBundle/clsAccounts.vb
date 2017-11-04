Imports System.IO
Imports System.Xml.Serialization


Public Class clsAccounts
    Implements ICollection
    Public CollectionName As String
    Private AccArray As New ArrayList()
    Default Public ReadOnly Property Item(index As Integer) As Account
        Get
            Return DirectCast(AccArray(index), Account)
        End Get
    End Property
    Public Sub CopyTo(a As Array, index As Integer)
        AccArray.CopyTo(a, index)
    End Sub
    Public ReadOnly Property Count() As Integer
        Get
            Return AccArray.Count
        End Get
    End Property
    Public ReadOnly Property SyncRoot() As Object
        Get
            Return Me
        End Get
    End Property
    Public ReadOnly Property IsSynchronized() As Boolean
        Get
            Return False
        End Get
    End Property
    Private ReadOnly Property ICollection_Count As Integer Implements ICollection.Count
        Get
            Throw New NotImplementedException()
        End Get
    End Property
    Private ReadOnly Property ICollection_SyncRoot As Object Implements ICollection.SyncRoot
        Get
            Throw New NotImplementedException()
        End Get
    End Property
    Private ReadOnly Property ICollection_IsSynchronized As Boolean Implements ICollection.IsSynchronized
        Get
            Throw New NotImplementedException()
        End Get
    End Property
    Public Function GetEnumerator() As IEnumerator
        Return AccArray.GetEnumerator()
    End Function
    Public Sub Add(acc As Account)
        AccArray.Add(acc)
    End Sub
    Private Sub ICollection_CopyTo(array As Array, index As Integer) Implements ICollection.CopyTo
        Throw New NotImplementedException()
    End Sub
    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Throw New NotImplementedException()
    End Function
    Public Sub SaveAccounts()
        Dim x As New XmlSerializer(GetType(clsAccounts))
        Dim writer As TextWriter = New StreamWriter(QGlobal.BaseDir & "Acconts.xml")
        x.Serialize(writer, Me)
        writer.Close()
        writer.Dispose()
        x = Nothing
    End Sub
    Public Sub LoadAccounts()
        Dim x As New XmlSerializer(GetType(clsAccounts))
        Dim Reader As TextReader = New StreamReader(QGlobal.BaseDir & "Acconts.xml")
        '  Me = DirectCast(x.Deserialize(Reader), clsAccounts)
        Reader.Close()
        Reader.Dispose()
        x = Nothing

    End Sub
    Sub New()
        Me.CollectionName = "Accounts"
    End Sub
End Class
Public Class Account
    Public AccountName As String
    Public BurstPassword As String
    Public PinCode As String
    Public Sub New()
    End Sub
    Public Sub New(Acc As String, Pass As String, Pin As String)
        AccountName = Acc
        BurstPassword = Pass
        PinCode = Pin
    End Sub
End Class
