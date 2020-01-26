Imports System
Imports System.Collections.Generic

Public Class BestFirst
    Inherits SearchAlgorithm

    Private _open As List(Of Node) = New List(Of Node)()

    Public Sub New()
    End Sub

    Public Sub New(map As Map, r As Integer)
        MyBase.New(map, r)
        Me._title = "Best First Search Algorithm"
    End Sub

    Protected Overrides Sub Reset()
        Me._open.Clear()
        MyBase.Reset()
    End Sub

    Protected Overrides Sub Init()
        Me._open.Add(Me._origin)
    End Sub

    Protected Overrides Function Update() As Boolean
        Me._stopwatch.Start()
        Dim currentNode As Node = Me._open(0)
        Dim minHeuristic As Integer = Me._map.GetHeuristic(currentNode, Me._stoxos)
        For Each node As Node In Me._open
            Dim heuristic As Integer = Me._map.GetHeuristic(node, Me._stoxos)
            If heuristic <= minHeuristic Then
                currentNode = node
                minHeuristic = heuristic
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
