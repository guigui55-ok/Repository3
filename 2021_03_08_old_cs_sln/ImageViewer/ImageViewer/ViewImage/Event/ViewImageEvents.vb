Public Class ViewImageEvents
    Inherits AbstractImageViewerChild

    'Friend mMainProcessorObject As MainProcesser
    'Friend mViewImageManager As ViewImageManager
    'Friend Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
    'Friend Sub New()
    'Friend Sub setMainProcessor(argMainProcessor As MainProcesser)
    'Friend Sub setViewImageManager(argViewImageManager As ViewImageManager)
    '=======================================================================
    Public Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
        MyBase.New(argMainProcessor, argViewImageManager)
        mMainProcessorObject = argMainProcessor
        mViewImageManager = argViewImageManager
    End Sub

    Public Sub SetViewTriger()
        '表示
        mViewImageManager.gState.gChangeFileBegin = True
        mViewImageManager.gEvents.gViewer.DoFadeOutBeginFlagToBeTrueIfFadeSettingON()
        mViewImageManager.gFunction.gViewer.PauseFile()
        mViewImageManager.gEvents.gViewer.View()
    End Sub

    Public Sub excuteSlideShow()
        Try
            mViewImageManager.gFunction.gSlideShow.TimerStart()
            mViewImageManager.gState.gChangeFileBegin = True
            mViewImageManager.gEvents.gViewer.DoFadeOutBeginFlagToBeTrueIfFadeSettingON()
            Me.View()
        Catch ex As Exception
            Console.WriteLine("PaintForSlideShow")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'Public Sub DisposePlay()
    '    Try
    '        'If mv Then
    '    Catch ex As Exception
    '        addLogForSystemError("DisposePlay")
    '        addLogForSystemError(ex.Message)
    '    End Try
    'End Sub

    'FadeInOut機能がOnならFadeOutBeginをTrueにする→画像切り替えを行う
    Public Sub DoFadeOutBeginFlagToBeTrueIfFadeSettingON()
        'If mViewImageManager.gState.gMovie.FlagIsMovieMode Then
        '    'Movie操作中なら抜ける
        '    Exit Sub
        'End If

        If mViewImageManager.gState.gFade.isNowFade Then
            'すでにFadeフラグがある
            Exit Sub
        Else
            'Exit Sub
        End If


        If mViewImageManager.gState.gChangeFileBegin Then
            If mViewImageManager.gImageManager.getPaintImage.getControlForPaint.hasImageIsNothing Then
                mViewImageManager.gState.gFade.FadeInBegin = True
                Exit Sub
            Else
                mViewImageManager.gState.gFade.FadeOutBegin = True
                Exit Sub
            End If
            If mViewImageManager.gState.gFade.currentAlphaPercent <= 0 Then
                mViewImageManager.gState.gFade.FadeInBegin = True
                Exit Sub
            Else
                mViewImageManager.gState.gFade.FadeOutBegin = True
                Exit Sub
            End If
        End If

        'If mViewImageManager.gState.gFade.AllFadeFlagIsFalse Then
        '    addLog(10, "AllFadeFlagIsFalse false")
        '    Exit Sub
        'End If

        If mViewImageManager.gState.gFade.currentAlphaPercent <= 0 Then
            mViewImageManager.gState.gFade.FadeInBegin = True
            mViewImageManager.gState.gChangeFileBegin = True
            Exit Sub
        Else
            If mViewImageManager.gState.gSlideShowExecute Then
                mViewImageManager.gState.gFade.FadeOutBegin = True
                mViewImageManager.gState.gChangeFileBegin = True
            End If
        End If
        If mViewImageManager.gState.gFade.FadeInOutNow Then
            mViewImageManager.gState.gFade.FadeOutBegin = True
            mViewImageManager.gState.gChangeFileBegin = True
        End If

        If mViewImageManager.gImageManager.getPaintImage.isDrawImageHasImageIsNothing Then
            mViewImageManager.gState.gFade.FadeInBegin = True
            mViewImageManager.gState.gChangeFileBegin = True
        End If
    End Sub

    'MainMethod
    Public Sub View()
        addLog(10, Me.ToString & ":View")
        If mViewImageManager.gState.gFade.isNowFade Then
            mMainProcessorObject.gState.gDebugFlag = 1
        Else
            mMainProcessorObject.gState.gDebugFlag = 0
        End If
        Try
            'addLog(3, "currentAlphaPercent=", mViewImageManager.gState.gFade.currentAlphaPercent)
            'DoFadeOutBeginFlagToBeTrueIfFadeSettingON()
            ViewMainMethod()
            mMainProcessorObject.gMainForm.gControls.gStatusStripEvents.updateText(
                mViewImageManager.gImageFileList.getNowFilePath)

            viewEndMethod()
        Catch ex As Exception
            addLogForSystemError("ImageViewerPaintFunction.View")
            addLogForSystemError(ex.Message)
        End Try

    End Sub

    Private Sub viewEndMethod()
        Try
            If Not mViewImageManager.gState.gFade.AllFadeFlagIsFalse Then
                'Me.View()
            End If
        Catch ex As Exception
            addLogForSystemError("ImageViewerPaintFunction.viewEndMethod")
            addLogForSystemError(ex.Message)
        End Try
    End Sub


    Private Sub ViewMainMethod()
        Try
            With mViewImageManager.gImageManager
                If .getPaintImage.getControlForPaint.SizeUpdateState Then
                    addLog(0, "getControlForPaint.SizeUpdateState is true")
                    Exit Sub
                End If

                'FormInitialize中は×
                If mViewImageManager.gState.gFormInitialize Then
                    addLog(0, "gState.gFormInitialize is true")
                    Exit Sub
                End If
                If mMainProcessorObject.gState.PaintFormInitialize Then
                    addLog(0, "mMainProcessorObject.gState.gFormInitialize is true")
                    Exit Sub
                End If
                '最小化中×
                If mMainProcessorObject.gMainForm.WindowState = FormWindowState.Minimized Then
                    addLog(0, "gFormWindowState.Minimized is true")
                    Exit Sub
                End If
                'PictureBoxがVisibleでないとき×
                If mViewImageManager.gFunction.gPictureBox.getVisible = False Then
                    addLog(0, "gPictureBoxFunction.getVisible is false")
                    Exit Sub
                End If

                'isErrorImage
                If mViewImageManager.gFunction.gPictureBox.isErrorImage Then
                    addLog(0, "gPictureBoxFunction.isErrorImage is true")
                    Exit Sub
                End If

                'ファイルカウントゼロ×
                If mViewImageManager.gImageFileList.getNowFilePath.Count <= 0 Then
                    addLog(0, "gImageFileList.getNowFilePath is Count Under Zero")
                    Exit Sub
                End If
                '表示可能ファイルNothing
                If mViewImageManager.gImageFileList.FlagAsViewAbleFileListIsNothing Then
                    addLog(0, "gImageFileList.FlagAsViewAbleFileListIsNothing = True")
                    Exit Sub
                End If
                'ファイル拡張子にタイプ非対応×

                If mViewImageManager.gState.gChangeFileBegin Then

                End If
                '----------------------
                'ControlChange
                'Effect-fade
                'FileType
                mViewImageManager.gFunction.gViewer.PaintMain()
                'mViewImageMainMethodFactory.excuteViewImage()
                '----------------------
                If mViewImageManager.gFunction.gPictureBox.getVisible = False Then
                    addLogForSystemError("gPictureBoxFunction.getVisible is false ")
                    Exit Sub
                End If
                If mViewImageManager.gImageManager.getPaintImage.getControlFrame.Visible = False Then
                    mViewImageManager.gImageManager.getPaintImage.getControlFrame.Visible = True
                End If
                If mViewImageManager.gState.gChangeFileBegin Then mViewImageManager.gState.gChangeFileBegin = False
            End With

        Catch ex As Exception
            addLogForSystemError("ImageViewerPaintFunction.ViewMainMethod")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    '次へ
    Public Sub NextPaintImage()
        mViewImageManager.gImageFileList.moveNext()
        mViewImageManager.gState.gChangeFileBegin = True
        mViewImageManager.gEvents.gViewer.DoFadeOutBeginFlagToBeTrueIfFadeSettingON()
        mViewImageManager.gFunction.gViewer.PauseFile()
        Me.View()
        addLog(3, "NextPaintImage", mViewImageManager.gImageFileList.getNowFilePath)
    End Sub

    Public Sub PreViousPaintImage()
        mViewImageManager.gImageFileList.movePreviousIndex()
        mViewImageManager.gState.gChangeFileBegin = True
        mViewImageManager.gEvents.gViewer.DoFadeOutBeginFlagToBeTrueIfFadeSettingON()
        mViewImageManager.gFunction.gViewer.PauseFile()
        Me.View()
        addLog(3, "NextPaintImage", mViewImageManager.gImageFileList.getNowFilePath)
    End Sub

End Class
