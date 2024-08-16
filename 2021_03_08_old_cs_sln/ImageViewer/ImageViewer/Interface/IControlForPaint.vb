Public Interface IControlForPaint
    'Sub New(argControl As Control)
    Sub setImage(argImage As Image)
    Sub setImageNotDispose(argImage As Image)
    Sub setImageAndDispose(argImage As Image)

    Sub setControl(control As Control)
    Sub setFrameRect(frameRect As RectangleF)
    Sub setScrollBarVisible(flag As Boolean)
    Sub setPaintImage(argImage As Image)
    Sub setSize(argSize As Size)
    Sub setImageSizeAndLocation(argSize As Size, argLocation As Point)

    Property Visible As Boolean
    ReadOnly Property SizeUpdateState As Boolean

    Function getImage() As Image
    Function getSize() As Size
    Function getImageSize() As Size
    Function getLocation() As Point
    Function getControlBridge() As AbstractControlBridge
    Function getMousePointOnThisControl() As Point
    Sub saveSizeAndLocationWhenSizeChangeByMouseWheel()
    Function getSizeBeforeSizeChange() As Size
    Function getLocationBeforeSizeChange() As Point

    Sub changeSize(argSize As Size)
    Sub changePosition(argPoint As Point)
    Sub changeLocation(argPoint As Point)
    'Sub calcPositionSizeChange(argFrameSize As Size, argControlSize As Size)
    Sub changePosition(CalcChangePositionObject As AbstractCalcChangeLocationForControl)
    Property ScrollVarVisible() As Boolean
    Sub changeLocation(e As MouseEventArgs, MousePoint As Point)
    Function hasImageIsNothing() As Boolean
    Function ImageIsError() As Boolean

    Sub changeLocationWithinFrame(getSize As Size, e As MouseEventArgs, downPoint As Point)
End Interface
