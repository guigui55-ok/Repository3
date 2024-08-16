Public Class PictureBoxForViewImageFunction
    Inherits AbstractImageViewerChild

    'Friend mMainProcessorObject As MainProcesser
    'Friend mViewImageManager As ViewImageManager
    'Friend Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
    'Friend Sub New()
    'Friend Sub setMainProcessor(argMainProcessor As MainProcesser)
    'Friend Sub setViewImageManager(argViewImageManager As ViewImageManager)
    '=======================================================================
    Public Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager, argPictureBox As PictureBox)
        MyBase.New(argMainProcessor, argViewImageManager)
        mMainProcessorObject = argMainProcessor
        mViewImageManager = argViewImageManager
        mPictureBox = argPictureBox
    End Sub
    Public Sub New(argMainProcessor As MainProcesser, argPictureBox As PictureBox)
        MyBase.New(argMainProcessor, argMainProcessor.gNowViewImageManager)
        mMainProcessorObject = argMainProcessor
        mPictureBox = argPictureBox
    End Sub
    '=======================================================================
    Private WithEvents mPictureBox As PictureBox
    '=======================================================================
    Public Sub setPictureBox(argPictureBox As PictureBox)
        mPictureBox = argPictureBox
    End Sub

    Public Function getPictureBox() As PictureBox
        Return mPictureBox
    End Function

    Public Sub refresh()
        Me.refreshNotView()
        mViewImageManager.gEvents.gViewer.View()
    End Sub

    Public Sub refreshNotView()
        addLog(3, Me.ToString & ".refresh")
        Dim beforeLocation As Point
        Try
            If mPictureBox Is Nothing Then
                Exit Sub
            Else
                beforeLocation = mPictureBox.Location
            End If
            If mPictureBox.Image Is Nothing Then
                Exit Sub
            End If
            'mPictureBox.Refresh()

            mPictureBox.Dispose()
            mPictureBox = Nothing
            mPictureBox = New PictureBox()
            mViewImageManager.gImageManager.setControlForPaint(New ControlForPaintAsPictureBox(mPictureBox))
            mViewImageManager.gEvents.gPictureBox.setPictureBox(mPictureBox)
            '設定
            mPictureBox.Size = mViewImageManager.gFunction.gPanel.getPanel.Size
            'mPictureBox.BackColor = Color.Aqua
            mPictureBox.Parent = mViewImageManager.gFunction.gPanel.getPanel
            mPictureBox.Location = beforeLocation
            mPictureBox.Visible = True
            mPictureBox.SizeMode = PictureBoxSizeMode.Zoom

        Catch ex As Exception
            addLogForSystemError(Me.ToString & ".refresh")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub resumeLayout()
        mPictureBox.ResumeLayout()
    End Sub

    Public Sub refreshNotPaint()
        addLog(3, Me.ToString & ".refreshNotPaint")
        Dim beforeLocation As Point
        Dim beforeSize As Size
        Try
            If mPictureBox Is Nothing Then
                Exit Sub
            Else
                beforeLocation = mPictureBox.Location
                beforeSize = mPictureBox.Size
            End If
            If mPictureBox.Image Is Nothing Then
                Exit Sub
            End If
            'mPictureBox.Refresh()

            mPictureBox.Dispose()
            mPictureBox = Nothing
            mPictureBox = New PictureBox()
            '描画無効化　'再開時はResumeLayout
            mPictureBox.SuspendLayout()
            mViewImageManager.gImageManager.setControlForPaint(New ControlForPaintAsPictureBox(mPictureBox))
            mViewImageManager.gEvents.gPictureBox.setPictureBox(mPictureBox)
            '設定
            mPictureBox.Size = mViewImageManager.gFunction.gPanel.getPanel.Size
            'mPictureBox.BackColor = Color.Aqua
            mPictureBox.Parent = mViewImageManager.gFunction.gPanel.getPanel
            mPictureBox.Location = beforeLocation
            mPictureBox.Size = beforeSize
            mPictureBox.Visible = True
            mPictureBox.SizeMode = PictureBoxSizeMode.Zoom
        Catch ex As Exception
            addLogForSystemError(Me.ToString & ".refreshNotPaint")
            addLogForSystemError(ex.Message)
        Finally
            beforeSize = Nothing
            beforeLocation = Nothing
        End Try
    End Sub

    'Private Sub resetPictureBox(nowPictureBox As PictureBox)
    '    Try
    '        mViewImageManager.gImageManager.setControlForPaint(New ControlForPaintAsPictureBox(mPictureBox))
    '        mViewImageManager.gEvents.gPictureBox.setPictureBox(mPictureBox)
    '        '設定
    '        mPictureBox.Size = mViewImageManager.gFunction.gPanel.getPanel.Size
    '        'mPictureBox.BackColor = Color.Aqua
    '        mPictureBox.Parent = mViewImageManager.gFunction.gPanel.getPanel
    '        mPictureBox.Location = beforeLocation
    '        mPictureBox.Size = beforeSize
    '        mPictureBox.Visible = True
    '        mPictureBox.SizeMode = PictureBoxSizeMode.Zoom
    '    Catch ex As Exception
    '        addLogForSystemError(Me.ToString & ".resetPictureBox")
    '        addLogForSystemError(ex.Message)
    '    End Try
    'End Sub


    Public Function isErrorImage() As Boolean
        Try
            If mPictureBox.Image Is Nothing Then
                Return False
            End If

            If mPictureBox.Image.Equals(mPictureBox.ErrorImage) Then
                Return True
            Else
                Return False
            End If

            '演算子 '=' は、型 'Image' および 'Image' に対して定義されていません
            'If mPictureBox.Image = mPictureBox.ErrorImage Then
            '    Return True
            'End If
        Catch ex As Exception
            addLogForSystemError(Me.ToString & ".isErrorImage")
            addLogForSystemError(ex.Message)
            Return False
        End Try
    End Function

    Public Function getVisible() As Boolean
        If mPictureBox Is Nothing Then
            addLog(0, "PictureBoxForViewImageFunction.getVisible")
            Return False
        End If
        Return mPictureBox.Visible
    End Function


    'ChangeLocattion_WidthinMouse
    'OK
    Public Sub PictureBoxChangeLocation(e As MouseEventArgs)
        mViewImageManager.gImageManager.getPaintImage.getControlForPaint.changeLocationWithinFrame(
            mViewImageManager.gImageManager.getPaintImage.getControlFrame.getSize,
            e, mMainProcessorObject.gState.gMouse.DownPoint)
    End Sub

    Public Function getMousePointInPictureBox(sender As Object, e As MouseEventArgs)
        Try
            'フォーム上の座標でマウスポインタの位置を取得する
            '画面座標でマウスポインタの位置を取得する
            Dim sp As System.Drawing.Point = System.Windows.Forms.Cursor.Position
            '画面座標をクライアント座標に変換する
            Dim cp As System.Drawing.Point = mPictureBox.PointToClient(sp)
            Return cp
            sp = Nothing
            cp = Nothing
        Catch ex As Exception
            Console.WriteLine("getMousePointInPictureBox")
            Console.WriteLine(ex.Message)
            Return New Point(0, 0)
        End Try
    End Function

    'ChangeSize_MouseWheel
    'Ok
    '旧　200708にChangeSizeAndLocationForPictureBoxへ変更
    Public Sub ChangeSizePictureBox(raito As Double)

        Try
            Dim ControlForPaintObject As ControlForPaintAsPictureBox
            ControlForPaintObject = mViewImageManager.gImageManager.getPaintImage.getControlForPaint

            Dim NewSize As Size = ControlForPaintObject.getSize
            NewSize = New Size(NewSize.Width * raito, NewSize.Height * raito)

            'SaveBeforeSizeAndLocation MouseWheel用に
            ControlForPaintObject.saveSizeAndLocationWhenSizeChangeByMouseWheel()

            'size変更
            mViewImageManager.gImageManager.getPaintImage.getControlForPaint.changeSize(NewSize)
        Catch ex As Exception
            Console.WriteLine("ChangeSizePictureBox")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub SuspendLayout(flag As Boolean)
        If True Then
            mPictureBox.SuspendLayout()
        Else
            mPictureBox.ResumeLayout()
        End If
    End Sub

    '200708
    'マウスホイール拡大縮小用
    Public Sub ChangeSizeAndLocationForPictureBox(raito As Double, beforeMp As Point)
        addLogNotFocus(3, Me.ToString & ".ChangeSizeAndLocationForPictureBox")
        Dim ControlForPaintObject As ControlForPaintAsPictureBox
        Dim tControlForPaintSetting As IControlForPaintSetting
        tControlForPaintSetting = New ControlForPaintSetting()
        Dim NewLocation As Point
        Try
            ControlForPaintObject = mViewImageManager.gImageManager.getPaintImage.getControlForPaint

            'Me.SuspendLayout(True)
            'mViewImageManager.gFunction.gPanel.SuspendLayout(True)
            'mMainProcessorObject.gMainForm.getForm.ToolStripContainer1.SuspendLayout()
            'Sizeを計算_拡大_縮小
            Dim NewSize As Size = ControlForPaintObject.getSize
            NewSize = New Size(NewSize.Width * raito, NewSize.Height * raito)
            'size変更
            mViewImageManager.gImageManager.getPaintImage.getControlForPaint.changeSize(NewSize)

            '現在のLocation
            tControlForPaintSetting.setInnerControlLocation(ControlForPaintObject.getLocation)

            'PanelとPictureBoxの大きさの関係によってLocation計算方法が違う
            If mViewImageManager.gImageManager _
                .EitherInnerPaintControlSizesWidthOrHeightIsGreatherThanFrameSize() Then
                'PictureBoxのWidth_HeightのいずれかがPanelより大きい
                'Location計算
                NewLocation = tControlForPaintSetting _
                    .getLocationWhenSizeChangePictureBoxByMouseWheel(
                     beforeMp,
                     raito,
                     ControlForPaintObject.getSize,
                     mViewImageManager.gImageManager.getPaintImage.getControlFrame.getSize
                    )
                'mMainProcessorObject.gState.gDebugFlag = 1
            Else
                'PictureBoxのWidth_HeightのどちらもPanelより小さい
                'PictureBoxのBeforeSize-AfterSizeを引いたものの1/2をLocationに加算
                NewLocation =
                    tControlForPaintSetting.
                    getLocationWhenSizeChangePictureBoxByMouseWheelWhenPictureBoxSmallerThanPanel(
                        raito,
                        ControlForPaintObject.getSize,
                        mViewImageManager.gImageManager.getPaintImage.getControlFrame.getSize
                )
            End If
            'Location変更
            'mViewImageManager.gImageManager.getPaintImage.getControlForPaint.changeLocation(NewLocation)
            mPictureBox.Location = NewLocation

            'Me.SuspendLayout(False)
            'mViewImageManager.gFunction.gPanel.SuspendLayout(False)
            'mMainProcessorObject.gMainForm.getForm.ToolStripContainer1.ResumeLayout()


            'SaveBeforeSizeAndLocation MouseWheel用に
            'ControlForPaintObject.saveSizeAndLocationWhenSizeChangeByMouseWheel()
            addLogNotFocus(3, "NewLocation:" & NewLocation.X & " , " & NewLocation.Y)
        Catch ex As Exception
            Console.WriteLine("ChangeSizePictureBox")
            Console.WriteLine(ex.Message)
        Finally
            ControlForPaintObject = Nothing
            tControlForPaintSetting = Nothing
            NewLocation = Nothing
        End Try
    End Sub

    Public Sub AlignLocationForPictureBox()
        Try
            '追従設定Panel = PictureBoxSizeのときは
            'WidthHeight等しいほうの高さサイズは追従する
            'そうでない方は比率を掛ける（あっているほうの）
            'Locationはそのまま
            If mMainProcessorObject.gSettings.PictureBoxFitWindow Then
                If mViewImageManager.gImageManager.InnerPaintControlSizeIsSmallerThanFrameSize Then
                    'PanelにPictureBox追従
                    'Panel=PictureBox Or WidthHeightどちらかが等しく、他方は小さい
                    Me.AlignLocationAndSizeForPictureBox(
                        mViewImageManager.gImageManager.getPaintImage.getControlForPaint.getSize)
                Else
                    If mViewImageManager.gImageManager.EitherInnerPaintControlSizesWidthOrHeightIsGreatherThanFrameSize Then
                        'どちらも大きい
                        'Me.KeepSizeChangeLocation()
                        addLog(2, Me.ToString & ".AlignLocationForPictureBox ")
                    Else
                        'どちらも小さい
                        Me.UpdatePictureBoxSize()
                    End If
                End If
            Else
                'PanelにPictureBox追従
            End If
        Catch ex As Exception
            addLogForSystemError("AlignLocationForPictureBox")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    'PictureBox=Panel用
    'PictureBox比率Image,PictureBoxSize=任意,
    'PictureBoxLocationサイズがWH両方小さいときはCenter、WHどちらかが大きいときは00
    'PictureBox比率Image,PictureBoxSize=Panel,PictureBoxLocation00
    '12_12使用中
    '追従用
    Private Sub AlignLocationAndSizeForPictureBox(argPictureBoxSize As Size)
        Try
            Dim PaintImageObject As IPaintImage = mViewImageManager.gImageManager.getPaintImage
            'ControlSetting
            Dim ControlForPaintSettingObject As IControlForPaintSetting _
                = New ControlForPaintSetting(
                    argPictureBoxSize, PaintImageObject.getControlForPaint.getLocation)

            Dim ImageSize As Size
            ImageSize = PaintImageObject.getControlForPaint.getImageSize
            If ImageSize.Width = 0 Or ImageSize.Height = 0 Then
                ImageSize = PaintImageObject.getControlFrame.getSize
            End If

            ControlForPaintSettingObject.SetInnerSizeByCalcSizeByImageRaito(
                ImageSize, 0)

            'PictureBoxのSizeをPanelサイズによって変える
            ControlForPaintSettingObject.setInnerSizeByCalcSizeForFitOuterSize(PaintImageObject.getControlFrame.getSize)

            'PictureBoxのLocationをPanelサイズとPictureBoxサイズによって変える
            ControlForPaintSettingObject.SetInnerLocationByCalcLocationForWindowResize(PaintImageObject.getControlFrame.getSize)
            PaintImageObject.setControlForPaintSetting(ControlForPaintSettingObject)
            'apply
            PaintImageObject.applyControlSetting()

        Catch ex As Exception
            Console.WriteLine("AlignLocationForPictureBox")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    'PictureBox比率Image,PictureBoxSize=任意,
    'xxxPictureBoxLocationサイズがWH両方小さいときはCenter、WHどちらかが大きいときは00xxx
    'PictureBoxLocationは基本動かさないが、MaxMinLocationRegion以外ならMaxMinに修正（外部で実装済み）
    'PictureBoxSize<PanelSize小さいとき用
    '小さいとき用
    'private
    Public Sub UpdatePictureBoxSize()
        Try
            Dim PaintImageObject As IPaintImage = mViewImageManager.gImageManager.getPaintImage
            'ControlSetting
            Dim ControlForPaintSettingObject As IControlForPaintSetting _
                = New ControlForPaintSetting(
                    PaintImageObject.getControlForPaint.getSize,
                    PaintImageObject.getControlForPaint.getLocation)

            Dim ImageSize As Size = PaintImageObject.getControlForPaint.getImageSize
            'Dim ImageSize As Size = PaintImageObject.getControlForPaint.getSize
            If ImageSize.Equals(New Size(0, 0)) Then
                ImageSize = PaintImageObject.getControlFrame.getSize
            End If

            'PictureBoxのサイズをImageの比率に合わせる
            '0ImageSize_1ControlFit_2Width_3Height
            ControlForPaintSettingObject.SetInnerSizeByCalcSizeByImageRaito(
                ImageSize, 1)
            'ControlForPaintSettingObject.setInnerControlSize(ImageSize)


            'PictureBoxのSizeをPanelサイズによって変える
            'ControlForPaintSettingObject.setInnerSizeByCalcSizeForFitOuterSize(PaintImageObject.getControlFrame.getSize)

            'PictureBoxのLocationをPanelサイズとPictureBoxサイズによって変える
            'ControlForPaintSettingObject.SetInnerLocationByCalcLocationForWindowResize(
            '    PaintImageObject.getControlFrame.getSize)

            PaintImageObject.setControlForPaintSetting(ControlForPaintSettingObject)
            'apply
            PaintImageObject.applyControlSetting()

        Catch ex As Exception
            Console.WriteLine("UpdatePictureBoxSize")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'PictureBoxSize>PanelSize orWidthHeightどちらかが大きく、他方は等しくない(大きいor小さい)
    'PictureBoxSizeそのままLocationそのまま
    'Size変えない
    'KeepSizeChangeLocation ForPictureBoxIsBig
    '12_12OK
    Public Sub KeepSizeChangeLocation(beforeMP As Point, raito As Double)
        Try
            Dim PaintImageObject As IPaintImage = mViewImageManager.gImageManager.getPaintImage
            'ControlSetting
            Dim ControlForPaintSettingObject As IControlForPaintSetting _
                = New ControlForPaintSetting(
                     PaintImageObject.getControlForPaint.getSize,
                     PaintImageObject.getControlForPaint.getLocation)

            'Location調整
            ControlForPaintSettingObject.resetLocationWhenPictureBoxGreatherThanPanelByMousePointAndPictureBoxSize(
                PaintImageObject.getControlFrame.getSize,
                PaintImageObject.getControlForPaint.getSize,
                PaintImageObject.getControlForPaint.getLocation,
                raito,
                beforeMP,
                PaintImageObject.getControlForPaint.getSizeBeforeSizeChange,
                PaintImageObject.getControlForPaint.getLocationBeforeSizeChange)

            Dim ImageSize As Size = PaintImageObject.getControlForPaint.getSize
            If ImageSize.Equals(New Size(0, 0)) Then
                ImageSize = PaintImageObject.getControlFrame.getSize
            End If
            'PictureBoxのサイズをImageの比率に合わせる
            '0ImageSize_1ControlFit_2Width_3Height
            ControlForPaintSettingObject.SetInnerSizeByCalcSizeByImageRaito(
                ImageSize, 1)


            'PictureBoxのSizeをPanelサイズによって変える
            'ControlForPaintSettingObject.setInnerSizeByCalcSizeForFitOuterSize(PaintImageObject.getControlFrame.getSize)

            'PictureBoxのLocationをPanelサイズとPictureBoxサイズによって変える
            'ControlForPaintSettingObject.SetInnerLocationByCalcLocationForWindowResize(
            '    PaintImageObject.getControlFrame.getSize)
            PaintImageObject.setControlForPaintSetting(ControlForPaintSettingObject)
            'apply
            PaintImageObject.applyControlSetting()
        Catch ex As Exception
            Console.WriteLine("changeSizeForWindowSizeChange")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'とりあえずOK
    '12_12 使用中
    Public Sub changeLocation()
        Try
            Dim PaintImageObject As IPaintImage = mViewImageManager.gImageManager.getPaintImage
            Dim PicBoxSize As Size = PaintImageObject.getControlForPaint.getSize
            'ControlSetting
            Dim ControlForPaintSettingObject As IControlForPaintSetting _
                = New ControlForPaintSetting(
                    PicBoxSize,
                    PaintImageObject.getControlForPaint.getLocation)

            'PictureBoxのサイズをImageの比率に合わせる
            '0ImageSize_1ControlFit_2Width_3Height
            ControlForPaintSettingObject.setInnerControlSize(PicBoxSize)

            'PictureBoxのLocationをPanelサイズとPictureBoxサイズによって変える        
            '0 OuterのTop_0
            '1 Outer_Center
            '2 Outer_Bottom_PanelHeight
            If mViewImageManager.gImageManager.InnerPaintControlSizeIsSmallerThanFrameSize Then
                'PictureBox=Panel
                'PictureBoxWidth=PanelWidth And PictureBoxHeight<=PanelHeight
                'PictureBoxWidth<=PanelWidth and PictureBoxHeight=PanelHeight
                'そのまま
                ControlForPaintSettingObject.SetInnerLocationByCalcLocation(
                    PaintImageObject.getControlFrame.getSize, 1, 1)
            Else
                If mViewImageManager.gImageManager.EitherInnerPaintControlSizesWidthOrHeightIsGreatherThanFrameSize Then
                    'PictureBoxWidth>PanelWidth and PictureBoxHeight>PanelHeight
                    'PictureBoxWidth>PanelWidth and (not PictureBoxHeight=PanelHeight)
                    '(not PictureBoxWidth=PanelWidth) and PictureBoxHeight>PanelHeight
                    'Width Heightどちらか　が大きい
                    'Width Heightどちら　も大きい
                    'Width Heightどちらか　が小さいくて　どちらか　が大きい(等しくない)
                    'どちらも大きいorどちらかが大きい
                    'ControlForPaintSettingObject.SetInnerLocationByCalcLocation(
                    '    PaintImageObject.getControlFrame.getSize, 0, 0)
                Else
                    'PictureBoxがPanelよりWidth Heightどちら　も小さい
                    ControlForPaintSettingObject.SetInnerLocationByCalcLocation(
                        PaintImageObject.getControlFrame.getSize, 1, 1)
                End If
            End If
            PaintImageObject.setControlForPaintSetting(ControlForPaintSettingObject)
            'apply
            PaintImageObject.applyControlSetting()
        Catch ex As Exception
            Console.WriteLine("changeLocation")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

End Class
