Imports System


Public Class Node
    Public Enum NodeState
        Impassable
        Unvisited
        InFrontier
        Visited
        Path
    End Enum

    Private _location As Entities.IGridCoordinates
    Private _predecessor As Node
    Private _cumulativeCost As Integer
    Private _state As Node.NodeState

    Public ReadOnly Property Location() As Entities.IGridCoordinates
        Get
            Return Me._location
        End Get
    End Property

    Public Property State() As Node.NodeState
        Get
            Return Me._state
        End Get
        Set(value As Node.NodeState)
            Me._state = value
        End Set
    End Property

    Public Property Predecessor() As Node
        Get
            Return Me._predecessor
        End Get
        Set(value As Node)
            Me._predecessor = value
        End Set
    End Property

    Public Property Cost() As Integer
        Get
            Return Me._cumulativeCost
        End Get
        Set(value As Integer)
            Me._cumulativeCost = value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Sub New(row As Integer, col As Integer, s As Node.NodeState)
        Me._location = New Entities.GridCoordinates(row, col)
        Me._state = s
        Me._predecessor = Nothing
        Me._cumulativeCost = 0
    End Sub

    Public Sub Reset()
        If Me._state <> Node.NodeState.Impassable Then
            Me._state = Node.NodeState.Unvisited
        End If
        Me._predecessor = Nothing
        Me._cumulativeCost = 0
    End Sub

    Public Sub FullReset()
        Me._state = Node.NodeState.Unvisited
        Me._predecessor = Nothing
        Me._cumulativeCost = 0
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0} (state: {1}, cost: {2})", New Object() {Me._location.ToString, Me._state.ToString, Me._cumulativeCost.ToString})
    End Function
End Class
