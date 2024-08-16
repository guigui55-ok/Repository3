Imports ImageViewer

Public Class PlayGifForImageViewer
    Inherits PlayGif
    'Implements IViewImageMain

    Private mViewImageManager As ViewImageManager

    Public Sub New(argViewImageManager As ViewImageManager)
        mViewImageManager = argViewImageManager
        Initialize(mViewImageManager.gImageFileList.getNowFilePath)
    End Sub

End Class
