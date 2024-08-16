Public Interface IControlForPaintSetting
    Sub setInnerControlLocation(argLocation As Point)
    Sub setInnerControlSize(argSize As Size)

    Function getInnerControlLocation() As Point
    Function getInnerControlSize() As Size

    Sub SetInnerSizeByCalcSizeByImageRaito(argAutorControlSize As Size, MatchFlaga As Integer)
    Sub setInnerSizeByCalcSizeForFitOuterSize(argOuterSize As Size)
    Sub SetInnerLocationByCalcLocation(argOuterSize As Size, HorizontalLocationFlag As Integer, VerticalLocationFlag As Integer)
    Sub SetInnerLocationByCalcLocationForWindowResize(argOuterSize As Size)
    Sub resetLocationWhenPictureBoxGreatherThanPanel(argOuterSize As Size)
    Sub resetLocationWhenPictureBoxGreatherThanPanelByMousePointAndPictureBoxSize(
        autorSizeNow As Size, innerSizeNow As Size, locationNow As Point, raito As Double, beforeMP As Point,
        beforeInnerSize As Size, beforeLocation As Point)


    Function getLocationWhenSizeChangePictureBoxByMouseWheel(
        BeforeMP As Point, raito As Double, BeforeInnerSize As Size, OuterSizeNow As Size) As Point
    Function getLocationWhenSizeChangePictureBoxByMouseWheelWhenPictureBoxSmallerThanPanel(
        raito As Double, InnerSizeNow As Size, OuterSizeNow As Size) As Point

End Interface
