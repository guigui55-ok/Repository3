Public Class PaintImageSettingChangeRotation
    Inherits AbstractPaintImageSetting
    Implements IPaintImageSetting

    Public Overloads Sub calcDrawRegion(argControlSize As Size, argImageSize As Size)
        InnerFrameSize = argControlSize
        ImageSize = argImageSize
        Try
            '左上、左下、右上　×
            '左上、左下、右下　ｘｙ逆　左に90度回転
            '左に90度回転
            DrawPoints = {
                New Point(InnerFrameSize.Width, 0),
                New Point(InnerFrameSize.Width, InnerFrameSize.Height),
                New Point(0, 0）
            }


            'x斜めになる　右下へ細長く
            'DrawPoints = {
            '    New Point(0, 0),
            '    New Point(0, InnerFrameSize.Height),
            '    New Point(InnerFrameSize.Width, InnerFrameSize.Height)
            '}

            '右上、左下、右下 　ｘｙ逆　右斜め45度　平行四辺形
            '180度回転
            'DrawPoints = {
            '    New Point(0, InnerFrameSize.Height),
            '    New Point(InnerFrameSize.Width, InnerFrameSize.Height),
            '    New Point(0, 0)
            '}

            '右上、左下、右下　×

            '通常　正位置
            '左上、右上、左下　○
            'DrawPoints = {
            '    New Point(0, 0),
            '    New Point(InnerFrameSize.Width, 0),
            '    New Point(0, InnerFrameSize.Height)
            '}

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

            '薄くなっていく
            'Me.setReadImageRegion(
            '    New Rectangle(New Point(0, 0), New Size(ImageSize.Height, ImageSize.Width))
            ')

            'Me.writeConsoleDrawPoints()
        Catch ex As Exception
            Console.WriteLine("calcRegionToReadAndDraw")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
End Class

