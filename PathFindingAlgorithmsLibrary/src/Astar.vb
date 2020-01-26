Imports System
Imports System.Collections.Generic

Public Class AStar
    Inherits SearchAlgorithm

    Private _open As List(Of Node) = New List(Of Node)()

    Public Sub New()
    End Sub

    Public Sub New(map As Map, r As Integer)
        MyBase.New(map, r)
        Me._title = "A* Search Algorithm"
    End Sub

    Protected Overrides Sub Reset()
        MyBase.Reset()
        Me._open.Clear()
    End Sub

    Protected Overrides Sub Init()
        Me._open.Add(Me._origin)
    End Sub

    Protected Overrides Function Update() As Boolean
        Me._stopwatch.Start()
        Dim currentNode As Node = Me._open(0)
        Dim minfScore As Integer = Me._map.GetFScore(currentNode, Me._stoxos)
        For Each node As Node In Me._open
            Dim fScore As Integer = Me._map.GetFScore(node, Me._stoxos)
            If fScore <= minfScore Then
                currentNode = node
                minfScore = fScore
            End If
        Next
        Me._open.Remove(currentNode)
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
                Me._open.Add(newNode)
            Next
            If Me._open.Count = 0 Then
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
