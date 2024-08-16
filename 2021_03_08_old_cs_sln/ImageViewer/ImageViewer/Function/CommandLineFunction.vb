Public Class CommandLineFunction
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
        setCommandLine(System.Environment.GetCommandLineArgs())
    End Sub
    '=======================================================================
    Private mCommandLineReader As CommandLineReader

    Public Sub disposeObjects()
        Try
            mCommandLineReader = Nothing
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'PrivateのListに格納、New時に実行
    Public Sub setCommandLine(ary() As String)
        Try
            mCommandLineReader = Nothing
            mCommandLineReader = New CommandLineReader()
            mCommandLineReader.setCommandLine(ary)
        Catch ex As Exception
            addLogForSystemError("setCommandLine")
            addLogForSystemError(ex.Message)
        End Try
    End Sub
    '=======================================================================
    'コマンドラインが1つかつ、ファイルならセットする
    'exeファイルにD&Dで起動用
    'CommandLineのカウントが１でファイルパスのみ引き渡されている場合
    Public Sub setFileListWhenCommandLineIsPathAsSingleValue()
        Try
            'CommandLineのカウントが１
            If mCommandLineReader.getListCount = 1 Then
                'CommandLine引数の1番目をGet
                Dim path As String = mCommandLineReader.getValueFromList(0)

                'パスか
                If New CommonFile().isFilePathExists(path) Then
                    'パスが存在したらリスト作成
                    Dim filelist As List(Of String) =
                        MainProcessorObject.gNowViewImageManager.gFunction.gImageFileLIst.makeListFromFilePath(
                            path, MainProcessorObject.gSettings.ReadSubFolder)
                    '作成したリストを格納
                    MainProcessorObject.gNowViewImageManager.gImageFileList.setViewFileList(filelist)
                Else
                    addLog(3, "setFileWhenCommandLineIsSingleValue : path Not Exists : " & path)
                End If

                '読み込み可能ファイル形式設定
                'MainProcessorObject.gNowViewImageManager.gImageFileList.setReadAbleFileTypeList(New List(Of String)(
                '    MainProcessorObject.gSettings.ReadFileTypeList))
            End If
        Catch ex As Exception
            Console.WriteLine("setFileListWhenCommandLineIsPathAsSingleValue")
            Console.WriteLine(ex.Message)
        End Try
    End Sub



    '-ws ウィンドウサイズ　読み込み、エラー時は現在の値(引数)を返す
    Public Function GetWindowSizeButReturnNowValueIfReadError(nowValue As Rectangle) As Rectangle
        Try
            Dim cval As String = mCommandLineReader.getValueCommandInCommandLine("-ws")
            Dim ws() As String = Split(cval, ",")
            Return New Rectangle(CInt(ws(0)), CInt(ws(1)), CInt(ws(2)), CInt(ws(3)))
        Catch ex As Exception
            addLogForSystemError("getWindowSizeButReturnNowValueIfReadError")
            addLogForSystemError(ex.Message)
            Return nowValue
        End Try
    End Function

    '読み込みフォルダの値を取得
    Public Function getPathForMakeFileList() As String
        Return mCommandLineReader.getPathForMakeFileListByCommandLine("-r")
    End Function

    'スライドショーフラグ
    Public Function getFlagForSlideShow() As Boolean
        Return mCommandLineReader.isExistsCommandInCommandLine("-ss")
    End Function

    '-ss interval を読み込み
    Public Function getSlideShowInterval(IntervalInitializeValueWhenValueIsInvalid As Integer) As Integer
        Try
            Dim cval As String = mCommandLineReader.getValueCommandInCommandLine("-ss")
            If IsNumeric(cval) Then
                Return CType(cval, Integer)
            Else
                Return IntervalInitializeValueWhenValueIsInvalid
            End If
        Catch ex As Exception
            addLogForSystemError("getSlideShowIntervaltByCommandLine")
            addLogForSystemError(ex.Message)
            Return IntervalInitializeValueWhenValueIsInvalid
        End Try
    End Function
    '=======================================================================

    '1つの文字列に,区切り文字は半角スペース
    Public Function getCommandlineAll() As String
        Try
            '1つの文字列に
            Dim buf As String = ""
            For Each value In System.Environment.GetCommandLineArgs()
                buf = buf & " " & value
            Next
            Return buf
        Catch ex As Exception
            addLogForSystemError("getCommandlineAll")
            addLogForSystemError(ex.Message)
            Return ""
        End Try
    End Function


End Class
