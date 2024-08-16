Imports System.Drawing.Imaging
Imports ImageViewer

Public Class PaintImageEffectSetting
    Implements IPaintImageEffectSetting
    Implements IDisposable

    '===============================================================================================
    Private isDisposed As Boolean = False ' リソースが破棄(解放)されていることを表すフラグ

    ' IDisposable.Disposeの実装
    '// Dispose() calls Dispose(true)
    Public Sub Dispose() Implements IPaintImageEffectSetting.Dispose, IDisposable.Dispose
        ' アンマネージリソースと、マネージリソースの両方を破棄させる
        Dispose(True)
        ' すべてのリソースが破棄されているため、以後ファイナライザの実行は不要であることをガベージコレクタに通知する
        GC.SuppressFinalize(Me)
    End Sub
    ' リソースの解放処理を行うためのメソッド
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        ' 既にリソースが破棄されている場合は何もしない
        If isDisposed Then Return

        ' 破棄されていないアンマネージリソースの解放処理を行う
        DrawImageAttributes_Dispose()
        ' リソースは破棄されている
        isDisposed = True
    End Sub
    'Finalize
    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
    '===============================================================================================
    Private Sub DrawImageAttributes_Dispose()
        Try
            If Not DrawImageAttributes Is Nothing Then
                DrawImageAttributes.Dispose()
                DrawImageAttributes = Nothing
            End If
            'DrawImageAttributes.Dispose()
        Catch ex As Exception
            Console.WriteLine("DrawImageAttributes_Dispose")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    '===============================================================================================

    Private AlphaPercent As Double = 100
    Private DrawImageAttributes As Imaging.ImageAttributes

    Public Sub New()
        setColorMatrix(AlphaPercent)
    End Sub

    Public Sub New(argAlphaPercent As Double)
        AlphaPercent = argAlphaPercent
        setColorMatrix(argAlphaPercent)
    End Sub

    Public Sub setAlphaPersent(argAlphaPercent As Double) Implements IPaintImageEffectSetting.setAlphaPersent
        AlphaPercent = argAlphaPercent
    End Sub

    Public Sub applySettingEffect() Implements IPaintImageEffectSetting.applySettingEffect
        Console.WriteLine(Me.ToString & ".applySettingEffect.Throw New NotImplementedException()")
    End Sub

    Public Function getImageAttributes() As Imaging.ImageAttributes Implements IPaintImageEffectSetting.getImageAttributes
        Return DrawImageAttributes
    End Function



    Public Sub setColorMatrix(alpha As Double)
        Try
            DrawImageAttributes_Dispose()
            DrawImageAttributes = New Imaging.ImageAttributes()
            DrawImageAttributes.SetColorMatrix(getColorMatrix(alpha))
        Catch ex As Exception
            Console.WriteLine("setColorMatrix")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Function getColorMatrix(argMatrix33 As Double) As Imaging.ColorMatrix
        Dim ColorMatrix As Imaging.ColorMatrix = New Imaging.ColorMatrix()
        Try
            ColorMatrix.Matrix00 = 1
            ColorMatrix.Matrix11 = 1
            ColorMatrix.Matrix22 = 1
            ColorMatrix.Matrix33 = argMatrix33 * 0.01F
            ColorMatrix.Matrix44 = 1
            Return ColorMatrix
        Catch ex As Exception
            Console.WriteLine("getColorMatrix")
            Console.WriteLine(ex.Message)
            Return ColorMatrix
        Finally
            If Not ColorMatrix Is Nothing Then

            End If
            ColorMatrix = Nothing
        End Try
    End Function

End Class
