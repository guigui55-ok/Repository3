Public Class MainProcesser
    Implements IDisposable

    Public gLog As ILog

    Public gSettings As Settings
    Public gState As ImageViewerState
    Public gMainForm As MainFormManager
    Public gViewImageManager As ViewImageManager
    Public gNowViewImageManager As ViewImageManager
    Public gFunction As FunctionManager
    Public gEvents As EventManager
    Public gControls As ControlsManager


    Public Sub New(argform As MainForm)
        gSettings = New Settings() 'ImageManageObject_New時に？使う
        gState = New ImageViewerState() '旧PictureBoxでNew時に使う

        gMainForm = New MainFormManager(argform, Me)
        gMainForm.setControlEventsByMainForm()
        'gMainFormManager.setMainProcessor(Me)

        gViewImageManager = New ViewImageManager(Me, argform.Panel1, argform.PictureBox1)
        gViewImageManager.gImageManager = New ImageManager()
        gViewImageManager.gImageManager.setControls(argform.Panel1, argform.PictureBox1)
        gNowViewImageManager = gViewImageManager

        gFunction = New FunctionManager(Me)
        gEvents = New EventManager(Me)
        gControls = New ControlsManager(Me)

    End Sub

    Public Sub setLog(argLog As Log)
        gLog = argLog
    End Sub

    Public Function getLog() As ILog
        Return gLog
    End Function

    Public Sub disposeObjects()
        Try
            gSettings.disposeObjects()
            gSettings = Nothing
            gState.disposeObjects()
            gState = Nothing
            If Not gViewImageManager Is Nothing Then
                gViewImageManager.Dispose()
                gViewImageManager = Nothing
            End If
            If Not gNowViewImageManager Is Nothing Then
                gNowViewImageManager.Dispose()
                gNowViewImageManager = Nothing
            End If
            If Not gMainForm Is Nothing Then
                gMainForm.Dispose()
                gMainForm = Nothing
            End If
            gFunction.disposeObjects()
            gFunction = Nothing
            gEvents.disposeObjects()
            gEvents = Nothing
            gControls.disposeObjects()
            gControls = Nothing
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

    Public Sub test()
        Try
            addLog(3, "test Method")


            gNowViewImageManager.gFunction.gViewer.paintImageRoTation90degree()


            'Me.gViewImageManager.gEvents.gViewer.View()
        Catch ex As Exception
            Console.WriteLine("MainProcesser_test")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub outputActiveControl()
        Try
            '現在アクティブなコントロールを取得する
            Me.gMainForm.getForm.ActiveControl = Me.gMainForm.getForm.PictureBox1
            Dim c As Control = Me.gMainForm.getForm.ActiveControl

            If Not (c Is Nothing) Then
                addLog(3, "現在アクティブなコントロール : ", c.Name)
            Else
                addLog(3, "現在アクティブなコントロールはありません。")
            End If

            'Dim c2 As Control = Me.gMainForm.getForm.ToolStripContainer1

            'If Not (c2 Is Nothing) Then
            '    addLog(3, "現在アクティブなコントロール : ", c2.Name)
            'Else
            '    addLog(3, "現在アクティブなコントロールはありません。")
            'End If
        Catch ex As Exception
            Console.WriteLine(Me.ToString & ".outputActiveControl")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub PictureBoxKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        'outputActiveControl()
        addLogNotFocus(3, Me.ToString & ".PictureBoxKeyDown")
        Try
            Dim MainProcessorObject As MainProcesser = Me
            MainProcessorObject.gState.KeyState.setDown(e)

            If e.KeyData = Keys.Right Then
                MainProcessorObject.gNowViewImageManager.gEvents.gViewer.SetViewTriger()
                MainProcessorObject.gNowViewImageManager.gEvents.gViewer.NextPaintImage()
            ElseIf e.KeyData = Keys.Left Then
                MainProcessorObject.gNowViewImageManager.gEvents.gViewer.SetViewTriger()
                MainProcessorObject.gNowViewImageManager.gEvents.gViewer.PreViousPaintImage()
            ElseIf e.KeyData = Keys.Down Then
            ElseIf e.KeyData = Keys.Up Then
                'test
                'MainProcessorObject.gState.Fade.FadeInBegin = True
                'Me.test()
            ElseIf e.KeyData = Keys.A Then

                addLogNotFocus(3, "Keys.A")
                test()
            ElseIf e.KeyData = Keys.Z Then
                gNowViewImageManager.gFunction.gViewer.paintImageRoTation90degree()
            ElseIf e.KeyData = Keys.Menu Then
                addLog(3, "Keys.Menu")
            ElseIf e.KeyData = Keys.Alt Then
                addLog(3, "Keys.Alt")
            ElseIf e.KeyData = Keys.Space Then
                Cursor.Current = Cursors.Hand
                'タイマーをリセット
                MainProcessorObject.gNowViewImageManager.gFunction.gSlideShow.TimerReset()
            ElseIf (e.KeyData = (Keys.Menu + Keys.Alt)) Then
                addLog(3, "Keys.Menu + Keys.Alt")
                'メニューバーを表示_非表示
                Me.gMainForm.gControls.gMenuStripEvents.changeVisible()
            ElseIf (e.KeyData = (Keys.Control + Keys.Add)) Then
                MainProcessorObject.gNowViewImageManager.gFunction.gPictureBox.ChangeSizePictureBox(1.05)
            ElseIf (e.KeyData = (Keys.Control + Keys.Subtract)) Then
                MainProcessorObject.gNowViewImageManager.gFunction.gPictureBox.ChangeSizePictureBox(0.952)
            ElseIf (e.KeyData = Keys.F5) Then
                MainProcessorObject.gNowViewImageManager.gFunction.gPictureBox.refresh()
            End If

        Catch ex As Exception
            Console.WriteLine("PictureBoxKeyDown")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

End Class
