Imports ImageViewer

Public Class PaintImageAsPictureBoxInPanel
    Implements IPaintImage

    Private ImagePath As String
    Private DrawImageObject As IDrawImage
    Private PaintImageSettingObject As IPaintImageSetting
    Private PaintImageEffectSettingObject As IPaintImageEffectSetting
    Private ControlForPaintAsPictureBoxObject As IControlForPaint
    Private ControlFrameAsPanelObject As IControlFrame
    Private ControlForPaintSettingObject As IControlForPaintSetting
    Private ImageCanvas As Image


    Public Sub disposeObjects() Implements IPaintImage.disposeObjects
        Try
            DrawImageObject.disposeImage()
            DrawImageObject = Nothing
            PaintImageSettingObject = Nothing
            PaintImageEffectSettingObject.Dispose()
            PaintImageEffectSettingObject = Nothing
            ControlForPaintAsPictureBoxObject = Nothing
            ControlFrameAsPanelObject = Nothing
            ControlForPaintSettingObject = Nothing
            ImageCanvas = Nothing
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'Public Sub paintImageAndApplySetting() Implements IPaintImage.paintImageAndApplySetting
    '    '描画領域等の設定
    '    DoSetting()
    '    '描画
    '    applySetting()
    '    'Controlへ
    '    paintImage()
    'End Sub

    Public Sub paintImageAndApplySetting() Implements IPaintImage.paintImageAndApplySetting
        Try
            '描画
            applySetting()
            'Controlへ
            paintImage()
        Catch ex As Exception
            Console.WriteLine("paintImageAndApplySetting")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub paintImageOnly() Implements IPaintImage.paintImageOnly
        '描画領域等の設定 外部でSettingVer
        'DoSetting()
        '描画
        applySetting()
        'Controlへ
        paintImage()
    End Sub

    Public Sub paintImageOnlyNotDispose() Implements IPaintImage.paintImageOnlyNotDispose
        '描画領域等の設定 外部でSettingVer
        'DoSetting()
        '描画
        applySetting()
        'Controlへ
        PaintImageNotDispose()
    End Sub

    Public Sub PaintImageNotDispose() Implements IPaintImage.setPaintImageNotDispose
        'Controlへ
        ControlForPaintAsPictureBoxObject.setImageNotDispose(ImageCanvas)
    End Sub

    Public Sub paintImage() Implements IPaintImage.paintImage
        'Controlへ
        'ImageCanvas.Width = ImageCanvas.Width
        ControlForPaintAsPictureBoxObject.setImageAndDispose(ImageCanvas)
    End Sub

    Public Sub setPath(path As String) Implements IPaintImage.setPath
        ImagePath = path
    End Sub

    Public Sub setDrawImage(argDrawImage As DrawImage) Implements IPaintImage.setDrawImage
        Me.DrawImageObject = argDrawImage
    End Sub

    Public Sub setImage(argImage As Image) Implements IPaintImage.setImage
        Try
            If argImage Is Nothing Then
                addLogForSystemError("image is nothing")
                Exit Sub
            End If
            If DrawImageObject Is Nothing Then
                DrawImageObject = New DrawImage(argImage)
            Else
                DrawImageObject.setImage(argImage)
                ImageCanvas = argImage
            End If
        Catch ex As Exception
            addLogForSystemError(Me.ToString & "setImage")
            addLogForSystemError(ex.Message)
        End Try
    End Sub


    Public Sub setDrawImageFromPath(argPath As String) Implements IPaintImage.setDrawImageFromPath
        ImagePath = argPath
        DrawImageObject.setImageFromPath(argPath)
    End Sub

    Public Sub setPaintImageSetting(argPaintImageSetting As IPaintImageSetting) Implements IPaintImage.setPaintImageSetting
        Me.PaintImageSettingObject = argPaintImageSetting
    End Sub

    Public Sub setPaintImageEffectSetting(argPaintImageEffectSetting As PaintImageEffectSetting) Implements IPaintImage.setPaintImageEffectSetting
        Me.PaintImageEffectSettingObject = argPaintImageEffectSetting
    End Sub

    Public Sub setControlForPaint(argControlForPaintAsPictureBox As ControlForPaintAsPictureBox) Implements IPaintImage.setControlForPaint
        Me.ControlForPaintAsPictureBoxObject = argControlForPaintAsPictureBox
    End Sub

    Public Sub setControlFrame(argControlFrame As ControlFrameAsPanel) Implements IPaintImage.setControlFrame
        Me.ControlFrameAsPanelObject = argControlFrame
    End Sub

    Public Sub setControlForPaintSetting(argControlForPaintSetting As ControlForPaintSetting) Implements IPaintImage.setControlForPaintSetting
        Me.ControlForpaintSettingObject = argControlForPaintSetting
    End Sub

    Public Sub applyControlSetting() Implements IPaintImage.applyControlSetting
        'ControlForPaintAsPictureBoxObject.setVisible(False)
        'Size
        ControlForPaintAsPictureBoxObject.changeSize(
            ControlForPaintSettingObject.getInnerControlSize)
        'Location
        ControlForPaintAsPictureBoxObject.changePosition(
            ControlForpaintSettingObject.getInnerControlLocation)
        '@@@
        'ControlForPaintAsPictureBoxObject.setVisible(True)
    End Sub

    Public Sub applySetting() Implements IPaintImage.applySetting
        '順番①Paint
        applyPaintSetting()
        '順番②Control
        applyControlSetting()
        'applyEffectSetting() '未実装
    End Sub

    Public Sub applyPaintSetting() Implements IPaintImage.applyPaintSetting
        Dim g As Graphics = Nothing
        Try
            If PaintImageSettingObject.InnerFrameSizeIsZero Then
                Console.WriteLine("PaintImageSetting.InnerFrameSizeIsZero")
                Exit Sub
            End If


            Dim canvas As New Bitmap(PaintImageSettingObject.getInnerFrameSize.X, PaintImageSettingObject.getInnerFrameSize.Y)
            'ImgのGraphicsオブジェクトを取得
            g = Graphics.FromImage(canvas)
            '画像描画領域
            'Dim srcRect As RectangleF = PaintImageSetting.getDrawRegionAsRectangle()
            Dim DrawPoints As PointF() = PaintImageSettingObject.getDrawRegionAspoints()
            '画像読み込み領域
            Dim destRect As RectangleF = PaintImageSettingObject.getReadImageRegion()
            'canbasへ描画実行
            'g.DrawImage(DrawImageObject.getImage(), DrawPoints, destRect, GraphicsUnit.Pixel)

            g.DrawImage(DrawImageObject.getImage(), DrawPoints, destRect, GraphicsUnit.Pixel, PaintImageEffectSettingObject.getImageAttributes())

            'PaintImageSettingObject.writeConsoleDrawPoints()

            ImageCanvas = canvas
        Catch ex As Exception
            Console.WriteLine("applySetting")
            Console.WriteLine(ex.Message)
        Finally
            DrawImageObject.disposeImage()
            If Not g Is Nothing Then
                g.Dispose()
                g = Nothing
            End If
        End Try
    End Sub

    Public Sub applyEffectSetting() Implements IPaintImage.applyEffectSetting
        PaintImageEffectSettingObject.applySettingEffect()
    End Sub

    'これだと　もとImageの比率が維持されない
    Public Sub DoSetting() Implements IPaintImage.DoSetting
        Try
            '******'デフォルト設定******
            'DoControlSetting
            ControlForPaintSettingObject.setInnerControlLocation(New Point(0, 0))
            ControlForpaintSettingObject.setInnerControlSize(ControlFrameAsPanelObject.getSize)

            'DoPaintSetting
            'Vertical_Horizontal_Position
            PaintImageSettingObject.SetPosition(0, 0)
            'PictureBoxの倍率　拡大縮小
            'canbasサイズ PictureBoxのサイズ
            PaintImageSettingObject.setInnerFrameSize(ControlForPaintAsPictureBoxObject.getSize)
            PaintImageSettingObject.setImageSize(DrawImageObject.getSize)
            '画像描画領域_ここで拡大縮小
            PaintImageSettingObject.calcDrawRegion()
            '画像読み込み領域
            PaintImageSettingObject.setReadImageRegion(
                New Rectangle(New Point(0, 0), New Size(DrawImageObject.width, DrawImageObject.height))
            )
        Catch ex As Exception
            Console.WriteLine("DoSetting")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Function getPath() As String Implements IPaintImage.getPath
        Return Me.ImagePath
    End Function

    Public Function getDrawImage() As DrawImage Implements IPaintImage.getDrawImage
        Return Me.DrawImageObject
    End Function

    Public Function getImageFromControl() As Image Implements IPaintImage.getImageFromControl
        Return ControlForPaintAsPictureBoxObject.getImage
    End Function

    Public Sub setDrawImage(argDrawImage As IDrawImage) Implements IPaintImage.setDrawImage
        Me.DrawImageObject = argDrawImage
    End Sub

    Public Function getControlForPaint() As IControlForPaint Implements IPaintImage.getControlForPaint
        Return Me.ControlForPaintAsPictureBoxObject
    End Function

    Public Function getControlFrame() As IControlFrame Implements IPaintImage.getControlFrame
        Return ControlFrameAsPanelObject
    End Function

    Public Function getControlForPaintSetting() As IControlForPaintSetting Implements IPaintImage.getControlForPaintSetting
        Return Me.ControlForpaintSettingObject
    End Function

    Public Function getPaintImageSetting() As IPaintImageSetting Implements IPaintImage.getPaintImageSetting
        Return Me.PaintImageSettingObject
    End Function

    Public Function isDrawImageHasImageIsNothing() As Boolean Implements IPaintImage.isDrawImageHasImageIsNothing
        Try
            Return DrawImageObject.hasImageIsNothing
        Catch ex As Exception
            Console.WriteLine("isDrawImageHasImageIsNothing")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function isControlForPaintHasImageIsNothing() As Boolean Implements IPaintImage.isControlForPaintHasImageIsNothing
        Try
            Return ControlForPaintAsPictureBoxObject.hasImageIsNothing
        Catch ex As Exception
            Console.WriteLine("isControlForPaintHasImageIsNothing")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Private Sub IPaintImage_PaintImageNotDispose() Implements IPaintImage.PaintImageNotDispose
        Throw New NotImplementedException()
        Stop
    End Sub
End Class
