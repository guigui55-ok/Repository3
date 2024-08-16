

Public Class IniManager
    'CommonFileクラスを使う
    Private mFileName As String
    Private mFilePath As String
    Private mSectionList As List(Of String) = New List(Of String)
    Private mPropertyList As List(Of String) = New List(Of String)
    Private mPropertyListHashtable As New Hashtable
    Private mReadDataList As List(Of String) = New List(Of String)

    Private mDefaultPropertyListHashtable As New Hashtable
    Private mDefaultSectionList As New List(Of String)

    Private mPropertyListHashtableForWrite As New Hashtable
    Private mSectionListForWrite As New List(Of String)
    '二次元配列

    Public Sub New()
        ''セクション名
        'mSectionList.Add("[section]")
        ''プロパティ値
        'mPropertyList.Add("Property1=")
        'mPropertyListHashtable.Add(mSectionList(0), mPropertyList)
    End Sub

    Public Function getFilePath() As String
        Return mFilePath
    End Function

    Public Sub setFilePath(path As String)
        mFilePath = path
    End Sub

    Public Sub setSectionList(list As List(Of String))
        mSectionList = list
    End Sub
    Public Sub setPropertyListAsHashtable(hs As Hashtable)
        mPropertyListHashtable = hs
    End Sub

    Public Sub setDefaultSectionList(list As List(Of String))
        mDefaultSectionList = list
    End Sub

    Public Sub setDefaultPropertyListAsHashtable(hs As Hashtable)
        mDefaultPropertyListHashtable = hs
    End Sub

    Public Function isExistsPath() As Boolean
        Try
            If New CommonFile().isExistsPath(mFilePath) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("IniManager.existsPath")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function isExistsDefalutValue() As Boolean
        Try
            Dim flag As Boolean = False
            If mDefaultSectionList.Count > 0 Then
                If mDefaultPropertyListHashtable.Count > 0 Then
                    flag = True
                End If
            End If
            Return flag
        Catch ex As Exception
            Console.WriteLine("IniManager.isExistsDefalutValue")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    'デフォルトと比較
    'デフォルトにあって、mメイン変数にない場合は追加する
    Public Sub InitializeForDefaultValue()
        Try
            Dim pList As List(Of String)

            For Each sName In mDefaultSectionList
                'Sectionなければ作る、あればそのまま
                Me.addSectionIfNothing(mSectionList, mPropertyListHashtable, sName)
                'Property
                If PropertyHashtableHasList(mDefaultPropertyListHashtable, sName) Then
                    pList = mDefaultPropertyListHashtable(sName)
                    If pList.Count > 0 Then
                        For Each pVal In mDefaultPropertyListHashtable(sName)
                            If Not IsExistsPropertyName(mPropertyListHashtable, sName, pVal) Then
                                '単に追加
                                mPropertyListHashtable(sName).add(pVal)
                            End If
                        Next
                    Else
                        'pList mDefaultPropertyListHashtable(sName)がCountゼロの場合
                    End If
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub


    '値を挿入
    Public Sub insertSettigsValue(SectionName As String, PropertyName As String)
        Try
            If Not IsExistsSectionName(SectionName) Then
                'ないときは追加
                mSectionList.Add(SectionName)
            End If
            If Not IsExistsPropertyName(SectionName, PropertyName) Then
                'ないときは追加
                addPropertyInTargetSectionToPrivate(SectionName, PropertyName)
            End If

        Catch ex As Exception
            Console.WriteLine("IniManager.insertSettigsValue")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    'Property値を得る
    Public Function getSettingsValue(SectionName As String, PropertyName As String) As String
        Try
            If Not IsExistsSectionName(SectionName) Then
                Return ""
            End If
            If Not IsExistsPropertyName(SectionName, PropertyName) Then
                Return ""
            End If

            For Each buf In mPropertyListHashtable(SectionName)
                If getPropertyName(buf) = getPropertyName(PropertyName) Then
                    Return getPropertyValue(buf)
                End If
            Next
            Return ""
        Catch ex As Exception
            Console.WriteLine("IniManager.existsPath")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function
    '============================================================
    Public Sub WriteSettingsToFile()
        Try
            '書き込みデータまとめ
            Dim writeData As String = getSettingsValuetAsString(mSectionList, mPropertyListHashtable)

            '書き込み先チェック、あってもなくても書き込み
            '処理はなし

            '書き込み
            'ファイルがない時なのでまとめて書き込み
            Dim tWriter As CommonSystemIOForReadAndWrite = New CommonSystemIOForReadAndWrite()
            tWriter.WriteAllText(mFilePath, writeData)
            tWriter = Nothing
        Catch ex As Exception
            Console.WriteLine("IniManager.WriteSettingsToFile")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'ない時に作る
    '戻り値０予期せぬエラー、-１システムエラー、-２SectionListNothing、
    '+2プロパティリストがない
    Public Function MakeSettingsFileIfFileNotExists(path As String) As Integer
        addLog(3, "MakeSettingsFileIfFileNotExists:", path)
        Try
            If mSectionList.Count <= 0 Then
                Console.WriteLine("IniManager.MakeSettingsFileIfFileNotExists  :  SectionList is nothing")
                Return -2
            End If

            'ハッシュテーブルのインデックスはmSectionListと一致しているはず
            For i = 0 To mSectionList.Count - 1
                'If TypeOf mPropertyListHashtable(i) Is List(Of String) Then
                '    If PropertyListIsNothing(mPropertyListHashtable(i)) Then
                '        'ハッシュテーブルのリストがカウントゼロ
                '        Console.WriteLine("IniManager.MakeSettingsFileIfFileNotExists  :  mPropertyListHashtable_List is CountZero")
                '    End If
                'Else
                '    Console.WriteLine("IniManager.MakeSettingsFileIfFileNotExists  :  mPropertyListHashtable_List is Different Type : section : " & mPropertyListHashtable(mSectionList(i)))
                '    Return -3
                'End If
                'If PropertyListIsNothing(mPropertyListHashtable) Then
            Next

            'Section
            'Property
            '書き込みデータまとめ
            Dim writeData As String = getDefaultSettingsValues()
            addLog(3, "MakeSettingsFileIfFileNotExists: char.Count=", Len(writeData))

            'ファイルがない時なのでまとめて書き込み
            Dim tmp As CommonSystemIOForReadAndWrite = New CommonSystemIOForReadAndWrite()
            tmp.WriteFile(path, writeData)
            tmp = Nothing
            Return 1
        Catch ex As Exception
            Console.WriteLine("IniManager.existsPath")
            Console.WriteLine(ex.Message)
            Return -1
        End Try
    End Function

    '書き込み用、Settingsの値mSectionListとmPropertyListHashtableをStringでGet
    Public Function getDefaultSettingsValues() As String
        Dim buf As String = ""
        Try
            For i = 0 To mSectionList.Count - 1
                buf = mSectionList(i) & vbNewLine
                'PropertyListのHashTable
                For Each listValue In mPropertyListHashtable(mSectionList(i))
                    'propertyList
                    buf = buf & listValue & vbNewLine
                Next
            Next
            Return buf
        Catch ex As Exception
            Console.WriteLine("IniManager.getDefaultSettingsValues")
            Console.WriteLine(ex.Message)
            Return buf
        End Try
    End Function

    Public Function getSettingsValueNowAsString() As String
        Return getSettingsValuetAsString(mSectionList, mPropertyListHashtable)
    End Function
    Public Function getSettingsValueDefaultAsString() As String
        Return getSettingsValuetAsString(mDefaultSectionList, mDefaultPropertyListHashtable)
    End Function

    '書き込み用、Settingsの値　SectionListと　PropertyListHashtableをStringでGet
    Public Function getSettingsValuetAsString(SectionList As List(Of String), PropertyListAsHashtable As Hashtable) As String
        Dim buf As String = ""
        Dim nowSectionName As String = ""
        Try
            For i = 0 To SectionList.Count - 1
                buf = buf & SectionList(i) & vbNewLine
                'nowSectionName = "[" & SectionList(i) & "]"
                nowSectionName = SectionList(i)
                'PropertyListのHashTable
                For Each listValue In PropertyListAsHashtable(nowSectionName)
                    'propertyList
                    buf = buf & listValue & vbNewLine
                Next
            Next i
            Return buf
        Catch ex As Exception
            addLogForSystemError("IniManager.getSettingsValuetAsString")
            addLogForSystemError(ex.Message)
            Return buf
        End Try
    End Function

    'Section,Propertyがあるときは何もしない
    Public Sub addSectionIfNothing(ByRef SectionList As List(Of String), ByRef PropertyHashTable As Hashtable, SectionName As String)
        Try
            If Not IsExistsSection(SectionList, SectionName) Then
                SectionList.Add(SectionName)
                PropertyHashTable.Add(SectionName, New List(Of String))
            End If

            If Not isExistsPropertyHashtable(PropertyHashTable, SectionName) Then
                PropertyHashTable.Add(SectionName, New List(Of String))
            End If
        Catch ex As Exception
            addLogForSystemError("IniManager.addSection")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub updateListInPropertyHashtableIfMakeValueWhenNothing(ByRef SectionList As List(Of String), ByRef PropertyHashTable As Hashtable, SectionName As String, PropertyValue As String)
        addSectionIfNothing(SectionList, PropertyHashTable, SectionName)
        updateListInPropertyHashtableIfMakeValueWhenNothingMain(SectionList, PropertyHashTable, SectionName, PropertyValue)
    End Sub

    Private Sub updateListInPropertyHashtableIfMakeValueWhenNothingMain(ByRef SectionList As List(Of String), ByRef PropertyHashTable As Hashtable, SectionName As String, PropertyValue As String)
        Try
            Dim flag As Boolean = False
            Dim pList As List(Of String) = PropertyHashTable(SectionName)
            For i = 0 To pList.Count
                If getPropertyName(pList(i)) = getPropertyName(PropertyValue) Then
                    pList(i) = PropertyValue
                    flag = True
                End If
            Next

            If Not flag Then
                pList.Add(PropertyValue)
            End If

            PropertyHashTable(SectionName) = pList
        Catch ex As Exception
            addLogForSystemError("IniManager.updateListInPropertyHashtable")
            addLogForSystemError(ex.Message)
        End Try
    End Sub


    Public Function getPropertyListIfMakeListWhenNothing(ByRef SectionList As List(Of String), ByRef PropertyHashTable As Hashtable, SectionName As String) As List(Of String)
        Try
            If PropertyHashtableHasList(PropertyHashTable, SectionName) Then
                Return PropertyHashTable(SectionName)
            Else
                addSectionIfNothing(SectionList, PropertyHashTable, SectionName)
                Return PropertyHashTable(SectionName)
            End If
        Catch ex As Exception
            addLogForSystemError("IniManager.updateListInPropertyHashtable")
            addLogForSystemError(ex.Message)
            Return Nothing
        End Try
    End Function
    'defaultと比較してなければ作る

    'ファイルを読み込み
    'セクションを書き込み
    'セクションを見つける

    'プロパティを更新
    'プロパティを挿入
    '値を更新SectionName,PropertyName両方ないときは作る
    Public Sub UpdateSettingsValueSingleIfAddValueIsNothing(sectionValue As String, propertyValue As String)
        Try
            If Not IsExistsSectionName(sectionValue) Then
                'Sectionなければ追加
                'mSectionList.Add(sectionValue)
                addSectionIfNothing(mSectionList, mPropertyListHashtable, sectionValue)
            End If
            If Not IsExistsPropertyName(sectionValue, propertyValue) Then
                'Propertyなければ追加
                mPropertyListHashtable(sectionValue).add(propertyValue)
            Else
                'Propertyあれば
                '値を更新
                UpdateSttingsValueSingle(sectionValue, propertyValue)
            End If
        Catch ex As Exception
            Console.WriteLine("IniManager.UpdateSettingsValueSingleIfAddValueIsNothing")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub addPropertyValueIfValueIsNothing(sectionList As List(Of String), PropertyHashtable As Hashtable, section As String, propertValue As String)
        Try
            If Not isExistsPropertyHashtable(mPropertyListHashtable, section) Then
                'make
                addSectionIfNothing(mSectionList, mPropertyListHashtable, section)
            End If
            If Not PropertyHashtableHasList(mPropertyListHashtable, section) Then
                '上で作っている
            End If

            If Not IsExistsPropertyName(PropertyHashtable, section, propertValue) Then
                mPropertyListHashtable(section).add(propertValue)
            End If

        Catch ex As Exception
            Console.WriteLine("IniManager.addPropertyValue")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub addPropertyInTargetSectionToPrivate(sectionName As String, PropertyValue As String)
        Try
            Me.addPropertyValueIfValueIsNothing(mSectionList, mPropertyListHashtable, sectionName, PropertyValue)
        Catch ex As Exception
            Console.WriteLine("IniManager.addPropertyInTargetSectionToPrivate")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Function getListInPropertyHashtable(PropertyHashtable As Hashtable, SectionName As String) As List(Of String)
        Try
            Return PropertyHashtable(SectionName)
        Catch ex As Exception
            Console.WriteLine("IniManager.getListInPropertyHashtable")
            Console.WriteLine(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function PropertyHashtableHasList(PropertyHashtable As Hashtable, SectionName As String) As Boolean
        Try
            If PropertyHashtable(SectionName) Is Nothing Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Console.WriteLine("IniManager.PropertyHashtablehasList")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    'Propertyを追加 mPropertyListHashtableからList取得済み
    Private Sub addPropertyInTargetSectionToPrivate(ByRef propertyList As List(Of String), propertyName As String)
        Try
            propertyList.Add(propertyName)
        Catch ex As Exception
            Console.WriteLine("IniManager.addPropertyInTargetSectionToPrivate")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    'mSectionに追加
    Private Sub addSectionToPrivate(addSectionName As String)
        Try
            mSectionList.Add(addSectionName)
        Catch ex As Exception
            Console.WriteLine("IniManager.addSectionToPrivate")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Function isExistsPropertyHashtable(PropertyHashtable As Hashtable, Section As String) As Boolean
        Try
            If PropertyHashtable(Section) Is Nothing Then
                Return False
            End If
            For Each buf In PropertyHashtable(Section)

            Next
            Return True
        Catch ex As Exception
            Console.WriteLine("IniManager.addSectionToPrivate")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    'mSectionにSectionNameが存在するか
    Public Function IsExistsSectionName(sectionName As String) As Boolean
        Try
            For Each buf In mSectionList
                If buf = sectionName Then
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            Console.WriteLine("IniManager.IsExistsSectionName")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function IsExistsSection(sectionList As List(Of String), section As String) As Boolean
        Try
            For Each buf In sectionList
                If buf = section Then
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            Console.WriteLine("IniManager.IsExistsSection")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    'mSection=SectionName,mPropertyListHashtableにPropertyNameに存在するか
    Public Function IsExistsPropertyName(sectionName As String, propertyName As String) As Boolean
        Try
            If IsExistsSectionName(sectionName) Then
                For Each buf In mPropertyListHashtable(sectionName)
                    If getPropertyName(buf) = getPropertyName(propertyName) Then
                        Return True
                    End If
                Next
            Else
                Return False
            End If
            Return False
        Catch ex As Exception
            Console.WriteLine("IniManager.IsExistsPropertyName")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    'mSection=SectionName,mPropertyListHashtableにPropertyNameに存在するか
    Public Function IsExistsPropertyName(PropertyHashtable As Hashtable, sectionName As String, propertyName As String) As Boolean
        Try
            If IsExistsSectionName(sectionName) Then
                For Each buf In PropertyHashtable(sectionName)
                    If getPropertyName(buf) = getPropertyName(propertyName) Then
                        Return True
                    End If
                Next
            Else
                Return False
            End If
            Return False
        Catch ex As Exception
            Console.WriteLine("IniManager.IsExistsPropertyName")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    'プロパティを一つだけ更新　ないときは何もしない
    Public Sub UpdateSttingsValueSingle(sectionValue As String, propertyValue As String)
        Try
            Dim bufPropertyList As List(Of String) = New List(Of String)

            '書き込む値を得る->NowSection_NowProperty
            'section loop
            For Each bufSection In mSectionList
                If sectionValue = bufSection Then
                    '同じSectionならリスト内をチェック
                    'Property Loop
                    bufPropertyList = mPropertyListHashtable(bufSection)
                    For i = 0 To bufPropertyList.Count - 1
                        '同じものProperyNameを更新
                        If getPropertyName(bufPropertyList(i)) = getPropertyName(propertyValue) Then
                            '更新するときは大元の変数を書き換え
                            mPropertyListHashtable(bufSection)(i) = propertyValue
                        End If
                    Next
                End If
            Next

        Catch ex As Exception
            Console.WriteLine("IniManager.UpdateSttingsValueSingle")
            Console.WriteLine(ex.Message)
        End Try
    End Sub



    '保持している値を書き込む
    '同じ値があれば更新
    '値がなければ挿入
    'デフォルト値にあって今のデータにないものはデフォルト値を追加
    Public Sub UpdateSettingsFile()
        Try
            'ファイルの値をすべて読み込む
            Dim readDataList As List(Of String) = New CommonSystemIOForReadAndWrite().readFileToList(mFilePath)

            'SectionPropertyそれぞれの型へ
            Dim tmpProperty As Hashtable = getPropertyValueFromReadDataListMain(readDataList)
            Dim tmpSectionList As List(Of String) = getSectionListFromReadDataListMain(readDataList)

            'それぞれの値を更新
            'セクションName、プロパティNameを比較して同じものがあれば更新
            '既存のものになければ追記挿入
            '引数がByrefでメソッド内で更新される
            UpdateValueAll(readDataList, tmpSectionList, tmpProperty)

            'Section
            'Property
            '書き込みデータまとめ
            Dim writeData As String = getSettingsValuetAsString(tmpSectionList, tmpProperty)

            '書き込み先チェック、あってもなくても書き込み
            '処理はなし

            '書き込み
            'ファイルがない時なのでまとめて書き込み
            Dim tWriter As CommonSystemIOForReadAndWrite = New CommonSystemIOForReadAndWrite()
            tWriter.writeFile(mFilePath, writeData)
            tWriter = Nothing

        Catch ex As Exception
            Console.WriteLine("IniManager.UpdateSettingsFile")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '値を更新なければ作る
    '値を更新
    '設定ファイルがないとき、ある時、同じ処理
    Private Sub UpdateValueAll(readDataList As List(Of String), ByRef aftSection As List(Of String), ByRef aftProperty As Hashtable)
        Try
            Dim NowSection As String = "", NowProperty As String = ""
            Dim NowPropertyList As List(Of String) = New List(Of String)
            Dim Num As Integer = 0

            '書き込む値を得る->NowSection_NowProperty
            'section loop
            For Each NowSection In aftSection
                'Property Loop
                NowPropertyList = aftProperty(NowSection)
                For Each NowProperty In NowPropertyList
                    'section + propery があるか　Lineごとに
                    'DataListにPeropertyあるか
                    Num = getInsertNumber(readDataList, NowSection, NowProperty)
                    'あればListのIndex番号を返す
                    If Num > 0 Then
                        'あれば更新
                        readDataList(Num) = NowProperty
                    Else
                        'なければ挿入、追加
                        addPropertyToListInNowSection(readDataList, NowSection, NowProperty)
                    End If
                Next
            Next
        Catch ex As Exception
            Console.WriteLine("IniManager.UpdateSettingsFile")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'Propertyがないときの追加
    Private Sub addPropertyToListInNowSection(ByRef readlist As List(Of String), AddSect As String, AddProp As String)
        Try
            'Sectionがあるか事前チェック済み
            'Sectionの最後を取得
            'ある時はSection合致のIndex、ないときは０エラーなし、エラーは-1
            Dim sectionNum As Integer = getStartIndexSection(readlist, AddSect)
            If sectionNum > 0 Then
                'Add追加 SectionがありPropertyがない
                readlist.Insert(sectionNum + 1, AddProp)
            Else
                'Add追加 SectionがないPropertyがない
                readlist.Add(AddSect)
                readlist.Add(AddProp)
            End If
        Catch ex As Exception
            Console.WriteLine("IniManager.addPropertyToList")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '=============================
    'Propertyがあるか
    'ある場合は０以降、ない場合(エラーなし)はー２
    Public Function getInsertNumber(readList As List(Of String), NowSection As String, NowProperty As String) As Integer
        Try
            'Sectionがない場合
            If NowSection = "" Or NowProperty = "" Then
                Return -3
            End If

            Dim startNum As Integer = getStartIndexSection(readList, NowSection)
            For i = startNum + 1 To readList.Count - 1
                '値が合致するもの
                If readList(i).StartsWith(getPropertyName(NowProperty)) Then
                    Return i
                End If
                'セクション内のみ
                If readList(i).StartsWith("[") Then
                    Return i - 1
                    Exit For
                End If
            Next
            Return -2
        Catch ex As Exception
            Console.WriteLine("IniManager.getPropertyName")
            Console.WriteLine(ex.Message)
            Return -1
        End Try
    End Function

    '--------------------------------------------

    '値がセクションである、プロパティではない
    Public Function ValueIsSection(value As String) As Boolean
        Try
            '[で始まり、]で終わる
            '[]のみもあるので、3文字以上
            If value.StartsWith("[") And value.EndsWith("]") And value.Length >= 3 Then
                '[[]のばあいもあるので後でチェック
                '2文字目からLength-2文字目まで記号があったらNG
                If SectionNameIsNotIncludedSymbol(value) Then
                    Return False
                End If
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("IniManager.ValueIsSection")
            Console.WriteLine(ex.Message)
            Return True
        End Try
    End Function

    'SectionNameに記号が含まれない
    Private Function SectionNameIsNotIncludedSymbol(value As String) As Boolean
        Try
            Dim buf As String
            For i = 1 To value.Length - 2
                buf = value(i)
                If IsASCII(buf) Then
                    '記号
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            Console.WriteLine("IniManager.SectionNameIsNotSymbol")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    '<summary>
    '文字列が半角英数字記号かどうかを判定します
    '</summary>
    '<param name="target">対象の文字列</param>
    '<returns>文字列が半角英数字記号の場合はtrue、それ以外はfalse</returns>
    Public Function IsASCII(target As String) As Boolean
        If String.IsNullOrEmpty(target) Then
            Return False
        End If
        Return System.Text.RegularExpressions.Regex.IsMatch(target, "^[a-zA-Z0-9!-/:-@¥[-`{-~]+$")
    End Function


    '値が合致した番号を取得
    Public Function getIndexNumberToMatchValueInList(readList As List(Of String), NowSection As String, NowProperty As String) As Integer
        Try
            For i = getStartIndexSection(readList, NowSection) + 1 To readList.Count - 1
                '値が合致するもの
                If readList(i).StartsWith(getPropertyName(NowProperty)) Then
                    Return i
                End If
                'セクション内のみ
                If readList(i).StartsWith("[") Then
                    Return i - 1
                    Exit For
                End If
            Next
            Return -2
        Catch ex As Exception
            Console.WriteLine("IniManager.isMatchValueInList")
            Console.WriteLine(ex.Message)
            Return -1
        End Try
    End Function

    'チェック開始インデックスを取得[section]
    'ある時はSection合致のIndex、ないときは０エラーなし、エラーは-1
    Public Function getStartIndexSection(readList As List(Of String), NowSection As String) As Integer
        Dim cnt As Integer = 0
        Try
            For Each buf In readList
                If buf = NowSection Then
                    Return cnt
                End If
                cnt += 1
            Next
            Return 0
        Catch ex As Exception
            Console.WriteLine("IniManager.getStartIndexSection")
            Console.WriteLine(ex.Message)
            Return -1
        End Try
    End Function

    'Propertyの＝の左側を得る
    Private Function getPropertyName(NowProperty As String) As String
        Try
            If NowProperty = "" Then
                Return ""
            End If

            Dim int As Integer = NowProperty.IndexOf("=")
            If NowProperty.IndexOf("=") <= 0 Then
                Return NowProperty
            End If

            Dim rtn As String = NowProperty.Substring(0, NowProperty.IndexOf("="))
            Return rtn
        Catch ex As Exception
            Console.WriteLine("IniManager.getPropertyName")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function
    '
    'Propertyの＝の右側を得る
    Private Function getPropertyValue(NowProperty As String) As String
        Try
            Dim str As String = NowProperty.Substring(NowProperty.IndexOf("=") + 1, NowProperty.Length - (NowProperty.IndexOf("=") + 1))
            Return str
        Catch ex As Exception
            Console.WriteLine("IniManager.getPropertyValue")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function


    '--------------------------------------------


    Private Function PropertyListIsNothing(list As List(Of String)) As Boolean
        Try
            If list.Count <= 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("IniManager.PropertyListIsNothing")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    '読み込み時用
    Public Sub readSettingFileAndSaveDataToPrivate()
        readSettingsFile()
        setValueFromReadDataList()
    End Sub

    Public Sub readSettingsFile()
        Try
            mReadDataList = New CommonSystemIOForReadAndWrite().readFileToList(mFilePath)
        Catch ex As Exception
            Console.WriteLine("IniManager.readSettingsFile")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    Private Sub setValueFromDataListByReadData()
        Console.WriteLine("IniManager.setValueFromDataListByReadData")
    End Sub

    'ファイルから読み込んだDataListからSectionListを得る　戻り値List
    Private Function getSectionListFromReadDataListMain(ReadDataList As List(Of String)) As List(Of String)
        Dim newSectionList As List(Of String) = New List(Of String)
        Dim newPropertyTable As Hashtable = New Hashtable()
        Dim nowreadList As List(Of String) = New List(Of String)
        Try
            For Each buf In ReadDataList
                If isSection(buf) Then
                    '前のものを格納
                    If newSectionList.Count > 0 Then
                        'sectionのみの場合もある
                        'HashtableにList追加
                        newPropertyTable.Add(buf, Me.getListDeepCoy(nowreadList))
                        nowreadList = New List(Of String) 'リセット
                        'next
                        newSectionList.Add(buf)
                    Else
                        '初めて
                        If nowreadList.Count > 0 Then
                            'sectionなしPropertyがある
                            '空セクションを追加
                            newSectionList.Add("")
                            'HashtableにList追加
                            newPropertyTable.Add(buf, Me.getListDeepCoy(nowreadList))
                            nowreadList = New List(Of String) 'リセット
                            'next
                            newSectionList.Add(buf)
                        Else
                            'sectionなしPropertyがない
                            'Sectionのみ追加
                            newSectionList.Add(buf)
                        End If
                    End If
                Else
                    'セクションじゃなくPropety値
                    'PropertyAdd
                    addListWhenNotBlank(buf, nowreadList)
                End If
            Next
            Return newSectionList
        Catch ex As Exception
            Console.WriteLine("IniManager.getSectionListFromReadDataListMain")
            Console.WriteLine(ex.Message)
            Return newSectionList
        End Try
    End Function
    'ファイルから読み込んだDataListからPropertyListを得る　戻り値Hashtable
    Private Function getPropertyValueFromReadDataListMain(ReadDataList As List(Of String)) As Hashtable
        Dim newSectionList As List(Of String) = New List(Of String)
        Dim newPropertyTable As Hashtable = New Hashtable()
        Dim nowreadList As List(Of String) = New List(Of String)
        Try
            For Each buf In ReadDataList
                If isSection(buf) Then
                    '前のものを格納
                    If newSectionList.Count > 0 Then
                        'sectionのみの場合もある
                        'HashtableにList追加
                        newPropertyTable.Add(buf, Me.getListDeepCoy(nowreadList))
                        nowreadList = New List(Of String) 'リセット
                        'next
                        newSectionList.Add(buf)
                    Else
                        '初めて
                        If nowreadList.Count > 0 Then
                            'sectionなしPropertyがある
                            '空セクションを追加
                            newSectionList.Add("")
                            'HashtableにList追加
                            newPropertyTable.Add(buf, Me.getListDeepCoy(nowreadList))
                            nowreadList = New List(Of String) 'リセット
                            'next
                            newSectionList.Add(buf)
                        Else
                            'sectionなしPropertyがない
                            'Sectionのみ追加
                            newSectionList.Add(buf)
                        End If
                    End If
                Else
                    'セクションじゃなくPropety値
                    'PropertyAdd
                    addListWhenNotBlank(buf, nowreadList)
                End If
            Next
            Return newPropertyTable.Clone
        Catch ex As Exception
            Console.WriteLine("IniManager.getPropertyValueFromReadDataListMain")
            Console.WriteLine(ex.Message)
            Return newPropertyTable.Clone
        End Try
    End Function

    'ファイルを読み込みmReadDataListに格納後
    'ReadDataListからPrivateに格納
    Private Sub setValueFromReadDataList()
        Dim newSectionList As List(Of String) = New List(Of String)
        Dim newPropertyTable As Hashtable = New Hashtable()
        Dim nowreadList As List(Of String) = New List(Of String)
        Dim nowSection As String = ""
        Try
            For Each buf In mReadDataList
                If isSection(buf) Then
                    '前のものを格納
                    If newSectionList.Count > 0 Then
                        'sectionのみの場合もある
                        'HashtableにList追加
                        newPropertyTable.Add(buf, Me.getListDeepCoy(nowreadList))
                        nowreadList = New List(Of String) 'リセット
                        'next
                        newSectionList.Add(buf)
                    Else
                        '初めて
                        If nowreadList.Count > 0 Then
                            'sectionなしPropertyがある
                            '空セクションを追加
                            newSectionList.Add("")
                            'HashtableにList追加
                            newPropertyTable.Add(buf, Me.getListDeepCoy(nowreadList))
                            nowreadList = New List(Of String) 'リセット
                            'next
                            newSectionList.Add(buf)
                            nowSection = buf
                        Else
                            'sectionなしPropertyがない
                            'Sectionのみ追加
                            newSectionList.Add(buf)
                            nowSection = buf
                        End If
                    End If
                Else
                    'セクションじゃなくPropety値
                    'PropertyAdd
                    addListWhenNotBlank(buf, nowreadList)
                End If
            Next
            newPropertyTable.Add(nowSection, nowreadList)

            mSectionList = getListDeepCoy(mSectionList)
            mPropertyListHashtable = newPropertyTable.Clone

        Catch ex As Exception
            Console.WriteLine("IniManager.readSettingsFile")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub addListWhenNotBlank(value As String, ByRef list As List(Of String))
        Try
            If Not value = "" Then
                list.Add(value)
            End If
        Catch ex As Exception
            Console.WriteLine("IniManager.addListWhenNotBlank")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '読み込み時用　
    Private Function isSection(val As String) As Boolean
        Try
            If val.StartsWith("[") And val.EndsWith("]") Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("IniManager.readSettingsFile")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Private Function getListDeepCoy(bufList As List(Of String)) As List(Of String)
        Dim listAfter As List(Of String) = New List(Of String)
        Try
            If bufList.Count < 1 Then
                Return New List(Of String)
            End If

            For Each buf In bufList
                listAfter.Add(buf)
            Next
            Return listAfter
        Catch ex As Exception
            Console.WriteLine("IniManager.readSettingsFile")
            Console.WriteLine(ex.Message)
            Return listAfter
        End Try
    End Function

End Class
