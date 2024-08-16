Public Class FadeState

    '現在の透明度
    Friend currentAlphaPercent As Integer = 0
    Public ImageNothing As Boolean = False

    Public FadeInNow As Boolean = False
    Public FadeOutNow As Boolean = False
    Public FadeInEndNow As Boolean = False
    Public FadeOutEndNow As Boolean = False
    Public FadeInBeginNow As Boolean = False
    Public FadeOutBeginNow As Boolean = False

    'フェードインアウト機能スイッチ
    Public FadeInOutNow As Boolean = False



    Public Property FadeOut() As Boolean
        Get
            Return FadeOutNow
        End Get
        Set(value As Boolean)
            If value Then
                FadeFlagAllReset()
                FadeOutNow = value
            End If
        End Set
    End Property

    Public Property FadeIn() As Boolean
        Get
            Return FadeInNow
        End Get
        Set(value As Boolean)
            If value Then
                FadeFlagAllReset()
            End If
            FadeInNow = value
        End Set
    End Property

    Public Property FadeOutBegin() As Boolean
        Get
            Return FadeOutBeginNow
        End Get
        Set(value As Boolean)
            If value Then
                FadeFlagAllReset()
                FadeOutBeginNow = value
            End If
        End Set
    End Property

    Public Property FadeOutEnd() As Boolean
        Get
            Return FadeOutEndNow
        End Get
        Set(value As Boolean)
            If value Then
                FadeFlagAllReset()
                FadeOutEndNow = value
            End If
        End Set
    End Property

    Public Property FadeInBegin() As Boolean
        Get
            Return FadeInBeginNow
        End Get
        Set(value As Boolean)
            If value Then
                FadeFlagAllReset()
                FadeInBeginNow = value
            End If
        End Set
    End Property

    Public Property FadeInEnd() As Boolean
        Get
            Return FadeInEndNow
        End Get
        Set(value As Boolean)
            If value Then
                FadeFlagAllReset()
            End If
            FadeInEndNow = value
        End Set
    End Property

    '====================================================================================
    Public Sub FadeFlagAllReset()
        FadeOutNow = False
        FadeInNow = False
        FadeOutBeginNow = False
        FadeInBeginNow = False
        FadeInEndNow = False
        FadeOutEndNow = False
    End Sub

    Public Function AllFadeFlagIsFalse() As Boolean
        'すべてFalseならTrue
        If (FadeInNow And FadeOutNow And FadeInEndNow And FadeOutEndNow And FadeInBeginNow And FadeOutBeginNow) Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Function isNowFade() As Boolean
        'どれかがTrueならTrue
        If (FadeInNow Or FadeOutNow Or FadeInEndNow Or FadeOutEndNow Or FadeInBeginNow Or FadeOutBeginNow) Then
            Return True
        Else
            Return False
        End If
    End Function
    '====================================================================================
End Class
