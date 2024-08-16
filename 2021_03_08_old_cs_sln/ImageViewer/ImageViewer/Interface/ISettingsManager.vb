Public Interface ISettingsManager
    Sub setPath()
    Sub readParameter(section As String, profile As String)
    Sub writeParameter(section As String, profile As String)
End Interface
