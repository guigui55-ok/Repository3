Public Class FinalizeEvents
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
    Public Sub excute()
        Try
            addLog(3, "FinalizeEvents.excute")
            '現在の状態をMainClassSettingsへ
            MainProcessorObject.gNowViewImageManager.gEvents.gSettings.FinalizeMethod()
            'MainClassSettingsから設定ファイルへ書き込み上書き
            MainProcessorObject.gFunction.gSettingsFile.writeSettingsToFile()
            'ログをログファイルへ出力
            MainProcessorObject.gLog.writeLogAllToFile()

        Catch ex As Exception
            addLogForSystemError("FinalizeEvents.excute")
            addLogForSystemError(ex.Message)
        End Try
    End Sub
End Class
