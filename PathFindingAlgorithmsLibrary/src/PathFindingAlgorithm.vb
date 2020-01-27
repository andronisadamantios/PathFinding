Imports System
Imports System.Collections.Generic
Imports System.Diagnostics

''' <summary>
''' 
''' </summary>
''' <remarks>if origin = destination then path isnot nothing andalso path.length = 0</remarks>
Public MustInherit Class PathFindingAlgorithm

    Public Shared ReadOnly BEST_FIRST As PathFindingAlgorithm = BestFirst.SINGLE_INSTANCE
    Public Shared ReadOnly BREADTH_FIRST As PathFindingAlgorithm = BreadthFirst.SINGLE_INSTANCE
    Public Shared ReadOnly A_STAR As PathFindingAlgorithm = AStar.SINGLE_INSTANCE

    Public MustOverride ReadOnly Property Title() As String
    'Public Overridable ReadOnly Property Title() As String
    '    Get
    '        ' todo: Throw New NotImplementedException("get titles from resources") 
    '    End Get
    'End Property

    Public Sub New()
    End Sub

    Public Function FindPath(ByVal map As Map, ByVal origin As Node, ByVal destination As Node) As Node()
        Return Me.Find(map, origin, destination).Path
    End Function
    Friend Function Find(ByVal map As Map, ByVal origin As Node, ByVal destination As Node) As IPathFindingResult
        Return Me.getPathFindingExecution(PathFindingInput.Get(map, origin, destination)).Result
    End Function
    Friend MustOverride Function getPathFindingExecution(ByVal pfi As IPathFindingInput) As PathFindingAlgorithmsLibrary.PathFindingExecution


    Public Overrides Function ToString() As String
        Return Me.Title
    End Function



End Class
