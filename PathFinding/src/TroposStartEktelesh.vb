
''' <summary>
''' μια κλαση που καθοριζει πως θα ξεκινησει η ευρεση διαδρομης και απεικονισει στο GUI
''' a class that defines how the execution of the pathFindingAlgorithm and the display of the result to the GUI will commence
''' </summary>
''' <remarks></remarks>
Public MustInherit Class TroposStartEktelesh

    Protected _owner As Form1


    Sub New(ByVal owner As Form1)
        Me._owner = owner
    End Sub

    Protected MustOverride ReadOnly Property Title() As String

    Friend Overridable Sub onCellDown(ByVal rg As wpfGraphics.GridDisplay, ByVal e As wpfGraphics.CoorClickEventArgs)

    End Sub
    Friend Overridable Sub onCellEnter(ByVal rg As wpfGraphics.GridDisplay, ByVal e As wpfGraphics.CoorClickEventArgs)

    End Sub

    Public Overrides Function ToString() As String
        Return Me.Title
    End Function
End Class


''' <summary>
''' the execution will only start when the user explicitly press the button
''' </summary>
''' <remarks></remarks>
Class TroposStartEktelesh_clickBtnToRun
    Inherits TroposStartEktelesh

    Protected Overrides ReadOnly Property Title() As String
        Get
            Return "Click Button to Run"
        End Get
    End Property

    Sub New(ByVal owner As Form1)
        MyBase.New(owner)
    End Sub


End Class

''' <summary>
''' the execution happens every time the user changes origin and destination by clicking on the map
''' </summary>
''' <remarks></remarks>
Class TroposStartEktelesh_clickMapToRun
    Inherits TroposStartEktelesh

    Protected Overrides ReadOnly Property Title() As String
        Get
            Return "Click Map to Run"
        End Get
    End Property

    Sub New(ByVal owner As Form1)
        MyBase.New(owner)
    End Sub

    Friend Overrides Sub onCellDown(ByVal rg As wpfGraphics.GridDisplay, ByVal e As wpfGraphics.CoorClickEventArgs)
        Me._owner.Run()
    End Sub

End Class

''' <summary>
''' the execution happens and the GUI updates as the mouse hovers over the map
''' </summary>
''' <remarks></remarks>
Class TroposStartEktelesh_hoverMapToRun
    Inherits TroposStartEktelesh

    Protected Overrides ReadOnly Property Title() As String
        Get
            Return "Hover Map to Run"
        End Get
    End Property

    Sub New(ByVal owner As Form1)
        MyBase.New(owner)
    End Sub

    Friend Overrides Sub onCellDown(ByVal rg As wpfGraphics.GridDisplay, ByVal e As wpfGraphics.CoorClickEventArgs)
        Me._owner.Run()
    End Sub

    Friend Overrides Sub onCellEnter(ByVal rg As wpfGraphics.GridDisplay, ByVal e As wpfGraphics.CoorClickEventArgs)
        Me._owner.Run()
    End Sub


End Class