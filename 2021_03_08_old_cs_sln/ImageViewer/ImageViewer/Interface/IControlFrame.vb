Public Interface IControlFrame
    Sub setControl(control As Control)

    Sub setFrameSize(size As PointF)
    Sub setSize(argSize As Size)

    Function getSize() As Size
    Function getLocation() As Point
    Function getControl() As AbstractControlBridge
    Function getMousePointOnThisControl() As Point

    Property Visible As Boolean

    Sub applySize()
    Sub changeSize(size As Size)
End Interface
