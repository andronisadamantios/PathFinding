Public Interface IPathFindingInput

    ReadOnly Property Map() As Map
    ReadOnly Property Origin() As Node
    ReadOnly Property Destination() As Node

End Interface

Public Class SelectorOriginDestination
    Implements IPathFindingInput



    Private _map As Map
    Private _origin As Entities.IGridCoordinates
    Private _destination As Entities.IGridCoordinates
    Private _pfi As IPathFindingInput

    Private ReadOnly Property pfi() As IPathFindingInput
        Get
            If Me._pfi Is Nothing Then
                If Not Me.Ready Then
                    Throw New InvalidOperationException("origin and/or destination not set")
                End If
                Me._pfi = PathFindingInput.Get(Me._map, Me._map.Get(Me._origin), Me._map.Get(Me._destination))
            End If
            Return Me._pfi
        End Get
    End Property

    Public Property Origin() As Entities.IGridCoordinates
        Get
            Return Me._origin
        End Get
        Set(ByVal value As Entities.IGridCoordinates)
            Me.SetOrigin(value)
        End Set
    End Property

    Public Property Destination() As Entities.IGridCoordinates
        Get
            Return Me._destination
        End Get
        Set(ByVal value As Entities.IGridCoordinates)
            Me.SetDestination(value)
        End Set
    End Property

    Public ReadOnly Property Map() As Map Implements IPathFindingInput.Map
        Get
            Return Me._map
        End Get
    End Property

    Private ReadOnly Property Origin1() As Node Implements IPathFindingInput.Origin
        Get
            Return Me.pfi.Origin
        End Get
    End Property

    Private ReadOnly Property Destination1() As Node Implements IPathFindingInput.Destination
        Get
            Return Me.pfi.Destination
        End Get
    End Property

    Public ReadOnly Property IsOriginSet() As Boolean
        Get
            Return Me._origin IsNot Nothing
        End Get
    End Property

    Public ReadOnly Property IsDestinationSet() As Boolean
        Get
            Return Me._destination IsNot Nothing
        End Get
    End Property

    Public ReadOnly Property Ready() As Boolean
        Get
            Return Me.IsOriginSet AndAlso Me.IsDestinationSet
        End Get
    End Property



    Sub New(ByVal map As Map)
        Me._map = map
    End Sub



    Public Function SetOrigin(ByVal p As Entities.IGridCoordinates) As Boolean
        If Me._map.Definition.IsValidCoor(p) AndAlso Not Object.Equals(Me._origin, p) Then
            Me._origin = p
            Me._pfi = Nothing
            Return True
        Else
            Return False
        End If
    End Function
    Public Function SetDestination(ByVal p As Entities.IGridCoordinates) As Boolean
        If Me._map.Definition.IsValidCoor(p) AndAlso Not Object.Equals(Me._destination, p) Then
            Me._destination = p
            Me._pfi = Nothing
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub ClearOrigin()
        Me._origin = Nothing
        Me._pfi = Nothing
    End Sub

    Public Sub ClearDestination()
        Me._destination = Nothing
        Me._pfi = Nothing
    End Sub


End Class

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
