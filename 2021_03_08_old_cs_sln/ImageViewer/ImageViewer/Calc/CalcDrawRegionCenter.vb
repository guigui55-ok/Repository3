Public Class CalcDrawRegionCenter
    Inherits AbstractCalcDrawRegion

    'Private PointsForDraw As PointF()
    'Private ImageSize As Size
    'Private ControlSize As Size
    'Public Sub setSize(argControlSize As Size, argImageSize As Size)
    'Public Sub calcPointsNormal(ByRef ImageSize As Size, ByRef ControlSize As Size)
    'Public Function calcImageSizeMatchToControlSize_Normal(
    '        ByRef argBaseImage As Image, ByRef argPictureBox As PictureBox)
    'Public Sub New(argControlSize As Size, argImageSize As Size)
    'Public Sub ImageSizeChangeToMatchControlSize()

    Public Sub New(argControlSize As Size, argImageSize As Size)
        setSize(argControlSize, argImageSize)
    End Sub


    Public Overrides Sub calcDrawRegionMatchToControlSize(argControlSize As Size, argImageSize As Size)
        ControlSize = argControlSize
        ImageSize = argImageSize
        calcDrawRegionMatchToControlSize()
    End Sub


    Public Overrides Sub calcDrawRegionMatchToControlSize()
        Try
            ImageSizeChangeToMatchControlSize()
            Dim NewRect As RectangleF
            '幅高さを計算
            NewRect = calcSizeWidthForMethodsetPointsForImageResize(0, ControlSize.Width, ImageSize.Width, NewRect)
            NewRect = calcSizeHeightForMethodsetPointsForImageResize(0, ControlSize.Height, ImageSize.Height, NewRect)

            '結果をPointFにして Private変数へ
            Me.PointsForDraw = {
                    New PointF(NewRect.X, NewRect.Y),
                    New PointF(NewRect.Width, NewRect.Y),
                    New PointF(NewRect.X, NewRect.Height)
                }
        Catch ex As Exception
            Console.WriteLine("calcDrawRegionMatchToControlSize")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '高さ
    Private Function calcSizeHeightForMethodsetPointsForImageResize(
            FlagInt As Integer, ControlHeight As Double, NowHeight As Double, argRectangle As RectangleF) As RectangleF
        Try
            Dim MinorsMargin As Double
            Dim Top As Double
            Dim Bottom As Double

            '大きい
            If FlagInt = 0 Then
                'center
                MinorsMargin = (ControlHeight - NowHeight) / 2
                Top = MinorsMargin
                Bottom = MinorsMargin + NowHeight
            ElseIf FlagInt = 1 Then
                'Top
                Top = 0
                Bottom = NowHeight
            ElseIf FlagInt = 2 Then
                'bottom
                Top = ControlHeight - NowHeight
                Bottom = ControlHeight
            End If

            If ControlHeight > NowHeight Then
                '大きい
            ElseIf ControlHeight < NowHeight Then
                '小さい
            Else
                '等しい
            End If
            Return New RectangleF(argRectangle.X, Top, argRectangle.Width, Bottom)
        Catch ex As Exception
            Console.WriteLine("calcSizeHeightForMethodsetPointsForImageResize")
            Console.WriteLine(ex.Message)
            Return argRectangle
        End Try
    End Function
    '幅計算
    Private Function calcSizeWidthForMethodsetPointsForImageResize(
            FlagInt As Integer, ControlWidth As Double, NowWidth As Double, argRectangle As RectangleF) As RectangleF
        Try
            Dim MinorsMargin As Double
            Dim Left As Double
            Dim Right As Double

            If ControlWidth > NowWidth Then
                '大きい
            ElseIf ControlWidth < NowWidth Then
                '小さい
            Else
                '等しい
            End If
            '大きい
            If FlagInt = 0 Then
                'center
                MinorsMargin = (ControlWidth - NowWidth) / 2
                Left = MinorsMargin
                Right = NowWidth + MinorsMargin
            ElseIf FlagInt = 1 Then
                'left
                Left = 0
                Right = NowWidth
            ElseIf FlagInt = 2 Then
                'right
                Left = ControlWidth - NowWidth
                Right = ControlWidth
            End If

            Return New RectangleF(Left, argRectangle.Y, Right, argRectangle.Height)
        Catch ex As Exception
            Console.WriteLine("calcSizeWidthForMethodsetPointsForImageResize")
            Console.WriteLine(ex.Message)
            Return argRectangle
        End Try
    End Function

    Public Sub setHorizontalAlign(FlagInt As Integer, argPoints() As PointF, FrameWidth As Double)
        Try
            Dim TopPos As Double = argPoints(0).X
            Dim LeftPos As Double = argPoints(0).Y
            Dim RightPos As Double = argPoints(1).X
            Dim BottomPos As Double = argPoints(2).Y
            If FlagInt = 0 Then
                'Center
                Dim AddPos As Double = (FrameWidth - RightPos) / 2
                LeftPos = LeftPos + AddPos
                RightPos = RightPos + AddPos
            ElseIf (FlagInt = 1) Then
                'Left
            ElseIf (FlagInt = 2) Then
                'right
                RightPos = FrameWidth
                LeftPos = FrameWidth - RightPos
            End If

            PointsForDraw = {
                    New PointF(LeftPos, TopPos),
                    New PointF(RightPos, TopPos),
                    New PointF(LeftPos, BottomPos)
                }
        Catch ex As Exception
            Console.WriteLine("setHorizontal")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    Public Sub setVerticalAlign(FlagInt As Integer, argPoints() As PointF, FrameHeight As Double)
        Try
            Dim TopPos As Double = argPoints(0).Y
            Dim LeftPos As Double = argPoints(0).X
            Dim RightPos As Double = argPoints(1).X
            Dim BottomPos As Double = argPoints(2).Y
            If FlagInt = 0 Then
                'Center
                Dim AddPos As Double = (FrameHeight - BottomPos) / 2
                TopPos = TopPos + AddPos
                BottomPos = BottomPos + AddPos
            ElseIf (FlagInt = 1) Then
                'Top
            ElseIf (FlagInt = 2) Then
                'bottom
                Dim AddPos As Double = FrameHeight
                TopPos = FrameHeight - BottomPos
                BottomPos = FrameHeight

            End If
            PointsForDraw = {
                    New PointF(LeftPos, TopPos),
                    New PointF(RightPos, TopPos),
                    New PointF(LeftPos, BottomPos)
                }
        Catch ex As Exception
            Console.WriteLine("setVerticalAlign")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

End Class
