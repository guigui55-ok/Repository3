Public Class PointsForDrawSquareImage
    Private PointsForDraw() As PointF
    Private ImageWidth As Double
    Private ImageHeight As Double
    Public PictureBoxSize As Point
    Public NowRaito As Double
    'Public ResultPoint As RectangleF

    Private AfterImageWidth As Double
    Private AfterImageHeight As Double

    Public Sub New()

    End Sub

    Public Sub New(x As Double, y As Double, width As Double, height As Double)
        setPoints(x, y, width, height)
    End Sub

    Public Function getPoints() As PointF()
        Return PointsForDraw
    End Function



    Public Sub setPoints(x As Double, y As Double, width As Double, height As Double)
        Try
            PointsForDraw = {
                New PointF(x, y),
                New PointF(width, y),
                New PointF(x, height)
            }
            ImageWidth = width - x
            ImageHeight = height - y
        Catch ex As Exception
            Console.WriteLine("New_x_y_width_height")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

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

    'Imageを表示する位置と大きさを設定
    'g.DrawImageするときの
    '描画する領域を計算する
    'PictureBoxの領域をイメージの比率に合わせる
    'ウィンドウの大きさに合わせる
    Public Function calcRaitoImageByPictureBox(ByRef argBaseImage As Image, ByRef argPictureBox As PictureBox) As PointF
        Try
            Dim NewWidth As Double
            Dim NewHeight As Double


            Dim VerticalRaito As Double = argPictureBox.Height / argBaseImage.Height
            Dim HorizontalRaito As Double = argPictureBox.Width / argBaseImage.Width

            '倍率が小さいほうを設定
            If VerticalRaito < HorizontalRaito Then
                NewHeight = argPictureBox.Height * 1
                NewWidth = argBaseImage.Width * VerticalRaito
                NowRaito = VerticalRaito
            Else
                NewHeight = argBaseImage.Height * HorizontalRaito
                NewWidth = argPictureBox.Width * 1
                NowRaito = HorizontalRaito
            End If

            Return New PointF(NewWidth, NewHeight)
        Catch ex As Exception
            Console.WriteLine("calcRaitoImageByPictureBox")
            Console.WriteLine(ex.Message)
            Return New PointF(0, 0)
        End Try
    End Function

    'サイズ型版
    Public Function calcRaitoImageByPictureBox(ByRef argBaseImage As Size, ByRef argPictureBox As Size) As PointF
        Try
            Dim NewWidth As Double
            Dim NewHeight As Double


            Dim VerticalRaito As Double = argPictureBox.Height / argBaseImage.Height
            Dim HorizontalRaito As Double = argPictureBox.Width / argBaseImage.Width

            '倍率が小さいほうを設定
            If VerticalRaito < HorizontalRaito Then
                NewHeight = argPictureBox.Height * 1
                NewWidth = argBaseImage.Width * VerticalRaito
                NowRaito = VerticalRaito
            Else
                NewHeight = argBaseImage.Height * HorizontalRaito
                NewWidth = argPictureBox.Width * 1
                NowRaito = HorizontalRaito
            End If

            Return New PointF(NewWidth, NewHeight)
        Catch ex As Exception
            Console.WriteLine("calcRaitoImageByPictureBox")
            Console.WriteLine(ex.Message)
            Return New PointF(0, 0)
        End Try
    End Function

    Public Function getDataForDebug() As String
        Dim rtn As String = ""
        rtn = rtn & " x:" & Me.PointsForDraw(0).X & vbNewLine
        rtn = rtn & " y:" & Me.PointsForDraw(0).Y & vbNewLine
        rtn = rtn & " x:" & Me.PointsForDraw(1).X & vbNewLine
        rtn = rtn & " y:" & Me.PointsForDraw(1).Y & vbNewLine
        rtn = rtn & " x:" & Me.PointsForDraw(2).X & vbNewLine
        rtn = rtn & " y:" & Me.PointsForDraw(2).Y & vbNewLine
        Return rtn
    End Function

    Public Sub OutPutRectF(rect As RectangleF)
        Try
            Dim rtn As String = ""
            rtn = rtn & " x:" & rect.X &
            rtn = rtn & " y:" & rect.Y & vbNewLine
            rtn = rtn & " w:" & rect.Width & vbNewLine
            rtn = rtn & " h:" & rect.Height & vbNewLine
            AddLog(rtn)
        Catch ex As Exception
            Console.WriteLine("OutPutRectF")
        End Try
    End Sub

    'サイズを決めるPointFへ
    Public Function setPointsForImageResize(
            argPictureBox As PictureBox, NowWidth As Double, NowHeight As Double,
            argNowRaito As Double, argAfterRaito As Double,
            VerticalAlign As Integer, HorizontalAlign As Integer
            ) As Integer
        Try
            setPointsForImageResize(
                argPictureBox.Width, argPictureBox.Height,
                NowWidth, NowHeight, argNowRaito, argAfterRaito, VerticalAlign, HorizontalAlign,
                System.Windows.Forms.Cursor.Position,
                New MousePointer().IsCursorOnControl(argPictureBox)
            )
            Return 1
        Catch ex As Exception
            Console.WriteLine("calcRaitoImageByPictureBox")
            Console.WriteLine(ex.Message)
            Return -1
        End Try
    End Function


    'サイズを決めるPointFへ
    '通常
    Public Function setPointsForImageResize(
            PicBoxWidth As Double, PicBoxHeight As Double, NowWidth As Double, NowHeight As Double,
            argNowRaito As Double, argAfterRaito As Double,
            VerticalAlign As Integer, HorizontalAlign As Integer
            ) As Integer
        Try
            setPointsForImageResize(
                PicBoxWidth, PicBoxHeight,
                NowWidth, NowHeight, argNowRaito, argAfterRaito, VerticalAlign, HorizontalAlign,
                System.Windows.Forms.Cursor.Position,
                False
            )
            Return 1
        Catch ex As Exception
            Console.WriteLine("calcRaitoImageByPictureBox")
            Console.WriteLine(ex.Message)
            Return -1
        End Try
    End Function

    Public Function setPointsForImageResize_LeftTop(
            DrawWidth As Double, DrawHeigh As Double, NowWidth As Double, NowHeight As Double,
            argNowRaito As Double, argAfterRaito As Double,
            VerticalAlign As Integer, HorizontalAlign As Integer
            ) As Integer
        Try
            'まず倍率をかけて画像の大きさを決める　サイズ
            Dim AfterWidth As Double = NowWidth * argAfterRaito
            Dim AfterHeight As Double = NowHeight * argAfterRaito

            '画像の大きさとControlの大きさ＋表示する位置をFlagintで決める
            Dim NewRect As RectangleF
            NewRect.X = 0
            NewRect.Y = 0
            NewRect.Width = AfterWidth
            NewRect.Height = AfterHeight
            '結果をPointFにして Private変数へ
            Me.PointsForDraw = {
                New PointF(NewRect.X, NewRect.Y),
                New PointF(NewRect.Width, NewRect.Y),
                New PointF(NewRect.X, NewRect.Height)
            }
            Return 1
        Catch ex As Exception
            Console.WriteLine("setPointsForImageResize_LeftTop")
            Console.WriteLine(ex.Message)
            Return -1
        End Try
    End Function



    'サイズを決めるPointFへ
    '拡大縮小用
    Public Function setPointsForImageResize(
            DrawWidth As Double, DrawHeigh As Double, NowWidth As Double, NowHeight As Double,
            argNowRaito As Double, argAfterRaito As Double,
            VerticalAlign As Integer, HorizontalAlign As Integer,
            mp As Point,mpInPicBox As boolean 
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
            NowRaito = argAfterRaito
            '画像の大きさとControlの大きさ＋表示する位置をFlagintで決める
            Dim NewRect As RectangleF

            If mpInPicBox Then
                NewRect = calcSizeHeightForMethodSetPointsForImageResizeByCursor(
                    0, argAfterRaito, DrawWidth, DrawHeigh, AfterWidth, AfterHeight, NewRect, mp)
            Else
                '幅高さを計算
                NewRect = calcSizeWidthForMethodsetPointsForImageResize(HorizontalAlign, DrawWidth, AfterWidth, NewRect)
                NewRect = calcSizeHeightForMethodsetPointsForImageResize(VerticalAlign, DrawHeigh, AfterHeight, NewRect)
            End If

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

    '拡大縮小Cursor考慮版
    Private Function calcSizeHeightForMethodSetPointsForImageResizeByCursor(
            FlagInt As Integer, argAfterRaito As Double,
            ControlWidth As Double, ControlHeight As Double,
            NowWidth As Double, NowHeight As Double, argRectangle As RectangleF,
            mp As Point) As RectangleF
        Try

            'Cursorが0,0にあるときに、0,0を中心にして拡大する
            'つまり、x-mp.x,y-mp.y  width - mp.x , height - mp.y となる
            'しかし、それでは画像拡大ではなくPictureBox拡大になる
            'imageのマイナス領域とimage以上の領域の描画は避けたいため
            'x.mp y.mp　と　drawimage.width height - picturebox.width height 　をくらべて
            '引く値を　mpのほうが大きければ　image-picbox　の値にする
            '新しいx y値がマイナスならゼロ(下限値)
            '新しいwidth height値がpicturebox以上なら上限値　とする
            'mpを中心に描きたいので
            'mp-picturebox/2　の値を足し引きする
            ' x + mp.x - picturebox.width/2 , y + mp.y - picturebox.height/2 ...　となる

            '隙間ができるのはNG

            'まず倍率をかけて画像の大きさを決める　サイズ
            Dim AfterWidth As Double = NowWidth
            Dim AfterHeight As Double = NowHeight
            '左上拡大
            Dim Top As Double = 0
            Dim Left As Double = 0
            Dim Right As Double = AfterWidth
            Dim Bottom As Double = AfterHeight

            Dim NewX As Double = mp.X - (ControlWidth / 2)
            Dim NewY As Double = mp.Y - (ControlHeight / 2)

            Top = Top + NewX
            Left = Left + NewY
            Right = Right + NewX
            Bottom = Bottom + NewY

            Dim NewRect As RectangleF
            If (AfterWidth < ControlWidth) Then
                'Center
                NewRect = calcSizeWidthForMethodsetPointsForImageResize(0, ControlWidth, AfterWidth, NewRect)
            Else
                '大きい
                If Left < 0 Then
                    Left = 0
                    Right = AfterWidth
                End If
                If Right > ControlWidth Then
                    Right = AfterWidth
                    Left = AfterWidth - ControlWidth
                End If
            End If

            If AfterHeight < ControlHeight Then
                'Center
                NewRect = calcSizeHeightForMethodsetPointsForImageResize(0, ControlHeight, AfterHeight, NewRect)
            Else
                If Top < 0 Then
                    Top = 0
                    Bottom = AfterHeight
                End If
                If Bottom > ControlHeight Then
                    Bottom = AfterHeight
                    Top = AfterHeight - ControlHeight
                End If
            End If

            OutPutRectF(New RectangleF(Left, Top, Right, Bottom))
            Return New RectangleF(Left, Top, Right, Bottom)
        Catch ex As Exception
            Console.WriteLine("calcSizeHeightForMethodSetPointsForImageResizeByCursor")
            Console.WriteLine(ex.Message)
            Return argRectangle
        End Try
    End Function

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

    Public Function getDrawImageWidth(point() As PointF) As Double
        Try
            Return point(1).X - point(0).X
        Catch ex As Exception
            Console.WriteLine("getDrawImageWidth")
            Console.WriteLine(ex.Message)
            Return 0
        End Try
    End Function

    Public Function getDrawImageHeight(point() As PointF) As Double
        Try
            Return point(2).Y - point(0).Y
        Catch ex As Exception
            Console.WriteLine("getDrawImageHeight")
            Console.WriteLine(ex.Message)
            Return 0
        End Try
    End Function

    Public Function isImageBigThanFrame(ImagePoint As PointF(), FrameWidth As Double, FrameHeight As Double) As Boolean
        Try
            If getDrawImageWidth(ImagePoint) > FrameWidth Then
                Return True
            End If
            If getDrawImageHeight(ImagePoint) > FrameHeight Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Console.WriteLine("isImageBigThanFrame")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function
End Class
