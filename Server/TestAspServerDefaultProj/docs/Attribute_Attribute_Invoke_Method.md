https://learn.microsoft.com/ja-jp/dotnet/api/system.attribute.system-runtime-interopservices-_attribute-invoke?view=netframework-4.8.1#system-attribute-system-runtime-interopservices-attribute-invoke(system-uint32-system-guid@-system-uint32-system-int16-system-intptr-system-intptr-system-intptr-system-intptr)
---
## Attribute._Attribute.Invoke メソッド

### 定義

**名前空間:** `System`
**アセンブリ:** `mscorlib.dll`

オブジェクトによって公開されているプロパティおよびメソッドへのアクセスを提供します。

```csharp
void _Attribute.Invoke(
    uint dispIdMember,
    ref Guid riid,
    uint lcid,
    short wFlags,
    IntPtr pDispParams,
    IntPtr pVarResult,
    IntPtr pExcepInfo,
    IntPtr puArgErr
);
```

---

## パラメーター

### `dispIdMember` (`UInt32`)

メンバーを識別します。

---

### `riid` (`Guid`)

将来使用するために予約されています。
`IID_NULL` でなければなりません。

---

### `lcid` (`UInt32`)

引数を解釈する際のロケールコンテキストです。

---

### `wFlags` (`Int16`)

呼び出しコンテキストを説明するフラグです。

---

### `pDispParams` (`IntPtr`)

以下を含む構造体へのポインタです。

* 引数配列
* 名前付き引数用の引数 DISPID 配列
* 各配列の要素数

---

### `pVarResult` (`IntPtr`)

結果を格納する場所へのポインタです。

---

### `pExcepInfo` (`IntPtr`)

例外情報を含む構造体へのポインタです。

---

### `puArgErr` (`IntPtr`)

エラーが発生した最初の引数のインデックスです。

---

## 実装

`Invoke(UInt32, Guid, UInt32, Int16, IntPtr, IntPtr, IntPtr, IntPtr)`

---

## 例外

### `NotImplementedException`

COM の `IDispatch` インターフェイスを使用した遅延バインディングアクセスはサポートされていません。

---

## 解説

このメソッドは、アンマネージコードからマネージクラスへアクセスするためのものです。
そのため、マネージコードから呼び出すべきではありません。

詳細については、`IDispatch::Invoke` を参照してください。
