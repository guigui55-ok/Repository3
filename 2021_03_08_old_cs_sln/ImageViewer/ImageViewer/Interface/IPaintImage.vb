Imports ImageViewer

Public Interface IPaintImage

    Sub setPath(Path As String)
    Function getPath() As String

    Sub disposeObjects()
    Sub setDrawImageFromPath(argPath As String)
    Sub setDrawImage(argDrawImage As IDrawImage)
    Sub setPaintImageSetting(argPaintImageSetting As IPaintImageSetting)
    Sub setPaintImageEffectSetting(argPaintImageEffectSetting As PaintImageEffectSetting)
    Sub setControlForPaint(argControlForPaintAsPictureBox As ControlForPaintAsPictureBox)
    Sub setControlFrame(argControlFrame As ControlFrameAsPanel)
    Sub setControlForPaintSetting(argControlForPaintSetting As ControlForPaintSetting)

    Sub applyControlSetting()
    Sub applyPaintSetting()
    Sub applyEffectSetting()
    Sub applySetting()
    Sub DoSetting()

    Sub paintImageOnly()
    Sub paintImageOnlyNotDispose()
    Sub paintImage()
    Sub paintImageAndApplySetting()
    Sub setPaintImageNotDispose()
    Sub PaintImageNotDispose()

    Function getDrawImage() As DrawImage
    Function getImageFromControl() As Image

    'Function getControlForPaint() As ControlForPaintAsPictureBox
    Sub setDrawImage(argDrawImage As DrawImage)
    Sub setImage(argImage As Image)
    Function getControlFrame() As IControlFrame
    Function getControlForPaint() As IControlForPaint
    Function getControlForPaintSetting() As IControlForPaintSetting
    Function getPaintImageSetting() As IPaintImageSetting
    'Function getSize() As Size

    Function isDrawImageHasImageIsNothing() As Boolean
    Function isControlForPaintHasImageIsNothing() As Boolean
End Interface
