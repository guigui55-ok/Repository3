Public Class ControlBridgeForPictureBox
    Inherits AbstractControlBridge

    Private PicBox As PictureBox

    Public Sub New(argPictureBox As PictureBox)
        PicBox = argPictureBox
    End Sub

    Public Overrides Function getControl() As Object
        Return PicBox
    End Function

    Public Overrides Sub setImage(argImage As Image)
        Try
            PicBox.Image = argImage
        Catch ex As Exception
            Console.WriteLine("setImage")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Overloads Sub dispose()
        If Not PicBox.Image Is Nothing Then
            PicBox.Image.Dispose()
            PicBox.Image = Nothing
        End If
        If Not PicBox Is Nothing Then
            PicBox.Dispose()
            PicBox = Nothing
        End If
    End Sub
End Class
