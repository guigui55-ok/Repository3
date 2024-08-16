Public Class PaintSettingTemplate
    Inherits AbstractImageViewerChild

    'Friend mMainProcessorObject As MainProcesser
    'Friend mViewImageManager As ViewImageManager
    'Friend Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
    'Friend Sub New()
    'Friend Sub setMainProcessor(argMainProcessor As MainProcesser)
    'Friend Sub setViewImageManager(argViewImageManager As ViewImageManager)
    '=======================================================================
    Public Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
        MyBase.New(argMainProcessor, argViewImageManager)
        mMainProcessorObject = argMainProcessor
        mViewImageManager = argViewImageManager
    End Sub

    '※設定テンプレート

    Public Sub PaintImageSettingDefault(PaintImageObject As IPaintImage)
        Try
            'PaintSetting
            Dim PaintImageSettingObject As IPaintImageSetting = PaintImageObject.getPaintImageSetting()
            PaintImageSettingObject.setPosition(0, 0)
            PaintImageSettingObject.setInnerFrameSize(PaintImageObject.getControlForPaint.getSize)
            PaintImageSettingObject.setImageSize(PaintImageObject.getDrawImage.getSize)
        Catch ex As Exception
            Console.WriteLine("PaintImageSettingDefault")
            Console.WriteLine(ex.Message)
        End Try
    End Sub




    Public Sub paintImageDefault()
        Try
            'If ViewFileListObject Is Nothing Then Exit Sub
            'ImageManagerObject.setImagePath(ViewFileListObject.getNowFilePath)
            'PaintActionObject.paintImageDefault()
            Dim PaintImageObject As IPaintImage = mViewImageManager.gImageManager.getPaintImage

            'ImagePath 'Private
            'ImageManagerObject.setPath(path) '外部でパスセット
            'MainProcessorObject .ImageManagerObject.setImagePath(MainProcessorObject .ViewFileListObject.getNowFilePath)
            'Setting<-Value

            'PaintSetting
            Dim PaintImageSettingObject As IPaintImageSetting = New PaintImageSettingNoMargin()
            PaintImageSettingObject.setPosition(0, 0)
            PaintImageSettingObject.setInnerFrameSize(PaintImageObject.getControlForPaint.getSize)
            PaintImageSettingObject.setImageSize(PaintImageObject.getDrawImage.getSize)

            'ControlSetting
            Dim ControlForPaintSettingObject As IControlForPaintSetting = PaintImageObject.getControlForPaintSetting
            ControlForPaintSettingObject.setInnerControlLocation(
                PaintImageObject.getControlFrame.getLocation
            )
            ControlForPaintSettingObject.setInnerControlSize(
                PaintImageObject.getControlFrame.getSize
            )
            'DoSetting
            'PictureBoxのサイズをImageの比率に合わせる
            '0ImageSize_1ControlFit_2Width_3Height
            ControlForPaintSettingObject.SetInnerSizeByCalcSizeByImageRaito(PaintImageObject.getDrawImage.getSize, 0)
            'PictureBoxのサイズをPanelのサイズによって変える
            ControlForPaintSettingObject.setInnerSizeByCalcSizeForFitOuterSize(PaintImageObject.getControlFrame.getSize)
            'PictureBoxのLocationをPanelサイズとPictureBoxサイズによって変える
            '0top_1Center_2Bottom
            'ControlForPaintSettingObject.SetInnerLocationByCalcLocation(PaintImageObject.getControlFrame.getSize, 1, 1)
            'LocationをPanelサイズによって変更
            ControlForPaintSettingObject.SetInnerLocationByCalcLocationForWindowResize(PaintImageObject.getControlFrame.getSize)

            PaintImageSettingObject.setInnerFrameSize(ControlForPaintSettingObject.getInnerControlSize)
            PaintImageSettingObject.calcRegionDefaultToReadAndDraw()
            'paintImage<-Setting
            PaintImageObject.setPaintImageSetting(PaintImageSettingObject)
            PaintImageObject.setControlForPaintSetting(ControlForPaintSettingObject)

            'setPanelSize()
        Catch ex As Exception
            Console.WriteLine("paintImageDefaultNew")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'Private Sub setPanelSize()
    '    Try
    '        Dim picBox As PictureBox = mViewImageManager.gImageManager.getPaintImage.getControlForPaint.getControlBridge.getControl
    '        Dim ppanel As Panel = mViewImageManager.gImageManager.getPaintImage.getControlFrame.getControl.getControl
    '        Dim pStatusStrip As StatusStrip = mMainProcessorObject.gMainForm.gControls.gStatusStripEvents.getStatusStrip

    '        Dim pMainForm As Form = mMainProcessorObject.gMainForm.getForm
    '        Dim scrollBarSize As Integer = 0
    '        ppanel.Size = New Size(pMainForm.Width - scrollBarSize, pMainForm.Height - pStatusStrip.Height - (scrollBarSize * 3))
    '        'MainProcessorObject.ImageManagerObject.getPaintImage.getControlForPaint.setSize(ppanel.Size)

    '        pStatusStrip.Visible = True
    '        pStatusStrip.Width = pMainForm.Width - scrollBarSize


    '    Catch ex As Exception
    '        Console.WriteLine("paintImageDefaultNew")
    '        Console.WriteLine(ex.Message)
    '    End Try
    'End Sub

    Public Sub paintImageDefaultOld()
        Try
            Dim PaintImageObject As IPaintImage = mViewImageManager.gImageManager.getPaintImage

            'ImagePath 'Private
            'ImageManagerObject.setPath(path) '外部でパスセット
            'MainProcessorObject .ImageManagerObject.setImagePath(MainProcessorObject .ViewFileListObject.getNowFilePath)
            'Setting<-Value
            'PaintSetting
            Dim PaintImageSettingObject As IPaintImageSetting = PaintImageObject.getPaintImageSetting()
            PaintImageSettingDefault(PaintImageObject)
            'ControlSetting
            Dim ControlForPaintSettingObject As IControlForPaintSetting = PaintImageObject.getControlForPaintSetting
            ControlForPaintSettingObject.setInnerControlLocation(
                PaintImageObject.getControlFrame.getLocation
            )
            ControlForPaintSettingObject.setInnerControlSize(
                PaintImageObject.getControlFrame.getSize
            )
            'DoSetting
            PaintImageSettingObject.calcRegionDefaultToReadAndDraw()
            'paintImage<-Setting
            PaintImageObject.setPaintImageSetting(PaintImageSettingObject)
            PaintImageObject.setControlForPaintSetting(ControlForPaintSettingObject)
            'PaintImage
            'PaintImageObject.paintImageOnly()
        Catch ex As Exception
            Console.WriteLine("paintImageDefault")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'SizeAndLocationKeep
    Public Sub paintImageRefreshSizeAndLocationKeep()
        Dim PaintImageSettingObject As IPaintImageSetting
        Dim ControlForPaintSettingObject As IControlForPaintSetting
        Dim PaintImageObject As IPaintImage = mViewImageManager.gImageManager.getPaintImage
        Try

            'ImagePath 'Private
            'ImageManagerObject.setPath(path) '外部でパスセット
            'MainProcessorObject .ImageManagerObject.setImagePath(MainProcessorObject .ViewFileListObject.getNowFilePath)
            'Setting<-Value
            'PaintSetting
            PaintImageSettingObject = PaintImageObject.getPaintImageSetting()
            PaintImageSettingDefault(PaintImageObject)
            'ControlSetting
            ControlForPaintSettingObject = PaintImageObject.getControlForPaintSetting
            ControlForPaintSettingObject.setInnerControlLocation(
                PaintImageObject.getControlForPaint.getLocation
            )
            ControlForPaintSettingObject.setInnerControlSize(
                PaintImageObject.getControlForPaint.getSize
            )
            'DoSetting
            PaintImageSettingObject.calcRegionDefaultToReadAndDraw()
            'paintImage<-Setting
            PaintImageObject.setPaintImageSetting(PaintImageSettingObject)
            PaintImageObject.setControlForPaintSetting(ControlForPaintSettingObject)
            'PaintImage
            'PaintImageObject.paintImageOnly()
        Catch ex As Exception
            Console.WriteLine("paintImageRefreshSizeAndLocationKeep")
            Console.WriteLine(ex.Message)
        Finally
            'PaintImageSettingObject = Nothing
            'ControlForPaintSettingObject = Nothing
            'PaintImageObject = Nothing
        End Try
    End Sub

    '引数argSizeのWidthとHeightを入れ替えたSizeを取得
    Private Function getSizeReplaceWidthToHeight(argSize As Size) As Size
        Try
            Return New Size(argSize.Height, argSize.Width)
        Catch ex As Exception
            Console.WriteLine("getSizeReplaceWidthToHeight")
            Console.WriteLine(ex.Message)
            Return argSize
        End Try
    End Function

    Public Sub paintImageRoTation90degree()
        Dim PaintImageSettingObject As IPaintImageSetting
        Dim ControlForPaintSettingObject As IControlForPaintSetting
        Dim PaintImageObject As IPaintImage = mViewImageManager.gImageManager.getPaintImage
        Dim InnerSize As Size
        Try
            'WidthとHeightを入れ替え_90度回転なので
            InnerSize = Me.getSizeReplaceWidthToHeight(PaintImageObject.getControlForPaint.getSize)
            'ImagePath 'Private
            'ImageManagerObject.setPath(path) '外部でパスセット
            'MainProcessorObject .ImageManagerObject.setImagePath(MainProcessorObject .ViewFileListObject.getNowFilePath)
            'Setting<-Value
            'PaintSetting
            PaintImageSettingObject = New PaintImageSettingChangeRotation()
            'PaintImageSettingObject = PaintImageObject.getPaintImageSetting()
            PaintImageSettingObject.setPosition(0, 0)
            'PaintImageSettingObject.setInnerFrameSize(PaintImageObject.getControlForPaint.getSize)
            'PaintImageSettingObject.setInnerFrameSize(InnerSize)
            'WidthとHeightを入れ替え_90度回転なので
            PaintImageSettingObject.setInnerFrameSize(InnerSize)
            PaintImageSettingObject.setImageSize(InnerSize)

            PaintImageSettingObject.setImageSize(PaintImageObject.getDrawImage.getSize)
            'PaintSetting_DoSetting
            PaintImageSettingObject.calcRegionDefaultToReadAndDraw()

            'ControlSetting
            'WidthとHeightを入れ替え_90度回転なので
            ControlForPaintSettingObject = PaintImageObject.getControlForPaintSetting
            ControlForPaintSettingObject.setInnerControlLocation(
                PaintImageObject.getControlForPaint.getLocation
            )
            ControlForPaintSettingObject.setInnerControlSize(
                Me.getSizeReplaceWidthToHeight(PaintImageObject.getControlForPaint.getSize)
            )
            'paintImage<-Setting
            PaintImageObject.setPaintImageSetting(PaintImageSettingObject)
            PaintImageObject.setControlForPaintSetting(ControlForPaintSettingObject)
            'PaintImage
            'PaintImageObject.paintImageOnly()

            If mMainProcessorObject.gState.gDebugFlag = 1 Then
                'Stop
            End If

        Catch ex As Exception
            Console.WriteLine("paintImageRefreshSizeAndLocationKeep")
            Console.WriteLine(ex.Message)
        Finally
            'PaintImageSettingObject = Nothing
            'ControlForPaintSettingObject = Nothing
            'PaintImageObject = Nothing
        End Try
    End Sub
    'paintImageNotDispose

End Class
