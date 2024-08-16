Public Class StatusStripEvents
    Inherits AbstractEvents
    Private WithEvents mStatusStrip As StatusStrip

    'Friend MainProcessorObject As MainProcesser
    'Public Sub New(argMainProcessor As MainProcesser)
    '    MainProcessorObject = argMainProcessor
    'End Sub
    'Public Sub New()
    'End Sub
    'Private Sub setMainProcessor(argMainProcessor As MainProcesser)
    '=======================================================================
    'Public Sub New(argMainProcessor As MainProcesser)
    '    setMainProcessor(argMainProcessor)
    'End Sub

    Public Sub New(argStatusStrip As StatusStrip, argMainProcessor As MainProcesser)
        mStatusStrip = argStatusStrip
        setMainProcessor(argMainProcessor)
    End Sub
    '=======================================================================
    Public Function getStatusStrip() As StatusStrip
        Return mStatusStrip
    End Function

    Public Sub DisposeObjects()
        Try
            mStatusStrip = Nothing
        Catch ex As Exception
            addLogForSystemError("StatusStripEvents.DisposeObjects")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Function getHeight() As Integer
        Return mStatusStrip.Height
    End Function

    Public Sub updateText(value As String)

        Try 'コンテキストメニューを表示する座標
            MainForm.StatusStrip1.Items(0).Text = value
            'mStatusStrip.Invalidate()
        Catch ex As Exception
            addLogForSystemError("StatusStripEvents.updateText")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    '=======================================================================
    'StatusStrip1
    '=======================================================================
    Private Sub StatusStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)

        'MenuStripEventsObject.setVisibleForMenuStripActionEvent()
    End Sub

    Private Sub StatusStrip_Click(sender As Object, e As EventArgs)
        Console.WriteLine("StatusStrip_Click")
        mStatusStrip.Text = "StatusStrip_Click"
        mStatusStrip.Invalidate()
    End Sub

End Class
