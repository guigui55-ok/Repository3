Public Class ContextMenuStripFunctionForViewImage
    Inherits AbstractImageViewerChild
    WithEvents mContextMenuStrip As ContextMenuStrip

    'Friend mMainProcessorObject As MainProcesser
    'Friend mViewImageManager As ViewImageManager
    'Friend Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
    'Friend Sub New()
    'Friend Sub setMainProcessor(argMainProcessor As MainProcesser)
    'Friend Sub setViewImageManager(argViewImageManager As ViewImageManager)
    '=======================================================================
    Public Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager, argContextMenuStrip As ContextMenuStrip)
        MyBase.New(argMainProcessor, argViewImageManager)
        mMainProcessorObject = argMainProcessor
        mViewImageManager = argViewImageManager
        mContextMenuStrip = argContextMenuStrip
    End Sub
    '=======================================================================
    Public Sub setObject(argContextMenuStrip As ContextMenuStrip)
        mContextMenuStrip = argContextMenuStrip
    End Sub
    Public Function getObject() As ContextMenuStrip
        Return mContextMenuStrip
    End Function

    Public Sub Initialize()
        Try
            addLog(3, "ContextMenuStripFunctionForViewImage.Initialize")
            'Dim dToolStripMenuItem As ToolStripMenuItem() = {
            '    New ToolStripMenuItem("終了"),
            '    New ToolStripMenuItem("スライドショーの開始"),
            '    New ToolStripMenuItem("リストをランダムにする"),
            '    New ToolStripMenuItem("スクロールバーの表示")}

            'mContextMenuStrip.Items.AddRange(dToolStripMenuItem)
            Dim tsmEditor As ToolStripMenuItemEditor = New ToolStripMenuItemEditor()
            tsmEditor.addToolStripMenuItemToParentObjectWhenNotAddIItemAlreadyExists(mContextMenuStrip, "終了")
            tsmEditor.addToolStripMenuItemToParentObjectWhenNotAddIItemAlreadyExists(mContextMenuStrip, "スライドショーの開始")
            tsmEditor.addToolStripMenuItemToParentObjectWhenNotAddIItemAlreadyExists(mContextMenuStrip, "リストをランダムにする")
            tsmEditor.addToolStripMenuItemToParentObjectWhenNotAddIItemAlreadyExists(mContextMenuStrip, "スクロールバーの表示")

        Catch ex As Exception
            addLogForSystemError("ContextMenuStripFunctionForViewImage.Initialize")
            addLogForSystemError(ex.Message)
        End Try


    End Sub

    'mContextMenuStripをコントロールに紐づけした後に実行
    Public Sub resistEvents()
        'Stop
        Try
            Dim tmpToolstrip As ToolStripMenuItem
            tmpToolstrip = New ToolStripMenuItemEditor().getToolStripMenuItem(mContextMenuStrip, "終了")
            AddHandler tmpToolstrip.Click, AddressOf ExitMenuItem_Click

            tmpToolstrip = New ToolStripMenuItemEditor().getToolStripMenuItem(mContextMenuStrip, "スライドショーの開始")
            AddHandler tmpToolstrip.Click, AddressOf SlideShowMenuItem_Click

            tmpToolstrip = New ToolStripMenuItemEditor().getToolStripMenuItem(mContextMenuStrip, "リストをランダムにする")
            AddHandler tmpToolstrip.Click, AddressOf RandomListMenuItem_Click
        Catch ex As Exception
            addLogForSystemError("ContextMenuStripFunctionForViewImage.resistEvents")
            addLogForSystemError(ex.Message)
        End Try
    End Sub


    '========================================================================
    'Event Method
    '========================================================================

    Private Sub ExitMenuItem_Click(sender As Object, e As EventArgs)
        addLog(3, "mContextMenuStrip.ItemClicked　ExitMenuItem_Click")
        mMainProcessorObject.gMainForm.getForm.Close()
    End Sub


    Private Sub SlideShowMenuItem_Click(sender As Object, e As EventArgs)
        addLog(3, "mContextMenuStrip.ItemClicked　SlideShowMenuItem_Click")
        'StateFlagとTimerStart_Stopを切り替える
        mViewImageManager.gFunction.gSlideShow.TimerSwitch(mViewImageManager.gState.gSlideShowExecute)
    End Sub

    Private Sub RandomListMenuItem_Click(sender As Object, e As EventArgs)
        addLog(3, "mContextMenuStrip.ItemClicked　RandomListMenuItem_Click")
        'RandomListを切り替える
        mViewImageManager.gEvents.gImageFileList.changeModeListOrder()
    End Sub


    Private Sub mContextMenuStrip_Click(sender As Object, e As EventArgs) Handles mContextMenuStrip.ItemClicked
        Try
            addLog(3, "mContextMenuStrip.ItemClicked")
        Catch ex As Exception
            addLogForSystemError("ContextMenuStripFunctionForViewImage.mContextMenuStrip_Click")
            addLogForSystemError(ex.Message)
        End Try
    End Sub


    Private Sub ContextMenuStrip_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles mContextMenuStrip.Opening
        addLog(3, "mContextMenuStrip.Opening")
        '0終了_1スライドショー_2ランダム
        doProcessWhenShowContextMenu()
    End Sub


    '========================================================================
    'method When Show 
    '========================================================================
    Public Sub ShowContextMenu(sender As Object, e As MouseEventArgs)
        Try
            doProcessWhenShowContextMenu()
            'mContextMenuStrip.Visible = True
            'mContextMenuStrip.Show(e.Location)
        Catch ex As Exception
            addLogForSystemError("ContextMenuStripFunctionForViewImage.ShowContextMenu")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    'ContextMenuを表示前の処理
    'フラグによってMenuItemのTextを変えている
    Public Sub doProcessWhenShowContextMenu()
        Try
            ChangeText("スライドショーの停止", "スライドショーの開始", mViewImageManager.gState.gSlideShowExecute)
            ChangeText("リストをファイル名順にする", "リストをランダムにする", mViewImageManager.gState.gListRandom)
        Catch ex As Exception
            addLogForSystemError("ContextMenuStripFunctionForViewImage.doProcessWhenShowContextMenu")
            addLogForSystemError(ex.Message)
        End Try
    End Sub
    '========================================================================
    'generic method↓
    '========================================================================

    'フラグによってMenuStripItemのTextを切り替える
    Private Sub ChangeText(TrueToFalseText As String, FalseToTrueText As String, flag As Boolean)
        Try
            'Object取得前処理①
            Dim gettext As String
            If New ToolStripMenuItemEditor().isExistsValueInObjectHasMenuStripItem(mContextMenuStrip, TrueToFalseText) Then
                gettext = TrueToFalseText
            ElseIf New ToolStripMenuItemEditor().isExistsValueInObjectHasMenuStripItem(mContextMenuStrip, FalseToTrueText) Then
                gettext = FalseToTrueText
            Else
                addLog(1, "ContextMenuStripFunctionForViewImage.ChangeText Error")
                gettext = FalseToTrueText
            End If

            'Object取得→Text変更
            Dim tmpToolStrip As ToolStripMenuItem = New ToolStripMenuItemEditor().getToolStripMenuItem(mContextMenuStrip, gettext)
            Dim changeText As String = ""
            If flag Then
                changeText = TrueToFalseText
            Else
                changeText = FalseToTrueText
            End If

            'Text変更
            If Not tmpToolStrip.Text = changeText Then
                tmpToolStrip.Text = changeText
            End If
        Catch ex As Exception
            addLogForSystemError("ContextMenuStripFunctionForViewImage.ChangeText")
            addLogForSystemError(ex.Message)
        End Try
    End Sub


    'Private Sub ChangeText(argToolstrip As ToolStripMenuItem, trueVal As String, falseVal As String, flag As Boolean)
    '    Try
    '        If flag Then
    '            argToolstrip.Text = trueVal
    '        Else
    '            argToolstrip.Text = falseVal
    '        End If
    '    Catch ex As Exception
    '        addLogForSystemError("ContextMenuStripFunctionForViewImage.ChangeText")
    '        addLogForSystemError(ex.Message)
    '    End Try
    'End Sub

    Private Sub changeFlag(ByRef flag As Boolean)
        If flag Then
            flag = False
        Else
            flag = True
        End If
    End Sub

    Private Function getAfterFlagWhenChangeFlag(ByRef ByRefFlag As Boolean) As Boolean
        If ByRefFlag Then
            ByRefFlag = False
        Else
            ByRefFlag = True
        End If
        Return ByRefFlag
    End Function


End Class
