Public Interface ICalcDrawRegion
    Sub calcDrawRegionByImage(ImageSize As Size, ControlSize As Size)
    Function calcPoints() As PointF()
    Sub setDrawPosition(VerticalAlign As Integer, HorizontalAlign As Integer)
End Interface
