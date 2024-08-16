Public Class PanelForViewImageFunction
    Inherits AbstractImageViewerChild

    'Friend mMainProcessorObject As MainProcesser
    'Friend mViewImageManager As ViewImageManager
    'Friend Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
    'Friend Sub New()
    'Friend Sub setMainProcessor(argMainProcessor As MainProcesser)
    'Friend Sub setViewImageManager(argViewImageManager As ViewImageManager)
    '=======================================================================
    Public Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager, argPanel As Panel)
        MyBase.New(argMainProcessor, argViewImageManager)
        mMainProcessorObject = argMainProcessor
        mViewImageManager = argViewImageManager
        mPanel = argPanel
    End Sub

    Private WithEvents mPanel As Panel
    '=======================================================================
    Public Sub setPanel(argPanel As Panel)
        mPanel = argPanel
    End Sub
    Public Function getPanel() As Panel
        Return mPanel
    End Function

    Public Sub SuspendLayout(flag As Boolean)
        If True Then
            mPanel.SuspendLayout()
        Else
            mPanel.ResumeLayout()
        End If
    End Sub
End Class
