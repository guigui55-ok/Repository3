Public Class MainFormFunction
    Inherits AbstractEvents

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
    Public WithEvents gMainForm As MainForm

    Public Sub ajustFormLocation()
        Try

        Catch ex As Exception
            addLogForSystemError("Form1_MouseMove")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub MainForm_SiseChanged(sender As Object, e As EventArgs) Handles gMainForm.SizeChanged
        Try
            Dim tToolStripContainer As ToolStripContainer =
                MainProcessorObject.gMainForm.gControls.gToolStripEvents.getToolStripContainer
            tToolStripContainer.Size = gMainForm.Size
            addLog(3, "tToolStripContainer.Size = " & tToolStripContainer.Size.Width & "," & tToolStripContainer.Size.Height)
        Catch ex As Exception
            addLogForSystemError("Form1_MouseMove")
            addLogForSystemError(ex.Message)
        End Try
    End Sub
End Class
