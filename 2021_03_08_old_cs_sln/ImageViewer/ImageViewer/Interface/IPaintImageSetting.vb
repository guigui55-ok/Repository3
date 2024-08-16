Public Interface IPaintImageSetting
    'Sub setPaintImageSetting()
    Sub setPath(path As String)
    Sub setImage(argImage As Image)
    Sub setImageSize(size As Point)
    Sub setInnerFrameSize(size As Size)
    Sub setAutorFrameSize(size As Size)

    Function getInnerFrameSize() As Point
    Function getDrawRegionAsRectangle() As Rectangle
    Function getDrawRegionAsPoints() As PointF()
    Function getReadImageRegion() As Rectangle
    Function getImageSize() As Size
    Function InnerFrameSizeIsZero() As Boolean

    Sub setDrawRegion(argRegion As Rectangle)
    Sub setReadImageRegion(argRegion As Rectangle)
    Sub calcDrawRegion()
    Sub calcDrawRegion(argControlSize As Size, argImageSize As Size)
    Sub calcRegionToReadAndDraw()
    Sub calcRegionDefaultToReadAndDraw()

    Sub setPosition(argVarticalPosition As Integer, argHorizontalPosition As Integer)

    Sub writeConsoleDrawPoints()

End Interface
