
Public Class ViewFileList

    'リスト委譲ではなく継承してそのまま使えるように

    Private ViewFileList As List(Of String) = New List(Of String)
    Private ViewFileListForRandom As List(Of String) = New List(Of String)
    Private ViewIndexList As List(Of Integer) = New List(Of Integer)
    Public NowIndex As Integer = 0
    Public NotAbleViewAllOfList = False 'リスト全てが全て表示不可
    Public ReadAbleFileTypeList As List(Of String) = New List(Of String)
    Private NowFolder As String = ""
    Public FlagAsViewAbleFileListIsNothing As Boolean = False

    Public Sub New()

    End Sub

    Public Sub New(argViewFileList As List(Of String), argReadAbleFileTypeList As List(Of String))
        setViewFileList(argViewFileList)
        setReadAbleFileTypeList(argReadAbleFileTypeList)
        NowIndex = 0
    End Sub

    Public Sub setViewFileList(argViewFileList As List(Of String))
        ViewFileList = argViewFileList
        Me.makeIndexList()
    End Sub

    Public Function getNowFolder() As String
        Return NowFolder
    End Function

    'ファイル移動時にフォルダをセット
    '非対応ファイル形式時の対応用などの用途
    Private Sub setNowFolder(indexNum As Integer)
        Try
            If ViewFileList.Count <= 0 Then
                Exit Sub
            End If
            '現在のパス
            NowFolder = Me.ViewFileList(Me.ViewIndexList(indexNum))
            'フォルダー部を得る
            NowFolder = getFolderPathFromFilePath(NowFolder)
        Catch ex As Exception
            Console.WriteLine("ViewFileList.setNowFolder")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'ファイルパスからフォルダ部のみを抜き出す
    Public Function getFolderPathFromFilePath(path As String) As String
        Try
            If path.LastIndexOf("\") >= 0 Then
                Return path.Substring(0, path.LastIndexOf("\"))
            Else
                Return ""
            End If
        Catch ex As Exception
            Console.WriteLine("getFolderPathFromFilePath")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function

    'ファイルパスからフォルダ部のみを抜き出す
    Public Function getFileType(path As String) As String
        Try
            If path.LastIndexOf(".") >= 0 Then
                Return path.Substring(path.LastIndexOf("."), path.Length - path.LastIndexOf("."))
            Else
                Return ""
            End If
        Catch ex As Exception
            Console.WriteLine("getFileType")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function

    '読み込み可能拡張子を設定
    Public Sub setReadAbleFileTypeList(argReadAbleFileTypeList As List(Of String))
        ReadAbleFileTypeList = argReadAbleFileTypeList
    End Sub

    '読み込み可能拡張子を追加
    Public Sub addReadAbleFileTypeList(argReadAbleFileTypeList As List(Of String))
        If argReadAbleFileTypeList.Count <= 0 Then
            Exit Sub
        End If
        If ReadAbleFileTypeList.Count <= 0 Then
            ReadAbleFileTypeList = argReadAbleFileTypeList
        Else
            ReadAbleFileTypeList.AddRange(argReadAbleFileTypeList)
        End If
    End Sub

    Public Function Count() As Long
        Try
            Return ViewFileList.Count
        Catch ex As Exception
            Console.WriteLine("ViewFileList.Count")
            Console.WriteLine(ex.Message)
            Return -1
        End Try
    End Function

    Public Function IsNotViewAll() As Boolean
        Try
            For Each FilePath In Me.ViewFileList
                If Me.IsAbleViewFile(FilePath) Then
                    '一つでも表示可能であればOK
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            Console.WriteLine("IsNotViewAll")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function IndexIsCorrect(argIndex As Integer) As Boolean
        Try
            If ((argIndex < ViewFileList.Count) And (argIndex >= 0)) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function NowFileTypeIs(argFileType As String, NowIndex As Long) As Boolean
        Try
            Dim NowFile As String = Me.getNowFilePath()
            If Right(UCase(NowFile), 4).Equals(argFileType) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("NowFileTypeIs")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function getFilePath(argIndex As Integer) As String
        Try
            If ViewFileList.Count <= 0 Then
                Console.WriteLine("ViewFileList.getNowFilePath : List Counst Is Zero")
                Return ""
            End If
            If IndexIsCorrect(argIndex) Then
                moveListIndex(argIndex, 0)
                Return ViewFileList(ViewIndexList(argIndex))
            Else
                Console.WriteLine("ViewFileList.getNowFilePath : index overflow error")
                Return ""
            End If
        Catch ex As Exception
            Console.WriteLine("getFilePath")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function

    '現在のファイルパスを取得
    Public Function getNowFilePath() As String
        Try
            If IndexIsCorrect(NowIndex) Then
                moveListIndex(NowIndex, 0)
                Return ViewFileList(ViewIndexList(NowIndex))
            Else
                Console.WriteLine("ViewFileList.getNowFilePath : index overflow error")
                Return ""
            End If
        Catch ex As Exception
            Console.WriteLine("getNowFilePath")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function

    Public Sub moveNext()
        Try
            moveNextListIndex(0)
        Catch ex As Exception
            Console.WriteLine("moveNext")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'カレントナンバーを移動 index指定
    'CountDirectionは（指定なし, 0:Up, 1:Down, 2:そのまま, それ以外：Up）
    Public Sub moveListIndex(MoveIndex As Integer, CountDirection As Integer)
        Try
            Dim CheckIndex As Integer = IsAbleViewIndex(MoveIndex)
            If IsAbleViewIndex(MoveIndex) Then
                '移動可
                NowIndex = MoveIndex
            Else
                '移動不可
                '移動不可の場合CountDirectionの値によって
                'Indexの（次/前)に移動する
                moveNextListIndex(CountDirection)
            End If
            setNowFolder(NowIndex)
        Catch ex As Exception
            Console.WriteLine("moveListIndex")
            Console.WriteLine(ex.Message)
            NowIndex = 0
            setNowFolder(NowIndex)
        End Try
    End Sub

    'カレントナンバーを移動　次へ
    Public Sub moveNextIndex()
        moveNextListIndex(0)
    End Sub

    'カレントナンバーを移動　前へ
    Public Sub movePreviousIndex()
        moveNextListIndex(1)
    End Sub

    Private Sub CheckNowIndexAndResetWhenFileTypeInvalved()

    End Sub

    'moveListIndexもある移動系メイン関数
    ''' <summary>
    ''' NowIndexを移動メイン　次へ 前へ（指定外ファイルタイプは飛ばす）
    ''' </summary>
    ''' <param name="CountDirection">CountDirectionは（指定なし, 0:Up, 1:Down, 2:そのまま, それ以外：Up）</param>
    Private Sub moveNextListIndex(CountDirection As Integer)
        Try
            Dim InvalidCount As Integer = 0
            Dim Count As Integer = 1 '無限ループ防止用
            Dim NextFileName As String
            Dim CheckIndex As Integer = 0

            'チェックインデックスをセット
            CheckIndex = getNextOrPreviousIndex(NowIndex, CountDirection) '端なら折り返す用

            Do While ((Count < ViewFileList.Count) And (ViewFileList.Count > (CheckIndex)))
                'Debug.Print(ViewIndexList(CheckIndex + InvalidCount))
                NextFileName = ViewFileList(Me.ViewIndexList(CheckIndex))
                '取り扱い可能ならばExitする,FileTypeCheck
                If IsAbleViewFile(NextFileName) Then
                    Exit Do
                Else
                    '無効な時(IsAbleViewFile=false)は無効カウントアップ
                    If CountDirection = 1 Then
                        InvalidCount -= 1
                    Else
                        InvalidCount += 1
                    End If
                    'ファイルリスト途中からチェックした場合折り返すので、その際は無効カウントをゼロに
                    If CheckIndex >= ViewFileList.Count - 1 Then
                        'Stop
                        InvalidCount = 0
                    End If
                    '非表示ファイルなので次のインデックスをセット
                    CheckIndex = getNextOrPreviousIndex(CheckIndex, CountDirection) '端なら折り返す用
                End If
                Count += 1
                If Count >= ViewFileList.Count Then
                    'Stop
                    Exit Do
                End If
            Loop

            If Count >= ViewFileList.Count Then
                FlagAsViewAbleFileListIsNothing = True
                'Console.WriteLine("ViewFileList.moveNextListIndex : Check Count Over =" & Count)
            Else
                FlagAsViewAbleFileListIsNothing = False
            End If

            If (ViewFileList.Count <= Count) Then
                NowIndex = 0
            Else
                NowIndex = CheckIndex
            End If
            If ViewFileList.Count > 0 Then
                'インデックスをセットしたらフォルダ名を格納
                Me.setNowFolder(NowIndex)
            End If
        Catch ex As Exception
            Console.WriteLine("moveNextListIndex")
            Console.WriteLine(ex.Message)
            NowIndex = 0
            Me.setNowFolder(NowIndex)
        End Try
    End Sub

    Private Function getNextOrPreviousIndex(argIndex As Integer, CountDirection As Integer) As Integer
        Try
            'CountDirectionは（指定なし, 0:Up, 1:Down, 2:そのまま, それ以外：Up）
            If (CountDirection = 1) Then
                Return getPreviousIndex(argIndex) '端なら折り返す用
            ElseIf CountDirection = 2 Then
                Return argIndex
                'Return getNextIndex(argIndex) '端なら折り返す用
            Else
                '↓
                Return getNextIndex(argIndex) '端なら折り返す用
                If argIndex >= ViewFileList.Count - 1 Then
                    Stop
                End If

            End If

                If CountDirection = 2 Then
                'そのまま
                Return argIndex
            End If
            Return argIndex
        Catch ex As Exception
            Console.WriteLine("getNextIndex")
            Console.WriteLine(ex.Message)
            Return 0
        End Try
    End Function

    '次のIndexNuberを得る
    'index番号のみの処理、他メソッドでファイルタイプによってindexを飛ばす処理有り
    '端なら折り返す用
    Private Function getNextIndex(argIndex As Integer) As Integer
        Try
            If (Not IndexOverLast(argIndex + 1)) And (argIndex >= 0) Then
                '端なら折り返す
                If argIndex = (ViewFileList.Count - 1) Then
                    Return 0
                Else
                    Return argIndex + 1
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Console.WriteLine("getNextIndex")
            Console.WriteLine(ex.Message)
            Return 0
        End Try
    End Function

    '前のIndexNuberを得る
    'index番号のみの処理、他メソッドでファイルタイプによってindexを飛ばす処理有り
    '端なら折り返す用
    Private Function getPreviousIndex(argIndex As String) As Integer
        Try
            If (Not IndexOverLast(argIndex + -1)) And (argIndex >= 0) Then
                '端なら折り返す
                If argIndex = 0 Then
                    Return ViewFileList.Count - 1
                Else
                    Return argIndex - 1
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Console.WriteLine("getPreviousIndex")
            Console.WriteLine(ex.Message)
            Return 0
        End Try
    End Function

    Private Function IndexOverLast(argIndex As Integer) As Boolean
        Try
            If ViewFileList.Count <= argIndex Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return True
        End Try
    End Function

    '=======================================================================
    '拡張子を判定　表示できるファイル数をカウント
    Public Function CountFileListIsViewAble() As Integer
        Try
            Dim count As Integer = 0
            For i = 0 To Me.ViewFileList.Count - 1
                For j = 0 To Me.ReadAbleFileTypeList.Count - 1
                    If IsAbleViewFile(Me.ViewFileList(i)) Then
                        count += 1
                    End If
                Next
            Next
            CountFileListIsViewAble = count
        Catch ex As Exception
            Console.WriteLine("CountFileListIsViewAble")
            Console.WriteLine(ex.Message)
            CountFileListIsViewAble = 0
        End Try
    End Function

    '指定したファイルパスが対応ファイルタイプか
    Public Function IsAbleViewFile(ArgFilePath As String) As Boolean
        Try
            Return New FileTypeUtility().FileMatchFileTypeList(Me.ReadAbleFileTypeList, ArgFilePath)
        Catch ex As Exception
            Return False
        End Try
    End Function

    '指定したIndex番号のファイルが対応ファイルタイプか
    Public Function IsAbleViewIndex(argIndex As String) As Boolean
        Try
            If (Not IndexOverLast(argIndex)) And (argIndex >= 0) Then
                'Dim FilePath As String = Me.ViewFileList(argIndex)
                Dim FilePath As String = Me.ViewFileList(ViewIndexList(argIndex))
                Return New FileTypeUtility().FileMatchFileTypeList(Me.ReadAbleFileTypeList, FilePath)
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        Finally
        End Try
    End Function

    Public Sub resetListOrder()
        Try
            'ViewFileList = New List(Of String)(ViewFileListForRandom) 'x
            'ViewFileList = ViewFileListForRandom.CopyTo(ViewFileList) 'x
            'ViewFileList = New ListStringClone().DoCloneList(ViewFileListForRandom)
            makeIndexList()
            'NowIndex = ViewIndexList(NowIndex)
        Catch ex As Exception
            Console.WriteLine("resetListOrder")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub makeRandomList()
        Try
            makeRandomListByFisherYates()
        Catch ex As Exception
            Console.WriteLine("makeRandomList")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub makeRandomListAndFileNameKeep()
        If ViewFileList.Count <= 0 Then
            Exit Sub
        End If
        Dim buf As String = ViewFileList(NowIndex)
        makeRandomList()
        'ファイル名を探す
        Dim moveindex As Integer = Me.getIndexToMatchEndWithValue(buf, 0)
        Me.moveListIndex(moveindex, 0)
    End Sub

    Public Sub makeIndexList()
        Try
            Dim tary(ViewFileList.Count) As Integer
            ViewIndexList = New List(Of Integer)(tary)

            For i = 0 To ViewFileList.Count - 1
                ViewIndexList(i) = i

                If ViewIndexList(i) = NowIndex Then
                    NowIndex = i
                End If
            Next
        Catch ex As Exception
            Console.WriteLine("makeIndexList")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub OutPutIndexList()
        Try
            For i = 0 To ViewIndexList.Count - 1
                Console.Write(" " & ViewIndexList(i).ToString)
            Next
        Catch ex As Exception
            Console.WriteLine("OutPutIndexList")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub makeRandomIndexList()
        Try
            Dim tary(ViewFileList.Count) As Integer
            Dim rng As New Random()
            'ViewIndexListForRandom

            tary = GetUniqRandomNumbers(
                1, ViewIndexList.Count - 1, ViewIndexList.Count - 2
            )
            ViewIndexList = New List(Of Integer)(tary)

            For i = 0 To ViewIndexList.Count - 1
                If ViewIndexList(i) = NowIndex Then
                    NowIndex = i
                End If
            Next

            Erase tary
            'OutPutIndexList()
        Catch ex As Exception
            Console.WriteLine("makeRandomList")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'Public Sub makeRandomIndexListxx()
    '    Try
    '        Dim tary(ViewFileList.Count) As Integer
    '        ViewIndexList = New List(Of Integer)(tary)
    '        Dim rng As New Random()
    '        'ViewIndexListForRandom
    '        Dim n As Integer = ViewFileList.Count
    '        Dim count As Integer
    '        While （n > 1)
    '            n -= 1
    '            Dim k As Integer = rng.Next(n + 1)
    '            Dim tmp As Integer = ViewIndexList(n)
    '            ViewIndexList(n) = k
    '            ViewIndexList(k) = tmp

    '            If NowIndex = n Then
    '                NowIndex = k
    '            End If
    '            count += 1
    '        End While
    '        Stop
    '        OutPutIndexList()

    '        rng = Nothing
    '    Catch ex As Exception
    '        Console.WriteLine("makeRandomList")
    '        Console.WriteLine(ex.Message)
    '    End Try
    'End Sub

    'xxx
    '「Coding Horror Shuffling」によると、上記の方法には2つの問題があるということです。
    '1つは、Randomクラスが生成する配列が予測可能であり、安全ではないという点で、もう1つは、複雑であるという点です。
    'これらの点を解消した方法として、OrderByメソッドで並び替え、Randomの代わりにGuidを使う方法が紹介されています。
    Public Sub makeRandomListByFisherYates()
        Try
            Dim tary(ViewFileList.Count) As Integer
            ViewIndexList = New List(Of Integer)(tary)
            Dim TempList As List(Of String) = ViewFileList
            Dim rng As New Random()
            Dim n As Integer = TempList.Count
            While （n > 1)
                n -= 1
                Dim k As Integer = rng.Next(n + 1)
                Dim tempdata As String = TempList(k)
                TempList(k) = TempList(n)
                TempList(n) = tempdata
                ViewIndexList(n) = k
                ViewIndexList(k) = n

                If NowIndex = n Then
                    NowIndex = k
                End If
            End While
            ViewFileListForRandom = New List(Of String)(ViewFileList)
            ViewFileList = TempList

            Console.WriteLine(ViewFileListForRandom)
        Catch ex As Exception
            Console.WriteLine("makeRandomListByFisherYates")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    Private Sub Swap(ByRef m As Integer, ByRef n As Integer)
        Dim work As Integer = m
        m = n
        n = work
    End Sub

    Private Function GetUniqRandomNumbers(rangeBegin As Integer, rangeEnd As Integer, count As Integer) As Integer()
        ' 指定された範囲の整数を格納できる配列を用意する
        Dim work(rangeEnd - rangeBegin) As Integer
        Try
            ' 配列を初期化する
            Dim i As Integer = 0
            For n As Integer = rangeBegin To rangeEnd
                work(i) = n
                i += 1
            Next

            ' ランダムに取り出しては先頭から順に置いていく（count回繰り返す）
            Dim rnd = New Random()
            For resultPos As Integer = 0 To count - 1
                ' （resultPosを含めて）resultPosの後ろからランダムに1つ選ぶ
                Dim nextResultPos As Integer = rnd.Next(resultPos, work.Length)

                ' nextResultPosの値をresultPosと入れ替える
                Swap(work(resultPos), work(nextResultPos))
            Next

            Return work ' workの先頭からcount個を返す
        Catch ex As Exception
            Console.WriteLine("GetUniqRandomNumbers")
            Console.WriteLine(ex.Message)
            Return work
        End Try

    End Function

    'リストの中からEndWith後方一致したもののインデックスに移動する
    'CountDirectionは（指定なし, 0:Up, 1:Down, 2:そのまま, それ以外：Up）
    Public Sub moveIndexToMatchEndWithValue(value As String, direction As Integer, Optional beginNum As Integer = 0)
        Try
            Dim index As Integer = Me.getIndexToMatchEndWithValue(value, beginNum)
            Me.moveListIndex(index, direction)
        Catch ex As Exception
            Console.WriteLine("moveIndexToMatchEndWithValue")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'リストの中からEndWith後方一致したもののインデックスを返す
    Public Function getIndexToMatchEndWithValue(value As String, Optional beginNum As Integer = 0) As Integer
        Try
            Dim buf As String = ""
            If beginNum > ViewFileList.Count Then
                Return 0
            End If
            For i = beginNum To ViewFileList.Count
                buf = ViewFileList(i)
                If buf.EndsWith(value) Then
                    Return i
                End If
            Next
            Return 0
        Catch ex As Exception
            Console.WriteLine("getIndexToMatchEndWithValue")
            Console.WriteLine(ex.Message)
            Return 0
        End Try
    End Function

End Class
