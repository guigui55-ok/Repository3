Public Interface ILog
    'enableLoging bool 
    Sub showWindow()
    Sub setWindowPosSize(position As Point, size As Size)
    Sub applyWindowPosSize()
    Sub setMode(mode As Integer)
    Sub startLoging()
    Sub stopLoging()
    Sub addLog(value As String)
    Sub initialize(mode As Integer, saveloglevel As Integer, windowVisible As Boolean)
    Sub setFilePath(path As String)
    Sub setFilePath()
    Sub setFileName(Filename As String)
    Sub setFileName()
    Sub initializeLogFile()
    Function getLogFileFullpath() As String

    Sub addLog(priority As Integer, functionName As String, Optional parameter As String = "")
    Function alingmentLog(functionName As String, Optional param As String = "") As String
    Sub addLogMain(value As String)

    Sub addLogToList(value As String)
    Sub addLogToFile(value As String)
    Sub updataWindow(value As String)

    Sub setNotFocus(flag As Boolean)
    Sub updateWindowNotFocus(value As String)
    Sub writeLogAllToFile()
    Sub Dispose()
End Interface
