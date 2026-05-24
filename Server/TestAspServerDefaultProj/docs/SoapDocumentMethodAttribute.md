https://learn.microsoft.com/ja-jp/dotnet/api/system.web.services.protocols.soapdocumentmethodattribute?view=netframework-4.8.1
----



## SoapDocumentMethodAttribute クラス

### 定義

**名前空間:** `System.Web.Services.Protocols`
**アセンブリ:** `System.Web.Services.dll`

`SoapDocumentMethodAttribute` をメソッドに適用すると、そのメソッドとの送受信に使用される SOAP メッセージが **Document 形式** を使用することを指定します。

```csharp
[System.AttributeUsage(System.AttributeTargets.Method)]
public sealed class SoapDocumentMethodAttribute : Attribute
```

### 継承

* Object

  * Attribute

    * SoapDocumentMethodAttribute

### 属性

* AttributeUsageAttribute

---

# 例

以下のコード例では、`GetUserName` XML Web サービスメソッドのメッセージスタイルを `Document` に設定しています。
さらに、SOAP リクエストおよび SOAP レスポンスの `Body` 要素に含まれる XML 要素名を、それぞれ `GetUserNameRequest` と `GetUserNameResponse` に設定しています。

## ASP.NET (C#)

```csharp
<%@ WebService Language="C#" class="MyUser" %>

using System;
using System.Web.Services;
using System.Web.Services.Protocols;

public class MyUser : WebService {

    [ SoapDocumentMethod(
        Action="http://www.contoso.com/Sample", 
        RequestNamespace="http://www.contoso.com/Request",
        RequestElementName="GetUserNameRequest",
        ResponseNamespace="http://www.contoso.com/Response",
        ResponseElementName="GetUserNameResponse")]
        
    [ WebMethod(Description="Obtains the User Name") ]

    public UserName GetUserName() {

        string temp;
        int pos;
        UserName NewUser = new UserName();

        // ドメイン名を含む完全なユーザー名を取得
        temp = User.Identity.Name;

        // バックスラッシュがあるか確認し、
        // ドメインユーザーかどうか判定
        pos = temp.IndexOf("\\");

        // ドメイン名部分を解析
        if (pos <= 0)
            NewUser.Name = User.Identity.Name;
        else {
            NewUser.Name = temp.Remove(0,pos+1);
            NewUser.Domain = temp.Remove(pos,temp.Length-pos);
        }

        return NewUser;
    }
}

public class UserName {

    public string Name;
    public string Domain;
}
```

---

# 解説（Remarks）

WSDL（Web Services Description Language）では、XML Web サービスメソッド（operation）の SOAP メッセージ形式として、次の 2 種類を定義しています。

* RPC
* Document

`Document` スタイルでは、XML Web サービスメソッドは **XSD スキーマに従ってフォーマット** されます。

`Document` スタイルでは、SOAP の `Body` 要素の中に、1 個以上のメッセージパーツを配置します。

各メッセージパーツの具体的な形式は、以下のプロパティによって決定されます。

* `Use`
* `ParameterStyle`

### Use プロパティ

パラメータ形式を以下のどちらにするか決定します。

* Encoded
* Literal

### ParameterStyle プロパティ

パラメータの格納方法を決定します。

* 1つの XML 要素にまとめる
* 各パラメータを個別メッセージパーツにする

詳細については、「Customizing SOAP Message Formatting」を参照してください。

この属性は以下の両方に適用できます。

* サーバー側の XML Web サービスメソッド
* クライアント側のプロキシクラスのメソッド

---

# コンストラクタ

| 名前                                    | 説明                                                  |
| ------------------------------------- | --------------------------------------------------- |
| `SoapDocumentMethodAttribute()`       | `SoapDocumentMethodAttribute` クラスの新しいインスタンスを初期化します。 |
| `SoapDocumentMethodAttribute(String)` | `Action` プロパティを指定値で初期化して、新しいインスタンスを生成します。           |

---

# プロパティ

| 名前                    | 説明                                                 |
| --------------------- | -------------------------------------------------- |
| `Action`              | SOAP リクエストの `SOAPAction` HTTP ヘッダーを取得または設定します。     |
| `Binding`             | XML Web サービスメソッドが実装するバインディングを取得または設定します。           |
| `OneWay`              | クライアントがサーバー処理完了を待機するかを取得または設定します。                  |
| `ParameterStyle`      | SOAP メッセージの `Body` 要素内で、パラメータを単一要素にまとめるかどうかを設定します。 |
| `RequestElementName`  | SOAP リクエスト用 XML 要素名を取得または設定します。                    |
| `RequestNamespace`    | SOAP リクエスト用 namespace を取得または設定します。                 |
| `ResponseElementName` | SOAP レスポンス用 XML 要素名を取得または設定します。                    |
| `ResponseNamespace`   | SOAP レスポンス用 namespace を取得または設定します。                 |
| `TypeId`              | 属性の一意識別子を取得します。                                    |
| `Use`                 | SOAP メッセージ内でのパラメータ形式を取得または設定します。                   |

---

# メソッド

| 名前                     | 説明                    |
| ---------------------- | --------------------- |
| `Equals(Object)`       | 指定オブジェクトと等しいか判定します。   |
| `GetHashCode()`        | ハッシュコードを返します。         |
| `GetType()`            | 現在のインスタンスの型を取得します。    |
| `IsDefaultAttribute()` | 既定値かどうかを判定します。        |
| `Match(Object)`        | 指定オブジェクトと一致するか判定します。  |
| `MemberwiseClone()`    | シャローコピーを作成します。        |
| `ToString()`           | 現在のオブジェクトを表す文字列を返します。 |

---

# 明示的インターフェース実装

| 名前                                 | 説明                         |
| ---------------------------------- | -------------------------- |
| `_Attribute.GetIDsOfNames(...)`    | 名前を Dispatch ID にマッピングします。 |
| `_Attribute.GetTypeInfo(...)`      | 型情報を取得します。                 |
| `_Attribute.GetTypeInfoCount(...)` | 提供する型情報数を取得します。            |
| `_Attribute.Invoke(...)`           | オブジェクトのプロパティやメソッドへアクセスします。 |
