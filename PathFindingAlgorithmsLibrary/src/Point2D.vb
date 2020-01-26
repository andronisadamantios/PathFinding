Public Structure Point2D
    Private _row As Integer
    Private _col As Integer

    Public ReadOnly Property Row As Integer
        Get
            Return Me._row
        End Get
    End Property
    Public ReadOnly Property Col As Integer
        Get
            Return Me._col
        End Get
    End Property


    Public Sub New(ByVal row As Integer, ByVal col As Integer)
        Me._row = row
        Me._col = col
    End Sub

    Public Function GetNewCoor(ByVal dRow As Integer, ByVal dCol As Integer) As Point2D
        Return New Point2D(Me._row + dRow, Me._col + dCol)
    End Function

    Public Shared Function GetRandomCoordiante(ByVal a As UInt16, ByVal b As UInt16, ByVal c As UInt16, ByVal d As UInt16) As Point2D
        'Return New Point2D(CType(Random.Next(a, b + 1), UInt16), CType(Random.Next(c, d + 1), UInt16))
    End Function

End Structure