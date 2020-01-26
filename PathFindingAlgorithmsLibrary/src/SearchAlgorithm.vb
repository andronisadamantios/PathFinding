Imports System
Imports System.Collections.Generic
Imports System.Diagnostics

Public MustInherit Class SearchAlgorithm

    Public Class FoundEventArgs
        Inherits EventArgs

        Public Sub New()

        End Sub
    End Class

    Public Event Found(sa As SearchAlgorithm, fea As FoundEventArgs)

    Protected _map As Map
    Protected _stopwatch As Stopwatch
    Protected _found As Boolean
    Private _searched As Boolean
    Private _path As Node()
    Protected _origin As Node
    Protected _stoxos As Node
    Protected _pathLength As Integer
    Protected _refreshRate As Integer
    Protected _title As String

    Public ReadOnly Property Origin As Node
        Get
            Return Me._origin
        End Get
    End Property
    Public ReadOnly Property Stoxos As Node
        Get
            Return Me._stoxos
        End Get
    End Property



    Public Sub New()
        Me._stopwatch = New Stopwatch
    End Sub

    Public Sub New(map As Map, r As Integer)
        Me.New()
        Me._map = map
        Me._refreshRate = r
        AddHandler Me._map.Reseted, AddressOf mapReseted
    End Sub

    Private Sub checkCoor(row As Integer, col As Integer)
        If Me._map.RowsCount - 1 < row Or Me._map.ColsCount - 1 < col Then Throw New ArgumentOutOfRangeException
        If Me._map.Get(row, col).State = Node.NodeState.Impassable Then Throw New InvalidOperationException
    End Sub
    Private Function isCoorValid(ByVal row As Integer, ByVal col As Integer) As Boolean
        Return Not (Me._map.RowsCount - 1 < row Or Me._map.ColsCount - 1 < col) And Not (Me._map.Get(row, col).State = Node.NodeState.Impassable)
    End Function

    Public Function SetStoxos(row As Integer, col As Integer) As Boolean
        If Not isCoorValid(row, col) Then Return False
        Dim n = Me._map.Get(row, col)
        If Object.ReferenceEquals(n, Me._stoxos) Then Return False
        Return SetStoxos(n)
    End Function
    Public Function SetOrigin(row As Integer, col As Integer) As Boolean
        If Not isCoorValid(row, col) Then Return False
        Dim n = Me._map.Get(row, col)
        Return SetOrigin(n)
    End Function
    Private Function SetStoxos(n As Node) As Boolean
        If Object.ReferenceEquals(n, Me._stoxos) Then Return False
        Me._stoxos = n
        Me.Reset()
        Return True
    End Function
    Private Function SetOrigin(n As Node) As Boolean
        If Object.ReferenceEquals(n, Me._origin) Then Return False
        Me._origin = n
        Me.Reset()
        Return True
    End Function
    Public Sub ClearStoxos()
        SetStoxos(Nothing)
    End Sub
    Public Sub ClearOrigin()
        SetOrigin(Nothing)
    End Sub

    Public ReadOnly Property Epityxia As Boolean
        Get
            If Not Me._searched Then Throw New InvalidOperationException("prepei na ginei search prwta")
            Return Me._found
        End Get
    End Property

    Public ReadOnly Property Path As IEnumerable(Of Node)
        Get
            'If Not Me._searched Then Throw New InvalidOperationException("prepei na ginei search prwta")
            If Not Me._searched Then Me.Search()
            If Me.Epityxia Then
                If Me._path Is Nothing Then
                    Me._path = Me.FindPath.ToArray
                End If
                Return Me._path
            Else
                Return Nothing
            End If
        End Get
    End Property

    Protected Overridable Sub Reset()
        Me._stopwatch.Reset()
        Me._found = False
        Me._searched = False
        Me._path = Nothing
    End Sub

    Public Sub Run()
        If Not Me._searched Then
            Me.Search()
        End If
        If Me._found Then
            Me._path = Me.FindPath().ToArray
        End If
        'Me.Print()
    End Sub


    Protected MustOverride Sub Init()
    Protected MustOverride Function Update() As Boolean

    Protected Sub Search()
        If Me._searched Then Return
        Me._map.Reset()
        Me.Init()
        Dim finished As Boolean = False
        While Not finished
            Dim i As Integer = 0
            While Not finished AndAlso i < Me._refreshRate
                Me._stopwatch.Start()
                finished = Me.Update()
                Me._stopwatch.[Stop]()
                i += 1
            End While
        End While
        Me._searched = True
        If Me._found Then RaiseEvent Found(Me, New FoundEventArgs)
    End Sub

    Protected Function FindPath() As IEnumerable(Of Node)
        If Not Me._found Then Return Nothing
        Me._stopwatch.Start()
        Dim path As List(Of Node) = New List(Of Node)()
        Dim currentNode As Node = Me._stoxos
        While Not currentNode.Location.Equals(Me._origin.Location)
            path.Add(currentNode)
            currentNode = currentNode.Predecessor
        End While
        Me._stopwatch.[Stop]()
        Me._pathLength = path.Count
        For Each node As Node In path
            node.State = node.NodeState.Path
        Next
        Return path
    End Function

    Private Sub mapReseted(ByVal m As Map, ByVal e As Map.ResetEventArgs)
        Me.Reset()
    End Sub

    Public Overrides Function ToString() As String
        Return Me._title
    End Function

    'Protected Sub Print()
    '    Dim time As Double = Me._stopwatch.Elapsed.TotalSeconds
    '    Dim s As Integer = CInt(time)
    '    Dim cs As Integer = CInt((time * 100.0))
    '    Dim r As Integer = CInt((time * 10000.0)) Mod 100
    '    Dim elapsedTime As String = String.Format("{0:00}:{1:00}.{2:00}", s, cs, r)
    '    Console.WriteLine("-------------------------------")
    '    Console.WriteLine(Me._title)
    '    Console.WriteLine(String.Concat(New Object() {"Elapsed time (s:cs): ", elapsedTime, " (", Me._stopwatch.Elapsed.Ticks, " ticks)"}))
    '    If Me._found Then
    '        Console.WriteLine("        Path length: " + Me._pathLength)
    '    Else
    '        Console.WriteLine("No path found. ")
    '    End If
    '    If Me._found Then
    '        Me._map.Draw(Me._root, Me._target, String.Concat(New Object() {Me._title, " (time (s:cs): ", elapsedTime, ", path length: ", Me._pathLength, ")"}))
    '    Else
    '        Me._map.Draw(Me._root, Me._target, Me._title + " (time (s:cs): " + elapsedTime + ", no path found)")
    '    End If
    'End Sub
End Class
