Public Class EventManagerForVewImage
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

        gPictureBox = New PictureBoxEvents(
            mMainProcessorObject,
            mViewImageManager,
            mViewImageManager.gImageManager.getPaintImage.getControlForPaint.getControlBridge.getControl
       )

        gPanel = New PanelEvents(
            mMainProcessorObject,
            mViewImageManager,
            mViewImageManager.gImageManager.getPaintImage.getControlFrame.getControl.getControl
        )

        gImageFileList = New ImageFileListEvents(mMainProcessorObject, mViewImageManager)
        gViewer = New ViewImageEvents(mMainProcessorObject, mViewImageManager)
        gInitialize = New InitializeEventsForViewImage(mMainProcessorObject, mViewImageManager)
        gSettings = New SettingsEventsForContents(mMainProcessorObject, mViewImageManager)
    End Sub

    Public gPictureBox As PictureBoxEvents
    Public gPanel As PanelEvents
    Public gImageFileList As ImageFileListEvents
    Public gViewer As ViewImageEvents
    Public gInitialize As InitializeEventsForViewImage
    Public gSettings As SettingsEventsForContents

    Public Sub disposeObjects()
        Try
            gPictureBox = Nothing
            gPanel = Nothing
            gImageFileList = Nothing
            gViewer = Nothing
            gInitialize = Nothing
            gSettings = Nothing
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
End Class
