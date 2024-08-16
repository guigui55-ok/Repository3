Public Class WordingManagerForImageViewer
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

        gSettings = New SettingsWording()
    End Sub

    Public gSettings As SettingsWording

End Class
