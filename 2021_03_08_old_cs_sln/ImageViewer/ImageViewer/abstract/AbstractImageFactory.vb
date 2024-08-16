Imports ImageViewer

Public Class AbstractImageFactory
    Implements IPaintImageFactory

    Public Sub disposeObjects() Implements IPaintImageFactory.disposeObjects
        Try

        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub setImageManager(imageManager As ImageManager) Implements IPaintImageFactory.setImageManager
        Throw New NotImplementedException()
    End Sub

    Public Sub paintImage(pictureBox As PictureBox, Path As String) Implements IPaintImageFactory.paintImage
        Throw New NotImplementedException()
    End Sub

    Public Sub disposeImage() Implements IPaintImageFactory.disposeImage
        Throw New NotImplementedException()
    End Sub

    Public Sub paintImageRefresh() Implements IPaintImageFactory.paintImageRefresh
        Throw New NotImplementedException()
    End Sub

    Public Sub DoSetting() Implements IPaintImageFactory.DoSetting
        Throw New NotImplementedException()
    End Sub

    Public Sub DoSettingForPaint() Implements IPaintImageFactory.DoSettingForPaint
        Throw New NotImplementedException()
    End Sub

    Public Sub DoSettingForControl() Implements IPaintImageFactory.DoSettingForControl
        Throw New NotImplementedException()
    End Sub

    Public Sub DoSettingForPaintEffect() Implements IPaintImageFactory.DoSettingForPaintEffect
        Throw New NotImplementedException()
    End Sub

End Class
