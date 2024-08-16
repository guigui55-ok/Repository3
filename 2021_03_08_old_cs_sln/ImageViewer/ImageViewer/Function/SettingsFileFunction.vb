Public Class SettingsFileFunction
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
        mIniManager = New IniManager()
        'デフォルト値をセット
        'Me.setValueDefaultToSettingsFile()
        'ファイルがない場合は書き込み_ファイル作成

    End Sub
    '=======================================================================
    Private mIniManager As IniManager

    Public Sub disposeObjects()
        Try
            mIniManager = Nothing
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub writeSettingsToFile()
        mIniManager.WriteSettingsToFile()
    End Sub

    Public Sub setSettingsFilePath(path As String)
        'iniファイルパス＋ファイル名
        mIniManager.setFilePath(path)
    End Sub

    Public Function getSettingsValue(SectionName As String, PropertyName As String) As String
        Return mIniManager.getSettingsValue(SectionName, PropertyName)
    End Function

    '値を更新
    Public Sub updateSettingsValue(SectionName As String, PropertyValue As String)
        mIniManager.UpdateSettingsValueSingleIfAddValueIsNothing(SectionName, PropertyValue)
    End Sub

    'デフォルトの値をセット 外部引数より
    Public Sub setValueDefaultTosettingsFile(sectionList As List(Of String), PropetyListAsHashtable As Hashtable)
        Me.setValueDefaultTosettingsFile(sectionList, PropetyListAsHashtable)
    End Sub

    'デフォルトの値をセット
    Public Sub setValueDefaultToSettingsFile()
        Try
            Dim sectionList As List(Of String) = New List(Of String)
            Dim NowSectionName As String = ""
            NowSectionName = "[Common]"
            sectionList.Add(NowSectionName)
            'common
            Dim commonPropertyList As List(Of String) = New List(Of String)
            'マイピクチャ
            commonPropertyList.Add("LastFolder=" & System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures))
            commonPropertyList.Add("LastFile=")
            commonPropertyList.Add("SlideShowTime=4500")
            commonPropertyList.Add("ImageBackGroundColor=")
            commonPropertyList.Add("WindowBackGroundColor=")
            'WindowSizePos Left Top Width Height

            Dim PropertyListHashtable As New Hashtable
            PropertyListHashtable.Add(NowSectionName, commonPropertyList)
            '===============================
            NowSectionName = "[Contents1]"
            sectionList.Add(NowSectionName)
            commonPropertyList = New List(Of String)
            commonPropertyList.Add("LastFolder=" & System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures))
            commonPropertyList.Add("LastFile=")
            commonPropertyList.Add("SlideShowTime=4500")

            PropertyListHashtable.Add(NowSectionName, commonPropertyList)

            mIniManager.setSectionList(sectionList)
            mIniManager.setPropertyListAsHashtable(PropertyListHashtable)

            mIniManager.setDefaultSectionList(sectionList)
            mIniManager.setDefaultPropertyListAsHashtable(PropertyListHashtable)

        Catch ex As Exception
            addLogForSystemError("setValueDefaultToSettingsFile")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub outputValueDefaultToCOnsole()
        Console.WriteLine("-----------------------------------")
        Console.WriteLine(mIniManager.getSettingsValueDefaultAsString)
        Console.WriteLine("-----------------------------------")
    End Sub
    Public Sub outputValueMainToCOnsole()
        Console.WriteLine("-----------------------------------")
        Console.WriteLine(mIniManager.getSettingsValueNowAsString)
        Console.WriteLine("-----------------------------------")
    End Sub

    Public Sub InitializeForDefaultValue()
        If mIniManager.isExistsDefalutValue Then
            mIniManager.InitializeForDefaultValue()
        Else
            addLog(3, "SettingsFileFunction.InitializeForDefaultValue : isExistsDefalutValue = false")
        End If
    End Sub

    Public Function isExistsSettingsFile() As Boolean
        Return mIniManager.isExistsPath()
    End Function

    'ないときに書き込む
    Public Sub MakeSettingsFileIfFileNotExists()
        Dim flag As Integer = mIniManager.MakeSettingsFileIfFileNotExists(mIniManager.getFilePath)
        If flag < 1 Then
            'error
            addLogForSystemError("MakeSettingsFileIfFileNotExists Error")
        Else
            addLogForSystemError("MakeSettingsFileIfFileNotExists Success")
        End If
    End Sub

    Public Sub readSettingFileAndSaveDataToPrivate()
        mIniManager.readSettingFileAndSaveDataToPrivate()
    End Sub

    '保持している値を書き込む→mIniManagerに
    '同じ値があれば更新
    '値がなければ挿入
    Public Sub UpdateSettingsFile()
        mIniManager.UpdateSettingsFile()
    End Sub



End Class
