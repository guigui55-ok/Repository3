Imports ImageViewer

Public Class CalcDrawRegion
    Inherits AbstractCalcDrawRegion
    'Implements ICalcDrawRegion
    'Shadows PointsForDraw As PointF()
    'Shadows ImageSize As Size
    'Shadows ControlSize As Size
    Public Sub New(argControlSize As Size, argImageSize As Size)
        setSize(argControlSize, argImageSize)
    End Sub

    'Abstract
    'Public Sub setSize(argControlSize As Size, argImageSize As Size)
    '    ImageSize = argImageSize
    '    ControlSize = argControlSize
    'End Sub

    Public Overrides Sub calcDrawRegionMatchToControlSize()
        calcPointsNormal(ImageSize, ControlSize)
    End Sub

    Public Overrides Sub calcDrawRegionMatchToControlSize(argImageSize As Size, argControlSize As Size)
        calcPointsNormal(ImageSize, ControlSize)
    End Sub


    Public Sub calcDrawRegionByImage(argImageSize As Size, argControlSize As Size)
        ImageSize = argImageSize
        ControlSize = argControlSize

        calcPointsNormal(ImageSize, ControlSize)

    End Sub

    'Public Function getPoints() As PointF()
    '    Return PointsForDraw
    'End Function


    Public Sub setDrawPosition(VerticalAlign As Integer, HorizontalAlign As Integer)
        Throw New NotImplementedException()
    End Sub

    Public Function calcPoints() As PointF()
        calcPointsNormal(ImageSize, ControlSize)
        Return PointsForDraw
    End Function

    Public Function calcPointsMain() As PointF()
        Try
            calcPointsNormal(ImageSize, ControlSize)
            Return PointsForDraw
        Catch ex As Exception
            Console.WriteLine("calcPointsMain")
            Console.WriteLine(ex.Message)
            Return PointsForDraw
        End Try
    End Function

    'Abstract
    'Align設定なし
    'Imageの比率を維持してControlに合わせる
    'Public Sub calcPointsNormal(ByRef ImageSize As Size, ByRef ControlSize As Size)
    '    Dim oPoints As New PointsForDrawSquareImage()
    '    Try
    '        If ImageSize.Width = 0 And ImageSize.Height = 0 Then
    '            Console.WriteLine("calcPoints_AlignCenter Image Is Nothing")
    '            Exit Sub
    '        End If
    '        '倍率を求める
    '        Dim sizePointF As PointF = oPoints.calcRaitoImageByPictureBox(ImageSize, ControlSize)
    '        '座標決定_セット
    '        oPoints.setPoints(0, 0, sizePointF.X, sizePointF.Y)
    '        'Align設定
    '        'oPoints.setHorizontalAlign(HorizontalAlign, oPoints.getPoints, argPictureBox.Width)
    '        'oPoints.setVerticalAlign(VerticalAlign, oPoints.getPoints, argPictureBox.Height)
    '        'Set
    '        PointsForDraw = oPoints.getPoints

    '        '倍率は保持
    '        'Me.NowRaito = oPoints.NowRaito
    '        oPoints = Nothing
    '    Catch ex As Exception
    '        Console.WriteLine("calcPointsNormal")
    '        Console.WriteLine(ex.Message)
    '        oPoints = Nothing
    '    End Try
    'End Sub
    '////////////////////////////////////////////////////////////////////////////////////////////
    'Imageを表示する位置と大きさを設定
    'g.DrawImageするときの
    '描画する領域を計算する
    'PictureBoxの領域をイメージの比率に合わせる
    'ウィンドウの大きさに合わせる
    Public Function calcToMatchRaitoImageByControl(ByRef ImageSize As Size, ByRef ControlSize As Size) As PointF
        Dim oPoints As New PointsForDrawSquareImage()
        Try
            '倍率を求める
            Dim sizePointF As PointF = oPoints.calcRaitoImageByPictureBox(ImageSize, ControlSize)

            Return sizePointF
            oPoints = Nothing
        Catch ex As Exception
            Console.WriteLine("calcToMatchRaitoImageByControl")
            Console.WriteLine(ex.Message)
            Return New PointF(0, 0)
            oPoints = Nothing
        End Try
    End Function


    'サイズを決めるPointFへ
    '拡大縮小用
    Public Function setPointsForImageResize(
            DrawWidth As Double, DrawHeigh As Double, NowWidth As Double, NowHeight As Double,
            argNowRaito As Double, argAfterRaito As Double,
            VerticalAlign As Integer, HorizontalAlign As Integer,
            mp As Point, mpInPicBox As Boolean
            ) As Integer
        Try
            '画像の大きさとPictureBoxの大きさを比べて
            'Width Height より PictureBoxのほうが大きい場合 縦横Center
            '各値のスペースをそれぞれ加算する
            'Width Height より PictureBoxのほうが小さい場合 縦横Center
            'MinorsMargin = PictureBox.Width - Width / 2
            'top = top - MinosMargin
            'Bottom = Bottom + MinorsMargin  = Image.Height - MinorsMargin
            'とする
            'Width
            Dim AfterWidth As Double
            Dim AfterHeight As Double
            'まず倍率をかけて画像の大きさを決める　サイズ
            AfterWidth = NowWidth * argAfterRaito
            AfterHeight = NowHeight * argAfterRaito
            'NowRaito = argAfterRaito
            '画像の大きさとControlの大きさ＋表示する位置をFlagintで決める
            Dim NewRect As RectangleF

            '幅高さを計算
            'NewRect = calcSizeWidthForMethodsetPointsForImageResize(HorizontalAlign, DrawWidth, AfterWidth, NewRect)
            'NewRect = calcSizeHeightForMethodsetPointsForImageResize(VerticalAlign, DrawHeigh, AfterHeight, NewRect)

            '結果をPointFにして Private変数へ
            Me.PointsForDraw = {
                New PointF(NewRect.X, NewRect.Y),
                New PointF(NewRect.Width, NewRect.Y),
                New PointF(NewRect.X, NewRect.Height)
            }
            Return 1
        Catch ex As Exception
            Console.WriteLine("calcRaitoImageByPictureBox")
            Console.WriteLine(ex.Message)
            Return -1
        End Try
    End Function

End Class
