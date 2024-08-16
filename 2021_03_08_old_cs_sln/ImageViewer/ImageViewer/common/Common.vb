

Public Module GlobalVariables
    Public MyGlobalString As String
    Public WithEvents gLog As New Log()

    Sub setLogForGlobal(logObject As Log)
        gLog = logObject
    End Sub

    Sub DisposeLog()
        Try
            If Not gLog Is Nothing Then
                gLog.Dispose()
                gLog = Nothing
            End If
        Catch ex As Exception
            Console.WriteLine("DisposeLog")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Sub addLog(argLog As String)
        Try
            gLog.addlog(argLog)
        Catch ex As Exception
            Stop
        End Try
    End Sub

    'AddLogCommon
    Sub addLogCom(argLog As String, Optional parameter As String = "")
        addLogForSystemError(argLog, parameter)
        Console.WriteLine(argLog)
    End Sub

    Sub addLog(priority As Integer, functionInfo As String, Optional parameter As String = "")
        gLog.addLog(priority, functionInfo, parameter)
    End Sub

    Sub addLogNotFocus(priority As Integer, functionInfo As String, Optional parameter As String = "")
        gLog.setNotFocus(True)
        gLog.addLog(priority, functionInfo, parameter)
        'Log追記時にmNotFocus=falseにしている_注意
    End Sub

    Sub addLog(intLog As Integer)
        addLog(intLog.ToString)
    End Sub

    Sub ConsoleWriteLine(argObject As Object)
        Try
            Console.WriteLine(argObject)
        Catch ex As Exception
            Console.WriteLine("ConsoleWriteLine")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Sub addLogForSystemError(functionInfo As String, Optional parameter As String = "")
        gLog.addLog(0, functionInfo, parameter)
    End Sub
End Module