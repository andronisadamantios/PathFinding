Module Module1

    Friend rows As Integer = 20
    Friend cols As Integer = 40

    Friend gridDefinition As Entities.GridDefinition = New Entities.GridDefinition( _
        Entities.GridSize.Get(rows, cols), _
        Entities.GridTopology.FLAT)


End Module
