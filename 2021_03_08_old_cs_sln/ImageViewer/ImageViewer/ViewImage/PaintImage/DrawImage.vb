Imports ImageViewer

Public Class DrawImage
    Implements IDrawImage

    Private ImageForDraw As Image
    Private ImagePath As String

    Public Sub New()

    End Sub

    Public Sub New(argImage As Image)
        setImage(argImage)
    End Sub

    Public Sub setImageFromPath(path As String) Implements IDrawImage.setImageFromPath
        ImagePath = path
        setImageByPath()
    End Sub

    Public Sub setImage(argImage As Image) Implements IDrawImage.setImage
        ImageForDraw = argImage
        ImagePath = ""
    End Sub

    Public Function getImageSize() As PointF() Implements IDrawImage.getImageSize
        Return {
            New PointF(0, 0),
            New PointF(ImageForDraw.Width, 0),
            New PointF(0, ImageForDraw.Height)
        }
    End Function


    Private Sub setImageByPath()
        Try
            If Not System.IO.File.Exists(ImagePath) Then
                Console.WriteLine("setImageByPath FilePath is Nothing")
                Exit Sub
            End If
            If Not ImageForDraw Is Nothing Then 'OK
                ImageForDraw.Dispose()
                ImageForDraw = Nothing
            End If
            ImageForDraw = Image.FromFile(ImagePath)
        Catch ex As Exception
            Console.WriteLine("DrawImage.setImageByPath")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Function getImage() As Image Implements IDrawImage.getImage
        Return Me.ImageForDraw
    End Function

    Public Sub disposeImage() Implements IDrawImage.disposeImage
        Try
            If Not ImageForDraw Is Nothing Then
                ImageForDraw.Dispose()
                ImageForDraw = Nothing
            End If
        Catch ex As Exception
            Console.WriteLine("disposeImage")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Function width() As Integer Implements IDrawImage.width
        Return ImageForDraw.Width
    End Function

    Public Function height() As Integer Implements IDrawImage.height
        Return ImageForDraw.Height
    End Function

    Public Function getSize() As Size Implements IDrawImage.getSize
        Try
            If ImageForDraw Is Nothing Then
                Console.WriteLine("DrawImage.getSize :  ImageForDraw Is Nothing")
                Return New Size(0, 0)
            Else
                Return ImageForDraw.Size
            End If
        Catch ex As Exception
            Console.WriteLine("DrawImage.getSize")
            Console.WriteLine(ex.Message)
            Return New Size(0, 0)
        End Try
    End Function

    Public Function hasImageIsNothing() As Boolean Implements IDrawImage.hasImageIsNothing
        If ImageForDraw Is Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
