Public Class MainFormControls
    Inherits AbstractFunction
    Implements IDisposable

    'Friend MainProcessorObject As MainProcesser
    'Public Sub New(argMainProcessor As MainProcesser)
    '    MainProcessorObject = argMainProcessor
    'End Sub
    'Public Sub New()
    'End Sub
    'Private Sub setMainProcessor(argMainProcessor As MainProcesser)

    Public Sub New(argMainProcessorObject As MainProcesser)
        MainProcessorObject = argMainProcessorObject
    End Sub
    '=======================================================================
    Public Sub New(mainform As MainForm, argMainProcessor As MainProcesser)
        MainProcessorObject = argMainProcessor
        setObjects()
    End Sub

    Private Sub setObjects()
        'gMenuStripEvents = New MenuStripEvents(MainProcessorObject.gMainForm.getForm.MenuStrip1, MainProcessorObject)
        gMenuStripEvents = New MenuStripEvents(MainProcessorObject)
        gMenuStripEvents.initializeObjects(MainProcessorObject.gMainForm.getForm.MenuStrip1, MainProcessorObject)

        gMenuStripResister = New MenuStripEventsResister(MainProcessorObject)
        gMenuStripResister.initializeObjects(MainProcessorObject.gMainForm.getForm.MenuStrip1, MainProcessorObject)
        gStatusStripEvents = New StatusStripEvents(MainProcessorObject.gMainForm.getForm.StatusStrip1, MainProcessorObject)
        gToolStripEvents = New ToolStripEvents(MainProcessorObject.gMainForm.getForm.ToolStripContainer1,
                                               MainProcessorObject.gMainForm.getForm.ToolStrip1, MainProcessorObject)
    End Sub

    Public gMenuStripResister As MenuStripEventsResister
    Public gMenuStripEvents As MenuStripEvents
    Public WithEvents gStatusStripEvents As StatusStripEvents
    Public gToolStripEvents As ToolStripEvents

    Public Sub DisposeObjects()
        Try
            gMenuStripEvents.disposeObjects()
            gMenuStripEvents = Nothing
            gMenuStripResister.DisposeObjects()
            gMenuStripResister = Nothing
            gStatusStripEvents.DisposeObjects()
            gStatusStripEvents = Nothing
            gToolStripEvents.Dispose()
            gToolStripEvents = Nothing
        Catch ex As Exception
            addLogForSystemError(Me.ToString & "DisposeObjects")
            addLogForSystemError(ex.Message)
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
        DisposeObjects()
        ' リソースは破棄されている
        isDisposed = True
    End Sub
    'Finalize
    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
    '===============================================================================================

    '設定を読み込んでMenuStripの表示をする
    'Public Sub InitializeVisible()
    '    '常に表示する設定か
    '    If MainProcessorObject.gSettings.MenuStripAlwaysVisible Then
    '        'MenuStripを常に表示する
    '        MainProcessorObject.gMainForm.getForm.MenuStrip1.Visible = True
    '    Else
    '        MainProcessorObject.gMainForm.getForm.MenuStrip1.Visible = False
    '    End If
    'End Sub

    Public Sub initializeSizeAndLocation()
        Try
            addLog(4, "-------------------------------------------- initializeSizeAndLocation")
            Dim tMainForm As MainForm = MainProcessorObject.gMainForm.getForm
            addLog(3, "MainForm.Size = " & tMainForm.Size.Width & " , " & tMainForm.Size.Height)
            'Panel
            Dim tpanel As Panel = MainProcessorObject.gNowViewImageManager.gEvents.gPanel.getPanel
            tpanel.Size = tMainForm.Size
            addLog(3, "Panel.Size = " & tpanel.Size.Width & " , " & tpanel.Size.Height)
            'tpanel.BackColor = Color.Aqua
            'StatusBar
            Dim tStatusStrip As StatusStrip
            tStatusStrip = MainProcessorObject.gMainForm.gControls.gStatusStripEvents.getStatusStrip
            'tStatusStrip.Height
            'ToolStrip
            Dim tToolContainer As ToolStripContainer = tMainForm.ToolStripContainer1
            tToolContainer.Size = New Point(tMainForm.Size.Width, tMainForm.Size.Height - tStatusStrip.Height)
            addLog(3, "tToolContainer.Size = " & tToolContainer.Size.Width & " , " & tToolContainer.Size.Height)

        Catch ex As Exception
            addLogForSystemError(Me.ToString & "initializeSizeAndLocation")
            addLogForSystemError(ex.Message)
        End Try
    End Sub


End Class
