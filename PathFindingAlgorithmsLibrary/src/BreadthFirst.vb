Imports System
Imports System.Collections
Imports System.Collections.Generic

Public Class BreadthFirst
    Inherits SearchAlgorithm

    Private _open As Queue = New Queue()

    Public Sub New()
    End Sub

    Public Sub New(map As Map, r As Integer)
        MyBase.New(map, r)
        Me._title = "Breadth First Search Algorithm"
    End Sub

    Protected Overrides Sub Reset()
        MyBase.Reset()
        Me._open.Clear()
    End Sub

    Protected Overrides Sub Init()
        Me._open.Enqueue(Me._origin)
    End Sub

    Protected Overrides Function Update() As Boolean
        Dim currentNode As Node = CType(Me._open.Dequeue(), Node)
        currentNode.State = Node.NodeState.Visited
        Dim result As Boolean
        If currentNode.Location.Equals(Me._stoxos.Location) Then
            Me._found = True
            result = True
        Else
            For Each newNode As Node In Me._map.getGeitones(currentNode.Location).Where(Function(n) n.State = Node.NodeState.Unvisited).ToArray
                newNode.State = Node.NodeState.InFrontier
                newNode.Predecessor = currentNode
                Me._open.Enqueue(newNode)
            Next
            If Me._open.Count = 0 Then
                Me._found = False
                result = True
            Else
                result = False
            End If
        End If
        Return result
    End Function
End Class
