Public Class MainFormManager
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

    Private mMainForm As MainForm
    Public gControls As MainFormControls
    Public gInitializeEvents As MainFormInitializeEvents
    Public gMainFormEvents As MainFormEvents

    'Public gControlEvents As ControlEventsForImageViewer '後で消す
    'Sub New()
    'End Sub
    Sub New(argForm1 As MainForm, argMainProcessor As MainProcesser)
        mMainForm = argForm1
        setMainProcessor(argMainProcessor)
    End Sub

    Public Sub setControlEventsByMainForm()
        gControls = New MainFormControls(mMainForm, MainProcessorObject)
        gControls.gMenuStripEvents = New MenuStripEvents(MainProcessorObject)
        gControls.gMenuStripEvents.initializeObjects(mMainForm.MenuStrip1, MainProcessorObject)
        gControls.gStatusStripEvents = New StatusStripEvents(mMainForm.StatusStrip1, MainProcessorObject)
        gControls.gToolStripEvents = New ToolStripEvents(mMainForm.ToolStripContainer1, mMainForm.ToolStrip1, MainProcessorObject)
        gInitializeEvents = New MainFormInitializeEvents(MainProcessorObject)
        gMainFormEvents = New MainFormEvents(mMainForm, MainProcessorObject)
    End Sub

    Public Sub DisposeObjects()
        Try
            If Not gControls Is Nothing Then
                gControls.Dispose()
                gControls = Nothing
            End If
            gInitializeEvents = Nothing
            gMainFormEvents = Nothing
        Catch ex As Exception
            addLogForSystemError("DisposeObjects")
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

    Public Function getForm() As MainForm
        Return mMainForm
    End Function

    Function getNowRectAngle() As Rectangle
        Try
            Return New Rectangle(mMainForm.Location, mMainForm.Size)
        Catch ex As Exception
            Console.WriteLine("getNowRectAngle")
            Console.WriteLine(ex.Message)
        End Try
    End Function

    'MainFormのLocationを変更、移動
    Sub moveLocation(e As MouseEventArgs, MousePoint As Point)
        Try
            If (e.Button And MouseButtons.Left) = MouseButtons.Left Then
                mMainForm.Left += e.X - MousePoint.X
                mMainForm.Top += e.Y - MousePoint.Y
            End If
        Catch ex As Exception
            Console.WriteLine("moveLocation")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Sub moveLocation(e As MouseEventArgs, MousePoint As MouseEventArgs)
        Try
            If MousePoint Is Nothing Then
                'Console.WriteLine("moveLocation MousePoint Is Nothing")
                Exit Sub
            End If
            moveLocation(e, New Point(MousePoint.X, MousePoint.Y))
        Catch ex As Exception
            Console.WriteLine("moveLocation")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Property WindowState As FormWindowState
        Get
            Return mMainForm.WindowState
        End Get
        Set(value As FormWindowState)
            mMainForm.WindowState = value
        End Set
    End Property

    Sub rockForm(argArea As Rectangle)
        Try
            'フォームが最小化、最大化状態の時は無視
            If mMainForm.WindowState <> FormWindowState.Normal Then
                Return
            End If

            'フォームが移動できる範囲
            'Dim area As Rectangle = Screen.PrimaryScreen.WorkingArea
            Dim area As Rectangle = argArea

            'フォームの大きさが移動できる範囲より大きくならないようにする
            If area.Width < mMainForm.Width Then
                mMainForm.Width = area.Width
            End If
            If area.Height < mMainForm.Height Then
                mMainForm.Height = area.Height
            End If

            'フォームの左の位置を修正
            If mMainForm.Left < area.Left Then
                mMainForm.Left = area.Left
            ElseIf area.Left + area.Width < mMainForm.Left + mMainForm.Width Then
                mMainForm.Left = area.Left + area.Width - mMainForm.Width
            End If

            'フォームの上の位置を修正
            If mMainForm.Top < area.Top Then
                mMainForm.Top = area.Top
            ElseIf area.Top + area.Height < mMainForm.Top + mMainForm.Height Then
                mMainForm.Top = area.Top + area.Height - mMainForm.Height
            End If

        Catch ex As Exception
            Console.WriteLine("rockMove")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

End Class
