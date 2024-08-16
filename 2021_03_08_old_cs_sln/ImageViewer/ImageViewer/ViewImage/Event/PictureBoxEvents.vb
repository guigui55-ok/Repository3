Imports ImageViewer

Public Class PictureBoxEvents
    Inherits AbstractImageViewerChild

    WithEvents mPictureBox As PictureBox
    Public State As StateForViewImage
    Private mMouseState As MouseState

    'Friend mMainProcessorObject As MainProcesser
    'Friend mViewImageManager As ViewImageManager
    'Friend Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
    'Friend Sub New()
    'Friend Sub setMainProcessor(argMainProcessor As MainProcesser)
    'Friend Sub setViewImageManager(argViewImageManager As ViewImageManager)
    '=======================================================================
    Public Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager, argPictureBox As PictureBox)
        MyBase.New(argMainProcessor, argViewImageManager)
        mMainProcessorObject = argMainProcessor
        mViewImageManager = argViewImageManager
        mPictureBox = argPictureBox
        mMouseState = New MouseState()
        State = mViewImageManager.gState
        mPictureBox = mPictureBox
    End Sub
    '=======================================================================
    Public Sub setPictureBox(argPictureBox As PictureBox)
        mPictureBox = argPictureBox
    End Sub

    '=======================================================================

    Public Sub test()
        Try
            'MainProcessorObject.test()
        Catch ex As Exception
            Console.WriteLine("test")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    Public Function ClickPointIsRightSideInPictureBox(e As MouseEventArgs, ControlSize As Size) As Boolean
        If (e.X > (ControlSize.Width / 2)) Then
            ClickPointIsRightSideInPictureBox = True
        Else
            ClickPointIsRightSideInPictureBox = False
        End If
    End Function
    '=======================================================================
    'Private Sub PaintByStateOfPictureBoxGifMode()
    '    Try
    '        'PaintImageToPictureBox()
    '    Catch ex As Exception
    '        Console.WriteLine("PaintByStateOfPictureBoxGifMode")
    '    End Try
    'End Sub
    '=======================================================================
    'Public Sub PaintByStateOfPictureBox()
    '    mViewImageManager.gEvents.gViewer.View()
    'End Sub
    '***********************************************************************
    'イベント　PicutreBox
    '***********************************************************************

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles mPictureBox.Click
        addLog(10, "PictureBox1_Click")
        Try
            If mMainProcessorObject.gState.gMouse.isDrag() Then
                Exit Sub
            End If
            mMainProcessorObject.gState.gMouse.setClick(e)

            '右側か左側か
            If mMainProcessorObject.gNowViewImageManager.gEvents.gPictureBox.ClickPointIsRightSideInPictureBox(e, mPictureBox.Size) Then
                '右なら次へ
                'mMainProcessorObject.gEvents.gPaintEvents.NextPaintImage(mViewImageManager)
                mMainProcessorObject.gNowViewImageManager.gEvents.gViewer.NextPaintImage()
            Else
                '左なら前へ
                'mMainProcessorObject.gEvents.gPaintEvents.PreviousPaintImage(mViewImageManager)
                mMainProcessorObject.gNowViewImageManager.gEvents.gViewer.PreViousPaintImage()
            End If
        Catch ex As Exception

        End Try
    End Sub


    'KeyDownイベントハンドラ
    Private Sub PictureBox1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles mPictureBox.KeyDown
        Try
            'addLog(3, "PictureBox1_KeyDown")
            Console.WriteLine("PictureBox1_KeyDown")
            'mMainProcessorObject.PictureBoxKeyDown(sender, e)

        Catch ex As Exception
            Console.WriteLine("PictureBox1_KeyDown")
            Console.WriteLine(ex.Message)
        End Try

    End Sub
    'KeyDownイベントハンドラ
    'Public Sub PictureBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
    '    mMainProcessorObject.PictureBoxKeyDown(sender, e)
    'End Sub


    'Public Sub PictureBox_PreviewKey(ByVal sender As Object, ByVal e As KeyEventArgs) Handles PictureBox.PreviewKeyDown
    '    Try
    '        'KeyDown true KeyUp false = LongKeyDown
    '    Catch ex As Exception
    '        Console.WriteLine("PictureBox_PreviewKey")
    '        Console.WriteLine(ex.Message)
    '    End Try
    'End Sub

    Public Sub PictureBox_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles mPictureBox.KeyUp
        Try
            mMainProcessorObject.gState.KeyState.setUp(e)

        Catch ex As Exception
            Console.WriteLine("PictureBox_KeyUp")
            Console.WriteLine(ex.Message)
        Finally
            mMainProcessorObject.gState.KeyState.clearUp()
        End Try
    End Sub


    Private Sub PictureBox_MouseWheel(sender As Object, e As MouseEventArgs) Handles mPictureBox.MouseWheel
        addLog(3, "PictureBox_MouseWheel")
        Try
            'mViewImageManager.gFunction.gPictureBox.SuspendLayout(True)
            'console.Writeline(e.Clicks)
        Catch ex As Exception
            Console.WriteLine("PictureBox_MouseWheel")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    '=======================================================================
    'PictureBox Mouse
    '=======================================================================
    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles mPictureBox.MouseDown
        addLog(3, "PictureBox1_MouseDown")
        Try
            mMainProcessorObject.gState.gMouse.setDown(e)
            'MainForm移動用
            mMainProcessorObject.gState.WindowPosition = New Rectangle(MainForm.Location, MainForm.Size)
            'PictureBox移動用
            If mMainProcessorObject.gState.KeyState.isKeyDown(Keys.Space) Then
                mMainProcessorObject.gState.WindowPosition = New Rectangle(MainForm.Location, MainForm.Size)
            End If
        Catch ex As Exception
            Console.WriteLine("PictureBox1_MouseDown")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    '=======================================================================
    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles mPictureBox.MouseUp
        addLog(3, "PictureBox1_MouseUp")
        mMainProcessorObject.gState.gMouse.setUp(e)
        Try

        Catch ex As Exception
            Console.WriteLine("PictureBox1_MouseUp")
            Console.WriteLine(ex.Message)
        Finally
            mViewImageManager.gState.gMouse.clearButton()
        End Try
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles mPictureBox.MouseMove
        'addLog(3, "PictureBox1_MouseMove")
        Try
            mMainProcessorObject.gState.gMouse.setMove(e)
            mViewImageManager.gFunction.gSlideShow.TimerReset() '何かの操作中はSlideShowをリセット

            If mMainProcessorObject.gState.KeyState.isKeyDown(Keys.Space) Then
                'PictureBoxドラッグ移動
                mViewImageManager.gFunction.gPictureBox.PictureBoxChangeLocation(e)
            Else
                '画面ドラッグ移動
                mMainProcessorObject.gFunction.gPaint.MainFormMoveLocation(e)
            End If
        Catch ex As Exception
            Console.WriteLine("Form1_MouseMove")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '=======================================================================
    '=======================================================================
    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles mPictureBox.Paint
        addLogNotFocus(3, "PictureBox1_Paint Location: " & mPictureBox.Location.X & " , " & mPictureBox.Location.Y)
        'mMainProcessorObject.outputActiveControl()

        Try
            If mViewImageManager.gState.gMovie.MoviePlayBegin Then
                addLog(3, "PictureBox1_Paint : MoviePlayBegin = true -> View")
                mViewImageManager.gEvents.gViewer.View()
            Else
                If mViewImageManager.gState.gFade.isNowFade Then
                    mViewImageManager.gFunction.gViewer.DisposeNowPlay()
                    mViewImageManager.gEvents.gViewer.View()

                End If
            End If

            If mViewImageManager.gState.gMovie.MoviePlay Then
                addLog(4, "PictureBox1_Paint : MoviePlay = true : Now Play")
                'mViewImageManager.gEvents.gViewer.View()
            End If

        Catch ex As Exception
            Console.WriteLine("PictureBox1_Paint")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    '=======================================================================
    '=======================================================================

    Private Sub PictureBoxDragDrop(sender As Object, e As DragEventArgs) Handles mPictureBox.DragDrop
        addLog(3, "PictureBoxEvents : PictureBoxDragDrop")
        mMainProcessorObject.gEvents.gMouse.MakeFileListByDragAndDrop(sender, e)
    End Sub

    Private Sub PictureBoxDragEnter(sender As Object, e As DragEventArgs) Handles mPictureBox.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    '=======================================================================

    Private Sub PictureBox1_ClientSizeChanged(sender As Object, e As EventArgs) Handles mPictureBox.ClientSizeChanged
        'プロパティの値が変更された場合
        addLog(3, "PictureBox1_ClientSizeChanged")
        Try
            mViewImageManager.gFunction.gSlideShow.TimerReset() '何かの操作中はSlideShowをリセット

            If mViewImageManager.gState.gMovie.MoviePlayBegin Then
                mPictureBox.Invalidate()
                mPictureBox.Image = mPictureBox.Image
            End If
            If mViewImageManager.gState.gFormInitialize Then Exit Sub
            'mMainProcessorObject.gNowViewImageManager.gFunction.gPictureBox.changeLocation()
        Catch ex As Exception
            Console.WriteLine("PictureBox1_ClientSizeChanged")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub PictureBox1_SizeChanged(sender As Object, e As EventArgs) Handles mPictureBox.SizeChanged
        Try
            addLog(3, "PictureBox1_SizeChanged")
            mViewImageManager.gFunction.gSlideShow.TimerReset() '何かの操作中はSlideShowをリセット
        Catch ex As Exception
            Console.WriteLine("PictureBox1_ClientSizeChanged")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub PictureBox_LocationChanged(sender As Object, e As EventArgs) Handles mPictureBox.LocationChanged
        addLogNotFocus(3, "PictureBox_LocationChanged: " & mPictureBox.Location.X & " , " & mPictureBox.Location.Y)
        'Console.WriteLine("PictureBox_LocationChanged: " & mPictureBox.Location.X & " , " & mPictureBox.Location.Y)
        'MainFormManagerObject.rockMove(MainFormManagerObject.getNowRectAngle)
        Try
            'mViewImageManager.gFunction.gSlideShow.TimerReset() '何かの操作中はSlideShowをリセット

            If mMainProcessorObject.gState.gDebugFlag = 1 Then
                'Stop
                mMainProcessorObject.gState.gDebugFlag = 0
            End If
        Catch ex As Exception
            Console.WriteLine("PictureBox_LocationChanged")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    '画像比率

    '画質調整
    '画像の色あい
    '画像コントラスト
    '画像明るさ

    '画像形
    '　四角、丸

    '画像位置

    '画像サイズ

    '表示するときに　今の倍率を必ず保持
    '=======================================================================
    '=======================================================================
    '=======================================================================

End Class
