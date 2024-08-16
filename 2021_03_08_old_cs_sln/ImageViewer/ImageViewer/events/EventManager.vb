Public Class EventManager
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

        gPaint = New PaintEvents(MainProcessorObject)
        gFileList = New FileListEvents(MainProcessorObject)
        gFileIo = New FileIoEvents(MainProcessorObject)
        gMouse = New MouseEvents(MainProcessorObject)
        gSettingsFile = New SettingsFileEvents(MainProcessorObject)
        gInitialize = New InitializeEvents(MainProcessorObject)
        gCommandLine = New CommandLineEvents(MainProcessorObject)
        gFinalize = New FinalizeEvents(MainProcessorObject)
    End Sub
    '=======================================================================
    Public gPaint As PaintEvents
    Public gFileList As FileListEvents
    Public gFileIo As FileIoEvents
    Public gMouse As MouseEvents
    Public gSettingsFile As SettingsFileEvents
    Public gInitialize As InitializeEvents
    Public gCommandLine As CommandLineEvents
    Public gFinalize As FinalizeEvents

    Public Sub disposeObjects()
        Try
            gPaint = Nothing
            gFileList = Nothing
            gFileIo = Nothing
            gMouse = Nothing
            gSettingsFile = Nothing
            gInitialize = Nothing
            gCommandLine = Nothing
            gFinalize = Nothing
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

End Class
