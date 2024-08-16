Public Class SlideShowFunction
    Inherits AbstractImageViewerChild

    'Friend mMainProcessorObject As MainProcesser
    'Friend mViewImageManager As ViewImageManager
    'Friend Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
    'Friend Sub New()
    'Friend Sub setMainProcessor(argMainProcessor As MainProcesser)
    'Friend Sub setViewImageManager(argViewImageManager As ViewImageManager)
    '=======================================================================
    Public Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
        'MyBase.New(argMainProcessor, argViewImageManager)
        'mMainProcessorObject = argMainProcessor
        'mViewImageManager = argViewImageManager

        'initializeObjects()
    End Sub

    Private mSettings As Settings
    Private mState As StateForViewImage
    Public WithEvents gTimerSlideShow As Timer

    Public Sub DisposeObjects()
        Try
            mSettings.disposeObjects()
            mSettings = Nothing
            mState.disposeObjects()
            mState = Nothing
            gTimerSlideShow.Dispose()
            gTimerSlideShow = Nothing
        Catch ex As Exception
            addLogForSystemError("DisposeObjects")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub initializeObjects(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)

        mMainProcessorObject = argMainProcessor
        mViewImageManager = argViewImageManager
        gTimerSlideShow = New Timer()
        AddHandler gTimerSlideShow.Tick, New EventHandler(AddressOf SlideShowMain)

        mState = mViewImageManager.gState
        mSettings = mMainProcessorObject.gSettings
        gTimerSlideShow.Interval = mMainProcessorObject.gSettings.SlideShowInterval
        addLog(3, "SlideShowFunction.initialize", "SlideShowInterval=" & gTimerSlideShow.Interval)
    End Sub

    Public Sub setObjects(argSettings As Settings, argState As StateForViewImage)
        mSettings = argSettings
        mState = argState
    End Sub

    Public Sub setAndApplyObjects(argSettings As Settings, argState As StateForViewImage)
        setObjects(argSettings, argState)
        applySettingOfObjects()
    End Sub

    Private Sub applySettingOfObjects()
        Try
            mSettings = mMainProcessorObject.gSettings
            gTimerSlideShow.Interval = mMainProcessorObject.gSettings.SlideShowInterval
            addLog(3, "SlideShowFunction.applySettingOfObjects", "SlideShowInterval=" & gTimerSlideShow.Interval)
            addLog(3, "SlideShowFunction.applySettingOfObjects", "mState.gSlideShowExecute=" & mState.gSlideShowExecute)


        Catch ex As Exception
            addLogForSystemError("applySettingOfObjects")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub setInterval(argInterval As Integer)
        gTimerSlideShow.Interval = argInterval
    End Sub

    Public Function getTimer() As Timer
        Return Me.gTimerSlideShow
    End Function

    Public Sub TimerReset()
        'スライドショー実行時ならタイマーをリセットする
        If gTimerSlideShow.Enabled = True Then
            gTimerSlideShow.Enabled = False
            gTimerSlideShow.Enabled = True
            gTimerSlideShow.Interval = mSettings.SlideShowInterval
            mState.gSlideShowExecute = True
        Else
        End If
    End Sub

    Public Sub TimerSwitch(ByRef ByRefFlag As Boolean)
        If ByRefFlag Then
            'true->false
            ByRefFlag = False
            Me.TimerStop()
        Else
            'false->true    
            ByRefFlag = True
            Me.TimerStart()
        End If
    End Sub

    Public Sub TimerSwitch()
        addLog(3, "SlideShowFunction.TimerStart", "gTimerSlideShow.TimerSwitch")

        If gTimerSlideShow.Enabled = False Then
            gTimerSlideShow.Start()
            gTimerSlideShow.Enabled = True ' timer.Start()と同じ
            mState.gSlideShowExecute = True
        Else
            gTimerSlideShow.Enabled = False
            mState.gSlideShowExecute = False
        End If
        addLog(3, "SlideShowFunction.TimerStart", "gTimerSlideShow.Enabled = " & gTimerSlideShow.Enabled)
    End Sub

    Public Sub TimerStart()
        If mState.gSlideShowExecute Then
            gTimerSlideShow.Start()
            gTimerSlideShow.Enabled = True ' timer.Start()と同じ
            addLog(3, "SlideShowFunction.TimerStart", "gTimerSlideShow.Enabled = " & gTimerSlideShow.Enabled)
        End If
    End Sub

    Public Sub TimerDispose()
        Try
            mState.gSlideShowExecute = False
            gTimerSlideShow.Enabled = False
            gTimerSlideShow.Dispose()
            addLog(3, "SlideShowFunction.TimerStart", "gTimerSlideShow.Enabled = " & gTimerSlideShow.Enabled)
        Catch ex As Exception
            addLogForSystemError("TimerDispose")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub TimerStop()
        TimerDispose()
    End Sub

    'スライドショーイベント
    Public Sub SlideShowMain(sender As Object, e As EventArgs)
        addLog(3, "SlideShowFunction.SlideShowMain", "Timer")
        mViewImageManager.gEvents.gViewer.NextPaintImage()
    End Sub

    'Protected Overrides Sub Finalize()
    '    MyBase.Finalize()
    'End Sub
End Class
