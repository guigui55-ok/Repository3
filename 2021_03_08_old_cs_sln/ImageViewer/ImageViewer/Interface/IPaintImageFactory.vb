Public Interface IPaintImageFactory
    Sub disposeObjects()
    Sub setImageManager(imageManager As ImageManager)
    Sub paintImage(pictureBox As PictureBox, Path As String)
    Sub disposeImage()
    Sub paintImageRefresh()

    Sub DoSetting()
    Sub DoSettingForPaint()
    Sub DoSettingForControl()
    Sub DoSettingForPaintEffect()

    '後で動画等も入る
    'ファイルリストはこっち？
End Interface
