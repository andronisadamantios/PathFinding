MustInherit Class PathFinding

    Private _owner As PathFindingAlgorithm
    Private _stopwatch As Stopwatch
    Private _map As Map
    Private _origin As Node
    Private _destination As Node
    Private _queue As Queue(Of Node)
    Private _result As IPathFindingResult
    Private _finished As Boolean
    Private _found As Boolean

    Public ReadOnly Property Origin() As Node
        Get
            Return Me._origin
        End Get
    End Property

    Public ReadOnly Property Destination() As Node
        Get
            Return Me._destination
        End Get
    End Property

    Public ReadOnly Property Result() As IPathFindingResult
        Get
            If Me._result Is Nothing Then
                Me.Run()
            End If
            Return Me._result
        End Get
    End Property

    Sub New(ByVal owner As PathFindingAlgorithm, ByVal map As Map, ByVal origin As Node, ByVal destination As Node)
        Me._owner = owner
        Me._map = map
        Me._origin = origin
        Me._destination = destination
    End Sub

    Protected Overridable Sub reset()
        Me._stopwatch.Reset()
        Me._found = False
        Me._hasRun = False
        Me._path = Nothing
    End Sub

    Friend Sub Run()
        Me._map.Reset()
        Me._queue = New Queue(Of Node)
        Me._queue.Enqueue(Me._origin)
        Dim finished As Boolean = False
        Me._stopwatch.Start()
        While Not finished
            finished = Me.doStep()
        End While
        Me._stopwatch.Stop()

        If Me._found Then
            Me._result = PathFindingResult.Get(Me.createPath)
        Else
            Me._result = PathFindingResult.NO_PATH
        End If
    End Sub

    Protected MustOverride Function doStep() As Boolean


    Protected Function createPath() As IEnumerable(Of Node)
        If Not Me._found Then
            Return Nothing
        End If
        Me._stopwatch.Start()
        Dim path As Stack(Of Node) = New Stack(Of Node)()
        Dim currentNode As Node = Me._destination
        While Not currentNode.Location.Equals(Me._origin.Location)
            path.Push(currentNode)
            currentNode = currentNode.Predecessor
        End While
        Me._stopwatch.Stop()
        For Each node As Node In path
            node.State = node.NodeState.Path
        Next
        Return path
    End Function

End Class