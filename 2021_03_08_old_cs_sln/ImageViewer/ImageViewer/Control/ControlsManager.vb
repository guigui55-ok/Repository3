Public Class ControlsManager
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

        gMainFormFunction = New MainFormFunction(MainProcessorObject)
        'gMenuStripEvents = New MenuStripEvents(MainProcessorObject.gMainForm.getForm.MenuStrip1, MainProcessorObject)
        gMenuStripEvents = New MenuStripEvents(MainProcessorObject)
        gMenuStripEvents.initializeObjects(MainProcessorObject.gMainForm.getForm.MenuStrip1, MainProcessorObject)
        gContextMenuStrip = New ContextMenuStripEvents(MainProcessorObject, New ContextMenuStrip())
    End Sub
    '=======================================================================
    Public gMainFormFunction As MainFormFunction
    Public gMenuStripEvents As MenuStripEvents
    'Public gMenuStirpResistor As MenuStripEventsResister
    Public gContextMenuStrip As ContextMenuStripEvents
    'Public gToolStripEvents As ToolStripEvents

    Public Sub disposeObjects()
        Try
            gMainFormFunction = Nothing
            gMenuStripEvents.disposeObjects()
            gMenuStripEvents = Nothing

            'gMenuStirpResistor = Nothing
            gContextMenuStrip = Nothing
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
End Class
