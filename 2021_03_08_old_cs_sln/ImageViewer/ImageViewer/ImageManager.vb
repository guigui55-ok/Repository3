Imports ImageViewer

Public Class ImageManager
    Implements IImageManager

    Private PaintImageObject As IPaintImage
    Private PaintImageFactoryObject As IPaintImageFactory
    Private ImagePath As String

    Public Sub New()
        PaintImageObject = New PaintImageAsPictureBoxInPanel()
        PaintImageFactoryObject = New PaintImageFactory()

        '=====================
        Dim PaintImageSettingObject As PaintImageSetting = New PaintImageSetting()
        Dim PaintImageEffectSettingObject As PaintImageEffectSetting = New PaintImageEffectSetting()
        Dim ControlForPaintAsPictureBox As ControlForPaintAsPictureBox = New ControlForPaintAsPictureBox()
        Dim ControlFrameAsPanelObject As ControlFrameAsPanel = New ControlFrameAsPanel()
        Dim ControlForSettingObject As ControlForPaintSetting = New ControlForPaintSetting()
        Dim DrawImageObject As DrawImage = New DrawImage()
        '=====================
        PaintImageObject.setPaintImageSetting(PaintImageSettingObject)
        PaintImageObject.setPaintImageEffectSetting(PaintImageEffectSettingObject)
        PaintImageObject.setControlForPaint(ControlForPaintAsPictureBox)
        PaintImageObject.setControlFrame(ControlFrameAsPanelObject)
        PaintImageObject.setControlForPaintSetting(ControlForSettingObject)
        PaintImageObject.setDrawImage(DrawImageObject)
        '=====================
    End Sub

    Public Sub disposeObjects() Implements IImageManager.disposeObjects
        Try
            PaintImageObject.disposeObjects()
            PaintImageObject = Nothing
            PaintImageFactoryObject.disposeObjects()
            PaintImageFactoryObject = Nothing

        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    Public Sub setControls(argPanel As Control, argPictureBox As Control) Implements IImageManager.setControls
        Try
            ''PictureBoxをセット
            Dim ControlForPaintAsPictureBox As ControlForPaintAsPictureBox = New ControlForPaintAsPictureBox()
            ControlForPaintAsPictureBox.setControl(argPictureBox)
            PaintImageObject.setControlForPaint(ControlForPaintAsPictureBox)
            ''Panelをセット
            Dim ControlFrameAsPanelObject As ControlFrameAsPanel = New ControlFrameAsPanel()
            ControlFrameAsPanelObject.setControl(argPanel)
            PaintImageObject.setControlFrame(ControlFrameAsPanelObject)

        Catch ex As Exception
            Console.WriteLine("setControls")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub setImagePath(argPath As String) Implements IImageManager.setImagePath
        ImagePath = argPath
        If True Then
            'isAbleSetImage@@@
        End If
        PaintImageObject.setDrawImageFromPath(argPath)
    End Sub


    Public Sub setPaintImage(argPaintImage As IPaintImage) Implements IImageManager.setPaintImage
        PaintImageObject = argPaintImage
    End Sub

    Public Sub setPaintImageSetting(argPaintImageSetting As IPaintImageSetting) Implements IImageManager.setPaintImageSetting
        PaintImageObject.setPaintImageSetting(argPaintImageSetting)
    End Sub

    Public Sub setPaintImageEffectSetting(argPaintImageEffectSetting As IPaintImageEffectSetting) Implements IImageManager.setPaintImageEffectSetting
        PaintImageObject.setPaintImageEffectSetting(argPaintImageEffectSetting)
    End Sub

    Public Sub setControlForPaint(argControlForPaint As IControlForPaint) Implements IImageManager.setControlForPaint
        PaintImageObject.setControlForPaint(argControlForPaint)
    End Sub

    Public Sub setControlFrame(argControlFrame As IControlFrame) Implements IImageManager.setControlFrame
        PaintImageObject.setControlFrame(argControlFrame)
    End Sub

    Public Sub setDrawImage(argDrawImage As DrawImage) Implements IImageManager.setDrawImage
        PaintImageObject.setDrawImage(argDrawImage)
    End Sub

    '////////////////////////////////////////////////////////////////////////////////////
    Public Sub paintTest()
        testPaint()
    End Sub


    Public Sub testPaint() Implements IImageManager.testPaint
        Try
            Dim Path As String = PaintImageObject.getPath
            paintImage(Path)
        Catch ex As Exception
            Console.WriteLine("PaintTest")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub test() Implements IImageManager.test
        Try
            'ImagePath 'Private
            'ImageManagerObject.setPath(path)
            'Setting<-Value
            Dim PaintImageSettingObject As PaintImageSetting = PaintImageObject.getPaintImageSetting()
            PaintImageSettingObject.SetPosition(0, 0)
            PaintImageSettingObject.setInnerFrameSize(PaintImageObject.getControlForPaint.getSize)
            PaintImageSettingObject.setImageSize(PaintImageObject.getDrawImage.getSize)
            'DoSetting
            PaintImageSettingObject.calcRegionDefaultToReadAndDraw()
            'paintImage<-Setting
            PaintImageObject.setPaintImageSetting(PaintImageSettingObject)
            'PaintImage
            paintImageOnlyNotSetting()
        Catch ex As Exception
            Console.WriteLine("test")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub paintImageOnlyNotSetting() Implements IImageManager.paintImageOnlyNotSetting
        PaintImageObject.paintImageOnly()
    End Sub

    Public Sub paintImageOnlyNotDispose() Implements IImageManager.paintImageOnlyNotDispose
        PaintImageObject.paintImageOnlyNotDispose()
    End Sub

    Public Sub paintImage() Implements IImageManager.paintImage
        paintImage(PaintImageObject.getPath)
    End Sub

    'normal default
    Public Sub paintImageNotDispose(argImage As Image) Implements IImageManager.paintImageNotDispose
        Try
            'ImagePathをセット
            PaintImageObject.setDrawImage(New DrawImage(argImage))
            '描画領域等の設定
            PaintImageObject.DoSetting()
            '描画
            PaintImageObject.applySetting()
            'Controlへ
            PaintImageObject.PaintImageNotDispose()
        Catch ex As Exception
            Console.WriteLine("paintImageNotDispose")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'normal default
    'Inner_PicBoxSize←Frame_PanelSize
    'Inner_Location_00
    'Vertical_Horizontal_Position_00
    Public Sub paintImage(argPath As String) Implements IImageManager.paintImage
        Try
            'ImagePathをセット
            PaintImageObject.setDrawImageFromPath(argPath)
            '描画領域等の設定
            PaintImageObject.DoSetting()
            '描画
            PaintImageObject.applySetting()
            'Controlへ
            PaintImageObject.paintImage()

            'basePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Catch ex As Exception
            Console.WriteLine("paintImage_path")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'normal default
    Public Sub paintImage(argImage As Image) Implements IImageManager.paintImage
        Try
            'ImagePathをセット
            PaintImageObject.setDrawImage(New DrawImage(argImage))
            '描画領域等の設定
            PaintImageObject.DoSetting()
            '描画
            PaintImageObject.applySetting()
            'Controlへ
            PaintImageObject.paintImage()
        Catch ex As Exception
            Console.WriteLine("paintImage_Image")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub PaintImageFirst() Implements IImageManager.paintImageFirst
        'ControlSize<-PanelSize
        PaintImageObject.getControlFrame.getSize()
    End Sub

    Public Sub PaintImageFirst(argPath As String) Implements IImageManager.PaintImageFirst
        Throw New NotImplementedException()
    End Sub

    Public Sub setPaintImageFactory(argPaintIMageFactory As IPaintImageFactory) Implements IImageManager.setPaintImageFactory
        PaintImageFactoryObject = argPaintIMageFactory
    End Sub

    Public Function getImageFromControl() As Image Implements IImageManager.getImageFromControl
        Return PaintImageObject.getImageFromControl
    End Function

    Public Function getPaintImage() As IPaintImage Implements IImageManager.getPaintImage
        Return PaintImageObject
    End Function

    Public Function getPaintImageSetting() As IPaintImageSetting Implements IImageManager.getPaintImageSetting
        Return PaintImageObject.getPaintImageSetting
    End Function

    Public Function InnerPaintControlSizeIsSmallerThanFrameSize() As Boolean Implements IImageManager.InnerPaintControlSizeIsSmallerThanFrameSize
        Try
            Dim innerSize As Size = PaintImageObject.getControlForPaint.getSize
            Dim outerSize As Size = PaintImageObject.getControlFrame.getSize
            'true
            'PictureBox=Panel
            'PictureBoxWidth=PanelWidth And PictureBoxHeight<=PanelHeight
            'PictureBoxWidth<=PanelWidth and PictureBoxHeight=PanelHeight
            If innerSize.Width = outerSize.Width And innerSize.Height = outerSize.Height Then
                Return True
            ElseIf (innerSize.Width = outerSize.Width And innerSize.Height <= outerSize.Height) Then
                Return True
            ElseIf (innerSize.Width <= outerSize.Width And innerSize.Height = outerSize.Height) Then
                Return True
            Else
                'Width Heightどちらか　が大きい
                'Width Heightどちら　も大きい
                'Width Heightどちらか　が小さいくて　どちらか　が大きい(等しくない)
                'Width Heightどちら　も小さい
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("InnerPaintControlSizeIsSameFrameSize")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    'pictureBoxのWidthOrHeightどちらかがFrameSizeより大きい
    Public Function EitherInnerPaintControlSizesWidthOrHeightIsGreatherThanFrameSize() As Boolean Implements IImageManager.EitherInnerPaintControlSizesWidthOrHeightIsGreatherThanFrameSize
        Try
            'Width Heightどちら　も大きい
            'Width Heightどちらか　が大きい
            'Width Heightどちらか　が小さいくて　どちらか　が大きいか小さい(等しくない)
            Dim innerSize As Size = PaintImageObject.getControlForPaint.getSize
            Dim outerSize As Size = PaintImageObject.getControlFrame.getSize
            'true
            'PictureBoxWidth>PanelWidth and PictureBoxHeight>PanelHeight
            'PictureBoxWidth>PanelWidth and (not PictureBoxHeight=PanelHeight)
            '(not PictureBoxWidth=PanelWidth) and PictureBoxHeight>PanelHeight
            'どちらも大きいorどちらかが大きい
            If innerSize.Width > outerSize.Width And innerSize.Height > outerSize.Height Then
                Return True
            ElseIf (innerSize.Width > outerSize.Width And (Not innerSize.Height = outerSize.Height)) Then
                Return True
            ElseIf ((Not innerSize.Width = outerSize.Height) And innerSize.Height > outerSize.Height) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("EitherInnerPaintControlSizesWidthOrHeightIsGreatherThanFrameSize")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function
End Class
