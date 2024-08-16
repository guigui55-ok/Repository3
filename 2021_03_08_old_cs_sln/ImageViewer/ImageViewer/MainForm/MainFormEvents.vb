Public Class MainFormEvents
    Inherits AbstractFunction

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
    Public Sub New(argMainForm As MainForm, argMainProcessor As MainProcesser)
        MainProcessorObject = argMainProcessor
        mMainForm = argMainForm
    End Sub
    Private WithEvents mMainForm As MainForm


    '=======================================================================
    'Form1
    '=======================================================================
    'フォームが閉じたとき
    Private Sub MainForm_Closed(ByVal sender As Object, e As EventArgs) Handles mMainForm.Closed
        Try
            MainProcessorObject = Nothing
        Catch ex As Exception
            Console.WriteLine("MainForm_Closed")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'フォームが閉じている間
    Private Sub MainForm_Closing(ByVal sender As Object, e As EventArgs) Handles mMainForm.Closing
        Try
            MainProcessorObject.gEvents.gFinalize.excute()
        Catch ex As Exception
            Console.WriteLine("MainForm_Closing")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles mMainForm.KeyDown
        Try
            addLog(3, "MyBase_KeyDown")
            'Console.WriteLine("MyBase_KeyDown")
            'If e.KeyData = Keys.Right Then

            '    'MsgBox("right")
            'ElseIf (e.KeyData = (Keys.Menu + Keys.Alt)) Then
            '    addLog(3, "MyBase_KeyDown", "Keys.Menu + Keys.Alt")
            'ElseIf (e.KeyData = (Keys.Alt)) Then
            'End If
            'MainProcessorObject.PictureBoxKeyDown(sender, e)
            MainProcessorObject.PictureBoxKeyDown(sender, e)

        Catch ex As Exception
            Console.WriteLine("MyBase_KeyDown")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub MainForm_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles mMainForm.PreviewKeyDown
        addLog(3, "MainForm_PreviewKeyDown")
        Try
            Select Case e.KeyCode
        '矢印キーが押されたことを表示する
                Case Keys.Up, Keys.Left, Keys.Right, Keys.Down
                    addLog(3, "矢印キーが押されました。")
            'Tabキーが押されてもフォーカスが移動しないようにする
                Case Keys.Tab
                    e.IsInputKey = True
            End Select
        Catch ex As Exception
            Console.WriteLine(Me.ToString & ".MainForm_PreviewKeyDown")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    Private Sub Form1_MouseWheel(sender As Object, e As MouseEventArgs) Handles mMainForm.MouseWheel
        Try

        Catch ex As Exception
            Console.WriteLine("Form1_MouseWheel")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub Form1_LocationChanged(sender As Object, e As EventArgs) Handles mMainForm.LocationChanged
        Try
        Catch ex As Exception
            Console.WriteLine("Form1_LocationChanged")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub Form1DragDrop(sender As Object, e As DragEventArgs) Handles mMainForm.DragDrop
    End Sub

    Private Sub Form1DragEnter(sender As Object, e As DragEventArgs) Handles mMainForm.DragEnter
    End Sub

    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles mMainForm.MouseDown

    End Sub

    Private Sub Form1_MouseClick(sender As Object, e As MouseEventArgs) Handles mMainForm.MouseClick

    End Sub

    Private Sub Form1_Closing(sender As Object, e As FormClosingEventArgs) Handles mMainForm.FormClosing
        My.Settings.MyClientSize = mMainForm.ClientSize
    End Sub

    Private Sub Form1_MouseUp(sender As Object, e As MouseEventArgs) Handles mMainForm.MouseUp
    End Sub

    Private Sub Form1_Move(sender As Object, e As EventArgs) Handles mMainForm.Move

    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles mMainForm.Paint
        Try
        Catch ex As Exception
            Console.WriteLine("Form1_Paint")
            Console.WriteLine(ex.Message)
        Finally

        End Try
    End Sub

    Private Sub mMainForm_SizeChanged(sender As Object, e As EventArgs) Handles mMainForm.SizeChanged
        Try
            Dim tToolStripContainer As ToolStripContainer =
                MainProcessorObject.gMainForm.gControls.gToolStripEvents.getToolStripContainer
            tToolStripContainer.Size = New Size(
                mMainForm.Size.Width,
                mMainForm.Size.Height - MainProcessorObject.gMainForm.gControls.gStatusStripEvents.getHeight)
            addLog(3, "tToolStripContainer.Size = " & tToolStripContainer.Size.Width & "," & tToolStripContainer.Size.Height)
        Catch ex As Exception
            addLogForSystemError("Form1_MouseMove")
            addLogForSystemError(ex.Message)
        End Try
    End Sub
End Class
