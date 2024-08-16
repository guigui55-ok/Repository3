Public Class PanelEvents
    Inherits AbstractImageViewerChild
    WithEvents mPanel As Panel

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
    '=======================================================================
    Public Sub setPanel(argPanel As Panel)
        mPanel = argPanel
    End Sub
    Public Function getPanel() As Panel
        Return mPanel
    End Function


    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles mPanel.MouseMove
        mViewImageManager.gFunction.gSlideShow.TimerReset() '何かの操作中はSlideShowをリセット
        mMainProcessorObject.gState.gMouse.setMove(e)
        '画面ドラッグ移動
        mMainProcessorObject.gEvents.gPaint.MainFormMoveLocation(e)
    End Sub

    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles mPanel.MouseUp
        mMainProcessorObject.gState.gMouse.setUp(e)
    End Sub

    Private Sub Panel1_MouseWheel(sender As Object, e As MouseEventArgs) Handles mPanel.MouseWheel
        addLogNotFocus(3, "Panel1_MouseWheel : e.Delta.ToString() = " & e.Delta.ToString())
        Dim nowMP As Point = mViewImageManager.gFunction.gPictureBox.getMousePointInPictureBox(sender, e)
        'If True Then Exit Sub
        mPanel.AutoScroll = False
        'mViewImageManager.gFunction.gPictureBox.getPictureBox()
        Try
            'mPanel.AutoScrollPosition = New Point(mPanel.AutoScrollPosition.X, mPanel.AutoScrollPosition.Y)
            If e.Delta > 0 Then
                'mViewImageManager.gFunction.gPictureBox.ChangeSizePictureBox(1.05)
                'mViewImageManager.gFunction.gPictureBox.KeepSizeChangeLocation(nowMP, 1.05)
                '拡大
                mViewImageManager.gFunction.gPictureBox.ChangeSizeAndLocationForPictureBox(
                    mMainProcessorObject.gSettings.ExpansionByMouseWheel, nowMP)
            Else
                'mViewImageManager.gFunction.gPictureBox.ChangeSizePictureBox(0.952)
                'mViewImageManager.gFunction.gPictureBox.KeepSizeChangeLocation(nowMP, 0.952)
                '縮小
                mViewImageManager.gFunction.gPictureBox.ChangeSizeAndLocationForPictureBox(
                    mMainProcessorObject.gSettings.ReductionImageByMouseWheel, nowMP)
            End If
            mViewImageManager.gFunction.gPictureBox.SuspendLayout(False)
        Catch ex As Exception
            Console.WriteLine("Panel1_MouseWheel")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles mPanel.MouseDown
        mMainProcessorObject.gState.gMouse.setDown(e)
        Try
            mViewImageManager.gFunction.gSlideShow.TimerReset() '何かの操作中はSlideShowをリセット

            Select Case e.Button
                Case MouseButtons.Right
                    addLog(3, "mPanel.MouseDown -> MouseEventArgs -> MouseButtons.Right")
                    'mViewImageManager.gControls.gContextMenuStrip.ShowContextMenu(sender, e)
                Case MouseButtons.Left
                    'addLog(3, "左クリック")
                Case MouseButtons.Middle
                    addLog(3, "中央ボタン")
                Case MouseButtons.XButton1
                    'Windows2000から5ボタンサポート
                    addLog(3, "ブラウザ戻る")
                Case MouseButtons.XButton2
                    'Windows2000から5ボタンサポート
                    addLog(3, "ブラウザ進む")
                Case MouseButtons.None
                    addLog(3, "なし")
            End Select
        Catch ex As Exception

        End Try
    End Sub

    '=======================================================================
    Public Sub changeSize(size As Size)
        Try
            mPanel.Size = size
        Catch ex As Exception
            addLog(0, "changeSize", ex.Message)
        End Try
    End Sub

    Public Sub changeSizeWithMenuMenuStripVisible(MenuStripHeight As Integer)
        Try
            If (mPanel.Height - MenuStripHeight) <= 0 Then
                'ゼロ以下なら変更しない
                addLog(0, "changeSizeWithMenuMenuStripVisible", "height value error")
            Else
                'size change
                mPanel.Size = New Size(mPanel.Size.Width, mPanel.Height - MenuStripHeight)
                'location
                mPanel.Location = New Size(0, MenuStripHeight)
            End If
            addLog(3, Me.ToString & ".changeSizeWithMenuMenuStripVisible : true")
            addLog(3, "PanelLocation = " & mPanel.Location.X & "," & mPanel.Location.Y)
            addLog(3, "PanelSize = " & mPanel.Size.Width & "," & mPanel.Size.Height)
        Catch ex As Exception
            addLogForSystemError("changeSizeWithMenuMenuStripVisible")
            addLogForSystemError(ex.Message)
        End Try
    End Sub


    Public Sub changeSizeWithMenuMenuStripUnVisible()
        Try
            'MainFormの大きさに戻す
            mPanel.Size = New Size(mPanel.Size.Width, mMainProcessorObject.gMainForm.getForm.Height)
            'Location change
            mPanel.Location = New Size(0, 0)

            addLog(3, Me.ToString & ".changeSizeWithMenuMenuStripUnVisible : false")
            addLog(3, "PanelLocation = " & mPanel.Location.X & "," & mPanel.Location.Y)
            addLog(3, "PanelSize = " & mPanel.Size.Width & "," & mPanel.Size.Height)
        Catch ex As Exception
            addLog(0, "changeSizeWithMenuMenuStripUnVisible", ex.Message)
        End Try
    End Sub

    'PictureBox1_Click
    Private Sub Panel1_MouseClick(sender As Object, e As MouseEventArgs) Handles mPanel.MouseClick
        addLog(4, "Panel_click")

        If mMainProcessorObject.gState.gMouse.isDrag Then
            'Drag後はクリックじゃない
            'マウスのクリック位置を記憶
            Exit Sub
        End If

        '右側か左側か
        If mMainProcessorObject.gNowViewImageManager.gEvents.gPictureBox.ClickPointIsRightSideInPictureBox(e, mPanel.Size) Then
            '右なら次へ
            'mMainProcessorObject.gEvents.gPaintEvents.NextPaintImage(mViewImageManager)
            mViewImageManager.gEvents.gViewer.NextPaintImage()
        Else
            '左なら前へ
            'mMainProcessorObject.gEvents.gPaint.PreviousPaintImage(mViewImageManager)
            mViewImageManager.gEvents.gViewer.PreViousPaintImage()
        End If

        'If e.Button = MouseButtons.Right Then
        '    addLog(3, "mPanel.MouseClick -> MouseEventArgs -> MouseButtons.Right")
        '    'mMainProcessorObject.gMainForm.getForm.ContextMenuStrip1 = mViewImageManager.gControls.gContextMenuStrip.getObject
        '    mViewImageManager.gControls.gContextMenuStrip.ShowContextMenu()
        'End If
    End Sub

    Private Sub panel1_paint(sender As Object, e As PaintEventArgs) Handles mPanel.Paint
        addLogNotFocus(3, "panel1_paint")
        Try
            If mMainProcessorObject.gNowViewImageManager.gState.gMovie.MoviePlay Then
                'Panel1.Invalidate()
            End If
        Catch ex As Exception
            Console.WriteLine("panel1_paint")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub Panel1_SizeChanged(sender As Object, e As EventArgs) Handles mPanel.SizeChanged
        Try
            mViewImageManager.gFunction.gSlideShow.TimerReset() '何かの操作中はSlideShowをリセット

            'Panel処理時はToolStripContainerも一緒に処理しないと表示がずれる

            'If Not mMainProcessorObject Is Nothing Then
            '    If mMainProcessorObject.gState.PaintFormInitialize Then Exit Sub
            '    mMainProcessorObject.gNowViewImageManager.gFunction.gPictureBox.AlignLocationForPictureBox()
            '    'MainProcessorObject.ConsoleWriteSize()
            'End If
            'Console.WriteLine(Panel1.Width & ":" & Panel1.Height)
        Catch ex As Exception
            Console.WriteLine("Panel1_SizeChanged")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub Panel1_DragDrop(sender As Object, e As DragEventArgs) Handles mPanel.DragDrop
        addLog(3, "PanelEvents : Panel1_DragDrop")
        mMainProcessorObject.gEvents.gMouse.MakeFileListByDragAndDrop(sender, e)
    End Sub

    Private Sub Panel1_DragEnter(sender As Object, e As DragEventArgs) Handles mPanel.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    '=======================================================================
    Private Sub Panel1_Click(sender As Object, e As EventArgs) Handles mPanel.Click
        Try
            'AddLog("Panel_click")


        Catch ex As Exception
            addLogForSystemError(Me.ToString & "Panel1_Click")
            addLogForSystemError("Panel1_Click")
        End Try
    End Sub

    'Private Sub mPanel_KeyDown(sender As Object, e As KeyEventArgs) Handles mPanel.KeyDown
    '    addLog(3, "mPanel_KeyDown : " & e.KeyValue)
    '    mMainProcessorObject.PictureBoxKeyDown(sender, e)
    'End Sub
End Class
