Public Class MenuStripEventsResister
    Public WithEvents gMenuStrip As MenuStrip
    Private MainProcessorObject As MainProcesser

    Public mMainForm As MainForm
    Private mMenuStoripEditor As MenuStripEditor
    Private mMainFormBridge As FormBridge

    Private mToolStripItemTextLists As MenuStripTextLists

    Public Sub New(argMainProcessorObject As MainProcesser)

        'MainProcessorObject = argMainProcessorObject
        ''Me.gMenuStrip = argMenuStrip
        'mMainForm = argMainProcessorObject.gMainForm.getForm

        'mMainFormBridge = New FormBridge(mMainForm)
        'mMenuStoripEditor = New MenuStripEditor(gMenuStrip, mMainFormBridge)
    End Sub

    Public Sub initializeObjects(argMenuStrip As MenuStrip, argMainProcessorObject As MainProcesser)
        MainProcessorObject = argMainProcessorObject
        Me.gMenuStrip = argMenuStrip
        mMainForm = argMainProcessorObject.gMainForm.getForm

        mMainFormBridge = New FormBridge(mMainForm)
        mMenuStoripEditor = New MenuStripEditor(gMenuStrip, mMainFormBridge)
    End Sub

    Public Sub setMenuStrip(argMenuStrip As MenuStrip)
        Me.gMenuStrip = argMenuStrip
    End Sub

    Public Sub DisposeObjects()
        Try
            gMenuStrip = Nothing
            MainProcessorObject = Nothing
            mMainForm = Nothing
            mMenuStoripEditor = Nothing
            mMainFormBridge = Nothing
            mToolStripItemTextLists = Nothing
        Catch ex As Exception
            addLogForSystemError("DisposeObjects")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub InitializeItem()
        addLog(3, "MenuStripEventsResister.InitializeItem")
        'レイアウトロジックを停止する
        mMainForm.SuspendLayout()
        mMainForm.MenuStrip1.SuspendLayout()

        'MenuStripに任意の各項目を追加
        initilizeMenuStripValue()

        'フォームにMenuStripを追加する
        mMainForm.Controls.Add(gMenuStrip)
        'フォームのメインメニューとする
        mMainForm.MainMenuStrip = gMenuStrip

        'レイアウトロジックを再開する
        mMainForm.MenuStrip1.ResumeLayout(False)
        mMainForm.MenuStrip1.PerformLayout()
        mMainForm.ResumeLayout(False)
        mMainForm.PerformLayout()

    End Sub

    Private Sub initilizeMenuStripValue()
        Try
            Dim msEditor As New MenuStripEditor(gMenuStrip, mMainFormBridge)

            Dim menuText As String = "ファイル(&F)"
            msEditor.addItemToMenuStrip(menuText, Keys.Control Or Keys.F)
            msEditor.addItemToMenuItem("ファイルを開く", Keys.Control Or Keys.O, menuText)
            msEditor.addItemToMenuItem("終了(&X)", Keys.Control Or Keys.X, menuText)

            '表示
            menuText = "表示(&V)"
            msEditor.addItemToMenuStrip(menuText, Keys.Control Or Keys.V)
            '次へ
            msEditor.addItemToMenuItem("次へ", Keys.Control Or Keys.Right, menuText)
            '前へ
            msEditor.addItemToMenuItem("前へ", Keys.Control Or Keys.Left, menuText)
            'リストの最初へ
            msEditor.addItemToMenuItem("リストの最初へ", Keys.Control Or Keys.Shift Or Keys.Right, menuText)
            'リストの最後へ
            msEditor.addItemToMenuItem("リストの最後へ", Keys.Control Or Keys.Shift Or Keys.Left, menuText)
            'スライドショーの実行

            'リストをランダムにする/リストをファイル名順にする
            '最初の設定でランダムにしていることがある
            If MainProcessorObject.gState.ListRandom Then
                msEditor.addItemToMenuItem("リストをファイル名順にする", Keys.Control Or Keys.Shift Or Keys.R, menuText)
            Else
                msEditor.addItemToMenuItem("リストをランダムにする", Keys.Control Or Keys.Shift Or Keys.R, menuText)
            End If

            '次のフォルダへ
            msEditor.addItemToMenuItem("次のフォルダへ", Keys.Control Or Keys.Shift Or Keys.Up, menuText)
            '前のフォルダへ
            msEditor.addItemToMenuItem("前のフォルダへ", Keys.Control Or Keys.Shift Or Keys.Down, menuText)

            gMenuStrip.Items.Add(New ToolStripSeparator())   'セパレータの追加

            'メニューバーを常時表示
            msEditor.addItemToMenuItem("メニューバーを常時表示する", Keys.Control Or Keys.M, menuText)
            'ツールバーを表示する
            msEditor.addItemToMenuItem("ツールバーを表示する", Keys.Control Or Keys.T, menuText)

        Catch ex As Exception
            addLogForSystemError("initilizeMenuStripValue")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    '========================================================================
End Class
