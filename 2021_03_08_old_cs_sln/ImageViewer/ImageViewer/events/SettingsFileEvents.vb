Public Class SettingsFileEvents
    Inherits AbstractEvents

    'Friend MainProcessorObject As MainProcesser
    'Public Sub New(argMainProcessor As MainProcesser)
    '    MainProcessorObject = argMainProcessor
    'End Sub
    'Public Sub New()
    'End Sub
    'Private Sub setMainProcessor(argMainProcessor As MainProcesser)
    '=======================================================================
    Public Sub New(argMainProcessor As MainProcesser)
        setMainProcessor(argMainProcessor)
    End Sub

    Public Sub setSlideShowFlag(flag As Boolean, interval As Integer)
        'Interval
        MainProcessorObject.gNowViewImageManager.gState.gSlideShowInterval = interval
        MainProcessorObject.gSettings.SlideShowInterval = interval
        'Flag
        MainProcessorObject.gNowViewImageManager.gState.gSlideShowExecute = flag
    End Sub

    Public Sub Initialize()
        Try
            Dim pSettingFileFunction As SettingsFileFunction = MainProcessorObject.gFunction.gSettingsFile
            addLog(3, "SettingsFileEvents.Initialize  path=", MainProcessorObject.gSettings.SettingsFilePath)
            'iniファイルパス＋ファイル名
            pSettingFileFunction.setSettingsFilePath(
                MainProcessorObject.gSettings.SettingsFilePath)
            'デフォルト値をセット
            pSettingFileFunction.setValueDefaultToSettingsFile()

            If Not pSettingFileFunction.isExistsSettingsFile() Then
                'ファイルがない場合は書き込み_ファイル作成
                'ファイルがなければファイルを作る
                pSettingFileFunction.MakeSettingsFileIfFileNotExists()
            End If
            '読み込み
            pSettingFileFunction.readSettingFileAndSaveDataToPrivate()

            'デフォルトの設定値がなければ値を挿入
            pSettingFileFunction.InitializeForDefaultValue()

            'SettingsObjectへ値を格納

        Catch ex As Exception
            addLogForSystemError("SettingsFileEvents.Initialize")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub applySettings()
        Try
            Dim value As String
            value = MainProcessorObject.gFunction.gSettingsFile.getSettingsValue("[Common]", "LastFolder")

            value = value
        Catch ex As Exception
            addLogForSystemError("SettingsFileEvents.Initialize")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub copyValueToViewImageObject()
        copyValueToViewImageObject(MainProcessorObject.gNowViewImageManager)
    End Sub

    Public Sub copyValueToViewImageObject(argViewImageManager As ViewImageManager)
        Try
            Dim sectionName As String = "[Contents1]"
            argViewImageManager.gSettings.SectionName = sectionName
            addLog(3, "SettingsFileEvents.copyValueToViewImageObject : sectionName = " & sectionName)
            Dim value As String
            value = MainProcessorObject.gFunction.gSettingsFile.getSettingsValue(sectionName, "LastFolder")
            argViewImageManager.gSettings.LastFolder = value
            addLog(3, "LastFolder = ", argViewImageManager.gSettings.LastFolder)
            value = MainProcessorObject.gFunction.gSettingsFile.getSettingsValue(sectionName, "LastFile")
            argViewImageManager.gSettings.LastFile = value
            addLog(3, "LastFile = ", argViewImageManager.gSettings.LastFile)
            value = MainProcessorObject.gFunction.gSettingsFile.getSettingsValue(sectionName, "SlideShowTime")
            argViewImageManager.gSettings.LastFile = value
            addLog(3, "SlideShowTime = ", argViewImageManager.gSettings.SlideShowTime)
            value = MainProcessorObject.gFunction.gSettingsFile.getSettingsValue(sectionName, "ImageBackGroundColor")
            argViewImageManager.gSettings.LastFile = value
            addLog(3, "ImageBackGroundColor = ", argViewImageManager.gSettings.ImageBackGroundColor)
        Catch ex As Exception
            addLogForSystemError("SettingsFileEvents.copyValueToViewImageObject")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub doFinalize()
        Try
            Dim pSettingFileFunction As SettingsFileFunction = MainProcessorObject.gFunction.gSettingsFile
            addLog(3, "SettingsFileEvents.Initialize  path=", MainProcessorObject.gSettings.SettingsFilePath)
            'iniファイルパス＋ファイル名
            pSettingFileFunction.setSettingsFilePath(
                MainProcessorObject.gSettings.SettingsFilePath)
            'ファイルがある場合は書き込み_ファイル作成
            'ファイルがなければファイルを作る
            'Privateの変数をファイルに書き込み
            pSettingFileFunction.UpdateSettingsFile()

        Catch ex As Exception
            addLogForSystemError("SettingsFileEvents.Finalize")
            addLogForSystemError(ex.Message)
        End Try
    End Sub
    'Public Sub readSettingsFile()

    'End Sub

    'setSettingsValueBySettingsFile
    '該当しないものもある
    'Section + Property withstart then　格納　必要に応じて？
End Class
