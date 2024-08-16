Imports ImageViewer

Public Class ViewMainDefault
    Implements IViewImageMain

    'Public gViewImageManager As ViewImageManager
    Public gMovieState As MovieState
    Private mViewImageManager As ViewImageManager

    Public Sub New()

    End Sub

    Public Sub New(argViewImageManager As ViewImageManager)
        mViewImageManager = argViewImageManager
        gMovieState = New MovieState()
        gMovieState.resetAll()
        addLog(3, "ViewMainDefault New")
    End Sub

    Public Sub excuteViewImage() Implements IViewImageMain.excuteViewImage
        addLog(3, "ViewMainDefault.excuteViewImage")
        'mViewImageManager.gImageManager.setDrawImage(mViewImageManager.gImageManager.getPaintImage.getDrawImage)
        'mViewImageManager.gImageManager.getPaintImage.setDrawImage(mViewImageManager.gImageManager.getPaintImage.getDrawImage)
        'paintImageDefault()
        'mViewImageManager.gImageManager.paintImageNotDispose(mViewImageManager.gImageManager.getPaintImage.getDrawImage.getImage)

        If mViewImageManager.gImageFileList Is Nothing Then Exit Sub
        mViewImageManager.gImageManager.setImagePath(mViewImageManager.gImageFileList.getNowFilePath)
        mViewImageManager.gFunction.gViewer.gPaintImageSettingTemplate.paintImageDefault()
        mViewImageManager.gImageManager.paintImageOnlyNotSetting()

        '静止画再生クラスなので表示が終わったらMovieStateはリセットする
        addLog(3, "ViewMainDefault.gMovieState.resetAll")
        gMovieState.resetAll()
        mViewImageManager.gState.gMovie.resetAll()
    End Sub
    '===========================================================================

    Public Sub Dispose() Implements IViewImageMain.Dispose
        'Me.StopFile()
        'Me.timerStop()
        If gMovieState Is Nothing Then Exit Sub
        gMovieState.resetAll()
        mViewImageManager.gState.gMovie.resetAll()
    End Sub

    Public Sub setImage(argImage As Image) Implements IViewImageMain.setImage
        Throw New NotImplementedException()
    End Sub

    Public Sub setImageFormPath(argPath As String) Implements IViewImageMain.setImageFormPath
        Throw New NotImplementedException()
    End Sub

    Public Sub setControlBridge(argControlBridge As AbstractControlBridge) Implements IViewImageMain.setControlBridge
        Throw New NotImplementedException()
    End Sub

    Public Sub setDrawSize(argSize As Size) Implements IViewImageMain.setDrawSize
        Throw New NotImplementedException()
    End Sub

    Public Sub PlayFile() Implements IViewImageMain.PlayFile
        Throw New NotImplementedException()
    End Sub

    Public Sub PlayFileEvent(sender As Object, e As EventArgs) Implements IViewImageMain.PlayFileEvent
        Throw New NotImplementedException()
    End Sub

    Public Sub ReplayFile(argControlSizeForPlay As Size) Implements IViewImageMain.ReplayFile
        Throw New NotImplementedException()
    End Sub

    Public Sub ReplayFile(argControlSizeForPlay As Size, PlayFrameNumber As Integer) Implements IViewImageMain.ReplayFile
        Throw New NotImplementedException()
    End Sub

    Public Sub StopFile() Implements IViewImageMain.StopFile
        Throw New NotImplementedException()
    End Sub

    Public Sub PauseFile() Implements IViewImageMain.PauseFile
        'Throw New NotImplementedException()
        addLog(20, Me.ToString & ".PauseFile")
    End Sub

    Public Sub PauseFile(argControlSizeForPlay As Size) Implements IViewImageMain.PauseFile
        Throw New NotImplementedException()
    End Sub

    Public Sub ForwardFile() Implements IViewImageMain.ForwardFile
        Throw New NotImplementedException()
    End Sub

    Public Sub RewindFile() Implements IViewImageMain.RewindFile
        Throw New NotImplementedException()
    End Sub

    Public Sub ChangeWindowSize() Implements IViewImageMain.ChangeWindowSize
        Throw New NotImplementedException()
    End Sub

    Public Sub setTimer(argTimer As Timer) Implements IViewImageMain.setTimer
        Throw New NotImplementedException()
    End Sub

    Public Sub TimerStart() Implements IViewImageMain.TimerStart
        Throw New NotImplementedException()
    End Sub

    Public Sub timerStop() Implements IViewImageMain.timerStop
        Throw New NotImplementedException()
    End Sub
    '===========================================================================

    'normal default
    'これだと　もとImageの比率が維持されない
    Private Sub paintImageTest(argImage As Image)
        Try
            Dim PaintImage As IPaintImage = mViewImageManager.gImageManager.getPaintImage
            'ImagePathをセット
            PaintImage.setDrawImage(New DrawImage(argImage))
            '描画領域等の設定
            PaintImage.DoSetting()
            '描画
            PaintImage.applySetting()
            'Controlへ
            PaintImage.paintImage()
        Catch ex As Exception
            Console.WriteLine("paintImage_Image")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    '①gViewImageManager.gImageManager.getPaintImage>getControlForPaintSetting　でコントロールの設定を保持している
    '②IPaintImageSettingに任意のクラスをセット　PaintImageSettingNoMargin
    '　PanelとPictureBoxとImageサイズそれぞれから描画する領域と位置を設定
    Private Sub paintImageDefault()
        Try
            'ImageManagerObject.setPath(path) '外部でパスセット
            Dim PaintImageObject As IPaintImage = mViewImageManager.gImageManager.getPaintImage

            'Setting<-Value
            '①ControlSetting
            'Public Class PaintImageAsPictureBoxInPanel Implements IPaintImage
            '　->Private ControlForPaintSettingObject As IControlForPaintSetting
            Dim ControlForPaintSettingObject As IControlForPaintSetting = PaintImageObject.getControlForPaintSetting
            ControlForPaintSettingObject.setInnerControlLocation(
                PaintImageObject.getControlFrame.getLocation
            )
            ControlForPaintSettingObject.setInnerControlSize(
                PaintImageObject.getControlFrame.getSize
            )
            'Check
            If PaintImageObject.getDrawImage.hasImageIsNothing Then
                addLog(0, "ViewMainDefault.paintImageDefault : Image hasImageIsNothing")
                Exit Sub
            End If

            'DoSetting
            'PictureBoxのサイズをImageの比率に合わせる
            '0ImageSize_1ControlFit_2Width_3Height
            ControlForPaintSettingObject.SetInnerSizeByCalcSizeByImageRaito(PaintImageObject.getDrawImage.getSize, 0)
            'PictureBoxのサイズをPanelのサイズによって変える
            ControlForPaintSettingObject.setInnerSizeByCalcSizeForFitOuterSize(PaintImageObject.getControlFrame.getSize)
            'PictureBoxのLocationをPanelサイズとPictureBoxサイズによって変える
            '0top_1Center_2Bottom
            'LocationをPanelサイズによって変更
            ControlForPaintSettingObject.SetInnerLocationByCalcLocationForWindowResize(PaintImageObject.getControlFrame.getSize)

            '②PaintSetting
            Dim PaintImageSettingObject As IPaintImageSetting = New PaintImageSettingNoMargin()
            PaintImageSettingObject.setPosition(0, 0)
            PaintImageSettingObject.setInnerFrameSize(PaintImageObject.getControlForPaint.getSize)
            PaintImageSettingObject.setImageSize(PaintImageObject.getDrawImage.getSize)
            PaintImageSettingObject.setInnerFrameSize(ControlForPaintSettingObject.getInnerControlSize)
            PaintImageSettingObject.calcRegionDefaultToReadAndDraw()
            PaintImageObject.setPaintImageSetting(PaintImageSettingObject)
            'paintImage<-Setting
            PaintImageObject.setControlForPaintSetting(ControlForPaintSettingObject)


        Catch ex As Exception
            Console.WriteLine("paintImageDefaultNew")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Function getCountFrame(argImage As Image) As Integer Implements IViewImageMain.getCountFrame
        Throw New NotImplementedException()
    End Function

    Public Function isThisClassType(argPath As String) As Boolean Implements IViewImageMain.isThisClassType
        Throw New NotImplementedException()
    End Function

    Public Function isMovie(argPath As String) As Boolean Implements IViewImageMain.isMovie
        Throw New NotImplementedException()
    End Function

    Public Function isEndFrameNow() As Boolean Implements IViewImageMain.isEndFrameNow
        Throw New NotImplementedException()
    End Function

    Public Function getFrameNow() As Integer Implements IViewImageMain.getFrameNow
        Throw New NotImplementedException()
    End Function

    Public Function getDrawImage() As Image Implements IViewImageMain.getDrawImage
        Throw New NotImplementedException()
    End Function
End Class
