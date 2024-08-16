Public Class InitializeEventsForViewImage
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

    Public Sub excute()
        addLog(3, Me.ToString & ".excute")
        Try
            mViewImageManager.gState.gFormInitialize = False
            mViewImageManager.gFunction.gSlideShow.setAndApplyObjects(
                mMainProcessorObject.gSettings, mViewImageManager.gState)

            'mViewImageManager.gEvents.gPanel.getPanel.ContextMenuStrip = mViewImageManager.gControls.gContextMenuStrip.getObject
            mViewImageManager.gEvents.gPanel.getPanel.ContextMenuStrip = New ContextMenuStrip()
            mViewImageManager.gControls.gContextMenuStrip.setObject(mViewImageManager.gEvents.gPanel.getPanel.ContextMenuStrip)
            mViewImageManager.gControls.gContextMenuStrip.Initialize()
            mViewImageManager.gControls.gContextMenuStrip.resistEvents()
        Catch ex As Exception
            addLogForSystemError("InitializeEventsForViewImage.excute")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub applySettings()
        mViewImageManager.gEvents.gSettings.setListByLastFolder()
    End Sub
End Class
