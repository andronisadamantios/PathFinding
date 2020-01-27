
Public Interface IPathFindingResult

    ReadOnly Property PathExists() As Boolean
    ReadOnly Property Path() As Node()
    ReadOnly Property PathLength() As Integer

End Interface

Friend MustInherit Class PathFindingResult
    Implements IPathFindingResult

    Public Shared ReadOnly NO_PATH As IPathFindingResult = PathFindingResult_NoPath.SINGLE_INSTANCE

    Public Shared Function [Get](ByVal path As IEnumerable(Of Node)) As IPathFindingResult
        If path Is Nothing Then
            Return NO_PATH
        Else
            Return New PathFindingResult_PathExists(path)
        End If
    End Function

    Private _durationSearch As TimeSpan?

    Public ReadOnly Property DurationSearch() As TimeSpan?
        Get
            Return Me._durationSearch
        End Get
    End Property

    Public MustOverride ReadOnly Property Path() As Node() Implements IPathFindingResult.Path

    Public MustOverride ReadOnly Property PathExists() As Boolean Implements IPathFindingResult.PathExists

    Public MustOverride ReadOnly Property PathLength() As Integer Implements IPathFindingResult.PathLength

    Sub New()
        Me._durationSearch = Nothing
    End Sub
    Sub New(ByVal durationSearch As TimeSpan?)
        Me.New()
        Me._durationSearch = durationSearch
    End Sub



    Private Class PathFindingResult_NoPath
        Inherits PathFindingResult

        Public Shared ReadOnly SINGLE_INSTANCE As IPathFindingResult = New PathFindingResult_NoPath
        Private Sub New()
        End Sub
        Public Overrides ReadOnly Property Path() As Node()
            Get
                Return New Node() {}
            End Get
        End Property

        Public Overrides ReadOnly Property PathExists() As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property PathLength() As Integer
            Get
                Return 0
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return MyBase.ToString()
        End Function

    End Class

    Private Class PathFindingResult_PathExists
        Inherits PathFindingResult

        Private _path As Node()

        Public Overrides ReadOnly Property PathExists() As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides ReadOnly Property Path() As Node()
            Get
                Return Me._path
            End Get
        End Property

        Public Overrides ReadOnly Property PathLength() As Integer
            Get
                Return Me.Path.Length
            End Get
        End Property

        Sub New(ByVal path As IEnumerable(Of Node), ByVal durationSearch As TimeSpan?)
            MyBase.New(durationSearch)
            Me._path = path.ToArray
        End Sub
        Sub New(ByVal path As IEnumerable(Of Node))
            Me.New(path, Nothing)
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("PATH EXISTS. length: {0}", Me.PathLength)
        End Function

    End Class


End Class
