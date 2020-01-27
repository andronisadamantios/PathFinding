Public Class Form1

    Private Shared s As Screen
    Dim cols, rows As Integer

#Region "color variables"
    Dim brushArxiko As System.Windows.Media.Brush = System.Windows.Media.Brushes.LightGray
    Dim brushToixos As System.Windows.Media.Brush = System.Windows.Media.Brushes.Black
    Dim brushOrigin As System.Windows.Media.Brush = System.Windows.Media.Brushes.Blue
    Dim brushStoxos As System.Windows.Media.Brush = System.Windows.Media.Brushes.Red
    Dim brushPath As System.Windows.Media.Brush = System.Windows.Media.Brushes.Green
#End Region

#Region "path-finding algorithms"
    Private Shared ReadOnly BREADTH_FIRST As PathFindingAlgorithmsLibrary.PathFindingAlgorithm
    Private Shared ReadOnly BEST_FIRST As PathFindingAlgorithmsLibrary.PathFindingAlgorithm
    Private Shared ReadOnly A_STAR As PathFindingAlgorithmsLibrary.PathFindingAlgorithm
#End Region

#Region "tropoi start eketelesh"
    Private ReadOnly tse_clickBtnToRun As TroposStartEktelesh = New TroposStartEktelesh_clickBtnToRun(Me)
    Private ReadOnly tse_clickMapToRun As TroposStartEktelesh = New TroposStartEktelesh_clickMapToRun(Me)
    Private ReadOnly tse_hoverMapToRun As TroposStartEktelesh = New TroposStartEktelesh_hoverMapToRun(Me)
#End Region

#Region "variables that control whether to draw or clear toixous while in drawing mode"
    Private bDrawToixo As Boolean
    Private bDrawClear As Boolean
#End Region

    ' the GUI grid
    Private WithEvents rg As wpfGraphics.RectangleGrid

    ' the abstract map object
    Private WithEvents map As PathFindingAlgorithmsLibrary.Map

    ' the selected algorithm
    Private WithEvents sa As PathFindingAlgorithmsLibrary.PathFindingAlgorithm

    ' the selected tropos start eketelesh
    Private _troposStartEktelesh As TroposStartEktelesh

    ' is in draw mode
    Friend ReadOnly Property IsDrawing() As Boolean
        Get
            Return _
                Me.tsbtnDrawToixous.Checked _
                Or _
                System.Windows.Input.Keyboard.IsKeyDown(Windows.Input.Key.LeftCtrl) _
                Or _
                System.Windows.Input.Keyboard.IsKeyDown(Windows.Input.Key.RightCtrl)
        End Get
    End Property


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.Panel1.HorizontalScroll.Enabled = True
        Me.Panel1.HorizontalScroll.Visible = True
        Me.Panel1.VerticalScroll.Enabled = True
        Me.Panel1.VerticalScroll.Visible = True

        ' Add any initialization after the InitializeComponent() call.
        Me.rows = 20 : Me.cols = 40

        Me.rg = New wpfGraphics.RectangleGrid(rows, cols)
        Me.rg.brushStart = brushArxiko
        Me.rg.Reset()

        Me.map = New PathFindingAlgorithmsLibrary.Map(rows, cols)

        Me.setElementHostSize()
        Me.ElementHost1.Child = rg.Grid

        Me.tscbAlgos.Items.Add(New PathFindingAlgorithmsLibrary.BreadthFirst(map))
        Me.tscbAlgos.Items.Add(New PathFindingAlgorithmsLibrary.BestFirst(map))
        Me.tscbAlgos.Items.Add(New PathFindingAlgorithmsLibrary.AStar(map))
        Me.tscbAlgos.SelectedIndex = 0

        Me.tscbTroposStartEktelesh.Items.Add(Me.tse_clickBtnToRun)
        Me.tscbTroposStartEktelesh.Items.Add(Me.tse_clickMapToRun)
        Me.tscbTroposStartEktelesh.Items.Add(Me.tse_hoverMapToRun)
        Me.tscbTroposStartEktelesh.SelectedIndex = 0

    End Sub


    Private Sub setElementHostSize()
        s = Screen.FromControl(Me)
        Dim s1 = System.Drawing.Size.Subtract(s.WorkingArea.Size, New Size(10, 10))
        Dim dx = CInt(System.Math.Floor(CDbl(s1.Width) / CDbl(cols)))
        Dim dy = CInt(System.Math.Floor(CDbl(s1.Height) / CDbl(rows)))
        Dim d = System.Math.Min(dx, dy)
        Me.ElementHost1.Size = New Size(cols * d, rows * d)
    End Sub

    Friend Sub Run()
        Me.map.Reset()
        If ((Me.sa.Origin IsNot Nothing) And (Me.sa.Stoxos IsNot Nothing)) Then
            Me.drawPath(Me.sa)
        End If
    End Sub

    Private Sub setOrigin(ByVal p As Entities.IGridCoordinates)
        If IsDrawing Then Return
        Dim prevOrigin = Me.sa.Origin
        If Me.sa.SetOrigin(p.Row, p.Col) Then
            If prevOrigin IsNot Nothing Then Me.rg.Color(prevOrigin.Location, brushArxiko)
            Me.rg.Color(p, brushOrigin)
        End If
    End Sub
    Private Sub setStoxos(ByVal p As Entities.IGridCoordinates)
        Dim prevStoxos = Me.sa.Stoxos
        If Me.sa.SetStoxos(p.Row, p.Col) Then
            If prevStoxos IsNot Nothing Then Me.rg.Color(prevStoxos.Location, brushArxiko)
        End If
        Me.rg.Color(p, brushStoxos)
    End Sub


    Friend Sub drawToixo(ByVal p As Entities.IGridCoordinates, ByVal b As Boolean)
        Dim n = Me.map.Get(p)
        If b Then
            n.State = PathFindingAlgorithmsLibrary.Node.NodeState.Impassable
            Me.rg.Color(p, brushToixos)
        Else
            n.State = PathFindingAlgorithmsLibrary.Node.NodeState.Unvisited
            Me.rg.Color(p, brushArxiko)
        End If
    End Sub
    Private Sub drawPath(ByVal sa As PathFindingAlgorithmsLibrary.PathFindingAlgorithm)
        If sa.Path Is Nothing Then Return
        For Each lmnt In sa.Path.Except(New PathFindingAlgorithmsLibrary.Node() {Me.sa.Origin, Me.sa.Stoxos})
            Me.rg.Color(lmnt.Location, brushPath)
        Next
    End Sub

    Private Sub map_Reseted(ByVal map As PathFindingAlgorithmsLibrary.Map, ByVal e As PathFindingAlgorithmsLibrary.Map.ResetEventArgs) Handles map.Reseted
        Me.rg.Reset()
        If Not e.Full Then
            For Each lmnt In CType(Me.map, IEnumerable(Of PathFindingAlgorithmsLibrary.Node))
                If lmnt.State = PathFindingAlgorithmsLibrary.Node.NodeState.Impassable Then
                    Me.rg.Color(lmnt.Location, brushToixos)
                Else
                    Me.rg.Color(lmnt.Location, brushArxiko)
                End If
            Next
        End If
        If Me.sa.Origin IsNot Nothing Then Me.rg.Color(Me.sa.Origin.Location, brushOrigin)
        If Me.sa.Stoxos IsNot Nothing Then Me.rg.Color(Me.sa.Stoxos.Location, brushStoxos)
    End Sub

#Region "Rectangle Grid Cell Events"

    Private Sub onRectangleGrid_CellDown(ByVal rg As wpfGraphics.RectangleGrid, ByVal e As wpfGraphics.CoorClickEventArgs) Handles rg.CellDown
        Dim changedButton = e.MouseButtonEventArgs.ChangedButton

        If Me.IsDrawing Then
            If changedButton = Windows.Input.MouseButton.Left Then
                Me.bDrawToixo = True
                Me.bDrawClear = False
            ElseIf changedButton = Windows.Input.MouseButton.Right Then
                Me.bDrawToixo = False
                Me.bDrawClear = True
            End If
        Else
            If changedButton = Windows.Input.MouseButton.Left Then
                Me.setOrigin(e.Coor)
            ElseIf changedButton = Windows.Input.MouseButton.Right Then
                Me.setStoxos(e.Coor)
            End If
            Me._troposStartEktelesh.onCellDown(rg, e)
        End If
    End Sub

    Private Sub onRectangleGrid_CellEnter(ByVal rg As wpfGraphics.RectangleGrid, ByVal e As wpfGraphics.CoorClickEventArgs) Handles rg.CellEnter
        If Me.IsDrawing Then
            If (Me.bDrawToixo Xor Me.bDrawClear) Then
                Me.drawToixo(e.Coor, Me.bDrawToixo)
            End If
        Else
            If e.MouseEventArgs.LeftButton = Windows.Input.MouseButtonState.Pressed Then
                Me.setOrigin(e.Coor)
            ElseIf e.MouseEventArgs.RightButton = Windows.Input.MouseButtonState.Pressed Then
                Me.setStoxos(e.Coor)
            End If
            Me._troposStartEktelesh.onCellEnter(rg, e)
        End If
    End Sub

    Private Sub onRectangleGrid_CellUp(ByVal rg As wpfGraphics.RectangleGrid, ByVal e As wpfGraphics.CoorClickEventArgs) Handles rg.CellUp
        Me.bDrawToixo = False
        Me.bDrawClear = False
    End Sub

#End Region

#Region "UI handlers"

    Private Sub btnRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRun.Click
        Me.Run()
    End Sub

    Private Sub tsBtnClearPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsBtnClearPath.Click
        Me.map.Reset()
    End Sub

    Private Sub tsBtnClearOla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsBtnClearOla.Click
        Me.sa.ClearOrigin()
        Me.sa.ClearStoxos()
        Me.map.FullReset()
    End Sub

    Private Sub tsBtnLoukoumas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsBtnLoukoumas.CheckedChanged
        Me.map.Loukoumas = Me.tsBtnLoukoumas.Checked
    End Sub

    Private Sub tsBtnCrossStavros_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsBtnCross.CheckedChanged, tsBtnStavros.CheckedChanged
        If Me.map Is Nothing Then Return
        Me.map.TyposGeitonias = _
            CType( _
                CInt(Math.Abs(CInt(Me.tsBtnCross.Checked))) * CInt(Entities.TyposGeitonias.Diagonia) _
                Or _
                CInt(Math.Abs(CInt(Me.tsBtnStavros.Checked))) * CInt(Entities.TyposGeitonias.Stavros) _
            , Entities.TyposGeitonias)
    End Sub

    Private Sub tscbAlgos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tscbAlgos.SelectedIndexChanged
        If Me.tscbAlgos.SelectedIndex = -1 Then Return
        Me.sa = CType(Me.tscbAlgos.SelectedItem, PathFindingAlgorithmsLibrary.PathFindingAlgorithm)
    End Sub

    Private Sub tscbTroposStartEktelesh_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tscbTroposStartEktelesh.SelectedIndexChanged
        If Me.tscbTroposStartEktelesh.SelectedIndex = -1 Then Return
        Me._troposStartEktelesh = CType(CType(sender, ToolStripComboBox).SelectedItem, TroposStartEktelesh)
    End Sub

#End Region


End Class
