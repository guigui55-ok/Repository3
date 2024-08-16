Public Class ViewImageFunction
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
    End Sub

    Private mViewImageMainMethodFactory As ViewImageMainMethodFactory

    'とりあえず機能が少ないものはここで
    Public Sub SlideShowTimerReset()
        mViewImageManager.gFunction.gSlideShow.TimerReset()
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
        If Not mViewImageMainMethodFactory Is Nothing Then
            mViewImageMainMethodFactory.Dispose()
            mViewImageMainMethodFactory = Nothing
        End If
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
