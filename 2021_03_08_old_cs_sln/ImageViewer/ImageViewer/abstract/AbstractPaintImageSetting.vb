Imports ImageViewer

Public Class AbstractPaintImageSetting
    Implements IPaintImageSetting

    Protected Friend ImagePath As String
    Protected Friend InnerFrameSize As Size
    Protected Friend AutorFrameSize As Size
    Protected Friend ImageSize As Size
    Protected Friend PaintImage As Image

    Protected Friend DrawRect As Rectangle 'destRect
    Protected Friend DrawPoints() As PointF
    Protected Friend ReadImageRegion As Rectangle 'srcRect

    Protected Friend VerticalPosition As Integer
    Protected Friend HorizontalPosition As Integer
    Protected Friend DrawPosition As Point
    'Private DrawLocation As Point

    Public Sub setImage(argImage As Image) Implements IPaintImageSetting.setImage
        PaintImage = argImage
    End Sub

    Public Sub setPath(path As String) Implements IPaintImageSetting.setPath
        ImagePath = path
    End Sub

    Public Sub setImageSize(size As Point) Implements IPaintImageSetting.setImageSize
        ImageSize = size
    End Sub

    Public Sub setInnerFrameSize(size As Size) Implements IPaintImageSetting.setInnerFrameSize
        InnerFrameSize = size
    End Sub

    Public Sub SetPosition(argVerticalPosition As Integer, ArgHorizontalPosition As Integer) Implements IPaintImageSetting.setPosition
        VerticalPosition = argVerticalPosition
        HorizontalPosition = ArgHorizontalPosition
    End Sub

    Public Sub setAutorFrameSize(size As Size) Implements IPaintImageSetting.setAutorFrameSize
        AutorFrameSize = size
    End Sub

    Public Sub setDrawRegion(argRegion As Rectangle) Implements IPaintImageSetting.setDrawRegion
        DrawRect = argRegion
    End Sub

    Public Sub setReadImageRegion(argRegion As Rectangle) Implements IPaintImageSetting.setReadImageRegion
        ReadImageRegion = argRegion
    End Sub

    Public Function getInnerFrameSize() As Point Implements IPaintImageSetting.getInnerFrameSize
        Return InnerFrameSize
    End Function

    '画像描画領域_ここで拡大縮小 計算
    Public Overridable Sub calcDrawRegion(argControlSize As Size, argImageSize As Size) Implements IPaintImageSetting.calcDrawRegion
        InnerFrameSize = argControlSize
        ImageSize = argImageSize
        Try
            Dim CalcDrawReginFactory As CalcDrawRegionFactory = New CalcDrawRegionFactory()

            '画像描画領域_ここで拡大縮小 計算
            '値をセット
            CalcDrawReginFactory.setRegionSetting(0, 0)
            CalcDrawReginFactory.setSize(argControlSize, argImageSize)

            Dim CalcDrawRegionObject As AbstractCalcDrawRegion = CalcDrawReginFactory.createCalcDrawRegion()
            CalcDrawRegionObject.calcDrawRegionMatchToControlSize()

            DrawPoints = CalcDrawRegionObject.getPoints

            CalcDrawRegionObject = Nothing
            CalcDrawReginFactory = Nothing
        Catch ex As Exception
            Console.WriteLine("calcDrawRegion")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Overridable Sub calcDrawRegion() Implements IPaintImageSetting.calcDrawRegion
        calcDrawRegion(Me.InnerFrameSize, Me.ImageSize)
    End Sub


    Public Function getImageSize() As Size Implements IPaintImageSetting.getImageSize
        Return ImageSize
    End Function

    Public Sub setDra()

    End Sub

    'Region領域
    Public Function getDrawRegionAsRectangle() As Rectangle Implements IPaintImageSetting.getDrawRegionAsRectangle
        Try

            '画像描画領域計算 DrawWidth DrawHeightに格納
            'calcDrawRectangleByImage(DrawImage, ArgPictureBox, 1)
            'Dim srcRect As Rectangle
            '画像描画領域
            'destRect = New Rectangle(0, 0, DrawWidth, DrawHeight)
            'calc
            Return DrawRect
        Catch ex As Exception
            Console.WriteLine("getDrawRegionAsRectangle")
            Console.WriteLine(ex.Message)
            Return DrawRect
        End Try
    End Function

    'Region領域
    Public Function getDrawRegionAspoints() As PointF() Implements IPaintImageSetting.getDrawRegionAsPoints
        Try

            '画像描画領域計算 DrawWidth DrawHeightに格納
            'calcDrawRectangleByImage(DrawImage, ArgPictureBox, 1)
            'Dim srcRect As Rectangle
            '画像描画領域
            'destRect = New Rectangle(0, 0, DrawWidth, DrawHeight)
            'calc
            Return DrawPoints
        Catch ex As Exception
            Console.WriteLine("getDrawRegionAspoints")
            Console.WriteLine(ex.Message)
            Return DrawPoints
        End Try
    End Function

    Public Function getReadImageRegion() As Rectangle Implements IPaintImageSetting.getReadImageRegion
        Try

            '画像読み込み領域
            'srcRect = New Rectangle(New Point(0, 0), New Size(DrawImage.Width, DrawImage.Height))
            Return ReadImageRegion
        Catch ex As Exception
            Console.WriteLine("getReadImageRegion")
            Console.WriteLine(ex.Message)
            Return ReadImageRegion
        End Try
    End Function

    Public Function InnerFrameSizeIsZero() As Boolean Implements IPaintImageSetting.InnerFrameSizeIsZero
        Try
            If InnerFrameSize.Width = 0 Or InnerFrameSize.Height = 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Return True
            Console.WriteLine("InnerFrameSizeIsZero")
            Console.WriteLine(ex.Message)
        End Try
    End Function

    'Default
    Public Overridable Sub calcRegionDefaultToReadAndDraw() Implements IPaintImageSetting.calcRegionDefaultToReadAndDraw
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

    Private Sub calcRegionToReadAndDraw() Implements IPaintImageSetting.calcRegionToReadAndDraw
        calcRegionDefaultToReadAndDraw()
    End Sub

    Public Sub writeConsoleDrawPoints() Implements IPaintImageSetting.writeConsoleDrawPoints
        Try
            Dim str As String
            str = DrawPoints(0).X & " ," & DrawPoints(0).Y & " ,"
            str = str & DrawPoints(1).X & " ," & DrawPoints(1).Y & " ,"
            str = str & DrawPoints(2).X & " ," & DrawPoints(2).Y
            ConsoleWriteLine(str)
        Catch ex As Exception
            Console.WriteLine("writeConsoleDrawPoints")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
End Class
