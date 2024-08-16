Public Class ContextMenuStripResister
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
    Public Sub New(argMainProcessorObject As MainProcesser, argContextMenuStrip As ContextMenuStrip)
        MainProcessorObject = argMainProcessorObject
        mContextMenuStrip = argContextMenuStrip

    End Sub
    Private WithEvents mContextMenuStrip As ContextMenuStrip
    Private mMainForm As MainForm
    '=======================================================================

End Class
