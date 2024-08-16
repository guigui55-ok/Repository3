Public Interface IImageManager
    Sub disposeObjects()
    Sub paintImage()
    Sub paintImage(argPath As String)
    Sub paintImage(argImage As Image)
    Sub paintImageFirst()
    Sub paintImageFirst(argPath As String)
    Sub paintImageNotDispose(argImage As Image)
    Sub paintImageOnlyNotSetting()
    Sub paintImageOnlyNotDispose()

    Sub setControls(argPanel As Control, argPictureBox As Control)
    Sub setImagePath(argPath As String)
    Sub test()
    Sub testPaint()


    Sub setPaintImage(argPaintImage As IPaintImage)
    Sub setPaintImageSetting(argPaintImageSetting As IPaintImageSetting)
    'overrides
    Sub setPaintImageEffectSetting(argPaintImageEffectSetting As IPaintImageEffectSetting)
    Sub setControlForPaint(argControlForPaint As IControlForPaint)
    Sub setControlFrame(argControlFrame As IControlFrame)
    Sub setDrawImage(argDrawImage As DrawImage)
    Sub setPaintImageFactory(argPaintIMageFactory As IPaintImageFactory)

    Function getImageFromControl() As Image
    Function getPaintImage() As IPaintImage
    Function getPaintImageSetting() As IPaintImageSetting

    Function InnerPaintControlSizeIsSmallerThanFrameSize() As Boolean
    Function EitherInnerPaintControlSizesWidthOrHeightIsGreatherThanFrameSize() As Boolean
End Interface
