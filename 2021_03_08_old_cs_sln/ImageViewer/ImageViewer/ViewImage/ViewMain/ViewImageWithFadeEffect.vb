Imports ImageViewer

Public Class ViewImageWithFadeEffect
    Inherits ViewMainDefault
    Implements IViewImageMain


    Private mPaintFade As PaintFade

    Public Sub New(argViewIMageManager As ViewImageManager)
        addLog(10, "ViewImageWithFadeEffect New")
        mPaintFade = New PaintFade(argViewIMageManager)
    End Sub

    Public Overloads Sub excuteViewImage() Implements IViewImageMain.excuteViewImage
        addLog(10, "ViewImageWithFadeEffect.excuteViewImage")
        mPaintFade.FadeTriger()
    End Sub

    Public Overloads Sub Dispose()
        If Not mPaintFade Is Nothing Then
            mPaintFade.Dispose()
            mPaintFade = Nothing
        End If
        mPaintFade = Nothing
    End Sub
End Class
