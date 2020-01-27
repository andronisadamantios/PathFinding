Imports System
Imports System.Collections
Imports System.Collections.Generic

Public Class BreadthFirst
    Inherits PathFindingAlgorithm

    Private Shared ReadOnly BREADTH_FIRST_TITLE As String = "Breadth First Search Algorithm"

    Public ReadOnly Property Title() As String
        Get
            Return BreadthFirst.BREADTH_FIRST_TITLE
        End Get
    End Property

    Public Sub New()
    End Sub

    Public Sub New(ByVal map As Map)
        MyBase.New(map)
    End Sub

    Protected Overrides Sub reset()
        MyBase.reset()
        Me._queue.Clear()
    End Sub



    Protected Overrides Function update() As Boolean
        Dim currentNode As Node = Me._queue.Dequeue
        currentNode.State = Node.NodeState.Visited
        Dim result As Boolean
        If currentNode.Location.Equals(Me._stoxos.Location) Then
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

    Protected Overloads Overrides Function update(ByVal map As Map, ByVal queue As System.Collections.Generic.Queue(Of Node), ByVal origin As Node, ByVal destination As Node) As Boolean

    End Function
End Class
