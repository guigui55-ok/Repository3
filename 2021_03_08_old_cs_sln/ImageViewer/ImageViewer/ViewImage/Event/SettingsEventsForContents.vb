Public Class SettingsEventsForContents
    Inherits AbstractImageViewerChild

    'Friend mMainProcessorObject As MainProcesser
    'Friend mViewImageManager As ViewImageManager
    'Friend Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
    'Friend Sub New()
    'Friend Sub setMainProcessor(argMainProcessor As MainProcesser)
    'Friend Sub setViewImageManager(argViewImageManager As ViewImageManager)
    '=======================================================================
    Public Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
        MyBase.New(argMainProcessor, argViewImageManager)
        mMainProcessorObject = argMainProcessor
        mViewImageManager = argViewImageManager
    End Sub

    'LastFolderのパス内のファイルリストをセット
    Public Sub setListByLastFolder()
        addLog(3, Me.ToString & ".setListByLastFolder")
        Try
            'パス存在チェック
            If Not New CommonFile().isExistsPath(mViewImageManager.gSettings.LastFolder) Then
                addLog(1, "setListByLastFolder : Folder IsNot Exists = " & mViewImageManager.gSettings.LastFolder)
                Exit Sub
            End If

            'ファイルリストをセット
            mViewImageManager.gEvents.gImageFileList.makeListFromFilePath(
                mViewImageManager.gSettings.LastFolder, mMainProcessorObject.gSettings.ReadSubFolder)

        Catch ex As Exception
            Console.WriteLine(Me.ToString & ".setListByLastFolder")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'LastFolder値をImageViewerSettingsへ
    Public Sub setLastFolder()
        Try
            mViewImageManager.gSettings.LastFolder = mViewImageManager.gFunction.gImageFileLIst.getNowFolerPath
        Catch ex As Exception
            Console.WriteLine(Me.ToString & ".setLastFolder")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '現在の状態をImageViewerSettingsへ
    Public Sub updateSettingsFromNowState()
        setLastFolder()
        mViewImageManager.gSettings.LastFile = mViewImageManager.gFunction.gImageFileLIst.getNowFilePath
    End Sub

    'ImageViewerSettings値をMainClassSettingsへ
    Public Sub setValueToMainClassSettings(MainClassSettingsFunction As SettingsFileFunction)
        Try
            Dim SectionName As String = mViewImageManager.gSettings.SectionName
            'LastFolder
            Dim UpdateValue As String =
                mViewImageManager.gWording.gSettings.LastFolder & "=" & mViewImageManager.gSettings.LastFolder
            MainClassSettingsFunction.updateSettingsValue(SectionName, UpdateValue)
            'LastFile
            UpdateValue = mViewImageManager.gWording.gSettings.LastFile & "=" & mViewImageManager.gSettings.LastFile
            MainClassSettingsFunction.updateSettingsValue(SectionName, UpdateValue)
            'SlideShowTime
            UpdateValue = mViewImageManager.gWording.gSettings.SlideShowTime & "=" & mViewImageManager.gSettings.SlideShowTime
            MainClassSettingsFunction.updateSettingsValue(SectionName, UpdateValue)
            'ImageBackGroundColor
            UpdateValue = mViewImageManager.gWording.gSettings.ImageBackGroundColor & "=" _
                & mViewImageManager.gSettings.ImageBackGroundColor
            MainClassSettingsFunction.updateSettingsValue(SectionName, UpdateValue)

        Catch ex As Exception
            Console.WriteLine(Me.ToString & ".setLastFolder")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub FinalizeMethod()
        '現在の状態をImageViewerSettingsへ
        updateSettingsFromNowState()
        'ImageViewerSettings値をMainClassSettingsへ
        setValueToMainClassSettings(mMainProcessorObject.gFunction.gSettingsFile)
    End Sub
End Class
