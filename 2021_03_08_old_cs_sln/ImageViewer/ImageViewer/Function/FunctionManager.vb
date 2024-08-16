Public Class FunctionManager
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

        gPaint = New PaintFunction(MainProcessorObject)
        gFileIo = New FileIoFunction(MainProcessorObject)
        gFileList = New FileListFunction(MainProcessorObject)
        gMouse = New MouseFunction(MainProcessorObject)
        gSettingsFile = New SettingsFileFunction(MainProcessorObject)
        gCommandLine = New CommandLineFunction(MainProcessorObject)
    End Sub
    '=======================================================================
    Public gPaint As PaintFunction
    Public gFileIo As FileIoFunction
    Public gFileList As FileListFunction
    Public gMouse As MouseFunction
    Public gSettingsFile As SettingsFileFunction
    Public gCommandLine As CommandLineFunction

    Public Sub disposeObjects()
        Try
            gPaint = Nothing
            gFileIo = Nothing
            gFileList = Nothing
            gMouse = Nothing
            gSettingsFile.disposeObjects()
            gSettingsFile = Nothing
            gCommandLine.disposeObjects()
            gCommandLine = Nothing
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub initializeFunction()
        Try

        Catch ex As Exception
            addLog(0, Me.ToString & "initializeFunctionClass")
            addLog(0, ex.Message)
        End Try
    End Sub
End Class
