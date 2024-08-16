Public Interface ICommandLineReader
    Sub setCommandLine(cmd As String())
    Sub setSettingObject(SettingObject As Object)
    Function getSettingsObject() As Object
    Function isExistsCommandInCommandLine(argCommand As String) As Boolean
    Function getValueCommandInCommandLine(argCommand As String) As String
    Sub ApplyToSettingsObjectFromCommandLine()
End Interface
