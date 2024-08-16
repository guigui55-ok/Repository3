Public Class CommonList
    Sub New()

    End Sub

    Public Function convertListToString(list As List(Of String), Optional sepaletor As String = vbNewLine) As String
        Try
            If list.Count <= 0 Then
                Return ""
            End If

            Dim result As String = ""
            Dim count As Long = 0
            For Each buf In list
                result = result & buf
                count = +count
                If count < list.Count Then
                    '最後だけSepaletorは追加しない
                    result = result & sepaletor
                End If
            Next
            Return result
        Catch ex As Exception
            Console.WriteLine("convertListToString")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function


End Class
