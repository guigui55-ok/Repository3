Public Class ControlEventsForImageViewer
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
        gContextMenuStrip = New ContextMenuStripFunctionForViewImage(mMainProcessorObject, mViewImageManager, New ContextMenuStrip())
    End Sub

    Public WithEvents PictureBoxEventsObject As PictureBoxEvents
    Public WithEvents MenuStripEventsObject As MenuStripEvents
    Public WithEvents StatusStripEventsObject As StatusStripEvents
    Public WithEvents PanelEventsObject As PanelEvents

    Public WithEvents gContextMenuStrip As ContextMenuStripFunctionForViewImage
    Public MainProcessorObject As MainProcesser


    Public Sub disposeObjects()
        Try
            PictureBoxEventsObject = Nothing
            MenuStripEventsObject = Nothing
            StatusStripEventsObject = Nothing
            PanelEventsObject = Nothing
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub setStatusStrip(argStatusStrip As StatusStrip)
        StatusStripEventsObject = New StatusStripEvents(argStatusStrip, mMainProcessorObject)
    End Sub

    Public Sub setMenuStrip(argMenustrip As MenuStrip)
        MenuStripEventsObject = New MenuStripEvents(mMainProcessorObject)
        MenuStripEventsObject.initializeObjects(argMenustrip, mMainProcessorObject)
    End Sub

    Public Sub setPanel(panel As Panel)
        'PanelEventsObject = New PanelEvents(panel, MainProcessorObject)
    End Sub


    'mMainProcessorObject.PaintMainObject.ChangeFormSize()
    Public Sub ChangeFormSize()
        Try
            If MainProcessorObject.gNowViewImageManager.gState.gMovie.FlagIsMovieMode Then
                Dim changeSize As Size =
                    MainProcessorObject.gNowViewImageManager.gImageManager.getPaintImage.getControlForPaint.getSize
                'PlayMovieFunctionObject.ChangeWindowSize(changeSize)
            End If
        Catch ex As Exception
            addLogForSystemError("ChangeFormSize")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

End Class
