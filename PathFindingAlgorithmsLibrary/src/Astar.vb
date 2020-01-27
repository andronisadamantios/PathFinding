Imports System
Imports System.Collections.Generic

Public Class AStar
    Inherits PathFindingAlgorithm

    Private Shared ReadOnly A_STAR_TITLE As String = "A* Search Algorithm"

    Public ReadOnly Property Title() As String
        Get
            Return AStar.A_STAR_TITLE
        End Get
    End Property

    Public Sub New(ByVal map As Map)
        MyBase.New(map)
    End Sub

    Protected Overrides Sub reset()
        MyBase.reset()
        Me._queue.Clear()
    End Sub


    Protected Overrides Function update() As Boolean
        Me._stopwatch.Start()
        Dim currentNode As Node = Me._queue(0)
        Dim minfScore As Integer = Me._map.GetFScore(currentNode, Me._stoxos)
        For Each node As Node In Me._queue
            Dim fScore As Integer = Me._map.GetFScore(node, Me._stoxos)
            If fScore <= minfScore Then
                currentNode = node
                minfScore = fScore
            End If
        Next
        Me._queue.Remove(currentNode)
        currentNode.State = Node.NodeState.Visited
        Dim result As Boolean
        If currentNode.Location.Equals(Me._stoxos.Location) Then
            Me._found = True
            result = True
        Else
            For Each newNode As Node In Me._map.getGeitones(currentNode.Location).Where(Function(n) n.State = Node.NodeState.Unvisited).ToArray
                newNode.State = Node.NodeState.InFrontier
                newNode.Cost = currentNode.Cost + 1
                newNode.Predecessor = currentNode
                Me._queue.Add(newNode)
            Next
            If Me._queue.Count = 0 Then
                Me._found = False
                result = True
            Else
                Me._stopwatch.[Stop]()
                result = False
            End If
        End If
        Return result
    End Function
End Class
