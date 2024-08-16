
Imports System.Text
Imports System.IO

Public Class CommonSystemIOForReadAndWrite
    Private mErrFlag As Integer = 0
    Private mErrMessage As String = ""
    Private mErrFunction As String = ""

    Public Function getErrInfo() As String
        If mErrFlag = 1 And mErrMessage = "" And mErrFunction = "" Then
            Return ""
        Else
            Return mErrFlag & " : " & mErrFunction & " : " & mErrMessage
        End If
    End Function

    Private Sub setFlagInfoSuccess()
        mErrFlag = 1
        mErrMessage = ""
        mErrFunction = ""
    End Sub

    'ファイルを書き込む
    Public Sub writeFile(path As String, value As String, Optional encoding As Encoding = Nothing)
        Try
            Dim writer As StreamWriter
            If encoding Is Nothing Then
                encoding = Encoding.GetEncoding("Shift_JIS")
            End If
            writer = New StreamWriter(path, True, encoding)
            writer.WriteLine(value)
            writer.Close()
            setFlagInfoSuccess()
            If Not writer Is Nothing Then
                writer.Close()
                writer = Nothing
            End If
        Catch ex As Exception
            mErrFlag = -1
            mErrMessage = ex.Message
            mErrFunction = "writeFile"
            Console.WriteLine(mErrFunction)
            Console.WriteLine(mErrMessage)
        Finally
        End Try
    End Sub

    'ファイルを書き込む上書き
    Public Sub WriteAllText(path As String, value As String, Optional encoding As Encoding = Nothing)
        Try
            If encoding Is Nothing Then
                encoding = Encoding.GetEncoding("Shift_JIS")
            End If

            File.WriteAllText(path, value, encoding)

            'Dim writer As StreamWriter = New StreamWriter(path, True, encoding)
            'writer.WriteLine(value)
            'writer.Close()
            setFlagInfoSuccess()
        Catch ex As Exception
            mErrFlag = -1
            mErrMessage = ex.Message
            mErrFunction = "writeFile"
            Console.WriteLine(mErrFunction)
            Console.WriteLine(mErrMessage)
        End Try
    End Sub


    Public Function writeFile_(path As String, value As String, Optional encoding As Encoding = Nothing) As Integer
        writeFile(path, value, encoding)
        Return mErrFlag
    End Function

    Public Function ReadFile(path As String, Optional encoding As Encoding = Nothing) As String
        Try
            If encoding Is Nothing Then
                encoding = Encoding.GetEncoding("Shift_JIS")
            End If
            Dim sr As New StreamReader(path, encoding)
            Dim text As String = sr.ReadToEnd()
            sr.Close()
            Return text

            sr = Nothing
        Catch ex As Exception
            Console.WriteLine("ReadFile")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function

    Public Function readFileToList(path As String, Optional encoding As Encoding = Nothing) As List(Of String)
        Dim al As New List(Of String)
        Try
            Dim line As String = ""

            If encoding Is Nothing Then
                encoding = Encoding.GetEncoding("Shift_JIS")
            End If

            Using sr As StreamReader = New StreamReader(path, encoding)

                line = sr.ReadLine()
                Do Until line Is Nothing
                    al.Add(line)
                    line = sr.ReadLine()
                Loop

            End Using

            'For i As Integer = 0 To al.Count - 1
            '    Console.WriteLine(al.Item(i))
            'Next i
            Return al
        Catch ex As Exception
            Console.WriteLine("readFileToList")
            Console.WriteLine(ex.Message)
            Return al
        End Try
    End Function
End Class
