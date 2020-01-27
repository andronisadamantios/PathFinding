MustInherit Class PathFindingExecution

    Private _owner As PathFindingAlgorithm
    Private _stopwatch As Stopwatch

    Protected _map As Map
    Private _origin As Node
    Protected _destination As Node

    Private _finished As Boolean
    Protected _found As Boolean
    Private _result As IPathFindingResult

    Friend ReadOnly Property Result() As IPathFindingResult
        Get
            If Me._result Is Nothing Then
                Me.Run()
            End If
            Return Me._result
        End Get
    End Property

    Sub New(ByVal owner As PathFindingAlgorithm, ByVal pfi As IPathFindingInput)
        Me._owner = owner
        Me._map = pfi.Map
        AddHandler Me._map.Reseted, AddressOf mapReseted
        Me._origin = pfi.Origin
        Me._destination = pfi.Destination
        Me._stopwatch = New Stopwatch
    End Sub

    Private Sub mapReseted(ByVal m As Map, ByVal e As Map.ResetEventArgs)
        Me.reset()
    End Sub

    Protected Overridable Sub reset()
        Me._found = False
        Me._finished = False
        Me._stopwatch.Reset()
    End Sub

    Protected MustOverride Sub addOrigin(ByVal origin As Node)

    Friend Sub Run()
        Me._map.Reset()
        Me.addOrigin(Me._origin)

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