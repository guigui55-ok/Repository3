Public Class CommandLineReader
    Inherits AbstractCommandLineReader

    Private ImageViewerSettings As Settings
    '親クラスメソッド
    'Public Sub setCommandLine(cmd() As String) Implements ICommandLineReader.setCommandLine
    'Public Sub setSettingByCommandLine(SettingName As String) Implements ICommandLineReader.setSettingByCommandLine
    'Public Function getSettings() As Settings Implements ICommandLineReader.getSettings
    'Public Function isExistsCommandInCommandLine(argCommand As String) As Boolean Implements ICommandLineReader.isExistsCommandInCommandLine
    'Public Function getValueCommandInCommandLine(argCommand As String) As String Implements ICommandLineReader.getValueCommandInCommandLine

    Public Overloads Sub setSettingObject(argSetting As Settings)
        ImageViewerSettings = argSetting
    End Sub

    Public Overloads Function getSettingsObject() As Settings
        Return ImageViewerSettings
    End Function

    Public Overloads Sub ApplyToSettingsObjectFromCommandLine()

    End Sub

    '=======================================================================
    'Public Function isExistsInCommandLine(optionName As String) As String
    '    Try
    '        Dim value As String = getValueCommandInCommandLine(optionName)
    '        If (value.Equals(optionName)) Then
    '            Return value
    '        Else
    '            Return value
    '        End If
    '    Catch ex As Exception
    '        Console.WriteLine("getValueByOptionName")
    '        Return ""
    '    End Try
    'End Function
    '=======================================================================
    Public Function getPathForMakeFileListByCommandLine(cmd As String) As String
        Try
            Dim value As String = getValueCommandInCommandLine(cmd)
            If (cmd.Equals("-r")) Then
                Return value
            Else
                Return value
            End If
        Catch ex As Exception
            Console.WriteLine("getPathForMakeFileListByCommandLine")
            Return ""
        End Try
    End Function
    '=======================================================================
    '-r ファイルを読み込み
    Public Function MakeImageFileListByCommandLine(argCommandName As String) As List(Of String)
        Try
            Dim CommandValue As String = getValueCommandInCommandLine(argCommandName)
            If (argCommandName.Equals("-r")) Then
                If Not (CommandValue = "") Then
                    Return AddImageFileList(CommandValue)
                Else
                    Return New List(Of String)
                End If
            Else
                Return New List(Of String)
            End If
        Catch ex As Exception
            Console.WriteLine("ExcuteSetImageFileListByCommandLine")
            Return New List(Of String)
        End Try
    End Function

    Public Function AddImageFileList(argPath As String) As List(Of String)
        Try
            Dim FileListMakerObject As New FileListMaker()
            FileListMakerObject.setCountReadSubFolderHierarchy(ImageViewerSettings.ReadSubFolder)
            FileListMakerObject.AddImageFileList(argPath)
            Return FileListMakerObject.getFileList
        Catch ex As Exception
            Console.WriteLine("AddImageFileList")
            Return New List(Of String)
        End Try
    End Function
    '=======================================================================
    '-ss スライドショー
    Public Function getSlideShowFlagtByCommandLine() As Boolean
        Try
            getSlideShowFlagtByCommandLine = isExistsCommandInCommandLine("-ss")
        Catch ex As Exception
            Console.WriteLine("getSlideShowFlagtByCommandLine")
            Return False
        End Try
    End Function

    '=======================================================================
    '-ws ウィンドウサイズ　読み込み
    Public Function getWindowSizeByCommandLine(nowRectAngle As Rectangle) As Rectangle
        Try
            Dim cval As String = getValueCommandInCommandLine("-ws")
            If True Then
                Dim ws() As String = Split(cval, ",")
                Return New Rectangle(ws(0), ws(1), ws(2), ws(3))
            Else
                Return nowRectAngle
            End If
        Catch ex As Exception
            Console.WriteLine("getWindowSizeByCommandLine")
            Return nowRectAngle
        End Try
    End Function

    '=======================================================================
    '-ss interval を読み込み
    Public Function getSlideShowIntervaltByCommandLine() As Integer
        Try
            Dim cval As String = getValueCommandInCommandLine("-ss")
            If IsNumeric(cval) Then
                getSlideShowIntervaltByCommandLine = CType(cval, Integer)
            Else
                getSlideShowIntervaltByCommandLine = ImageViewerSettings.SlideShowInterval
            End If
        Catch ex As Exception
            Console.WriteLine("getSlideShowIntervaltByCommandLine")
            getSlideShowIntervaltByCommandLine = -1
        End Try
    End Function

End Class
