Public MustInherit Class AbstractCalcDrawRegion
    ' Sharedは不可
    ' Privateは不可
    ' 仮想メンバのオーバーライド：任意、Overrides修飾子が必要
    Friend PointsForDraw() As PointF
    Friend ControlSize As Size
    Friend ImageSize As Size

    ' 抽象メンバ：MustOverride修飾子を付ける
    Public MustOverride Sub calcDrawRegionMatchToControlSize()
    Public MustOverride Sub calcDrawRegionMatchToControlSize(argControlSize As Size, argImageSize As Size)

    Public Function getPoints()
        Return PointsForDraw
    End Function

    Public Sub New()

    End Sub

    Public Sub New(argControlSize As Size, argImageSize As Size)
        setSize(argControlSize, argImageSize)
    End Sub

    Public Sub setSize(argControlSize As Size, argImageSize As Size)
        ImageSize = argImageSize
        ControlSize = argControlSize
    End Sub

    'イメージサイズをコントロールサイズに合わせる、イメージの比率のまま
    Public Sub ImageSizeChangeToMatchControlSize()
        Try
            Dim NewWidth As Double
            Dim NewHeight As Double
            Dim Raito As Double

            Dim VerticalRaito As Double = ControlSize.Height / ImageSize.Height
            Dim HorizontalRaito As Double = ControlSize.Width / ImageSize.Width

            '倍率が小さいほうを設定
            If VerticalRaito < HorizontalRaito Then
                NewHeight = ControlSize.Height * 1
                NewWidth = ImageSize.Width * VerticalRaito
                Raito = VerticalRaito
            Else
                NewHeight = ImageSize.Height * HorizontalRaito
                NewWidth = ControlSize.Width * 1
                Raito = HorizontalRaito
            End If

            ImageSize = New Size(NewWidth, NewHeight)
        Catch ex As Exception
            Console.WriteLine("ImageSizeChangeToMatchControlSize")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'Align設定なし
    'Imageの比率を維持してControlに合わせる
    Public Sub calcPointsNormal(ByRef ImageSize As Size, ByRef ControlSize As Size)
        Dim oPoints As New PointsForDrawSquareImage()
        Try
            If ImageSize.Width = 0 And ImageSize.Height = 0 Then
                Console.WriteLine("calcPoints_AlignCenter Image Is Nothing")
                Exit Sub
            End If
            '倍率を求める
            Dim sizePointF As PointF = oPoints.calcRaitoImageByPictureBox(ImageSize, ControlSize)
            '座標決定_セット
            oPoints.setPoints(0, 0, sizePointF.X, sizePointF.Y)
            'Align設定
            'oPoints.setHorizontalAlign(HorizontalAlign, oPoints.getPoints, argPictureBox.Width)
            'oPoints.setVerticalAlign(VerticalAlign, oPoints.getPoints, argPictureBox.Height)
            'Set
            PointsForDraw = oPoints.getPoints

            '倍率は保持
            'Me.NowRaito = oPoints.NowRaito
            oPoints = Nothing
        Catch ex As Exception
            Console.WriteLine("calcPointsNormal")
            Console.WriteLine(ex.Message)
            oPoints = Nothing
        End Try
    End Sub

    Public Function calcImageSizeMatchToControlSize_Normal(
            ByRef argBaseImage As Image, ByRef argPictureBox As PictureBox)
        Try
            Dim NewWidth As Double
            Dim NewHeight As Double
            Dim VerticalRaito As Double = argPictureBox.Height / argBaseImage.Height
            Dim HorizontalRaito As Double = argPictureBox.Width / argBaseImage.Width

            '倍率が小さいほうを設定
            If VerticalRaito < HorizontalRaito Then
                NewHeight = argPictureBox.Height * 1
                NewWidth = argBaseImage.Width * VerticalRaito
                'NowRaito = VerticalRaito
            Else
                NewHeight = argBaseImage.Height * HorizontalRaito
                NewWidth = argPictureBox.Width * 1
                'NowRaito = HorizontalRaito
            End If

            Return New PointF(NewWidth, NewHeight)
        Catch ex As Exception
            Console.WriteLine("calcImageSizeMatchToControlSize_Normal")
            Console.WriteLine(ex.Message)
            Return New PointF(0, 0)
        End Try
    End Function
End Class
