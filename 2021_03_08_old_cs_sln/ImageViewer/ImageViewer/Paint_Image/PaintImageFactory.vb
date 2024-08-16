Imports ImageViewer

Public Class PaintImageFactory
    Implements IPaintImageFactory

    Private ImageManagerObject As ImageManager
    Private ImageState As New ImageViewerState
    Private Settings As New Settings



    Public Sub disposeObjects() Implements IPaintImageFactory.disposeObjects
        Try
            'ImageManagerObject.disposeObjects()
            'ImageManagerObject = Nothing
            ImageState.disposeObjects()
            ImageState = Nothing
            Settings.disposeObjects()
            Settings = Nothing
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub paintImage(pictureBox As PictureBox, Path As String) Implements IPaintImageFactory.paintImage
        Throw New NotImplementedException()
    End Sub

    Public Sub disposeImage() Implements IPaintImageFactory.disposeImage
        Throw New NotImplementedException()
    End Sub

    Public Sub paintImageRefresh() Implements IPaintImageFactory.paintImageRefresh
        Throw New NotImplementedException()
    End Sub

    Public Sub setImageManager(argImageManager As ImageManager) Implements IPaintImageFactory.setImageManager
        Try
            Me.ImageManagerObject = argImageManager
        Catch ex As Exception
            Console.WriteLine("setImageManager")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub DoSetting() Implements IPaintImageFactory.DoSetting
        DoSettingForPaint()
        DoSettingForControl()
        DoSettingForPaintEffect()
    End Sub

    'Default
    'setInnerFrameSize=InnerControlSize
    'ReadImageSize=ImageSize
    'Vertical_Horizontal=0_0
    Public Sub DoSettingForPaint() Implements IPaintImageFactory.DoSettingForPaint
        Try
            '******'デフォルト設定******
            'ControlForpaintSettingObject.setInnerControlLocation(New Point(0, 0))
            'ControlForpaintSettingObject.setInnerControlSize(ControlFrameAsPanelObject.getSize)
            Dim PaintImageSettingObject As PaintImageSetting = New PaintImageSetting()

            PaintImageSettingObject.SetPosition(0, 0)
            'PictureBoxの倍率　拡大縮小
            'canbasサイズ PictureBoxのサイズ
            PaintImageSettingObject.setInnerFrameSize(ImageManagerObject.getPaintImage.getControlForPaint.getSize)
            PaintImageSettingObject.setImageSize(ImageManagerObject.getPaintImage.getDrawImage.getSize)
            'Dim ControlSize As Size = ControlForPaint.getSize
            '画像描画領域_ここで拡大縮小
            PaintImageSettingObject.calcDrawRegion()
            'PaintImageSetting.setDrawRegion(New Rectangle(0, 0, ControlSize.Width, ControlSize.Height))

            '画像読み込み領域
            PaintImageSettingObject.setReadImageRegion(
                New Rectangle(New Point(0, 0),
                    New Size(ImageManagerObject.getPaintImage.getDrawImage.width, ImageManagerObject.getPaintImage.getDrawImage.height))
            )

            'これだと　もとImageの比率が維持されない
            ImageManagerObject.setPaintImageSetting(PaintImageSettingObject)
        Catch ex As Exception
            Console.WriteLine("DoSetting")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub DoSettingForControl() Implements IPaintImageFactory.DoSettingForControl
        Try

        Catch ex As Exception
            Console.WriteLine("DoSettingForControl")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub DoSettingForPaintEffect() Implements IPaintImageFactory.DoSettingForPaintEffect
        Try

        Catch ex As Exception
            Console.WriteLine("DoSettingForPaintEffect")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

End Class
