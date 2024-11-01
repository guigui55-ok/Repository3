Imports ImageViewer
Imports System.IO

Public Class ToolStripEvents
    Inherits AbstractEvents
    Implements IDisposable

    'Friend MainProcessorObject As MainProcesser
    'Public Sub New(argMainProcessor As MainProcesser)
    '    MainProcessorObject = argMainProcessor
    'End Sub
    'Public Sub New()
    'End Sub
    'Private Sub setMainProcessor(argMainProcessor As MainProcesser)
    '=======================================================================
    'Public Sub New(argMainProcessor As MainProcesser)
    '    setMainProcessor(argMainProcessor)
    'End Sub

    Public Sub New(argToolStripContainer As ToolStripContainer, argToolStrip As ToolStrip, argMainProcessor As MainProcesser)
        mToolStripContainer = argToolStripContainer
        mToolStrip = argToolStrip
        SetMainProcessor(argMainProcessor)
    End Sub

    Private Overloads Sub SetMainProcessor(argMainProcessor As MainProcesser)
        Me.MainProcessorObject = argMainProcessor
    End Sub

    Private WithEvents mToolStripContainer As ToolStripContainer
    Private WithEvents mToolStrip As ToolStrip
    Private mToolStripButton As ToolStripButton
    Private mToolTip As ToolTip

    '===============================================================================================
    Private isDisposed As Boolean = False ' リソースが破棄(解放)されていることを表すフラグ

    ' IDisposable.Disposeの実装
    '// Dispose() calls Dispose(true)
    Public Sub Dispose() Implements IDisposable.Dispose
        ' アンマネージリソースと、マネージリソースの両方を破棄させる
        Dispose(True)
        ' すべてのリソースが破棄されているため、以後ファイナライザの実行は不要であることをガベージコレクタに通知する
        GC.SuppressFinalize(Me)
    End Sub
    ' リソースの解放処理を行うためのメソッド
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        ' 既にリソースが破棄されている場合は何もしない
        If isDisposed Then Return

        ' 破棄されていないアンマネージリソースの解放処理を行う
        doDispose()
        ' リソースは破棄されている
        isDisposed = True
    End Sub
    'Finalize
    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
    '===============================================================================================

    Private Sub doDispose()

        ' dispose managed resources
        If Not mToolStripContainer Is Nothing Then
            mToolStripContainer.Dispose()
        End If
        mToolStripContainer = Nothing
        If Not mToolStrip Is Nothing Then
            mToolStrip.Dispose()
        End If
        mToolStrip = Nothing
        MainProcessorObject = Nothing
        If Not mToolStripButton Is Nothing Then
            mToolStripButton.Dispose()
        End If
        mToolStripButton = Nothing
        If Not mToolTip Is Nothing Then
            mToolTip.Dispose()
        End If
        mToolTip = Nothing
        GC.SuppressFinalize(Me)
        ' free native resources
    End Sub 'Dispose


    Public Sub mToolStripContainer_SizeChanged(sender As Object, e As EventArgs) Handles mToolStripContainer.SizeChanged
        Try
            'Stop
        Catch ex As Exception
            addLogForSystemError("mToolStripContainer_SizeChanged")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Function getToolStripContainer() As ToolStripContainer
        Return mToolStripContainer
    End Function
    '=======================================================================

    Private Sub mToolStripContainer_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) _
        Handles mToolStripContainer.PreviewKeyDown
        e.IsInputKey = True
        Try
            Select Case e.KeyCode
                Case Keys.Up, Keys.Left, Keys.Right, Keys.Down
                    '矢印キーが押された
                    addLog(3, "mToolStripContainer_PreviewKeyDown : e.KeyData = " & e.KeyData.ToString)
                    MainProcessorObject.PictureBoxKeyDown(sender, Me.convertPreviewKeyDownEventArgsToKeyEventArgs(e))
                Case Keys.Tab
                    'Tabキーが押されてもフォーカスが移動しないようにする
                    e.IsInputKey = True
            End Select
        Catch ex As Exception
            Console.WriteLine(Me.ToString & ".MainForm_PreviewKeyDown")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Function convertPreviewKeyDownEventArgsToKeyEventArgs(e As PreviewKeyDownEventArgs) As KeyEventArgs
        Try
            Return New KeyEventArgs(e.KeyData)
        Catch ex As Exception
            Console.WriteLine(Me.ToString & ".convertPreviewKeyDownEventArgsToKeyEventArgs")
            Console.WriteLine(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Sub mToolStripContainer_KeyDown(sender As Object, e As KeyEventArgs) Handles mToolStripContainer.KeyDown
        Try
            'addLog(3, "mToolStripContainer_KeyDown : " & e.KeyData.ToString)
            'Console.WriteLine("mToolStripContainer_KeyDown : " & e.KeyData.ToString)
        Catch ex As Exception
            addLogForSystemError("mToolStripContainer_KeyDown")
            addLogForSystemError(ex.Message)
        End Try
    End Sub



    'ツールバーを表示する_しないの切り替え
    Public Sub changeVisible()
        Dim tCommonGeneral As New CommonGeneral
        Try
            If tCommonGeneral.getAfterFlagWhenChangeFlag(MainProcessorObject.gSettings.ToolStripVisible) Then
                '表示
                mToolStrip.Visible = True
                'Location処理
                doVisible()
                MainProcessorObject.gMainForm.gControls.gMenuStripEvents.changeText(
                    "ツールバーを表示しない", "表示(&V)", "ツールバーを表示する")
            Else
                '表示
                mToolStrip.Visible = False
                '処理なし
                doVisible()
                MainProcessorObject.gMainForm.gControls.gMenuStripEvents.changeText(
                    "ツールバーを表示する", "表示(&V)", "ツールバーを表示しない")
            End If

            addLog(3, "MainProcessorObject.gSettings.ToolStripVisible = " & mToolStrip.Visible)
            '切り替え後にPanelのリサイズ
            'MainProcessorObject.gNowViewImageManager.gEvents.gPanel.changeSizeWithMenuMenuStripVisible(menuBarHeight)
        Catch ex As Exception
            addLogForSystemError("changeVisible")
            addLogForSystemError(ex.Message)
        Finally
            tCommonGeneral = Nothing
        End Try
    End Sub
    '=======================================================================
    'ツールバーを表示
    Public Sub doVisible()
        Try
            'メニューバーの下に表示するため、メニューバーVisibleによって表示位置を変える
            'Dim y As Integer = MainProcessorObject.gMainForm.gControls.gMenuStripEvents.getHeight()
            'mToolStripContainer.TopToolStripPanel.Visible = True
            'mToolStripContainer.TopToolStripPanel.Height = mToolStripContainer.TopToolStripPanel.Height + y + y
            'addLog(3, Me.ToString & "doVisible : mToolStripContainer.TopToolStripPanel.Height = " & mToolStripContainer.TopToolStripPanel.Height)
            'mToolStripContainer.TopToolStripPanel.Location = New Point(y, 0)
            'addLog(3, Me.ToString & "doVisible : mToolStripContainer.TopToolStripPanel.Location = " _
            '       & mToolStripContainer.TopToolStripPanel.Location.X & "," & mToolStripContainer.TopToolStripPanel.Location.Y)
            'mToolStrip.Location = New Point(y, 0)
            'addLog(3, Me.ToString & "doVisible : mToolStrip.Location = " & mToolStrip.Location.X & "," & mToolStrip.Location.Y)

            Dim tMenuStrip As MenuStrip = MainProcessorObject.gMainForm.getForm.MenuStrip1
            mToolStripContainer.TopToolStripPanel.Join(tMenuStrip, 0, 0)
            'ToolStrip1をToolStripContainer1の上部の先頭に移動する
            mToolStripContainer.TopToolStripPanel.Join(mToolStrip, 0, 0)


        Catch ex As Exception
            addLogForSystemError("doVisible")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub doVisibleForMenuStrip()
        Try
            'メニューバーを表示
            Dim tMenuStrip As MenuStrip = MainProcessorObject.gMainForm.getForm.MenuStrip1
            mToolStripContainer.TopToolStripPanel.Join(tMenuStrip, 0, 0)
            'ツールバーを表示
            If MainProcessorObject.gSettings.ToolStripVisible Then
                mToolStripContainer.TopToolStripPanel.Join(mToolStrip, 0, 0)
            Else
                'ひょうじしない
            End If
        Catch ex As Exception
            addLogForSystemError("doVisibleForMenuStrip")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub Initialize(argForm As MainForm)
        addLog(3, Me.ToString & "Initialize")
        Try

            addLog(3, mToolStripContainer.ParentForm.ToString)
            'ToolStripContainerオブジェクトを作成
            'mToolStripContainer = New ToolStripContainer()
            'MainFormへ紐づけ
            Dim mMainForm As MainForm = MainProcessorObject.gMainForm.getForm

            'レイアウトを停止
            mMainForm.SuspendLayout()
            mToolStripContainer.SuspendLayout()
            mToolStripContainer.TopToolStripPanel.SuspendLayout()

            'フォームいっぱいに広げる
            mToolStripContainer.Dock = DockStyle.None
            'ToolStripが右端に移動できないようにする
            mToolStripContainer.RightToolStripPanelVisible = False

            mToolStrip = mMainForm.ToolStrip1

            'レイアウトを停止
            mToolStrip.SuspendLayout()
            'ToolStripにItemを登録
            Me.resistToolStripItem()


            'toolStripContainer1の上部にtoolStrip1を追加
            mToolStripContainer.TopToolStripPanel.Join(MainProcessorObject.gMainForm.gControls.gMenuStripEvents.getObject)
            mToolStripContainer.TopToolStripPanel.Join(mToolStrip)

            'フォームにtoolStripContainer1を追加
            argForm.Controls.Add(mToolStripContainer)
            'レイアウトを再開
            mToolStripContainer.TopToolStripPanel.ResumeLayout(False)
            mToolStripContainer.TopToolStripPanel.PerformLayout()
            mToolStripContainer.ResumeLayout(False)
            mToolStripContainer.PerformLayout()
            mToolStrip.ResumeLayout(False)
            mToolStrip.PerformLayout()
            mMainForm.ResumeLayout(False)

            mToolStripContainer.Parent = mMainForm
            '例外がスローされました: 'System.NotSupportedException' (System.Windows.Forms.dll の中)
            'mToolStrip.Parent = mToolStripContainer



        Catch ex As Exception
            addLogForSystemError(Me.ToString & "Initialize")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    '=======================================================================
    Private Sub resistToolStripItem()
        Try
            Dim path As String = New CommonFile().getCurrentDirectory() & "\icon\icons8-ファイル-48.png"
            addLog(String.Format("### icon Path = {0}", path))
            '# ファイル存在チェック
            If Not File.Exists(path) Then
                Throw New FileNotFoundException(String.Format("Path Not Found = {0}", path))
            End If
            'Buttonを追加
            Dim ttoolStripButton As ToolStripButton = Me.addButtonToToolStrip(
                mToolStrip, "", "ファイルを開く", path)
            AddHandler ttoolStripButton.Click, AddressOf FileOpen_Click
            'Buttonを追加
            ttoolStripButton = Me.addButtonToToolStrip(mToolStrip, "", "前へ", My.Resources.icon_previousfile)
            AddHandler ttoolStripButton.Click, AddressOf ViewPreviousImage_Click
            'Buttonを追加
            ttoolStripButton = Me.addButtonToToolStrip(mToolStrip, "", "次へ", My.Resources.icon_nextfile)
            AddHandler ttoolStripButton.Click, AddressOf ViewNextImage_Click

        Catch ex As Exception
            addLogForSystemError(Me.ToString & "resistToolStripItem")
            addLogForSystemError(ex.Message)
        End Try
    End Sub
    '=======================================================================
    Public Sub ViewNextImage_Click()
        addLog(3, "toolstrip button click : ViewNextImage_Click")
        MainProcessorObject.gNowViewImageManager.gEvents.gViewer.NextPaintImage()
    End Sub

    Public Sub ViewPreviousImage_Click()
        addLog(3, "toolstrip button click : ViewPreviousImage_Click")
        MainProcessorObject.gNowViewImageManager.gEvents.gViewer.PreViousPaintImage()
    End Sub

    Public Sub FileOpen_Click()
        addLog(3, "toolstrip button click : FileOpen_Click")
        MainProcessorObject.gEvents.gFileIo.openFile(MainProcessorObject.gNowViewImageManager)
    End Sub

    '=======================================================================

    Private Function isAbleObjectToSetToolStrip(argObject As Object) As Boolean
        Try
            If VarType(argObject) = VarType(New ToolStripButton()) Then
                Return True
            End If
            Return False
        Catch ex As Exception
            addLogForSystemError(Me.ToString & "isAbleObjectToSetToolStrip")
            addLogForSystemError(ex.Message)
            Return False
        End Try
    End Function

    'CastExceptionが起こる
    Private Sub setToolTip(argToolStripParts As Object, TextValue As String)
        Try
            If Not isAbleObjectToSetToolStrip(argToolStripParts) Then
                addLog(3, Me.ToString & "isAbleObjectToSetToolStrip : Object is default")
                Exit Sub
            End If

            mToolTip = New ToolTip()
            'ToolTipが表示されるまでの時間
            mToolTip.InitialDelay = MainProcessorObject.gSettings.DefaultSettings.Forms.ToolTrip_InitialDelay
            'ToolTipが表示されている時に、別のToolTipを表示するまでの時間
            mToolTip.ReshowDelay = MainProcessorObject.gSettings.DefaultSettings.Forms.ToolTrip_ReshowDelay
            'ToolTipを表示する時間
            mToolTip.AutoPopDelay = MainProcessorObject.gSettings.DefaultSettings.Forms.ToolTrip_AutoPopDelay
            'フォームがアクティブでない時でもToolTipを表示する
            mToolTip.ShowAlways = True

            mToolTip.SetToolTip(argToolStripParts, TextValue)
        Catch ex As Exception
            addLogForSystemError(Me.ToString & "setToolTip")
            addLogForSystemError(ex.Message)
        Finally
        End Try
    End Sub

    'ToolTipをセット
    Private Sub setToolTip(argToolStripButton As ToolStripButton, TextValue As String)
        Try
            argToolStripButton.AutoToolTip = True
            argToolStripButton.ToolTipText = TextValue
        Catch ex As Exception
            addLogForSystemError(Me.ToString & "setToolTip2")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    'ToolStripにボタンを追加
    Private Function addButtonToToolStrip(argToolStrip As ToolStrip, textValue As String, ToolTipText As String, imagePath As String) As ToolStripButton
        Try
            'ToolStripの作成
            'argToolStrip = New ToolStrip()
            'ToolStripButtonオブジェクトを作成
            Dim tToolStripButton = New ToolStripButton()
            tToolStripButton.Text = textValue
            tToolStripButton.Image = Image.FromFile(imagePath)

            'ToolTipの設定
            Me.setToolTip(tToolStripButton, ToolTipText)

            'ToolStripにアイテムを追加
            argToolStrip.Items.Add(tToolStripButton)

            Return tToolStripButton
        Catch ex As Exception
            addLogForSystemError(Me.ToString & "addButtonToToolStrip")
            addLogForSystemError(ex.Message)
            Return Nothing
        End Try
    End Function

    'ToolStripにボタンを追加、引数をイメージに
    Private Function addButtonToToolStrip(argToolStrip As ToolStrip, textValue As String, ToolTipText As String, argimage As Bitmap) As ToolStripButton
        Try
            If argimage Is Nothing Then
                addLogForSystemError(Me.ToString & "addButtonToToolStrip : image is nothing")
                Return Nothing
            End If
            'ToolStripの作成
            'argToolStrip = New ToolStrip()
            'ToolStripButtonオブジェクトを作成
            Dim tToolStripButton = New ToolStripButton()
            tToolStripButton.Text = textValue
            tToolStripButton.Image = argimage

            'ToolTipの設定
            Me.setToolTip(tToolStripButton, ToolTipText)

            'ToolStripにアイテムを追加
            argToolStrip.Items.Add(tToolStripButton)

            Return tToolStripButton
        Catch ex As Exception
            addLogForSystemError(Me.ToString & "addButtonToToolStrip")
            addLogForSystemError(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Sub InitializeVisible()

        Try
            Dim tMainForm As MainForm = MainProcessorObject.gMainForm.getForm
            Dim tpanel As Panel = MainProcessorObject.gNowViewImageManager.gEvents.gPanel.getPanel


            mToolStripContainer.Visible = True
            mToolStripContainer.Location = New Point(0, 0)

            'tMainForm.ToolStripStatusLabel1.Height = tMainForm.ToolStripStatusLabel1.Height
            'Panel_Location設定
            tpanel.Height = tMainForm.Height - tMainForm.ToolStripStatusLabel1.Height
            tpanel.Location = New Point(tpanel.Location.X, tpanel.Location.Y - tMainForm.ToolStripStatusLabel1.Height)
            mToolStripContainer.Location = New Point(0, 0)

            'tpanel.Refresh()

            addLog(3, "mToolStripContainer.height = " & mToolStripContainer.Height)
            addLog(3, "tMainForm.ToolStripStatusLabel1.height = " & tMainForm.ToolStripStatusLabel1.Height)
            addLog(3, "mToolStrip.height = " & mToolStrip.Height)
            addLog(3, "mToolStrip.location.Y = " & mToolStrip.Location.Y)
            addLog(3, "tPanel.Size.Width = " & tpanel.Size.Width)
            addLog(3, "tPanel.Size.Height = " & tpanel.Size.Height)
            addLog(3, "tPanel.Location.X = " & tpanel.Location.X)
            addLog(3, "tPanel.Location.Y = " & tpanel.Location.Y)
            addLog(3, "tMainForm.Size.Width = " & tMainForm.Size.Width)
            addLog(3, "tMainForm.Size.Height = " & tMainForm.Size.Height)

            tMainForm = Nothing
            tpanel = Nothing
        Catch ex As Exception
            addLogForSystemError(Me.ToString & "InitializeVisible")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

End Class
