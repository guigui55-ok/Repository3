Public Class MenuStripEditor
    Private WithEvents mMenuStrip As MenuStrip
    Private mForm As FormBridge
    Private mNowMenuItem As ToolStripMenuItem
    Private mMainForm As MainForm

    Public Sub New()

    End Sub

    Public Sub New(menustrip As MenuStrip, mMainForm As FormBridge)
        mMenuStrip = menustrip
        mMainForm = mMainForm
    End Sub

    Public Sub DisposeObjetcs()
        Try
            mMenuStrip = Nothing
            mNowMenuItem = Nothing
            mForm = Nothing
            mMainForm = Nothing
        Catch ex As Exception
            Console.WriteLine("DisposeObjetcs")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'メニューのTextを変更
    '戻り値エラーコード
    Public Function changeText(textAfter As String, ParamArray TargetText() As String) As Boolean
        Try
            Dim item As ToolStripMenuItem = Me.getToolStripMenuItemBySomeText(TargetText)
            If item Is Nothing Then
                Console.WriteLine("changeText Error")
                Return False
            End If
            '同じならそのままTrue
            If item.Text = textAfter Then Return True
            '違うときText変更
            item.Text = textAfter
            Return True
        Catch ex As Exception
            Console.WriteLine("changeText")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function


    'MenuStrip内のToolStripMenuItemの、valueに合致したものを得る
    Public Function getToolStripMenuItem(value As String) As ToolStripMenuItem
        Try
            'If mMenuStrip Is Nothing Then Return Nothing
            Dim item As ToolStripMenuItem
            For Each item In mMenuStrip.Items
                If item.Text = value Then
                    Return item
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Console.WriteLine("getToolStripMenuItem")
            Console.WriteLine(ex.Message)
            Return Nothing
        End Try
    End Function


    'MenuStripの子メニューから、Valueに合致したものを得る
    Public Function getToolStripMenuItemInToolStripMenuItem(MenuItem As ToolStripMenuItem, value As String) As ToolStripMenuItem
        Try
            Dim item As ToolStripMenuItem
            If Not MenuItem.HasDropDown Then
                Return Nothing
            End If
            For Each item In MenuItem.DropDownItems
                If item.Text = value Then
                    Return item
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Console.WriteLine("getToolStripMenuItemInToolStripMenuItem")
            Console.WriteLine(ex.Message)
            Return Nothing
        End Try
    End Function

    Private Function isArrayExists(ary() As String) As Boolean
        Try
            For i = 0 To UBound(ary)

            Next
            Return True
        Catch ex As IndexOutOfRangeException
            Return False
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "isArrayExists")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    '複数のTextから複数の階層の（奥のほうにある）MenuItemを得る
    Public Function getToolStripMenuItemBySomeText(ParamArray TargetText() As String) As ToolStripMenuItem
        Try
            'TargetText存在チェック
            If Not isArrayExists(TargetText) Then
                Console.WriteLine("addItemToMenuItem TargetText not exists")
                Return Nothing
            End If

            Dim itemAdded As ToolStripMenuItem = getToolStripMenuItem(TargetText(0))

            If TargetText.Length <= 1 Then
                '1階層目だけで終わり
                Return itemAdded
            End If

            '2階層目以降
            For i = 1 To UBound(TargetText)
                itemAdded = getToolStripMenuItemInToolStripMenuItem(itemAdded, TargetText(i))

                If i = UBound(TargetText) Then
                    '終わりなら抜ける
                    Return itemAdded
                End If
            Next
            'ここに来る場合は
            'MenuStrip1階層目は合致、2階層目以降で合致したものがない
            Console.WriteLine("Worning : " & Me.ToString & "getToolStripMenuItemBySomeText")
            Return itemAdded
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "getToolStripMenuItemBySomeText")
            Console.WriteLine(ex.Message)
            Return Nothing
        End Try
    End Function

    Private Function isUsedTypeToObject(argObject As Object) As Boolean
        Try
            If VarType(argObject) = VarType(New MenuStrip()) Then
                Return True
            End If
            If VarType(argObject) = VarType(New ContextMenuStrip()) Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Console.WriteLine("getToolStripMenuItem")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function


    'TargetTextは階層一番上から指定 Keyも登録Ver
    Public Sub addItemToMenuItem(addText As String, addKeys As Keys, ParamArray TargetText() As String)
        addItemToMenuItem(addText, TargetText)
        setShortCutKeys(mNowMenuItem, addKeys)
    End Sub

    'TargetTextは階層一番上から指定
    Public Sub addItemToMenuItem(addText As String, ParamArray TargetText() As String)
        Try
            '目的のMenuItemを得る
            Dim ItemAdded As ToolStripMenuItem = getToolStripMenuItemBySomeText(TargetText)
            '追加
            addItemToMenuItem(ItemAdded, addText)
        Catch ex As Exception
            Console.WriteLine("addItemToMenuItem")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '============================================================
    Public Sub addToolStripSeparator(ParamArray TargetText() As String)
        Try
            Dim ItemAdded As ToolStripMenuItem = getToolStripMenuItemBySomeText(TargetText)
            ItemAdded.DropDownItems.Add(New ToolStripSeparator())

        Catch ex As Exception
            addLog("addToolStripSeparator")
            addLog(ex.Message)
        End Try
    End Sub

    'Textだけ登録Ver
    Public Sub addItemToMenuStrip(addText As String)
        addItemToMenuStrip(mMenuStrip, addText)
    End Sub

    'TextとKey登録Ver
    Public Sub addItemToMenuStrip(addText As String, AddKeys As Keys)
        addItemToMenuStrip(mMenuStrip, addText, AddKeys)
    End Sub


    'Keyを登録
    Public Sub setShortCutKeys(MenuItemAdded As ToolStripMenuItem, AddKeys As Keys)
        Try
            mNowMenuItem.ShortcutKeys = AddKeys
            mNowMenuItem.ShowShortcutKeys = True
        Catch ex As Exception
            addLog("setShortCutKeys")
            addLog(ex.Message)
        End Try
    End Sub

    'Keyも登録Ver
    Public Sub addItemToMenuStrip(MenuStripAdded As MenuStrip, addText As String, addKeys As Keys)
        Try
            'Menuに追加、追加したItemをnowMenuItemに格納
            addItemToMenuStrip(MenuStripAdded, addText)
            setShortCutKeys(mNowMenuItem, addKeys)
        Catch ex As Exception
            Console.WriteLine("addItemToMenuStrip")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    'Keyも登録Ver
    Public Sub addItemToMenuItem(ToolStripMenuItemAdded As ToolStripMenuItem, addText As String, addKeys As Keys)
        Try
            'Menuに追加、追加したItemをnowMenuItemに格納
            addItemToMenuItem(ToolStripMenuItemAdded, addText)
            setShortCutKeys(mNowMenuItem, addKeys)
        Catch ex As Exception
            Console.WriteLine("addItemToMenuItem")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'オブジェクト受け取らないVer
    Public Sub addItemToMenuStrip(MenuStripAdded As MenuStrip, addText As String)
        Try
            'メニュー項目を作成する
            Dim tempMenuItem As New ToolStripMenuItem()
            tempMenuItem.Text = addText
            MenuStripAdded.Items.Add(tempMenuItem) 'MenuStripに追加する

            mNowMenuItem = tempMenuItem　'ほかのプロパティ設定用
        Catch ex As Exception
            Console.WriteLine("addItemToMenuStrip")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'オブジェクト受け取らないVer
    Public Sub addItemToMenuItem(ToolStripMenuItemAdded As ToolStripMenuItem, addText As String)
        Try
            '「開く(&O)...」メニュー項目を作成する
            Dim tempMenuItem As New ToolStripMenuItem()
            tempMenuItem.Text = addText
            'ショートカットキー「Ctrl+O」を設定する
            'tempMenuItem.ShortcutKeys = Keys.Control Or Keys.O
            'openMenuItem.ShowShortcutKeys = True
            'Clickイベントハンドラを追加する
            'AddHandler openMenuItem.Click, AddressOf openMenuItem_Click
            '「ファイル(&F)」のドロップダウンメニューに追加する
            ToolStripMenuItemAdded.DropDownItems.Add(tempMenuItem)

            mNowMenuItem = tempMenuItem　'ほかのプロパティ設定用
        Catch ex As Exception
            Console.WriteLine("addItemToMenuItemInMenuStrip")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'New ToolStripMenuItemで受け取るVer
    Public Sub addItems(MenuStrip As MenuStrip, toolStripMenuItem As ToolStripMenuItem, ItemText As String)
        Try
            'メニュー項目を作成する
            toolStripMenuItem.Text = ItemText
            MenuStrip.Items.Add(toolStripMenuItem) 'MenuStripに追加する

            mNowMenuItem = toolStripMenuItem　'ほかのプロパティ設定用
        Catch ex As Exception
            Console.WriteLine("addMenuItem")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    'New ToolStripMenuItemで受け取るVer
    Public Sub addDropDownItems(toolStripMenuItemParent As ToolStripMenuItem, toolStripMenuItemChild As ToolStripMenuItem, ItemText As String)
        Try
            'DropDownメニュー項目を作成する
            toolStripMenuItemParent.DropDownItems.Add(toolStripMenuItemChild)

            mNowMenuItem = toolStripMenuItemChild　'ほかのプロパティ設定用
        Catch ex As Exception
            Console.WriteLine("addDropDownItems")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


End Class
