Imports ImageViewer

'最初の実装時は、PictureBoxの中にPaddingを設けていたので
'（Imageの外にMarginを設けていたので）
'それを除外したNoMarginクラス

Public Class PaintImageSettingNoMargin
    Inherits AbstractPaintImageSetting
    Implements IPaintImageSetting

    Public Overloads Sub calcDrawRegion(argControlSize As Size, argImageSize As Size)
        InnerFrameSize = argControlSize
        ImageSize = argImageSize
        Try
            '左上、左下、右上　×
            '左上、左下、左下　ｘｙ逆　左に90度回転
            '右上、左下、右下 　ｘｙ逆　右斜め45度　平行四辺形

            '右上、左下、右下　×
            '左上、右上、左下　○
            DrawPoints = {
                New Point(0, 0),
                New Point(InnerFrameSize.Width, 0),
                New Point(0, InnerFrameSize.Height)
            }

        Catch ex As Exception
            Console.WriteLine("calcDrawRegion")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Overrides Sub calcDrawRegion() Implements IPaintImageSetting.calcDrawRegion
        calcDrawRegion(Me.InnerFrameSize, Me.ImageSize)
    End Sub

    Public Overrides Sub calcRegionDefaultToReadAndDraw() Implements IPaintImageSetting.calcRegionDefaultToReadAndDraw
        Try
            '画像描画領域_ここで拡大縮小_DrawPointsをセット
            Me.calcDrawRegion()

            '画像読み込み領域
            Me.setReadImageRegion(
                New Rectangle(New Point(0, 0), New Size(ImageSize.Width, ImageSize.Height))
            )

            'Me.writeConsoleDrawPoints()
        Catch ex As Exception
            Console.WriteLine("calcRegionToReadAndDraw")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
End Class
