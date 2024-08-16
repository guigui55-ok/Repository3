Public Class PaintFade
    Public FadeStateObject As FadeState
    Public FadeSettingsObject As FadeSetting
    'Public PaintMainBridgeObject As PaintMain

    Private mViewImageManager As ViewImageManager

    Sub New(argViewImageManager As ViewImageManager)
        mViewImageManager = argViewImageManager
        SetObjects(mViewImageManager.gState.gFade, mViewImageManager.gState.gFadeSettings)
    End Sub

    'Sub New(argPaintMainBridgeObject As PaintMain)
    '    PaintMainBridgeObject = argPaintMainBridgeObject
    '    SetObjects(argPaintMainBridgeObject.getFadeState, argPaintMainBridgeObject.getFadeSettings)
    'End Sub

    Public Sub SetObjects(argFadeStateObject As FadeState, argFadeSettingsObject As FadeSetting)
        FadeStateObject = argFadeStateObject
        FadeSettingsObject = argFadeSettingsObject
    End Sub

    Public Sub Dispose()
        Try
            FadeStateObject = Nothing
            FadeSettingsObject = Nothing
            mViewImageManager = Nothing
        Catch ex As Exception
            Console.WriteLine("Dispose")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    'フェードアウトするときは
    'FadeOutBegin = True　として
    'このときFadeOut_Startメソッドを実行
    'FadeOut_Startメソッド終了時に FadeOutBegin=False、FadeOut=Trueとし
    'PictureBoxPaintByFadeOutメソッドを終了条件まで実行
    '上記終了時FadeOut=False、FadeOutEnd=Trueにして
    'FadeOut時の処理があれば処理してFadeOutEnd=Falseにして終了
    Public Sub FadeTriger()
        addLog(10, Me.ToString & ":FadeTriger")
        Try
            '画像切り替えトリガー→FadeOutBegin今の画像→FadeOut→FadeInBegin次の画像→FadeIn→FadeInEnd
            If mViewImageManager.gFunction.gViewer.ControlsImageIsNothing Then
                FadeStateObject.ImageNothing = True
                FadeStateObject.FadeInBegin = True
            Else
                FadeStateObject.ImageNothing = False
            End If
            'is image nothing then fadeInBegin true
            If mViewImageManager.gFunction.gViewer.ControlsImageIsError Then
                FadeStateObject.FadeInBegin = True
            End If

            If Not FadeStateObject.ImageNothing Then
                'フェードアウトはイメージがあるときのみ
                If FadeSettingsObject.ImageFadeOut Then
                    If FadeStateObject.currentAlphaPercent > FadeSettingsObject.MinAlphaPercent Then
                        If FadeStateObject.FadeOut Then
                            'フェードアウト_メイン
                            PictureBoxPaintByFadeOut()
                            FadeStateObject.FadeOut = getFadeOutStateContinue()
                            '開始時用 FadeOut->FadeInBegin
                            If FadeStateObject.currentAlphaPercent <= FadeSettingsObject.MinAlphaPercent Then
                                FadeStateObject.FadeInBegin = True
                            End If
                        End If
                    Else
                        '透明のときandFadeOout_True時は描画しない
                        If FadeStateObject.FadeOut Then
                            'ここは来ないいる？
                            addLog(0, Me.ToString & ": FadeTriger:ここには来ないはず！！")

                            '透明を維持
                            PaintImageAtAlphaZero()
                            'FadeStateObject.FadeOut = getFadeOutStateContinue()
                            FadeStateObject.FadeInBegin = True
                            'Exit Sub
                        End If
                    End If

                    'フェードアウトBegin
                    If FadeStateObject.FadeOutBegin Then
                        PictureBoxPaintByFadeOut_start()
                        FadeStateObject.FadeOut = getFadeOutStateContinue()
                    End If
                    'フェードアウトend
                    If FadeStateObject.FadeOutEnd Then
                        PictureBoxPaintByFadeOut_end()
                    End If
                End If
            End If

            If Not FadeStateObject.isNowFade Then
                FadeStateObject.FadeInBegin = True
            End If

            'フェードインはイメージがある・なし両方
            If FadeSettingsObject.ImageFadeIn Then
                If FadeStateObject.FadeIn Then
                    'フェードイン_Main
                    PictureBoxPaintByFadeIn()
                    FadeStateObject.FadeIn = getFadeInStateContinue()
                    If Not FadeStateObject.FadeIn Then
                        'State.SlideShowPaintEnd()
                        FadeStateObject.FadeInEnd = True
                    End If
                End If

                If FadeStateObject.FadeInEnd Then
                    mViewImageManager.gFunction.gViewer.setTrueFlagPlayMovieBegin()
                    FadeStateObject.FadeInEnd = False
                    'FadeInOut_End
                End If

                If FadeStateObject.FadeInBegin = True Then
                    'フェードイン_Begin
                    FadeStateObject.currentAlphaPercent = FadeSettingsObject.FadeinFirstAlphaPercent
                    PictureBoxPaintByFadeIn_start()
                    FadeStateObject.FadeIn = True
                End If
            End If


            If mViewImageManager.gFunction.gViewer.ControlsImageIsError Then
                addLog(0, "mViewImageManager.gFunction.gViewer.ControlsImageIsError = True")
                FadeStateObject.FadeFlagAllReset()
                'FadeStateObject.FadeInBegin = True
                'mViewImageManager.gFunction.gViewer.PaintImageDefaultNotDispose()
            End If

            If mViewImageManager.gFunction.gViewer.ControlsImageIsNothing Then
                Console.WriteLine("FadeTriger_Image_Is_Nothing")
            End If
        Catch ex As Exception
            Console.WriteLine("FadeTriger")
            Console.WriteLine(ex.Message)
        Finally

        End Try
    End Sub


    'フェードイン続けるか
    Public Function getFadeInStateContinue() As Boolean
        If FadeStateObject.currentAlphaPercent >= FadeSettingsObject.MaxAlphaPercent Then
            getFadeInStateContinue = False
        Else
            getFadeInStateContinue = True
        End If
    End Function

    'フェードアウト続けるか
    Public Function getFadeOutStateContinue() As Boolean
        If FadeStateObject.currentAlphaPercent <= FadeSettingsObject.MinAlphaPercent Then
            getFadeOutStateContinue = False
        Else
            getFadeOutStateContinue = True
        End If
    End Function

    'フェードアウト用　透明描画
    '透明状態を維持する
    Public Sub PaintImageAtAlphaZero()
        FadeStateObject.currentAlphaPercent = 0
    End Sub

    Public Sub SetFadeInBeginWhenImageIsNothing()
        If mViewImageManager.gFunction.gViewer.PaintImageisDrawImageHasImageIsNothing Then
            FadeStateObject.FadeInBegin = True
        End If
    End Sub

    'フェードアウト初回
    Public Sub PictureBoxPaintByFadeOut_start()
        addLog(10, Me.ToString & ":PictureBoxPaintByFadeOut_start")
        'FadeOutBegin->FadeOut
        FadeStateObject.FadeOut = True
        mViewImageManager.gFunction.gViewer.paintImageRefreshSizeAndLocationKeepWithTransparncy()
    End Sub

    'フェードアウト
    Public Sub PictureBoxPaintByFadeOut()
        addLog(10, Me.ToString & ":PictureBoxPaintByFadeOut")
        If FadeStateObject.currentAlphaPercent <= FadeSettingsObject.MinAlphaPercent Then
            Exit Sub
            'FadeStateObject.FadeOut = False
        Else
            FadeStateObject.FadeOut = True
        End If
        FadeStateObject.currentAlphaPercent -= FadeSettingsObject.FadeoutBaseSpeed
        mViewImageManager.gFunction.gViewer.paintImageRefreshSizeAndLocationKeepWithTransparncy(FadeStateObject.currentAlphaPercent)

        System.Threading.Thread.Sleep(FadeSettingsObject.FadeOutTimeInterbal)
    End Sub

    Public Sub PictureBoxPaintByFadeOut_end()
        FadeStateObject.FadeInBegin = True
    End Sub

    'フェードイン初回
    Public Sub PictureBoxPaintByFadeIn_start()
        addLog(10, Me.ToString & ": PictureBoxPaintByFadeIn_start")
        Try
            FadeStateObject.FadeIn = True
            FadeStateObject.currentAlphaPercent = FadeSettingsObject.FadeinFirstAlphaPercent

            mViewImageManager.gState.gFade = FadeStateObject
            mViewImageManager.gFunction.gViewer.paintImageWithTransparncy()
        Catch ex As Exception
            Console.WriteLine("PictureBoxPaintByFadeIn_start")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'フェードイン
    Public Sub PictureBoxPaintByFadeIn()
        addLog(10, Me.ToString & ": PictureBoxPaintByFadeIn")
        If FadeStateObject.currentAlphaPercent >= FadeSettingsObject.MaxAlphaPercent Then
            'FadeStateObject.FadeIn = False
            Exit Sub
        Else
            FadeStateObject.FadeIn = True
        End If
        FadeStateObject.currentAlphaPercent += FadeSettingsObject.FadeinBaseSpeed
        mViewImageManager.gState.gFade = FadeStateObject

        mViewImageManager.gFunction.gViewer.paintImageWithTransparncyWithEffect()
        System.Threading.Thread.Sleep(FadeSettingsObject.FadeInTimeInterval)
    End Sub
End Class
