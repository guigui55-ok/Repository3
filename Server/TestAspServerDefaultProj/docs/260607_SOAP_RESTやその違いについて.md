以下のような流れで整理すると、今回理解した内容がかなり繋がると思います。

# ASP.NET における SOAP / REST の整理メモ

## 1. 用語整理

### SOAP（Simple Object Access Protocol）

**「リモートのメソッド（関数）を呼び出すための通信プロトコル」**

特徴：

* 通信形式：主に XML
* 契約（仕様）を厳密に定義する
* クライアントコードを自動生成しやすい
* 型や構造の一致を重視

ASP.NET では主に：

* ASMX
* WCF（basicHttpBinding など）

イメージ：

```text
Client.GetUser()
↓
SOAP XML生成
↓
HTTP送信
↓
サーバのWebMethod実行
```

例：

```csharp
service.GetUser("001")
```

内部：

```xml
<soap:Envelope>
  <soap:Body>
    <GetUser>
      <id>001</id>
    </GetUser>
  </soap:Body>
</soap:Envelope>
```

---

### REST（Representational State Transfer）

**「HTTPを使ってリソース（データ）を操作する設計思想」**

特徴：

* 通信形式：JSON が主流（XMLも可能）
* URL＋HTTPメソッドで操作する
* 比較的疎結合
* 変更に強い

ASP.NET では主に：

* ASP.NET Web API
* ASP.NET Core API

イメージ：

```text
GET /users/100
↓
JSON返却
↓
クライアント側で解析
```

例：

```http
GET /api/user/001
```

返却：

```json
{
  "id":1,
  "name":"Yamada"
}
```

---

## 2. SOAP と REST の違い

|        | SOAP     | REST    |
| ------ | -------- | ------- |
| 基本思想   | メソッド呼出   | リソース操作  |
| 通信     | XML      | JSON中心  |
| 呼び出し   | Webメソッド  | URL     |
| 定義     | WSDL     | OpenAPI |
| 型      | 厳密       | 比較的柔軟   |
| クライアント | 自動生成しやすい | 手実装も多い  |
| 変更耐性   | 低い       | 比較的高い   |

イメージ：

SOAP

```text
client.GetUser()
```

REST

```text
GET /users/100
```

---

# 3. 今回の疑問点と整理

---

## 疑問①

### ASP.NET は SOAP が通常？

結論：
**昔はそうだったが、現在は REST が主流。**

時代ごとのイメージ：

```text
ASP.NET Web Forms
↓
ASMX（SOAP）

↓

ASP.NET Core
↓
REST API
```

ただし業務系・工場系では今も SOAP が多数存在。

---

## 疑問②

### REST は XML を直接やり取りするもの？

結論：
**違う。REST は設計思想。**

REST：

```text
HTTP
＋
JSON（主流）
```

SOAP：

```text
HTTP
＋
SOAP XML
```

補足：

* RESTでもXMLは可能
* 実務ではJSONがほぼ主流

---

## 疑問③

### REST はシグネチャが多少違っても動く？

結論：
**ある程度は合っている。**

例。

サーバ：

```json
{
 "id":1,
 "name":"A",
 "memo":"追加"
}
```

クライアント：

```csharp
class User
{
 int Id;
 string Name;
}
```

→ `memo` を無視できる場合が多い。

ただし、

```json
{
 "userId":1
}
```

のような破壊的変更は普通に壊れる。

つまり、

```text
REST
＝変更に強い

≠
仕様管理不要
```

むしろ、

* API設計
* バージョン管理
* OpenAPI管理

が重要。

---

## 疑問④

### 「型が厳密」「既存連携大量」とは？

結論：
**変更時の影響範囲が大きい。**

SOAP：

変更前

```csharp
GetUser(string id)
```

↓

変更後

```csharp
GetUser(string id, string memo)
```

すると、

```text
クライアントA修正
↓
クライアントB修正
↓
結合試験
↓
リリース
```

となる。

工場系・基幹系は連携先が多く影響大。

---

## 疑問⑤

### WSDL とは？

結論：
**SOAP の API 仕様書＋契約定義。**

含まれるもの：

* メソッド名
* 引数
* 戻り値
* 型
* XML形式

流れ：

```text
WSDL
↓
Service Reference
↓
Proxy生成
↓
client.GetUser()
```

Visual Studio：

```text
サービス参照追加
```

がこれ。

---

## 疑問⑥

### REST の WSDL に相当するものは？

結論：
**OpenAPI（Swagger）**

例：

```yaml
paths:
 /users/{id}:
   get:
```

流れ：

```text
OpenAPI
↓
Client生成
↓
HttpClient
```

---

## 疑問⑦

### SOAP（ASMX）は REST を WSDL 化して XML化したもの？

結論：
**似て見えるが違う。**

SOAP：

```text
メソッド中心
↓
WSDL
↓
XML
```

REST：

```text
リソース中心
↓
HTTP
↓
JSON
```

近い理解としては、

> SOAP（ASMX）は、Webメソッド呼び出しを契約（WSDL）として定義し、XML形式で通信する仕組み

という理解がかなり近い。

---

# 最終イメージ

```text
SOAP（ASMX）

WebMethod
↓
WSDL
↓
Proxy生成
↓
SOAP XML
↓
HTTP


REST API

Endpoint
↓
OpenAPI（任意）
↓
HTTP
↓
JSON
↓
Deserialize
```

今回の理解でいうと、ユーザーさんが最終的に辿り着いていた

> 「RESTは柔軟だが、その分仕様管理をしないと不具合調査が難しい」

これはかなり実務寄りの見方です。実際の現場でも、RESTでも結局 OpenAPI・型生成・バージョニングで厳密管理に寄せることはよくあります。
