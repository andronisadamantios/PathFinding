Imports System
Imports System.Collections
Imports System.Collections.Generic

Public Class BreadthFirst
    Inherits PathFindingAlgorithm

    Public Shared ReadOnly SINGLE_INSTANCE As BreadthFirst = New BreadthFirst
    Private Shared ReadOnly BREADTH_FIRST_TITLE As String = "Breadth First Search Algorithm"

    Public Overrides ReadOnly Property Title() As String
        Get
            Return BreadthFirst.BREADTH_FIRST_TITLE
        End Get
    End Property

    Private Sub New()
    End Sub


    Friend Overrides Function getPathFindingExecution(ByVal pfi As IPathFindingInput) As PathFindingAlgorithmsLibrary.PathFindingExecution
        Return New pathFindingExecution(Me, pfi)
    End Function


    Private Class pathFindingExecution
        Inherits PathFindingAlgorithmsLibrary.PathFindingExecution

        Private _queue As Queue(Of Node)
        Sub New(ByVal owner As BreadthFirst, ByVal pfi As IPathFindingInput)
            MyBase.New(owner, pfi)
            Me._queue = New Queue(Of Node)
        End Sub

        Protected Overrides Sub addOrigin(ByVal origin As Node)
            Me._queue.Enqueue(origin)
        End Sub

        Protected Overrides Function doStep() As Boolean
            Dim currentNode As Node = Me._queue.Dequeue
            currentNode.State = Node.NodeState.Visited
            Dim result As Boolean
            If currentNode.Location.Equals(Me._destination.Location) Then
                Me._found = True
                result = True
            Else
                For Each newNode As Node In Me._map.getGeitones(currentNode.Location) _
                                            .Where(Function(n) n.State = Node.NodeState.Unvisited).ToArray
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
