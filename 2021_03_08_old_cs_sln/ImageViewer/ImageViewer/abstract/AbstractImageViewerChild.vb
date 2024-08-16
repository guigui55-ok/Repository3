Public Class AbstractImageViewerChild
    'MustOverride Sub New() 'MustOverride + New はNG
    Friend mMainProcessorObject As MainProcesser
    Friend mViewImageManager As ViewImageManager

    Friend Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
        MyBase.New()
        mMainProcessorObject = argMainProcessor
        mViewImageManager = argViewImageManager
    End Sub

    Friend Sub New()

    End Sub

    Friend Sub setMainProcessor(argMainProcessor As MainProcesser)
        mMainProcessorObject = argMainProcessor
    End Sub

    Friend Sub setViewImageManager(argViewImageManager As ViewImageManager)
        mViewImageManager = argViewImageManager
    End Sub

    'Friend mMainProcessorObject As MainProcesser
    'Friend mViewImageManager As ViewImageManager
    'Friend Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
    'Friend Sub New()
    'Friend Sub setMainProcessor(argMainProcessor As MainProcesser)
    'Friend Sub setViewImageManager(argViewImageManager As ViewImageManager)


End Class
