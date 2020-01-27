Imports System
Imports System.Collections.Generic
Imports Entities

Public Class Map
    Inherits Entities.Grid(Of Node)

    Public Class ResetEventArgs
        Inherits EventArgs

        Private _full As Boolean
        Public ReadOnly Property Full As Boolean
            Get
                Return Me._full
            End Get
        End Property

        Public Sub New(full As Boolean)
            Me._full = full
        End Sub
    End Class

    Public Event Reseted(ByVal map As Map, ByVal e As Map.ResetEventArgs)

    'Private _isReseted As Boolean



    Public Sub New(rows As Integer, cols As Integer)
        MyBase.New(rows, cols)

        For i As Integer = 0 To Me._matrix.GetUpperBound(0)
            For j As Integer = 0 To Me._matrix.GetUpperBound(1)
                Me._matrix(i, j) = New Node(i, j, Node.NodeState.Unvisited)
            Next
        Next

        Me.TyposGeitonias = Entities.TyposGeitonias.Stavros
    End Sub

    Public Sub Reset()
        For Each lmnt In Me._matrix
            lmnt.Reset()
        Next
        RaiseEvent Reseted(Me, New ResetEventArgs(False))
    End Sub

    Public Sub FullReset()
        For Each lmnt In Me._matrix
            lmnt.FullReset()
        Next
        RaiseEvent Reseted(Me, New ResetEventArgs(True))
    End Sub


    Public Function GetHeuristic(a As Node, b As Node) As Integer
        Dim dRow = CInt(Math.Abs(CInt(a.Location.Row) - CInt(b.Location.Row)))
        Dim dCol = CInt(Math.Abs(CInt(a.Location.Col) - CInt(b.Location.Col)))

        If Me.Loukoumas Then
            Dim dRow2, dCol2 As Integer

            'Dim min, max As Integer
            'min = System.Math.Min(a.Location.Row, b.Location.Row)
            'max = System.Math.Max(a.Location.Row, b.Location.Row)
            'dRow2 = Me._rows - max + min
            'min = System.Math.Min(a.Location.Col, b.Location.Col)
            'max = System.Math.Max(a.Location.Col, b.Location.Col)
            'dCol2 = Me._cols - max + min

            drow2 = Me._rows - dRow
            dCol2 = Me._cols - dCol

            dRow = System.Math.Min(dRow, dRow2)
            dCol = System.Math.Min(dCol, dCol2)
        End If
        Return dRow + dCol
    End Function

    Public Function GetFScore(a As Node, b As Node) As Integer
        Return a.Cost + GetHeuristic(a, b)
    End Function

    'Public Function GetNeighbours(node As Node) As List(Of Node)
    '    Dim neighbours As List(Of Node) = New List(Of Node)()
    '    Me.TryAdd(neighbours, CInt(node.Location.Row) - 1, CInt(node.Location.Col))
    '    Me.TryAdd(neighbours, CInt(node.Location.Row), CInt(node.Location.Col) - 1)
    '    Me.TryAdd(neighbours, CInt(node.Location.Row), CInt(node.Location.Col) + 1)
    '    Me.TryAdd(neighbours, CInt(node.Location.Row) + 1, CInt(node.Location.Col))
    '    Return neighbours
    'End Function

    'Private Sub TryAdd(neighbours As List(Of Node), x As Integer, y As Integer)
    '    If x >= 0 AndAlso y >= 0 AndAlso x <= Me.map.GetUpperBound(0) AndAlso y <= Me.map.GetUpperBound(1) Then
    '        Dim neighbour As Node = Me.map(x, y)
    '        If neighbour.State = Node.NodeState.Unvisited Then
    '            neighbours.Add(neighbour)
    '        End If
    '    End If
    'End Sub



    'Public Sub Draw(root As Node, target As Node, title As String)
    '    SwinGame.ClearScreen(System.Drawing.Color.Black)
    '    For i As Integer = 0 To Me.map.GetLength(0) - 1
    '        For j As Integer = 0 To Me.map.GetLength(1) - 1
    '            Dim colour As System.Drawing.Color = System.Drawing.Color.Black
    '            Dim node As Node = Me.map(i, j)
    '            Select Case AddressOf node.State
    '                Case Node.NodeState.Unvisited
    '                    colour = System.Drawing.Color.Gray
    '                Case Node.NodeState.InFrontier
    '                    colour = System.Drawing.Color.Gold
    '                Case Node.NodeState.Visited
    '                    colour = System.Drawing.Color.LightGray
    '                Case Node.NodeState.Path
    '                    colour = System.Drawing.Color.Blue
    '            End Select
    '            If AddressOf node.Location.Equals(AddressOf root.Location) Then
    '                colour = System.Drawing.Color.Blue
    '            End If
    '            If AddressOf node.Location.Equals(AddressOf target.Location) Then
    '                colour = System.Drawing.Color.Red
    '            End If
    '            SwinGame.FillRectangle(colour, CSng((10 + i * Me.SIZE)), CSng((50 + j * Me.SIZE)), Me.SIZE - 1, Me.SIZE - 1)
    '        Next
    '    Next
    '    SwinGame.DrawText(title, System.Drawing.Color.White, "Arial", 25, SwinGame.PointAt(10.0F, 10.0F))
    '    SwinGame.RefreshScreen()
    'End Sub
    'Public SIZE As Integer = 20
    'CInt((GameMain.WINDOW_SIZE.X - 20.0F)) / Me.SIZE - 1, CInt((GameMain.WINDOW_SIZE.Y - 70.0F)) / Me.SIZE - 1



End Class
