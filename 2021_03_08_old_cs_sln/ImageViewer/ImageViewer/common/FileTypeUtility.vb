Public Class FileTypeUtility


    'argFilePathの拡張子(後ろ4文字)がargReadFileTypeList(拡張子群)とあっているか
    Public Function FileMatchFileTypeList(
            ByRef argReadFileTypeList As List(Of String), argFilePath As String) As Boolean
        Try
            'FileMatchFileTypeList = False
            For i = 0 To argReadFileTypeList.Count - 1
                If CharactorOfRightFourOfStringAIsMatchStringB(
                    argFilePath, argReadFileTypeList(i)) Then
                    Return True
                    Exit For
                End If
            Next
            Return False
        Catch ex As Exception
            Console.WriteLine("FileMatchFileTypeList")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function
    '拡張子を判定　合致すればTRUE
    Public Function CharactorOfRightFourOfStringAIsMatchStringB(StringA As String, StringB As String) As Boolean
        Try
            If Right(UCase(StringA), 4).Equals(UCase(StringB)) Then
                CharactorOfRightFourOfStringAIsMatchStringB = True
            Else
                CharactorOfRightFourOfStringAIsMatchStringB = False
            End If
        Catch ex As Exception
            Console.WriteLine("CharactorOfRightFourOfStringAIsMatchStringB")
            Console.WriteLine(ex.Message)
            CharactorOfRightFourOfStringAIsMatchStringB = False
        End Try
    End Function
End Class
