Imports System.Text
Imports MediaFoundation
Imports MediaFoundation.Misc

Public Class MFPresentationDescriptor
    Implements MediaFoundation.IMFPresentationDescriptor

    Public Function GetItem(guidKey As Guid, pValue As PropVariant) As HResult Implements IMFPresentationDescriptor.GetItem
        Throw New NotImplementedException()
    End Function

    Public Function GetItemType(guidKey As Guid, ByRef pType As MFAttributeType) As HResult Implements IMFPresentationDescriptor.GetItemType
        Throw New NotImplementedException()
    End Function

    Public Function CompareItem(guidKey As Guid, Value As ConstPropVariant, ByRef pbResult As Boolean) As HResult Implements IMFPresentationDescriptor.CompareItem
        Throw New NotImplementedException()
    End Function

    Public Function Compare(pTheirs As IMFAttributes, MatchType As MFAttributesMatchType, ByRef pbResult As Boolean) As HResult Implements IMFPresentationDescriptor.Compare
        Throw New NotImplementedException()
    End Function

    Public Function GetUINT32(guidKey As Guid, ByRef punValue As Integer) As HResult Implements IMFPresentationDescriptor.GetUINT32
        Throw New NotImplementedException()
    End Function

    Public Function GetUINT64(guidKey As Guid, ByRef punValue As Long) As HResult Implements IMFPresentationDescriptor.GetUINT64
        Throw New NotImplementedException()
    End Function

    Public Function GetDouble(guidKey As Guid, ByRef pfValue As Double) As HResult Implements IMFPresentationDescriptor.GetDouble
        Throw New NotImplementedException()
    End Function

    Public Function GetGUID(guidKey As Guid, ByRef pguidValue As Guid) As HResult Implements IMFPresentationDescriptor.GetGUID
        Throw New NotImplementedException()
    End Function

    Public Function GetStringLength(guidKey As Guid, ByRef pcchLength As Integer) As HResult Implements IMFPresentationDescriptor.GetStringLength
        Throw New NotImplementedException()
    End Function

    Public Function GetString(guidKey As Guid, pwszValue As StringBuilder, cchBufSize As Integer, ByRef pcchLength As Integer) As HResult Implements IMFPresentationDescriptor.GetString
        Throw New NotImplementedException()
    End Function

    Public Function GetAllocatedString(guidKey As Guid, ByRef ppwszValue As String, ByRef pcchLength As Integer) As HResult Implements IMFPresentationDescriptor.GetAllocatedString
        Throw New NotImplementedException()
    End Function

    Public Function GetBlobSize(guidKey As Guid, ByRef pcbBlobSize As Integer) As HResult Implements IMFPresentationDescriptor.GetBlobSize
        Throw New NotImplementedException()
    End Function

    Public Function GetBlob(guidKey As Guid, pBuf() As Byte, cbBufSize As Integer, ByRef pcbBlobSize As Integer) As HResult Implements IMFPresentationDescriptor.GetBlob
        Throw New NotImplementedException()
    End Function

    Public Function GetAllocatedBlob(guidKey As Guid, ByRef ip As IntPtr, ByRef pcbSize As Integer) As HResult Implements IMFPresentationDescriptor.GetAllocatedBlob
        Throw New NotImplementedException()
    End Function

    Public Function GetUnknown(guidKey As Guid, riid As Guid, ByRef ppv As Object) As HResult Implements IMFPresentationDescriptor.GetUnknown
        Throw New NotImplementedException()
    End Function

    Public Function SetItem(guidKey As Guid, Value As ConstPropVariant) As HResult Implements IMFPresentationDescriptor.SetItem
        Throw New NotImplementedException()
    End Function

    Public Function DeleteItem(guidKey As Guid) As HResult Implements IMFPresentationDescriptor.DeleteItem
        Throw New NotImplementedException()
    End Function

    Public Function DeleteAllItems() As HResult Implements IMFPresentationDescriptor.DeleteAllItems
        Throw New NotImplementedException()
    End Function

    Public Function SetUINT32(guidKey As Guid, unValue As Integer) As HResult Implements IMFPresentationDescriptor.SetUINT32
        Throw New NotImplementedException()
    End Function

    Public Function SetUINT64(guidKey As Guid, unValue As Long) As HResult Implements IMFPresentationDescriptor.SetUINT64
        Throw New NotImplementedException()
    End Function

    Public Function SetDouble(guidKey As Guid, fValue As Double) As HResult Implements IMFPresentationDescriptor.SetDouble
        Throw New NotImplementedException()
    End Function

    Public Function SetGUID(guidKey As Guid, guidValue As Guid) As HResult Implements IMFPresentationDescriptor.SetGUID
        Throw New NotImplementedException()
    End Function

    Public Function SetString(guidKey As Guid, wszValue As String) As HResult Implements IMFPresentationDescriptor.SetString
        Throw New NotImplementedException()
    End Function

    Public Function SetBlob(guidKey As Guid, pBuf() As Byte, cbBufSize As Integer) As HResult Implements IMFPresentationDescriptor.SetBlob
        Throw New NotImplementedException()
    End Function

    Public Function SetUnknown(guidKey As Guid, pUnknown As Object) As HResult Implements IMFPresentationDescriptor.SetUnknown
        Throw New NotImplementedException()
    End Function

    Public Function LockStore() As HResult Implements IMFPresentationDescriptor.LockStore
        Throw New NotImplementedException()
    End Function

    Public Function UnlockStore() As HResult Implements IMFPresentationDescriptor.UnlockStore
        Throw New NotImplementedException()
    End Function

    Public Function GetCount(ByRef pcItems As Integer) As HResult Implements IMFPresentationDescriptor.GetCount
        Throw New NotImplementedException()
    End Function

    Public Function GetItemByIndex(unIndex As Integer, ByRef pguidKey As Guid, pValue As PropVariant) As HResult Implements IMFPresentationDescriptor.GetItemByIndex
        Throw New NotImplementedException()
    End Function

    Public Function CopyAllItems(pDest As IMFAttributes) As HResult Implements IMFPresentationDescriptor.CopyAllItems
        Throw New NotImplementedException()
    End Function

    Public Function GetStreamDescriptorCount(ByRef pdwDescriptorCount As Integer) As HResult Implements IMFPresentationDescriptor.GetStreamDescriptorCount
        Throw New NotImplementedException()
    End Function

    Public Function GetStreamDescriptorByIndex(dwIndex As Integer, ByRef pfSelected As Boolean, ByRef ppDescriptor As IMFStreamDescriptor) As HResult Implements IMFPresentationDescriptor.GetStreamDescriptorByIndex
        Throw New NotImplementedException()
    End Function

    Public Function SelectStream(dwDescriptorIndex As Integer) As HResult Implements IMFPresentationDescriptor.SelectStream
        Throw New NotImplementedException()
    End Function

    Public Function DeselectStream(dwDescriptorIndex As Integer) As HResult Implements IMFPresentationDescriptor.DeselectStream
        Throw New NotImplementedException()
    End Function

    Public Function Clone(ByRef ppPresentationDescriptor As IMFPresentationDescriptor) As HResult Implements IMFPresentationDescriptor.Clone
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_GetItem(guidKey As Guid, pValue As PropVariant) As HResult Implements IMFAttributes.GetItem
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_GetItemType(guidKey As Guid, ByRef pType As MFAttributeType) As HResult Implements IMFAttributes.GetItemType
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_CompareItem(guidKey As Guid, Value As ConstPropVariant, ByRef pbResult As Boolean) As HResult Implements IMFAttributes.CompareItem
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_Compare(pTheirs As IMFAttributes, MatchType As MFAttributesMatchType, ByRef pbResult As Boolean) As HResult Implements IMFAttributes.Compare
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_GetUINT32(guidKey As Guid, ByRef punValue As Integer) As HResult Implements IMFAttributes.GetUINT32
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_GetUINT64(guidKey As Guid, ByRef punValue As Long) As HResult Implements IMFAttributes.GetUINT64
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_GetDouble(guidKey As Guid, ByRef pfValue As Double) As HResult Implements IMFAttributes.GetDouble
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_GetGUID(guidKey As Guid, ByRef pguidValue As Guid) As HResult Implements IMFAttributes.GetGUID
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_GetStringLength(guidKey As Guid, ByRef pcchLength As Integer) As HResult Implements IMFAttributes.GetStringLength
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_GetString(guidKey As Guid, pwszValue As StringBuilder, cchBufSize As Integer, ByRef pcchLength As Integer) As HResult Implements IMFAttributes.GetString
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_GetAllocatedString(guidKey As Guid, ByRef ppwszValue As String, ByRef pcchLength As Integer) As HResult Implements IMFAttributes.GetAllocatedString
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_GetBlobSize(guidKey As Guid, ByRef pcbBlobSize As Integer) As HResult Implements IMFAttributes.GetBlobSize
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_GetBlob(guidKey As Guid, pBuf() As Byte, cbBufSize As Integer, ByRef pcbBlobSize As Integer) As HResult Implements IMFAttributes.GetBlob
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_GetAllocatedBlob(guidKey As Guid, ByRef ip As IntPtr, ByRef pcbSize As Integer) As HResult Implements IMFAttributes.GetAllocatedBlob
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_GetUnknown(guidKey As Guid, riid As Guid, ByRef ppv As Object) As HResult Implements IMFAttributes.GetUnknown
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_SetItem(guidKey As Guid, Value As ConstPropVariant) As HResult Implements IMFAttributes.SetItem
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_DeleteItem(guidKey As Guid) As HResult Implements IMFAttributes.DeleteItem
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_DeleteAllItems() As HResult Implements IMFAttributes.DeleteAllItems
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_SetUINT32(guidKey As Guid, unValue As Integer) As HResult Implements IMFAttributes.SetUINT32
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_SetUINT64(guidKey As Guid, unValue As Long) As HResult Implements IMFAttributes.SetUINT64
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_SetDouble(guidKey As Guid, fValue As Double) As HResult Implements IMFAttributes.SetDouble
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_SetGUID(guidKey As Guid, guidValue As Guid) As HResult Implements IMFAttributes.SetGUID
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_SetString(guidKey As Guid, wszValue As String) As HResult Implements IMFAttributes.SetString
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_SetBlob(guidKey As Guid, pBuf() As Byte, cbBufSize As Integer) As HResult Implements IMFAttributes.SetBlob
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_SetUnknown(guidKey As Guid, pUnknown As Object) As HResult Implements IMFAttributes.SetUnknown
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_LockStore() As HResult Implements IMFAttributes.LockStore
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_UnlockStore() As HResult Implements IMFAttributes.UnlockStore
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_GetCount(ByRef pcItems As Integer) As HResult Implements IMFAttributes.GetCount
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_GetItemByIndex(unIndex As Integer, ByRef pguidKey As Guid, pValue As PropVariant) As HResult Implements IMFAttributes.GetItemByIndex
        Throw New NotImplementedException()
    End Function

    Private Function IMFAttributes_CopyAllItems(pDest As IMFAttributes) As HResult Implements IMFAttributes.CopyAllItems
        Throw New NotImplementedException()
    End Function
End Class
