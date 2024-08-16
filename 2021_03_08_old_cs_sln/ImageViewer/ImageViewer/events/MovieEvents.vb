Imports ImageViewer

Public Class MovieEvents
    Inherits AbstractEvents
    Public State As ImageViewerState

    Public Sub New(argMainProcessor As MainProcesser)
        'MyBase.New(argMainProcessor)
        MainProcessorObject = argMainProcessor
    End Sub

    Public Sub Image_FrameChanged(ByVal o As Object, ByVal e As EventArgs)
        Try
            Dim PicBox As PictureBox =
                MainProcessorObject.gNowViewImageManager.gImageManager.getPaintImage.getControlForPaint.getControlBridge.getControl()
            PicBox.Invalidate()
        Catch ex As Exception
            Console.WriteLine("Image_FrameChanged")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub Image_FrameStoped(ByVal o As Object, ByVal e As EventArgs)
        Try

        Catch ex As Exception
            Console.WriteLine("Image_FrameStoped")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


End Class
