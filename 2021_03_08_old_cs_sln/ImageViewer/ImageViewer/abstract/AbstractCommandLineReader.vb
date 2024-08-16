Imports ImageViewer

Public Class AbstractCommandLineReader
    Implements ICommandLineReader


    Private CommandLineList As List(Of String) = New List(Of String)
    Private CommandLines() As String

    Public Sub setCommandLine(cmd() As String) Implements ICommandLineReader.setCommandLine
        CommandLineList = New List(Of String)(cmd)
        CommandLines = cmd
    End Sub

    Public Sub setSettingObject(SettingObject As Object) Implements ICommandLineReader.setSettingObject
        Throw New NotImplementedException()
    End Sub

    Public Function getSettingsObject() As Object Implements ICommandLineReader.getSettingsObject
        Throw New NotImplementedException()
    End Function

    Public Sub ApplyToSettingsObjectFromCommandLine() Implements ICommandLineReader.ApplyToSettingsObjectFromCommandLine
        Throw New NotImplementedException()
    End Sub

    Public Function getListCount() As Integer
        Return CommandLineList.Count
    End Function

    Public Function getValueFromList(getIndex As Integer) As String
        Try
            Return CommandLineList(getIndex)
        Catch ex As Exception
            Console.WriteLine("getValueFromList")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function
    '=======================================================================
    'コマンドライン存在チェック
    Public Function isExistsCommandInCommandLine(argCommand As String) As Boolean Implements ICommandLineReader.isExistsCommandInCommandLine
        Try

            '要素の最後に-rがある場合は、実行されない旨を最初に警告
            Dim cmd As String = ""
            Dim i As Integer
            isExistsCommandInCommandLine = False
            For i = 1 To CommandLines.Length - 1
                If (CommandLines(i).Equals(argCommand)) Then
                    isExistsCommandInCommandLine = True
                    Return isExistsCommandInCommandLine
                    Exit For
                End If
            Next
        Catch ex As Exception
            Console.WriteLine("isExistsCommandInCommandLine")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function
    '=======================================================================
    'コマンドライン値取得
    '配列の次の値を得ている
    Public Function getValueCommandInCommandLine(argCommand As String) As String Implements ICommandLineReader.getValueCommandInCommandLine
        Try
            Dim i As Integer
            getValueCommandInCommandLine = ""
            For i = 1 To CommandLines.Length - 1
                If (CommandLines(i).Equals(argCommand)) Then
                    getValueCommandInCommandLine = CommandLines(i + 1)
                    Exit For
                End If
            Next
        Catch ex As IndexOutOfRangeException
            Console.WriteLine("getValueCommandInCommandLine Index Error")
            getValueCommandInCommandLine = ""
        Catch ex As Exception
            Console.WriteLine("getValueCommandInCommandLine")
            getValueCommandInCommandLine = ""
        End Try
    End Function
End Class
