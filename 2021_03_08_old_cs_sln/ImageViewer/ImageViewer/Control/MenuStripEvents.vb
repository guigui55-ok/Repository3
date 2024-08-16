Public Class MenuStripEvents
    Inherits AbstractEvents

    'Friend MainProcessorObject As MainProcesser
    'Friend Sub New(argMainProcessor As MainProcesser)
    'Friend Sub New()
    'Friend Sub setMainProcessor(argMainProcessor As MainProcesser)

    Public WithEvents MenuStripObject As MenuStrip

    Private mMainForm As MainForm
    Private mMenuStripEditor As MenuStripEditor
    Private mToolStripMenuEditor As ToolStripMenuItemEditor
    Private mMainFormBridge As FormBridge

    'Public Sub New(argMenuStrip As MenuStrip, argMainProcessorObject As MainProcesser)
    '    MyBase.New(argMainProcessorObject)
    '    'MainProcessorObject = argMainProcessorObject
    '    initializeObjects(argMenuStrip, argMainProcessorObject)
    'End Sub
    Public Sub New(argMainProcessorObject As MainProcesser)

    End Sub

    Public Sub initializeObjects(argMenuStrip As MenuStrip, argMainProcessorObject As MainProcesser)
        MainProcessorObject = argMainProcessorObject
        MenuStripObject = argMenuStrip


        mMainForm = argMainProcessorObject.gMainForm.getForm
        mMainFormBridge = New FormBridge(mMainForm)

        mMenuStripEditor = New MenuStripEditor(MenuStripObject, mMainFormBridge)

        'setMenuStrip(argMenuStrip)

        mToolStripMenuEditor = New ToolStripMenuItemEditor()
    End Sub

    Public Sub disposeObjects()
        Try
            mMenuStripEditor.DisposeObjetcs()
            mMenuStripEditor = Nothing
            mMainFormBridge = Nothing
            mToolStripMenuEditor = Nothing
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '旧クラス
    Public Sub setMenuStrip(argMenuStrip As MenuStrip)
        Me.MenuStripObject = argMenuStrip
    End Sub


    Public Sub mMenuStrip_KeyDown(sender As Object, e As KeyEventArgs) Handles MenuStripObject.KeyDown
        addLog(3, "mMenuStrip_KeyDown")
    End Sub

    Public Sub resistEvents()
        Try
            'イベント定義実行するクラス

            mMenuStripEditor = New MenuStripEditor(MenuStripObject, New FormBridge(mMainForm))
            Dim ToolStripMenuItem As ToolStripMenuItem

            Dim menuText As String = "ファイル(&F)"
            'Clickイベントハンドラを追加する
            ToolStripMenuItem = mMenuStripEditor.getToolStripMenuItemBySomeText(menuText, "ファイルを開く")
            AddHandler ToolStripMenuItem.Click, AddressOf OpenFile_Click
            ToolStripMenuItem = mMenuStripEditor.getToolStripMenuItemBySomeText(menuText, "終了(&X)")
            AddHandler ToolStripMenuItem.Click, AddressOf exitMenuItem_Click

            menuText = "表示(&V)"
            ToolStripMenuItem = mMenuStripEditor.getToolStripMenuItemBySomeText(menuText, "次へ")
            AddHandler ToolStripMenuItem.Click, AddressOf NextImageMenuItem_Click
            ToolStripMenuItem = mMenuStripEditor.getToolStripMenuItemBySomeText(menuText, "前へ")
            AddHandler ToolStripMenuItem.Click, AddressOf PreviousImageMenuItem_Click
            ToolStripMenuItem = mMenuStripEditor.getToolStripMenuItemBySomeText(menuText, "リストの最初へ")
            AddHandler ToolStripMenuItem.Click, AddressOf FirstImageOfListMenuItem_Click
            ToolStripMenuItem = mMenuStripEditor.getToolStripMenuItemBySomeText(menuText, "リストの最後へ")
            AddHandler ToolStripMenuItem.Click, AddressOf LastImageOfListMenuItem_Click
            ToolStripMenuItem = mMenuStripEditor.getToolStripMenuItemBySomeText(menuText, "リストをランダムにする")
            AddHandler ToolStripMenuItem.Click, AddressOf ListToBeRandomMenuItem_Click
            ToolStripMenuItem = mMenuStripEditor.getToolStripMenuItemBySomeText(menuText, "次のフォルダへ")
            AddHandler ToolStripMenuItem.Click, AddressOf NextFolderMenuItem_Click
            ToolStripMenuItem = mMenuStripEditor.getToolStripMenuItemBySomeText(menuText, "前のフォルダへ")
            AddHandler ToolStripMenuItem.Click, AddressOf PreviousFolderMenuItem_Click
            ToolStripMenuItem = mMenuStripEditor.getToolStripMenuItemBySomeText(menuText, "メニューバーを常時表示する")
            AddHandler ToolStripMenuItem.Click, AddressOf AlwaysVisibleMenuBar_Click
            ToolStripMenuItem = mMenuStripEditor.getToolStripMenuItemBySomeText(menuText, "ツールバーを表示する")
            AddHandler ToolStripMenuItem.Click, AddressOf ToolStripVisible_Click

        Catch ex As Exception
            addLogForSystemError("resistEvents")
            addLogForSystemError(ex.Message)
        End Try
    End Sub


    'ファイルを開く
    Private Sub OpenFile_Click(sender As Object, e As EventArgs)
        'MainProcessorObject.gEvents.gFileListEvents.openFile()
        MainProcessorObject.gEvents.gFileIo.openFile(MainProcessorObject.gNowViewImageManager)
    End Sub
    '「終了(&X)」メニュー項目のClickイベントハンドラ
    Private Sub exitMenuItem_Click(sender As Object, e As EventArgs)
        mMainForm.Close()
    End Sub

    '次へ
    Private Sub NextImageMenuItem_Click(sender As Object, e As EventArgs)
        MainProcessorObject.gEvents.gPaint.NextPaintImage(MainProcessorObject.gNowViewImageManager)
    End Sub
    '前へ
    Private Sub PreviousImageMenuItem_Click(sender As Object, e As EventArgs)
        MainProcessorObject.gEvents.gPaint.PreviousPaintImage(MainProcessorObject.gNowViewImageManager)
    End Sub
    'リストの最初へ
    Private Sub FirstImageOfListMenuItem_Click(sender As Object, e As EventArgs)
        MainProcessorObject.gEvents.gPaint.FirstImageOfList(MainProcessorObject.gNowViewImageManager)
    End Sub
    'リストの最後へ
    Private Sub LastImageOfListMenuItem_Click(sender As Object, e As EventArgs)
        MainProcessorObject.gEvents.gFileList.moveLastIndex(MainProcessorObject.gNowViewImageManager)
        '表示
        'MainProcessorObject.gState.Fade.FadeOutBegin = True
        MainProcessorObject.gFunction.gPaint.PaintMain(MainProcessorObject.gNowViewImageManager)
    End Sub
    'リストをファイル名順にする
    'リストをランダムにする
    Private Sub ListToBeRandomMenuItem_Click(sender As Object, e As EventArgs)
        MainProcessorObject.gEvents.gFileList.changeModeListOrder(MainProcessorObject.gNowViewImageManager)
    End Sub
    '次のフォルダへ
    Private Sub NextFolderMenuItem_Click(sender As Object, e As EventArgs)
        MainProcessorObject.gEvents.gFileIo.moveNextFolder(MainProcessorObject.gNowViewImageManager)
    End Sub
    '前のフォルダへ
    Private Sub PreviousFolderMenuItem_Click(sender As Object, e As EventArgs)
        MainProcessorObject.gEvents.gFileIo.moveBeforeFolder(MainProcessorObject.gNowViewImageManager)
    End Sub
    'メニューバーを常時表示する
    Private Sub AlwaysVisibleMenuBar_Click(sender As Object, e As EventArgs)
        addLog(3, "MenuStripEvents メニューバーを常時表示する Click")
        'MainProcessorObject.gEvents.gFileIo.moveBeforeFolder(MainProcessorObject.gNowViewImageManager)
        Me.changeSettingMenuStripVisible()
    End Sub
    'ツールバーを表示する
    Private Sub ToolStripVisible_Click(sender As Object, e As EventArgs)
        addLog(3, "MenuStripEvents ツールバーを表示する/しない Click")
        MainProcessorObject.gMainForm.gControls.gToolStripEvents.changeVisible()
    End Sub

    '========================================================================
    Public Sub MenuStripObject_MouseEnter() Handles MenuStripObject.MouseEnter

    End Sub

    Public Sub MenuStripObject_ChangeUICues() Handles MenuStripObject.ChangeUICues
        addLog(3, "Event MenuStripObject_ChangeUICues ChangeUICues")
    End Sub

    'Focusを受け取ったとき
    Public Sub MenuStripObject_GotFocus() Handles MenuStripObject.GotFocus
        addLog(3, "Event MenuStripObject_GotFocus")
        doProcessWhenShowMenuStrip()
    End Sub
    '========================================================================
    Public Sub MenuVisible(sender As Object, e As EventArgs) Handles MenuStripObject.VisibleChanged
        Try
            If MenuStripObject.Visible Then
                addLog(3, Me.ToString & ".MenuVisible : Visible = true")
                MainProcessorObject.gMainForm.gControls.gToolStripEvents.doVisible()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function getObject() As MenuStrip
        Return Me.MenuStripObject
    End Function

    Public Function getHeight() As Integer
        Try
            If MenuStripObject.Visible Then
                Return MenuStripObject.Height
            Else
                Return 0
            End If
        Catch ex As Exception
            addLogForSystemError("getHeight")
            addLogForSystemError(ex.Message)
            Return 0
        End Try
    End Function

    'MenuStrip表示時の処理、フラグによってmenuTextを変える等
    Public Sub doProcessWhenShowMenuStrip()
        Try
            'tmpToolStripMenuItem = mMenuStripEditor.getToolStripMenuItemBySomeText()
            Dim tmpToolStripMenuItem As ToolStripMenuItem
            'メニューバー常時表示Itemを取得
            tmpToolStripMenuItem = mToolStripMenuEditor.getToolStripMenuItemToMatchTowValues(
                MenuStripObject, "メニューバーを常時表示する", "メニューバーを常時表示しない")
            'メニューバー常時表示、フラグによってTextを変更
            mToolStripMenuEditor.ChangeTextOfToolStripMenuItem(
                tmpToolStripMenuItem,
                "メニューバーを常時表示しない", "メニューバーを常時表示する",
                MainProcessorObject.gSettings.MenuStripAlwaysVisible
            )
        Catch ex As Exception
            addLogForSystemError("doProcessWhenShowMenuStrip")
            addLogForSystemError(ex.Message)
        End Try
    End Sub



    Public Sub changeTextForRandom(argViewImageManager)
        If argViewImageManager.gState.gListRandom Then
            'Normal→Randomになったとき
            mMenuStripEditor.changeText("リストをファイル名順にする", "表示(&V)", "リストをランダムにする")
        Else
            'Random->normalになったとき
            mMenuStripEditor.changeText("リストをランダムにする", "表示(&V)", "リストをファイル名順にする")
        End If
    End Sub

    Public Sub changeSettingMenuStripVisible()
        Try
            'Dim menuBarHeight As Integer = 0
            'メニューを常に表示するの切り替え
            If New CommonGeneral().getAfterFlagWhenChangeFlag(MainProcessorObject.gSettings.MenuStripAlwaysVisible) Then
                MenuStripObject.Visible = True
                mMenuStripEditor.changeText("メニューバーを常時表示しない", "表示(&V)", "メニューバーを常時表示する")
                'menuBarHeight = MenuStripObject.Height
            Else
                MenuStripObject.Visible = False
                mMenuStripEditor.changeText("メニューバーを常時表示する", "表示(&V)", "メニューバーを常時表示しない")
            End If

            addLog(3, "MainProcessorObject.gSettings.MenuStripAlwaysVisible = " & MenuStripObject.Visible)
            '切り替え後にPanelのリサイズ
            'MainProcessorObject.gNowViewImageManager.gEvents.gPanel.changeSizeWithMenuMenuStripVisible(menuBarHeight)
        Catch ex As Exception
            addLogForSystemError("changeSettingMenuStripVisible")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub changeText(textAfter As String, ParamArray TargetText() As String)
        mMenuStripEditor.changeText(textAfter, TargetText)
    End Sub

    '非アクティブになったときに、AlwaysVisible_Falseなら消す
    Public Sub LostForcus() Handles MenuStripObject.MenuDeactivate
        InitializeVisible()
        'メニューバー非表示で空いた領域の分Panelをリサイズ
        MainProcessorObject.gNowViewImageManager.gEvents.gPanel.changeSizeWithMenuMenuStripUnVisible()
    End Sub

    'MenuStripを常に表示する
    Public Sub changeVisible()
        addLog(3, Me.ToString & ".changeVisible")
        '常に表示する設定か
        If MainProcessorObject.gSettings.MenuStripAlwaysVisible Then
            MenuStripObject.Visible = True
            Exit Sub
        End If


        'altキー押下によって表示非表示を切り替える
        If MenuStripObject.Visible Then
            MenuStripObject.Visible = False
            MainProcessorObject.gNowViewImageManager.gEvents.gPanel.changeSizeWithMenuMenuStripUnVisible()
        Else
            MenuStripObject.Visible = True
            MainProcessorObject.gNowViewImageManager.gEvents.gPanel.changeSizeWithMenuMenuStripVisible(
                MenuStripObject.Height
            )
        End If

        MainProcessorObject.gMainForm.gControls.gToolStripEvents.doVisibleForMenuStrip()
    End Sub

    '設定を読み込んでMenuStripの表示をする
    Public Sub InitializeVisible()
        '常に表示する設定か
        If MainProcessorObject.gSettings.MenuStripAlwaysVisible Then
            'MenuStripを常に表示する
            MenuStripObject.Visible = True
        Else
            MenuStripObject.Visible = False
        End If
    End Sub



End Class
