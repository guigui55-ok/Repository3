Public Interface IDrawImage
    'Private DrawImage As Image
    Sub setImageFromPath(path As String)
    Sub setImage(image As Image)
    Function getImageSize() As PointF()
    Function getImage() As Image
    Sub disposeImage()
    Function width() As Integer
    Function height() As Integer
    Function getSize() As Size
    Function hasImageIsNothing() As Boolean
End Interface
