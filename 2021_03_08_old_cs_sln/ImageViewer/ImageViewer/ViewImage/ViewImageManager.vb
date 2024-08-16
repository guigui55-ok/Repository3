Public Class ViewImageManager
    Inherits AbstractImageViewerChild
    Implements IDisposable

    'Friend mMainProcessorObject As MainProcesser
    'Friend mViewImageManager As ViewImageManager
    'Friend Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
    'Friend Sub New()
    'Friend Sub setMainProcessor(argMainProcessor As MainProcesser)
    'Friend Sub setViewImageManager(argViewImageManager As ViewImageManager)
    '=======================================================================
    Public Sub New(argMainProcessor As MainProcesser, argPanel As Panel, argPictureBox As PictureBox)
        MyBase.New(argMainProcessor, argMainProcessor.gNowViewImageManager)
        mMainProcessorObject = argMainProcessor
        mViewImageManager = Me

        '旧クラス
        gImageManager = New ImageManager()
        Me.gImageManager.setControls(
            mMainProcessorObject.gMainForm.getForm.Panel1 _
            , mMainProcessorObject.gMainForm.getForm.PictureBox1)
        '新クラス
        gState = New StateForViewImage()
        gImageFileList = New ViewFileList()
        gEvents = New EventManagerForVewImage(mMainProcessorObject, Me)
        gFunction = New FunctionManagerForViewImage(mMainProcessorObject, Me)
        gControls = New ControlEventsForImageViewer(mMainProcessorObject, Me)
        gSettings = New SettingsForImageViewer()
        gWording = New WordingManagerForImageViewer(mMainProcessorObject, Me)
        'gEvents.gInitialize.excute()
    End Sub
    '備考
    'MainProcessorObject.gViewImageManager.gImageManager.getPaintImage() as IPaintImage
    '=======================================================================
    '複数のPanel_PictureBoxを表示できるように
    Public gImageManager As IImageManager
    Public gImageFileList As ViewFileList

    Public gEvents As EventManagerForVewImage
    Public gFunction As FunctionManagerForViewImage
    Public gState As StateForViewImage
    Public gControls As ControlEventsForImageViewer
    Public gSettings As SettingsForImageViewer
    Public gWording As WordingManagerForImageViewer

    Public Sub disposeObjects()
        Try
            If Not gImageManager Is Nothing Then
                gImageManager.disposeObjects()
                gImageManager = Nothing
            End If
            If Not gState Is Nothing Then
                gState.disposeObjects()
                gState = Nothing
            End If
            If Not gEvents Is Nothing Then
                gEvents.disposeObjects()
                gEvents = Nothing
            End If
            If Not gFunction Is Nothing Then
                gFunction.Dispose()
                gFunction = Nothing
            End If
            If Not gControls Is Nothing Then
                gControls.disposeObjects()
                gControls = Nothing
            End If
            'gSettings.dispose
            gSettings = Nothing
            'gWording.dispose
            gWording = Nothing
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    'FunctionManagerForViewImage' であるフィールド 'ViewImageManager.gFunction' を含んでいます。
    'gFunction.disposeObjects()
    'gFunction = Nothing
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

    Public Sub InitializeToViewImageControls()
        addLog(3, "ViewImageManager.InitializeToViewImageControls")
        Try
            Dim MainForm As MainForm = mMainProcessorObject.gMainForm.getForm

            'PictureBox1.Parent = Panel1 
            MainForm.PictureBox1.Parent = MainForm.Panel1
            'MainForm.Panel1.Parent = MainForm.ToolStripContainer1

        Catch ex As Exception
            addLogForSystemError("ViewImageManager.InitializeToViewImageControls")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

End Class
