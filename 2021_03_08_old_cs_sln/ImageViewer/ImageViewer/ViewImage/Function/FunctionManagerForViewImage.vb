Public Class FunctionManagerForViewImage
    Inherits AbstractImageViewerChild
    Implements IDisposable

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


        gPictureBox = New PictureBoxForViewImageFunction(
            mMainProcessorObject,
            mViewImageManager,
            mViewImageManager.gImageManager.getPaintImage.getControlForPaint.getControlBridge.getControl
       )

        gPanel = New PanelForViewImageFunction(
            mMainProcessorObject,
            mViewImageManager,
            mViewImageManager.gImageManager.getPaintImage.getControlFrame.getControl.getControl
        )
        gImageFileLIst = New ImageFileListFunction(mMainProcessorObject, mViewImageManager)
        gViewer = New ImageViewerPaintFunction(mMainProcessorObject, mViewImageManager)
        gSlideShow = New SlideShowFunction(mMainProcessorObject, mViewImageManager)
        gSlideShow.initializeObjects(mMainProcessorObject, mViewImageManager)

    End Sub
    '=======================================================================
    Public gPictureBox As PictureBoxForViewImageFunction
    Public gPanel As PanelForViewImageFunction
    Public gImageFileLIst As ImageFileListFunction
    Public gViewer As ImageViewerPaintFunction
    Public gSlideShow As SlideShowFunction

    Public Sub disposeObjects()
        Try
            'If Not gPictureBox Is Nothing Then
            '    'gPictureBox.dispose
            'End If
            If Not gPanel Is Nothing Then
                'gPanel.dispose
                gPanel = Nothing
            End If

            gImageFileLIst = Nothing
            'FunctionManagerForViewImage.gViwer-> Dispose()
            If Not gViewer Is Nothing Then
                gViewer.Dispose()
                gViewer = Nothing
            End If
            If Not gSlideShow Is Nothing Then
                gSlideShow.DisposeObjects()
                gSlideShow = Nothing
            End If
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
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
        disposeObjects()
        ' リソースは破棄されている
        isDisposed = True
    End Sub
    'Finalize
    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
    '===============================================================================================
End Class
