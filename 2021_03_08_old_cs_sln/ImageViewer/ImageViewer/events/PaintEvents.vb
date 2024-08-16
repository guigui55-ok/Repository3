Public Class PaintEvents
    Inherits AbstractEvents

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
    'PaintMain
    '=====================================================================================
    Public Sub PaintMain(argViewImageManager As ViewImageManager)
        Try
            argViewImageManager.gEvents.gViewer.SetViewTriger()
            'argViewImageManager.gEvents.gViewer.View()
            'MainProcessorObject.gMainForm.gControls.gStatusStripEvents.updateText(
            '    argViewImageManager.gImageFileList.getNowFilePath)
        Catch ex As Exception
            addLogForSystemError("PaintMain")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    'Public Sub PaintForSlideShow(argViewImageManager As ViewImageManager)
    '    Try
    '        MainProcessorObject.gEvents.gPaint.NextPaintImage(argViewImageManager)
    '        'PaintMain()
    '    Catch ex As Exception
    '        Console.WriteLine("PaintForSlideShow")
    '        Console.WriteLine(ex.Message)
    '    End Try
    'End Sub
    '=====================================================================================
    'PaintActionEvent
    '=====================================================================================
    Public Sub PaintForSlideShow(argViewImageManager As ViewImageManager)
        Try
            '表示
            'MainProcessorObject.gState.Fade.FadeOutBegin = True
            'argViewImageManager.gState.gFade.FadeOutBegin = True
            MainProcessorObject.gEvents.gPaint.NextPaintImage(argViewImageManager)
            'PaintMain()
        Catch ex As Exception
            addLogForSystemError("PaintForSlideShow")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    '=====================================================================================
    Public Sub MoveListIndex(argViewImageManager As ViewImageManager, argIndex As Integer, DirectionFlag As Integer)
        argViewImageManager.gImageFileList.moveListIndex(argIndex, DirectionFlag)
        'MainProcessorObject.gState.Gif.resetFlagAll()
        'addLogForSystemError("MainProcessorObject.State.Gif.resetFlagAll() remake")
    End Sub

    '次へ
    Public Sub NextPaintImage(argViewImageManager As ViewImageManager)
        'MainProcessorObject.gState.Fade.FadeOutBegin = True
        'MainProcessorObject.gState.ChangeFileBeginNow = True

        'argViewImageManager.gState.gFade.FadeOutBegin = True
        'argViewImageManager.gEvents.gViewer.DoFadeOutBeginFlagToBeTrueIfFadeSettingON()
        argViewImageManager.gImageFileList.moveNext()
        'argViewImageManager.gState.gChangeFileBegin = True
        PaintMain(argViewImageManager)
        'MainProcessorObject.gMainForm.gControls.gStatusStripEvents.updateText(
        '    argViewImageManager.gImageFileList.getNowFilePath)
        addLog(3, "MainProcessorObject.argViewImageManager : NextPaintImage", argViewImageManager.gImageFileList.getNowFilePath)
    End Sub

    '前へ
    Public Sub PreviousPaintImage(argViewImageManager As ViewImageManager)
        'MainProcessorObject.gState.Fade.FadeOutBegin = True
        'MainProcessorObject.gState.ChangeFileBeginNow = True

        'argViewImageManager.gState.gFade.FadeOutBegin = True
        'argViewImageManager.gEvents.gViewer.DoFadeOutBeginFlagToBeTrueIfFadeSettingON()
        argViewImageManager.gImageFileList.movePreviousIndex()
        'argViewImageManager.gState.gChangeFileBegin = True
        PaintMain(argViewImageManager)
        'MainProcessorObject.gMainForm.gControls.gStatusStripEvents.updateText(
        '    argViewImageManager.gImageFileList.getNowFilePath)
        addLog(3, "MainProcessorObject.argViewImageManager : PreviousPaintImage", argViewImageManager.gImageFileList.getNowFilePath)
    End Sub

    'リストの最初へ
    Public Sub FirstImageOfList(argViewImageManager As ViewImageManager)
        'MainProcessorObject.gState.Fade.FadeOutBegin = True
        'MainProcessorObject.gState.ChangeFileBeginNow = True

        'argViewImageManager.gState.gFade.FadeOutBegin = True
        argViewImageManager.gImageFileList.moveListIndex(0, 1)
        'argViewImageManager.gState.gChangeFileBegin = True
        PaintMain(argViewImageManager)
        'MainProcessorObject.gMainForm.gControls.gStatusStripEvents.updateText(
        '    argViewImageManager.gImageFileList.getNowFilePath)
        addLog(3, "MainProcessorObject.argViewImageManager : FirstImageOfList", argViewImageManager.gImageFileList.getNowFilePath)
    End Sub

    Public Sub LastImageOfList(argViewImageManager As ViewImageManager)
        'MainProcessorObject.gState.Fade.FadeOutBegin = True
        'MainProcessorObject.gState.ChangeFileBeginNow = True

        'argViewImageManager.gState.gFade.FadeOutBegin = True
        argViewImageManager.gImageFileList.moveListIndex(argViewImageManager.gImageFileList.Count, 2)
        'argViewImageManager.gState.gChangeFileBegin = True
        PaintMain(argViewImageManager)
        'MainProcessorObject.gMainForm.gControls.gStatusStripEvents.updateText(
        '    argViewImageManager.gImageFileList.getNowFilePath)
        addLog(3, "MainProcessorObject.argViewImageManager : LastImageOfList", argViewImageManager.gImageFileList.getNowFilePath)
    End Sub

    Public Sub MainFormMoveLocation(e As MouseEventArgs)
        MainProcessorObject.gMainForm.moveLocation(e, MainProcessorObject.gState.gMouse.DownPoint)
    End Sub

    '=====================================================================================
    '=====================================================================================
    'Default
    Public Sub PaintImageDefaultNotDispose(argViewImageManager As ViewImageManager)
        'Try
        '    If argViewImageManager.gImageFileList Is Nothing Then Exit Sub
        '    MainProcessorObject.ImageManagerObject.setImagePath(argViewImageManager.gImageFileList.getNowFilePath)
        '    MainProcessorObject.PaintSettingTemplateObject.paintImageDefault()
        '    MainProcessorObject.ImageManagerObject.paintImageOnlyNotSetting()
        'Catch ex As Exception
        '    addLogForSystemError("PaintImageDefaultNotDispose")
        '    addLogForSystemError(ex.Message)
        'End Try
        argViewImageManager.gFunction.gViewer.PaintImageDefaultNotDispose()
    End Sub

    'Default
    Public Sub PaintImageDefault(argViewImageManager As ViewImageManager)
        'Try
        '    If argViewImageManager.gImageFileList Is Nothing Then Exit Sub
        '    MainProcessorObject.ImageManagerObject.setImagePath(argViewImageManager.gImageFileList.getNowFilePath)
        '    MainProcessorObject.PaintSettingTemplateObject.paintImageDefault()
        '    MainProcessorObject.ImageManagerObject.paintImageOnlyNotSetting()
        'Catch ex As Exception
        '    addLogForSystemError("PaintImageDefault")
        '    addLogForSystemError(ex.Message)
        'End Try
        argViewImageManager.gFunction.gViewer.PaintImageDefault()
    End Sub

    'SizeAndLocationKeep
    Public Sub paintImageRefreshSizeAndLocationKeep(argViewImageManager As ViewImageManager)
        'MainProcessorObject.ImageManagerObject.setImagePath(argViewImageManager.gImageFileList.getNowFilePath())
        'MainProcessorObject.PaintSettingTemplateObject.paintImageRefreshSizeAndLocationKeep()
        'MainProcessorObject.ImageManagerObject.paintImageOnlyNotSetting()
        argViewImageManager.gFunction.gViewer.paintImageRefreshSizeAndLocationKeep()
    End Sub

    'currentAlphaPercent
    'SizeAndLocationKeep
    '透過させて描画
    'Drawing with transparency
    Public Sub paintImageRefreshSizeAndLocationKeepWithTransparncy(argViewImageManager As ViewImageManager, currentAlphaPercent As Integer)
        'MainProcessorObject.ImageManagerObject.getPaintImage.setDrawImage(New DrawImage(MainProcessorObject.ImageManagerObject.getImageFromControl()))
        'MainProcessorObject.ImageManagerObject.setPaintImageEffectSetting(New PaintImageEffectSetting(currentAlphaPercent))
        'MainProcessorObject.PaintSettingTemplateObject.paintImageRefreshSizeAndLocationKeep()
        'MainProcessorObject.ImageManagerObject.paintImageOnlyNotSetting()
        argViewImageManager.gFunction.gViewer.paintImageRefreshSizeAndLocationKeepWithTransparncy(currentAlphaPercent)
    End Sub

    'Default
    Public Sub paintImageWithTransparncy(argViewImageManager As ViewImageManager, currentAlphaPercent As Integer)
        'Try
        '    If argViewImageManager.gImageFileList Is Nothing Then Exit Sub
        '    'Movieの場合は最初のイメージだけ取り出してフェードイン
        '    MainProcessorObject.ImageManagerObject.setImagePath(argViewImageManager.gImageFileList.getNowFilePath())
        '    MainProcessorObject.ImageManagerObject.setPaintImageEffectSetting(New PaintImageEffectSetting(currentAlphaPercent))
        '    MainProcessorObject.PaintSettingTemplateObject.paintImageDefault()
        '    MainProcessorObject.ImageManagerObject.paintImageOnlyNotSetting()
        'Catch ex As Exception
        '    addLogForSystemError("paintImageWithTransparncy")
        '    addLogForSystemError(ex.Message)
        'End Try
        argViewImageManager.gFunction.gViewer.paintImageWithTransparncy(currentAlphaPercent)
    End Sub

    Public Sub PaintImageNormal(argViewImageManager As ViewImageManager)
        MainProcessorObject.gFunction.gPaint.PaintImageDefault(argViewImageManager)
        argViewImageManager.gFunction.gViewer.PaintImageDefault()
    End Sub

End Class
