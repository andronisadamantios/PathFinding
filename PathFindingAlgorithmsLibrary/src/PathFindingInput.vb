Public Interface IPathFindingInput

    ReadOnly Property Map() As Map
    ReadOnly Property Origin() As Node
    ReadOnly Property Destination() As Node

End Interface
Public Class PathFindingInput
    Implements IPathFindingInput

    Public Shared Function [Get](ByVal map As Map, ByVal origin As Node, ByVal destination As Node) As IPathFindingInput
        Dim map1 = CType(map, IEnumerable(Of Node))
        If Not map1.Contains(origin) AndAlso map1.Contains(destination) Then
            Throw New ArgumentException("given origin and/or destination does not belong to the given map")
        End If
        Return New PathFindingInput(map, origin, destination)
    End Function

    Private _map As Map
    Private _origin As Node
    Private _destination As Node

    Public ReadOnly Property Map() As Map Implements IPathFindingInput.Map
        Get
            Return Me._map
        End Get
    End Property

    Public ReadOnly Property Origin() As Node Implements IPathFindingInput.Origin
        Get
            Return Me._origin
        End Get
    End Property

    Public ReadOnly Property Destination() As Node Implements IPathFindingInput.Destination
        Get
            Return Me._destination
        End Get
    End Property

    Sub New(ByVal map As Map, ByVal origin As Node, ByVal destination As Node)
        Me._map = map
        Me._origin = origin
        Me._destination = destination
    End Sub


End Class
