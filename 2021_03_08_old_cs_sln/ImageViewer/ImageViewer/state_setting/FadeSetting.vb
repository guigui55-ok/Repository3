Public Class FadeSetting

    Public FadeInTime As Integer 'フェードイン時にかける時間
    Public FadeOutTime As Integer 'フェードアウト時にかける時間
    Public MaxAlphaPercent As Integer = 100 'フェードイン(画像描画)時の透明度の最大値(0で透明、100で非透明)
    Public MinAlphaPercent As Integer = 30 'フェードアウト時の透明度の最小値
    'フェードイン時の透明化の加速度
    'フェードアウト時の透明化の加速度
    Public FadeinBaseSpeed As Integer = 8　'フェードイン時の透明する加速度　等比用
    Public FadeoutBaseSpeed As Integer = 7 'フェードアウト時の透明する加速度　等比用
    Public FadeinFirstAlphaPercent As Integer = 30
    Public FadeoutFirstAlphaPercent As Integer = MaxAlphaPercent
    Public FadeInTimeInterval As Integer = 9 'フェードイン時の描画更新頻度
    Public FadeOutTimeInterbal As Integer = 9 'フェードアウト時の描画更新頻度

    Public ImageFadeIn As Boolean = True
    Public ImageFadeOut As Boolean = True
    Public FadeInOut As Boolean = True
End Class
