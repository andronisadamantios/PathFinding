Public Class Form1

#Region "color variables"
    Dim brushArxiko As System.Windows.Media.Brush = System.Windows.Media.Brushes.LightGray
    Dim brushToixos As System.Windows.Media.Brush = System.Windows.Media.Brushes.Black
    Dim brushOrigin As System.Windows.Media.Brush = System.Windows.Media.Brushes.Blue
    Dim brushDestination As System.Windows.Media.Brush = System.Windows.Media.Brushes.Red
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


    ' the selected algorithm
    Private _selectedAlgorithm As PathFindingAlgorithmsLibrary.PathFindingAlgorithm

    ' the selected tropos start eketelesh
    Private _selectedTroposStartEktelesh As TroposStartEktelesh

    ' an object that holds the currently selected origin and destination
    Private _selectorOriginDestination As PathFindingAlgorithmsLibrary.SelectorOriginDestination

    ' the abstract map object
    Private WithEvents map As PathFindingAlgorithmsLibrary.Map

    ' the GUI grid
    Private WithEvents gd As wpfGraphics.GridDisplay

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
        Me.TableLayoutPanel1.HorizontalScroll.Enabled = True
        Me.TableLayoutPanel1.HorizontalScroll.Visible = True
        Me.TableLayoutPanel1.VerticalScroll.Enabled = True
        Me.TableLayoutPanel1.VerticalScroll.Visible = True

        ' Add any initialization after the InitializeComponent() call.

        Me.gd = New wpfGraphics.GridDisplay(Module1.rows, Module1.cols)
        Me.gd.Settings.BrushNormal = brushArxiko

        Me.map = New PathFindingAlgorithmsLibrary.Map(Module1.gridDefinition)

        Me._selectorOriginDestination = New PathFindingAlgorithmsLibrary.SelectorOriginDestination(Me.map)

        Me.setElementHostSize()
        Me.ElementHost1.Child = gd.Grid
        Me.gd.Reset()

        Me.tscbAlgos.Items.Add(PathFindingAlgorithmsLibrary.PathFindingAlgorithm.BEST_FIRST)
        Me.tscbAlgos.Items.Add(PathFindingAlgorithmsLibrary.PathFindingAlgorithm.BREADTH_FIRST)
        Me.tscbAlgos.Items.Add(PathFindingAlgorithmsLibrary.PathFindingAlgorithm.A_STAR)
        Me.tscbAlgos.SelectedIndex = 0

        Me.tscbTroposStartEktelesh.Items.Add(Me.tse_clickBtnToRun)
        Me.tscbTroposStartEktelesh.Items.Add(Me.tse_clickMapToRun)
        Me.tscbTroposStartEktelesh.Items.Add(Me.tse_hoverMapToRun)
        Me.tscbTroposStartEktelesh.SelectedIndex = 0

    End Sub


    Private Sub TableLayoutPanel1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TableLayoutPanel1.SizeChanged
        Me.setElementHostSize()
    End Sub

    Private Sub setElementHostSize()
        Dim width As Double = Me.TableLayoutPanel1.Size.Width
        width = 0.95 * width
        Dim height = (CDbl(Module1.rows) / CDbl(Module1.cols)) * width
        Me.ElementHost1.Size = New Size(CInt(Math.Round(width)), CInt(Math.Round(height)))
    End Sub


    Friend Sub Run()
        If Me._selectorOriginDestination.Ready Then
            Me.map.Reset()
            Me.drawPath()
        End If
    End Sub


    Private Sub setOrigin(ByVal p As Entities.IGridCoordinates)
        Dim prevOrigin = Me._selectorOriginDestination.Origin
        If Me._selectorOriginDestination.SetOrigin(p) Then
            If prevOrigin IsNot Nothing Then
                Me.gd.Color(prevOrigin, brushArxiko)
            End If
            Me.gd.Color(p, brushOrigin)
        End If
    End Sub

    Private Sub setDestination(ByVal p As Entities.IGridCoordinates)
        Dim prevDestination = Me._selectorOriginDestination.Destination
        If Me._selectorOriginDestination.SetDestination(p) Then
            If prevDestination IsNot Nothing Then
                Me.gd.Color(prevDestination, brushArxiko)
            End If
            Me.gd.Color(p, brushDestination)
        End If
    End Sub


    Friend Sub drawToixo(ByVal p As Entities.IGridCoordinates, ByVal b As Boolean)
        Dim n = Me.map.Get(p)
        If b Then
            n.State = PathFindingAlgorithmsLibrary.Node.NodeState.Impassable
            Me.gd.Color(p, brushToixos)
        Else
            n.State = PathFindingAlgorithmsLibrary.Node.NodeState.Unvisited
            Me.gd.Color(p, brushArxiko)
        End If
    End Sub

    Private Sub drawPath()
        Dim pfi = CType(Me._selectorOriginDestination, PathFindingAlgorithmsLibrary.IPathFindingInput)
        Dim origin = pfi.Origin
        Dim destination = pfi.Destination
        Dim path = Me._selectedAlgorithm.FindPath(Me.map, origin, destination)

        For Each lmnt In path.Except(New PathFindingAlgorithmsLibrary.Node() {origin, destination})
            Me.gd.Color(lmnt.Location, brushPath)
        Next
    End Sub


    Private Sub map_Reseted(ByVal map As PathFindingAlgorithmsLibrary.Map, ByVal e As PathFindingAlgorithmsLibrary.Map.ResetEventArgs) Handles map.Reseted
        Me.gd.Reset()
        If Not e.Full Then
            For Each lmnt In CType(Me.map, IEnumerable(Of PathFindingAlgorithmsLibrary.Node))
                If lmnt.State = PathFindingAlgorithmsLibrary.Node.NodeState.Impassable Then
                    Me.gd.Color(lmnt.Location, brushToixos)
                Else
                    Me.gd.Color(lmnt.Location, brushArxiko)
                End If
            Next
        End If
        If Me._selectorOriginDestination.Origin IsNot Nothing Then
            Me.gd.Color(Me._selectorOriginDestination.Origin, brushOrigin)
        End If
        If Me._selectorOriginDestination.Destination IsNot Nothing Then
            Me.gd.Color(Me._selectorOriginDestination.Destination, brushDestination)
        End If
    End Sub


#Region "Rectangle Grid Cell Events"

    Private Sub onGridDisplay_CellDown(ByVal rg As wpfGraphics.GridDisplay, ByVal e As wpfGraphics.CoorClickEventArgs) Handles gd.CellDown
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
                Me.setOrigin(e.Coors)
            ElseIf changedButton = Windows.Input.MouseButton.Right Then
                Me.setDestination(e.Coors)
            End If
            Me._selectedTroposStartEktelesh.onCellDown(rg, e)
        End If
    End Sub

    Private Sub onGridDisplay_CellEnter(ByVal rg As wpfGraphics.GridDisplay, ByVal e As wpfGraphics.CoorClickEventArgs) Handles gd.CellEnter
        If Me.IsDrawing Then
            If (Me.bDrawToixo Xor Me.bDrawClear) Then
                Me.drawToixo(e.Coors, Me.bDrawToixo)
            End If
        Else
            If e.MouseEventArgs.LeftButton = Windows.Input.MouseButtonState.Pressed Then
                Me.setOrigin(e.Coors)
            ElseIf e.MouseEventArgs.RightButton = Windows.Input.MouseButtonState.Pressed Then
                Me.setDestination(e.Coors)
            End If
            Me._selectedTroposStartEktelesh.onCellEnter(rg, e)
        End If
    End Sub

    Private Sub onGridDisplay_CellUp(ByVal rg As wpfGraphics.GridDisplay, ByVal e As wpfGraphics.CoorClickEventArgs) Handles gd.CellUp
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
        Me._selectorOriginDestination.ClearOrigin()
        Me._selectorOriginDestination.ClearDestination()
        Me.map.FullReset()
    End Sub

    Private Sub tsBtnWrapBoth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsBtnWrapBoth.CheckedChanged
        Me.map.Definition.Topology.WrapBoth = Me.tsBtnWrapBoth.Checked
    End Sub

    Private Sub tsBtnTyposGeitonias_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsBtnDiagonals.CheckedChanged, tsBtnPlus.CheckedChanged
        If Me.map Is Nothing Then Return

        Dim bDiag = Me.tsBtnDiagonals.Checked
        Dim bPlus = Me.tsBtnPlus.Checked
        If bDiag AndAlso bPlus Then
            Me.map.Definition.TypeNeighbourHood = Entities.GridPattern.SQUARE3X3
        ElseIf bDiag Then
            Me.map.Definition.TypeNeighbourHood = Entities.GridPattern.DIAGONALS
        ElseIf bPlus Then
            Me.map.Definition.TypeNeighbourHood = Entities.GridPattern.PLUS
        Else
            Me.map.Definition.TypeNeighbourHood = Entities.GridPattern.EMPTY
        End If

    End Sub

    Private Sub tscbAlgos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tscbAlgos.SelectedIndexChanged
        If Me.tscbAlgos.SelectedIndex = -1 Then Return
        Me._selectedAlgorithm = CType(Me.tscbAlgos.SelectedItem, PathFindingAlgorithmsLibrary.PathFindingAlgorithm)
    End Sub

    Private Sub tscbTroposStartEktelesh_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tscbTroposStartEktelesh.SelectedIndexChanged
        If Me.tscbTroposStartEktelesh.SelectedIndex = -1 Then Return
        Me._selectedTroposStartEktelesh = CType(CType(sender, ToolStripComboBox).SelectedItem, TroposStartEktelesh)
    End Sub

#End Region

End Class
