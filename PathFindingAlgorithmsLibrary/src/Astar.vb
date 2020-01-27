Imports System
Imports System.Collections.Generic

Public Class AStar
    Inherits PathFindingAlgorithm

    Public Shared ReadOnly SINGLE_INSTANCE As AStar = New AStar
    Private Shared ReadOnly A_STAR_TITLE As String = "A* Search Algorithm"

    Public Overrides ReadOnly Property Title() As String
        Get
            Return AStar.A_STAR_TITLE
        End Get
    End Property

    Private Sub New()
    End Sub


    Friend Overrides Function getPathFindingExecution(ByVal pfi As IPathFindingInput) As PathFindingAlgorithmsLibrary.PathFindingExecution
        Return New pathFindingExecution(Me, pfi)
    End Function


    Private Class pathFindingExecution
        Inherits PathFindingAlgorithmsLibrary.PathFindingExecution

        Private _list As List(Of Node)
        Sub New(ByVal owner As AStar, ByVal pfi As IPathFindingInput)
            MyBase.New(owner, pfi)
            Me._list = New List(Of Node)
        End Sub

        Protected Overrides Sub addOrigin(ByVal origin As Node)
            Me._list.Add(origin)
        End Sub

        Protected Overrides Function doStep() As Boolean
            Dim currentNode As Node = Me._list.Item(0)
            Dim minfScore As Integer = Me._map.GetFScore(currentNode, Me._destination)
            For Each node As Node In Me._list
                Dim fScore As Integer = Me._map.GetFScore(node, Me._destination)
                If fScore <= minfScore Then
                    currentNode = node
                    minfScore = fScore
                End If
            Next
            Me._list.Remove(currentNode)
            currentNode.State = Node.NodeState.Visited
            Dim result As Boolean
            If currentNode.Location.Equals(Me._destination.Location) Then
                Me._found = True
                result = True
            Else
                For Each newNode As Node In Me._map.getGeitones(currentNode.Location) _
                                            .Where(Function(n) n.State = Node.NodeState.Unvisited).ToArray
                    newNode.State = Node.NodeState.InFrontier
                    newNode.Cost = currentNode.Cost + 1
                    newNode.Predecessor = currentNode
                    Me._list.Add(newNode)
                Next
                If Me._list.Count = 0 Then
                    Me._found = False
                    result = True
                Else
                    result = False
                End If
            End If
            Return result
        End Function


    End Class

End Class
