Public Class ImageViewerPaintFunction
    Inherits AbstractImageViewerChild
    Implements IDisposable

    'Friend mMainProcessorObject As MainProcesser
    'Friend mViewImageManager As ViewImageManager
    'Friend Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
    'Friend Sub New()
    'Friend Sub setMainProcessor(argMainProcessor As MainProcesser)
    'Friend Sub setViewImageManager(argViewImageManager As ViewImageManager)
    '=======================================================================
    Public Sub New(argMainProcessor As MainProcesser)
        MyBase.New(argMainProcessor, argMainProcessor.gViewImageManager)

        mViewImageMainMethodFactory = New ViewImageMainMethodFactory(argMainProcessor.gViewImageManager)
    End Sub

    Public Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
        MyBase.New(argMainProcessor, argViewImageManager)
        mMainProcessorObject = argMainProcessor
        mViewImageManager = argViewImageManager

        gPaintImageSettingTemplate = New PaintSettingTemplate(mMainProcessorObject, argViewImageManager)
        mViewImageMainMethodFactory = New ViewImageMainMethodFactory(mViewImageManager)
    End Sub
    '=======================================================================

    Private mViewImageMainMethodFactory As ViewImageMainMethodFactory
    Public gPaintImageSettingTemplate As PaintSettingTemplate

    '=====================================================================================
    '=====================================================================================
    Public Sub DisposeObjects()
        Try
            If Not mViewImageMainMethodFactory Is Nothing Then
                mViewImageMainMethodFactory.Dispose()
                mViewImageMainMethodFactory = Nothing
            End If
            If Not gPaintImageSettingTemplate Is Nothing Then
                'gPaintImageSettingTemplate.dispose
                gPaintImageSettingTemplate = Nothing
            End If

        Catch ex As Exception
            addLogForSystemError("DisposeObjects")
            addLogForSystemError(ex.Message)
        End Try
    End Sub
    '===============================================================================================
    Private isDisposed As Boolean = False ' リソースが破棄(解放)されていることを表すフラグ

    ' IDisposable.Disposeの実装
    '// Dispose() calls Dispose(true)
    Public Sub Dispose() Implements IDisposable.Dispose
        ' アンマネージリソースと、マネージリソースの両方を破棄させる
        Dispose(True)
        ' すべてのリソースが破棄されているため、以後ファイナライザの実行は不要であることをガベージコレクタに通知する
        GC.SuppressFinalize(Me)
    End Sub
    ' リソースの解放処理を行うためのメソッド
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        ' 既にリソースが破棄されている場合は何もしない
        If isDisposed Then Return

        ' 破棄されていないアンマネージリソースの解放処理を行う
        DisposeObjects()
        ' リソースは破棄されている
        isDisposed = True
    End Sub
    'Finalize
    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
    '===============================================================================================

    Public Sub setTrueFlagPlayMovieBegin()
        addLog(3, "setTrueFlagPlayMovieBegin True")
        mViewImageManager.gState.gMovie.MoviePlayBegin = True
    End Sub

    Public Sub DisposeNowPlay()
        Try
            addLog(10, "ImageViewerPaintFunction.DisposeNowPlay")
            mViewImageManager.gState.gMovie.resetFlagAll()
            mViewImageMainMethodFactory.Dispose()
        Catch ex As Exception
            addLogForSystemError("DisposePlay")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub PauseFile()
        addLog(3, "ImageViewerPaintFunction.Pause")
        mViewImageManager.gState.gMovie.MoviePause = True
        mViewImageMainMethodFactory.PauseFile()
    End Sub

    Public Sub PaintMain()
        Try
            mViewImageMainMethodFactory.excuteViewImage()
            'setValueToStatusBar(mMainProcessorObject.gViewImageManager.gImageFileList.getNowFilePath)
        Catch ex As Exception
            Console.WriteLine("PaintMain")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '90 degree rotation
    Public Sub paintImageRoTation90degree()
        addLog(3, "paintImageRoTation90degree")


        With mViewImageManager
            .gImageManager.getPaintImage.setImage(.gImageManager.getImageFromControl())

            'SaveBeforeSize_Location
            'PictureBox New
            'ImageViewObjects <- PictureBox
            'PictureBox Invalidate true
            mViewImageManager.gFunction.gPictureBox.refreshNotPaint()
            '90degreeProcess
            .gFunction.gViewer.gPaintImageSettingTemplate.paintImageRoTation90degree()
            'PictureBox Invalidate false
            mViewImageManager.gFunction.gPictureBox.resumeLayout()
            'Paint
            .gImageManager.paintImageOnlyNotDispose()
        End With
    End Sub

    Public Sub paintImageRoTation90degree_before()
        addLog(3, "paintImageRoTation90degree")
        'SaveBeforeSize_Location
        'PictureBox New
        'ImageViewObjects <- PictureBox
        'PictureBox Invalidate false
        '90degreeProcess
        'PictureBox Invalidate true
        'mViewImageManager.gFunction.gPictureBox.refreshNotView()
        With mViewImageManager
            '.gImageManager.getPaintImage.setDrawImage(New DrawImage(.gImageManager.getImageFromControl()))
            '.gImageManager.setPaintImageEffectSetting(New PaintImageEffectSetting(50))
            .gImageManager.getPaintImage.setDrawImage(New DrawImage(.gImageManager.getImageFromControl()))
            .gFunction.gViewer.gPaintImageSettingTemplate.paintImageRoTation90degree()
            .gImageManager.paintImageOnlyNotDispose()
        End With
    End Sub

    'Default
    Public Sub PaintImageDefault()
        Try
            If mViewImageManager.gImageFileList Is Nothing Then Exit Sub
            mViewImageManager.gImageManager.setImagePath(
                mViewImageManager.gImageFileList.getNowFilePath)
            mViewImageManager.gFunction.gViewer.gPaintImageSettingTemplate.paintImageDefault()
            mViewImageManager.gImageManager.paintImageOnlyNotSetting()
        Catch ex As Exception
            addLogForSystemError("PaintFunction.PaintImageDefault")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    'Default
    Public Sub PaintImageDefaultNotDispose()
        Try
            If mViewImageManager.gImageFileList Is Nothing Then Exit Sub
            mViewImageManager.gImageManager.setImagePath(mViewImageManager.gImageFileList.getNowFilePath)
            mViewImageManager.gFunction.gViewer.gPaintImageSettingTemplate.paintImageDefault()
            mViewImageManager.gImageManager.paintImageOnlyNotSetting()
        Catch ex As Exception
            Console.WriteLine("PaintImageDefaultNotDispose")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'Default
    Public Sub paintImageWithTransparncy(currentAlphaPercent As Integer)
        Try
            With mViewImageManager
                If .gImageFileList Is Nothing Then Exit Sub
                'Movieの場合は最初のイメージだけ取り出してフェードイン
                .gImageManager.setImagePath(.gImageFileList.getNowFilePath())
                .gImageManager.setPaintImageEffectSetting(New PaintImageEffectSetting(currentAlphaPercent))
                .gFunction.gViewer.gPaintImageSettingTemplate.paintImageDefault()
                .gImageManager.paintImageOnlyNotSetting()
            End With
        Catch ex As Exception
            addLogForSystemError("paintImageWithTransparncy")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    'SizeAndLocationKeep
    '透過させて描画
    'Drawing with transparency
    Public Sub paintImageRefreshSizeAndLocationKeepWithTransparncy(currentAlphaPercent As Integer)
        With mViewImageManager
            .gImageManager.getPaintImage.setDrawImage(New DrawImage(.gImageManager.getImageFromControl()))
            .gImageManager.setPaintImageEffectSetting(New PaintImageEffectSetting(currentAlphaPercent))
            .gFunction.gViewer.gPaintImageSettingTemplate.paintImageRefreshSizeAndLocationKeep()
            .gImageManager.paintImageOnlyNotSetting()
        End With
    End Sub

    'SizeAndLocationKeep
    Public Sub paintImageRefreshSizeAndLocationKeep()
        With mViewImageManager
            .gImageManager.setImagePath(.gImageFileList.getNowFilePath)
            .gFunction.gViewer.gPaintImageSettingTemplate.paintImageRefreshSizeAndLocationKeep()
            .gImageManager.paintImageOnlyNotSetting()
        End With
    End Sub

    '=====================================================================================
    Public Sub MoveListIndex(argIndex As Integer, DirectionFlag As Integer)
        mViewImageManager.gImageFileList.moveListIndex(argIndex, DirectionFlag)
        'mViewImageManager.gState.Gif.resetFlagAll()
    End Sub
    '=====================================================================================
    Public Sub setValueToStatusBar(argValue As String)
        Try
            mMainProcessorObject.gMainForm.gControls.gStatusStripEvents.getStatusStrip.Items(0).Text _
                = argValue
        Catch ex As Exception
            Console.WriteLine("setValueToStatusBar")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    'NowSize
    Sub paintImageWithTransparncyWithEffect()
        mViewImageManager.gImageManager.setPaintImageEffectSetting(
            New PaintImageEffectSetting(mViewImageManager.gState.gFade.currentAlphaPercent))

        Me.paintImageWithTransparncy(mViewImageManager.gState.gFade.currentAlphaPercent)
    End Sub

    '=====================================================================================
    'fade用
    '=====================================================================================
    Public Function ControlsImageIsNothing()
        Return mViewImageManager.gImageManager.getPaintImage.getControlForPaint.hasImageIsNothing
    End Function

    Public Function ControlsImageIsError()
        Return mViewImageManager.gImageManager.getPaintImage.getControlForPaint.ImageIsError
    End Function

    Public Function PaintImageisDrawImageHasImageIsNothing() As Boolean
        Try
            If mViewImageManager.gImageManager.getPaintImage.isDrawImageHasImageIsNothing Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("PaintImageisDrawImageHasImageIsNothing")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function
    'PictureBox_SizeAndLocationKeep
    Sub paintImageRefreshSizeAndLocationKeepWithTransparncy()
        mViewImageManager.gFunction.gViewer.paintImageRefreshSizeAndLocationKeepWithTransparncy(
            mViewImageManager.gState.gFade.currentAlphaPercent)
    End Sub
    'NowSize
    Sub paintImageWithTransparncy()
        paintImageWithTransparncy(mViewImageManager.gState.gFade.currentAlphaPercent)
    End Sub

    '旧メソッド
    'Public Sub PlayMovieBegin()
    '    '個々の処理はfactoryでやる

    '    If mViewImageManager.gState.gFade.FadeInEnd Then
    '        mViewImageManager.gState.MoviePlayBegin = True
    '    End If

    '    Dim path As String =
    '        mViewImageManager.gImageFileList.getNowFilePath
    '    Dim disposeFlag As Boolean
    '    If mViewImageManager.gState.Gif.FlagIsGifMode Then
    '        If New PlayGif().isThisClassType(path) Then
    '            If New PlayGif().isMovie(path) Then
    '                mViewImageManager.gState.MoviePlayBegin = True
    '            Else
    '                disposeFlag = True
    '            End If
    '        Else
    '            disposeFlag = True
    '        End If
    '    End If
    '    If mViewImageManager.gState.gMovie.FlagIsMovieMode Then
    '        If New FileTypeMovie().isThisClassType(path) Then
    '            mViewImageManager.gState.MoviePlayBegin = True
    '        Else
    '            disposeFlag = True
    '        End If
    '    End If
    '    If disposeFlag Then
    '        'DisposeMovie()
    '    End If
    '    '-------------------------------
    '    If New PlayGif().isMovie(path) Then
    '        mViewImageManager.gState.GifAnimeBegin = True
    '    End If
    '    If New FileTypeMovie().isThisClassType(path) Then
    '        mViewImageManager.gState.MoviePlayBegin = True
    '    End If
    'End Sub
End Class
