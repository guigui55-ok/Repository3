Public Interface IPaintImageEffectSetting
    Sub setAlphaPersent(argAlphaPercent As Double)
    Sub applySettingEffect()
    Sub Dispose()
    Function getImageAttributes() As Imaging.ImageAttributes
End Interface
