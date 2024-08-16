Public Class PaintFunction
    Inherits AbstractFunction

    'Friend MainProcessorObject As MainProcesser
    'Public Sub New(argMainProcessor As MainProcesser)
    '    MainProcessorObject = argMainProcessor
    'End Sub
    'Public Sub New()
    'End Sub
    'Private Sub setMainProcessor(argMainProcessor As MainProcesser)
    Public Sub New(argMainProcessorObject As MainProcesser)
        MainProcessorObject = argMainProcessorObject
    End Sub
    '=======================================================================

    '=====================================================================================
    'ChangeLocattion_WidthinMouse
    'OK
    Public Sub PictureBoxChangeLocation(e As MouseEventArgs)
        MainProcessorObject.gNowViewImageManager.gImageManager.getPaintImage.getControlForPaint.changeLocationWithinFrame(
            MainProcessorObject.gNowViewImageManager.gImageManager.getPaintImage.getControlFrame.getSize,
            e, MainProcessorObject.gState.gMouse.DownPoint)
    End Sub
    '=====================================================================================
    'PaintAction
    '=====================================================================================

    '=====================================================================================
    'PaintMain
    '=====================================================================================
    'PaintMain旧
    Public Sub PaintMain(argViewImageManager As ViewImageManager)
        Try
            argViewImageManager.gState.gFade.FadeOutBegin = True
            argViewImageManager.gEvents.gViewer.View()
            Me.setValueToStatusBar(argViewImageManager.gImageFileList.getNowFilePath)
        Catch ex As Exception
            addLogForSystemError("PaintMain")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    'Default
    Public Sub PaintImageDefault(argViewImageManager As ViewImageManager)
        argViewImageManager.gFunction.gViewer.PaintImageDefault()
    End Sub

    'Default
    Public Sub PaintImageDefaultNotDispose(argViewImageManager As ViewImageManager)
        argViewImageManager.gFunction.gViewer.PaintImageDefaultNotDispose()
    End Sub

    'SizeAndLocationKeep
    Public Sub paintImageRefreshSizeAndLocationKeep(argViewImageManager As ViewImageManager)
        argViewImageManager.gFunction.gViewer.paintImageRefreshSizeAndLocationKeep()
    End Sub

    'SizeAndLocationKeep
    '透過させて描画
    'Drawing with transparency
    Public Sub paintImageRefreshSizeAndLocationKeepWithTransparncy(argViewImageManager As ViewImageManager, currentAlphaPercent As Integer)
        argViewImageManager.gFunction.gViewer.paintImageRefreshSizeAndLocationKeepWithTransparncy(currentAlphaPercent)
    End Sub

    'Default
    Public Sub paintImageWithTransparncy(argViewImageManager As ViewImageManager, currentAlphaPercent As Integer)
        argViewImageManager.gFunction.gViewer.paintImageWithTransparncy(currentAlphaPercent)
    End Sub

    '=====================================================================================
    Public Sub setValueToStatusBar(argValue As String)
        Try
            MainProcessorObject.gMainForm.gControls.gStatusStripEvents.updateText(argValue)
        Catch ex As Exception
            addLogForSystemError("setValueToStatusBar")
            addLogForSystemError(ex.Message)
        End Try
    End Sub
    '=====================================================================================
    Public Sub MoveListIndex(argViewImageManager As ViewImageManager, argIndex As Integer, DirectionFlag As Integer)
        argViewImageManager.gFunction.gViewer.MoveListIndex(argIndex, DirectionFlag)
    End Sub

    Public Sub MainFormMoveLocation(e As MouseEventArgs)
        MainProcessorObject.gMainForm.moveLocation(e, MainProcessorObject.gState.gMouse.DownPoint)
    End Sub

End Class
