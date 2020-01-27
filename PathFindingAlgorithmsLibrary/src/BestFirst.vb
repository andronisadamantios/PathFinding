Imports System
Imports System.Collections.Generic

Public Class BestFirst
    Inherits PathFindingAlgorithm

    Private Shared ReadOnly BEST_FIRST_TITLE As String = "Best First Search Algorithm"

    Public Overrides ReadOnly Property Title() As String
        Get
            Return BestFirst.BEST_FIRST_TITLE
        End Get
    End Property
    Public Sub New()
    End Sub

    Public Sub New(ByVal map As Map)
        MyBase.New(map)
    End Sub

    Protected Overrides Sub reset()
        Me._queue.Clear()
        MyBase.reset()
    End Sub


    Protected Overrides Function update(ByVal map As Map, ByVal queue As Queue(Of Node), ByVal origin As Node, ByVal destination As Node) As Boolean
        Dim currentNode As Node = Me._queue.Dequeue
        Dim minHeuristic As Integer = map.GetHeuristic(currentNode, destination)
        For Each node As Node In Me._queue
            Dim heuristic As Integer = map.GetHeuristic(node, destination)
            If heuristic <= minHeuristic Then
                currentNode = node
                minHeuristic = heuristic
            End If
        Next
        currentNode.State = Node.NodeState.Visited
        Dim result As Boolean
        If currentNode.Location.Equals(destination.Location) Then
            Me._found = True
            result = True
        Else
            For Each newNode As Node In map.getGeitones(currentNode.Location).Where(Function(n) n.State = Node.NodeState.Unvisited).ToArray
                newNode.State = Node.NodeState.InFrontier
                newNode.Predecessor = currentNode
                Me._queue.Add(newNode)
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
