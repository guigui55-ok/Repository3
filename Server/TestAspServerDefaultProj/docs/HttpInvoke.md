https://learn.microsoft.com/ja-jp/dotnet/api/system.web.services.protocols.httpsimpleclientprotocol.invoke?view=netframework-4.8.1
-----
## メソッド

### 定義

名前空間: `System.Web.Services.Protocols`
アセンブリ: `System.Web.Services.dll`

HTTP を使用して XML Web サービスのメソッドを呼び出します。

```csharp
protected object Invoke(string methodName, string requestUrl, object[] parameters);
```

---

## パラメーター

### methodName

`String`

`Invoke(String, String, Object[])` メソッドを呼び出す派生クラス内の XML Web サービスメソッド名。

---

### requestUrl

`String`

クライアントが要求している XML Web サービスメソッドの URL。

---

### parameters

`Object[]`

リモート XML Web サービスへ渡すパラメーターを含むオブジェクト配列。
配列内の値の順序は、派生クラスの呼び出し元メソッドに定義されたパラメーター順に対応します。

---

## 戻り値

### Object

戻り値、および参照渡し (`ref`) または `out` パラメーターを含むオブジェクト配列。

---

## 例外

### Exception

要求はサーバーコンピューターへ到達したが、正常に処理されなかった場合に発生します。

---

## 使用例

以下のコード例は ASP.NET Web フォームです。
`Math` という XML Web サービスを呼び出します。

`EnterBtn_Click` 関数内で、Web フォームは `Add` XML Web サービスメソッドを同期的に呼び出します。

```csharp
（コード省略）
```

---

以下のコード例は、Web Services Description Language ツール (`Wsdl.exe`) によって生成された、`Math` XML Web サービス用のプロキシクラスです。

プロキシクラスの `Add` メソッド内で、`Invoke` メソッドが `Add` Web サービスメソッドを同期的に呼び出しています。

```csharp
namespace MyMath {
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System;
    using System.Web.Services.Protocols;
    using System.Web.Services;

    [System.Web.Services.WebServiceBindingAttribute(Name="MathSoap", Namespace="http://tempuri.org/")]
    public class Math : System.Web.Services.Protocols.SoapHttpClientProtocol {

        [System.Diagnostics.DebuggerStepThroughAttribute()]
        public Math() {
            this.Url = "http://www.contoso.com/math.asmx";
        }

        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute(
            "http://tempuri.org/Add",
            Use=System.Web.Services.Description.SoapBindingUse.Literal,
            ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int Add(int num1, int num2) {

            object[] results = this.Invoke("Add", new object[] {num1, num2});

            return ((int)(results[0]));
        }

        [System.Diagnostics.DebuggerStepThroughAttribute()]
        public System.IAsyncResult BeginAdd(
            int num1,
            int num2,
            System.AsyncCallback callback,
            object asyncState) {

            return this.BeginInvoke(
                "Add",
                new object[] {num1, num2},
                callback,
                asyncState);
        }

        [System.Diagnostics.DebuggerStepThroughAttribute()]
        public int EndAdd(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }
    }
}
```

---

以下のコード例は、上記のプロキシクラス生成元となった `Math` XML Web サービスです。

### ASP.NET (C#)

```aspx
<%@ WebService Language="C#" Class="Math"%>

using System.Web.Services;
using System;

public class Math {

    [ WebMethod ]
    public int Add(int num1, int num2) {
        return num1 + num2;
    }
}
```

---

## 解説 (Remarks)

`methodName` パラメーターは、`Invoke` メソッドを呼び出しているメソッドのパラメーター型や戻り値型を特定するために使用されます。

また、メソッドへ追加されたカスタム属性を検索するためにも使用されます。

`HttpMethodAttribute` および `XmlElementAttribute` は、SOAP プロトコルで必要となる派生メソッドに関する追加情報を提供します。
