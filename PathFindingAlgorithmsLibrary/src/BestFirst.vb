Imports System
Imports System.Collections.Generic

Public Class BestFirst
    Inherits PathFindingAlgorithm

    Public Shared ReadOnly SINGLE_INSTANCE As BestFirst = New BestFirst
    Private Shared ReadOnly BEST_FIRST_TITLE As String = "Best First Search Algorithm"

    Public Overrides ReadOnly Property Title() As String
        Get
            Return BestFirst.BEST_FIRST_TITLE
        End Get
    End Property

    Private Sub New()
    End Sub




    Friend Overrides Function getPathFindingExecution(ByVal pfi As IPathFindingInput) As PathFindingAlgorithmsLibrary.PathFindingExecution
        Return New pathFindingExecution(Me, pfi)
    End Function

    Private Class pathFindingExecution
        Inherits PathFindingAlgorithmsLibrary.PathFindingExecution

        Sub New(ByVal owner As BestFirst, ByVal pfi As IPathFindingInput)
            MyBase.New(owner, pfi)
        End Sub

        Protected Overrides Function doStep() As Boolean
            Dim currentNode As Node = Me._queue.Dequeue
            Dim minHeuristic As Integer = Me._map.GetHeuristic(currentNode, Me._destination)
            For Each node As Node In Me._queue
                Dim heuristic As Integer = Me._map.GetHeuristic(node, Me._destination)
                If heuristic <= minHeuristic Then
                    currentNode = node
                    minHeuristic = heuristic
                End If
            Next
            currentNode.State = Node.NodeState.Visited
            Dim result As Boolean
            If currentNode.Location.Equals(Me._destination.Location) Then
                Me._found = True
                result = True
            Else
                For Each newNode As Node In Me._map.getGeitones(currentNode.Location).Where(Function(n) n.State = Node.NodeState.Unvisited).ToArray
                    newNode.State = Node.NodeState.InFrontier
                    newNode.Predecessor = currentNode
                    Me._queue.Enqueue(newNode)
                Next
                If Me._queue.Count = 0 Then
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
