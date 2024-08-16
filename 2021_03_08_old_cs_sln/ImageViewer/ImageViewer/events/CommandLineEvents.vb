Public Class CommandLineEvents
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


    '=======================================================================
    'MainMethod
    Public Sub setVriousValueByReadCommandLine()
        addLog(3, "CommandLineEvents.setVriousValueByReadCommandLine : ", MainProcessorObject.gFunction.gCommandLine.getCommandlineAll)

        'CommandLineのカウントが１でファイルパスのみ引き渡されている場合にリストをセット
        MainProcessorObject.gFunction.gCommandLine.setFileListWhenCommandLineIsPathAsSingleValue()
        'ファイル読み込み-r
        setFileListFromThis()
        'スライドショー-ss
        setFlagSlideShow()
        'ウィンドウサイズws
        setWindowSizeToState()
    End Sub

    Public Sub setWindowSizeToState()
        Try
            'ウィンドウサイズws
            MainProcessorObject.gState.NowWindowSize = MainProcessorObject.gFunction.gCommandLine.
                GetWindowSizeButReturnNowValueIfReadError(
                MainProcessorObject.gState.NowWindowSize)
        Catch ex As Exception
            addLogForSystemError("setWindowSizeToState")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub setFlagSlideShow()
        Try
            'スライドショー-ss
            Dim SlideShowExcute As Boolean = MainProcessorObject.gFunction.gCommandLine.getFlagForSlideShow()
            'IntervalTime
            '引数はGetした値が無効であった時の初期値
            MainProcessorObject.gSettings.SlideShowInterval = MainProcessorObject.gFunction.gCommandLine.getSlideShowInterval(
                MainProcessorObject.gSettings.SlideShowIntarvalInitialize)

            '値をStateとSettingsへ
            'ViewImageManagerにも渡す
            MainProcessorObject.gEvents.gSettingsFile.setSlideShowFlag(
                SlideShowExcute,
                MainProcessorObject.gSettings.SlideShowInterval)

        Catch ex As Exception
            addLogForSystemError("setFlagSlideShow")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    'ファイル読み込み-r
    Public Sub setFileListFromThis()
        Try
            'ファイル読み込み-r
            Dim readPath As String = MainProcessorObject.gFunction.gCommandLine.getPathForMakeFileList()

            'FileListへ
            MainProcessorObject.gNowViewImageManager.gEvents.gImageFileList.makeListFromFilePath(
                readPath, MainProcessorObject.gSettings.ReadSubFolder)
        Catch ex As Exception
            addLogForSystemError("setFileListFromThis")
            addLogForSystemError(ex.Message)
        End Try
    End Sub


End Class
