Public MustInherit Class AbstractFunction
    'MustOverride Sub New() 'MustOverride + New はNG
    Friend MainProcessorObject As MainProcesser

    Friend Sub New(argMainProcessor As MainProcesser)
        MainProcessorObject = argMainProcessor
    End Sub

    Friend Sub New()

    End Sub

    Friend Sub setMainProcessor(argMainProcessor As MainProcesser)
        MainProcessorObject = argMainProcessor
    End Sub

    'Friend MainProcessorObject As MainProcesser
    'Friend Sub New(argMainProcessor As MainProcesser)
    'Friend Sub New()
    'Friend Sub setMainProcessor(argMainProcessor As MainProcesser)
End Class
