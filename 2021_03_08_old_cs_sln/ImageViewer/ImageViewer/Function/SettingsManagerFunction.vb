Public Class SettingsProcesser
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
    Public Settings As Settings

    Public Sub New(ByRef argSettings As Settings)
        Settings = argSettings
    End Sub

    Public Sub setSettingForInitialize()
        Settings.ImageFadeOut = True
        Settings.ImageFadeIn = True
    End Sub
End Class
