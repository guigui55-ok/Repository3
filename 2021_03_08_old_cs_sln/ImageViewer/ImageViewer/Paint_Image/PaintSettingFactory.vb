Imports ImageViewer

Public Class PaintSettingFactory
    Inherits AbstractPaintImageSettingFactory

    'Friend PaintImageSettingObject As PaintImageSetting
    'Public Overloads Sub setSettingObject(argSettingObject As Object)
    '    PaintImageSettingObject = argSettingObject
    'End Sub
    'Public Function getSettingObject() As Object
    '    Return PaintImageSettingObject
    'End Function

    Public Overloads Sub DoSetting(argPaintImageSettingObject As PaintImageSetting)
        Try
            '******'デフォルト設定******
            'DoPaintSetting
            'Vertical_Horizontal_Position
            'PaintImageSettingObject.SetPosition(0, 0)
            'PictureBoxの倍率　拡大縮小
            'canbasサイズ PictureBoxのサイズ
            'PaintImageSettingObject.setInnerFrameSize(ControlForPaintAsPictureBoxObject.getSize)
            'PaintImageSettingObject.setImageSize(DrawImageObject.getSize)

            '画像描画領域_ここで拡大縮小
            PaintImageSettingObject.calcDrawRegion()
            'PaintImageSetting.setDrawRegion(New Rectangle(0, 0, ControlSize.Width, ControlSize.Height))

            '画像読み込み領域
            PaintImageSettingObject.setReadImageRegion(
                New Rectangle(New Point(0, 0),
                              New Size(PaintImageSettingObject.getImageSize.Width, PaintImageSettingObject.getImageSize.Height))
            )
            'これだと　もとImageの比率が維持されない
        Catch ex As Exception
            Console.WriteLine("DoSetting")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
End Class
