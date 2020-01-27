Imports System
Imports System.Collections.Generic
Imports System.Diagnostics

''' <summary>
''' 
''' </summary>
''' <remarks>if origin = destination then path isnot nothing andalso path.length = 0</remarks>
Public MustInherit Class PathFindingAlgorithm

    Public Class FoundEventArgs
        Inherits EventArgs

        Public Sub New()

        End Sub
    End Class

    Public Overridable ReadOnly Property Title() As String
        Get
            ' todo: Throw New NotImplementedException("get titles from resources") 
        End Get
    End Property

    Public Sub New()
        Me._stopwatch = New Stopwatch
    End Sub

    Public Sub New(ByVal map As Map)
        Me.New()
        Me._map = map
        AddHandler Me._map.Reseted, AddressOf mapReseted
    End Sub

    Private Sub checkCoor(ByVal row As Integer, ByVal col As Integer)
        If Me._map.RowsCount - 1 < row Or Me._map.ColsCount - 1 < col Then Throw New ArgumentOutOfRangeException
        If Me._map.Get(row, col).State = Node.NodeState.Impassable Then Throw New InvalidOperationException
    End Sub
    Private Function isCoorValid(ByVal row As Integer, ByVal col As Integer) As Boolean
        Return Not (Me._map.RowsCount - 1 < row Or Me._map.ColsCount - 1 < col) And Not (Me._map.Get(row, col).State = Node.NodeState.Impassable)
    End Function

    Public Function FindPath(ByVal map As Map, ByVal origin As Node, ByVal destination As Node) As Node()
        Return Me.Find(map, origin, destination).Path
    End Function
    Friend Function Find(ByVal map As Map, ByVal origin As Node, ByVal destination As Node) As IPathFindingResult
        Return Me.Find(PathFindingInput.Get(map, origin, destination))
    End Function
    Friend MustOverride Function Find(ByVal pfi As IPathFindingInput) As IPathFindingResult


    Private Sub mapReseted(ByVal m As Map, ByVal e As Map.ResetEventArgs)
        Me.reset()
    End Sub

    Public Overrides Function ToString() As String
        Return Me.Title
    End Function



End Class
