Public Interface ICalcDrawRagionFactory
    Sub setRegionSetting(VerticalAlignFlag As Integer, HorizontalAlignFlag As Integer)
    Function createCalcDrawRegion() As AbstractCalcDrawRegion
End Interface
