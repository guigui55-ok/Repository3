User:
OneNoteのデータをプログラム上から取得・更新したいです。
どのような方法がありますか？c#プログラムからできますかね？


ChatGPT:
はい、**C#プログラムからOneNoteのデータ取得・作成・更新は可能**です。
ただし、基本は **Microsoft Graph API 経由**です。ローカルの `.one` ファイルを直接読み書きする方式は、実務的にはあまりおすすめしません。

## 結論

一番現実的な方法はこれです。

```text
C#アプリ
  ↓
Microsoft Graph API
  ↓
OneNote / OneDrive / Microsoft 365 上のノートブック
```

Microsoft Graph の OneNote API では、ノートブック、セクション、ページにアクセスできます。取得は `GET`、ページ作成は `POST`、ページ内容の更新は `PATCH` で行います。([Microsoft Learn][1])

## できること

主に以下が可能です。

| やりたいこと               |      可否 | 方法                |
| -------------------- | ------: | ----------------- |
| ノートブック一覧取得           |      可能 | Graph API         |
| セクション一覧取得            |      可能 | Graph API         |
| ページ一覧取得              |      可能 | Graph API         |
| ページ内容取得              |      可能 | HTMLとして取得         |
| ページ作成                |      可能 | HTMLをPOST         |
| ページ内容更新              |      可能 | JSON形式の変更命令をPATCH |
| ページ削除                |      可能 | Graph API         |
| ローカル `.one` ファイル直接編集 | 非推奨/難しい | 公式API用途ではない       |

## 注意点

OneNote APIでは、ページ内容は基本的に **HTMLとして扱います**。
ページ内容取得は、たとえば次のようなイメージです。

```http
GET /me/onenote/pages/{page-id}/content
```

ページ作成は、HTML本文を送ります。

```http
POST /me/onenote/sections/{section-id}/pages
Content-Type: text/html
```

ページ更新は少しクセがあり、ページ全体を単純に文字列置換するというより、HTML内の要素に対して「追加」「置換」などの変更命令を送ります。Microsoftのドキュメントでは、ページの `content` エンドポイントに `PATCH` を送り、JSON形式の変更オブジェクトを指定すると説明されています。([Microsoft Learn][2])

## 認証について

ここが重要です。

OneNote APIは、現在 **アプリのみ認証は非対応/非推奨**で、基本的には **ユーザーがサインインする委任認証**を使います。Microsoftのドキュメントでも、Graph OneNote APIはアプリのみ認証をサポートしない、または2025年3月31日以降サポートしない旨が案内されています。([Microsoft Learn][1])

つまり、C#のバッチ処理で完全無人実行したい場合は少し注意が必要です。

```text
向いている：
ユーザーがサインインして、自分のOneNoteを操作するアプリ

注意が必要：
サーバー側で無人実行して、特定ユーザーのOneNoteを定期更新する処理
```

## C#でやる場合の構成

C#では主に2パターンあります。

### 1. Microsoft Graph SDKを使う

C#らしく書きたいならこちらです。

```text
Microsoft.Graph
Azure.Identity
```

ざっくり流れは以下です。

```csharp
// かなり簡略化したイメージです
var scopes = new[] { "Notes.ReadWrite" };

var credential = new InteractiveBrowserCredential(new InteractiveBrowserCredentialOptions
{
    ClientId = "Azureに登録したアプリのClientId",
    TenantId = "common"
});

var graphClient = new GraphServiceClient(credential, scopes);

// ページ一覧取得のイメージ
var pages = await graphClient.Me.Onenote.Pages.GetAsync();
```

実際には、Azureポータルでアプリ登録をして、`Notes.Read` や `Notes.ReadWrite` などの権限を設定します。

### 2. HttpClientでGraph REST APIを直接叩く

Graph SDKを使わず、アクセストークンを取得して `HttpClient` で呼ぶ方法です。

```csharp
GET https://graph.microsoft.com/v1.0/me/onenote/notebooks
GET https://graph.microsoft.com/v1.0/me/onenote/sections
GET https://graph.microsoft.com/v1.0/me/onenote/pages
GET https://graph.microsoft.com/v1.0/me/onenote/pages/{id}/content
```

APIの動きが見えやすいので、最初の学習や検証ではこちらも分かりやすいです。

## 使い分け

おすすめは次のような感じです。

```text
個人用ツール・学習用
→ C# + Microsoft Graph SDK + 対話ログイン

業務アプリでOneNote連携
→ Microsoft Graph API + Azure AD認証設計

既存OneNoteを定期的にバックアップ/検索
→ Graph APIで取得、必要ならDBやMarkdownに保存

OneNoteをDB代わりに更新
→ あまりおすすめしない
```

## 実務上の注意

OneNoteは「データベース」ではなく「ノート」なので、プログラムから頻繁に更新するデータ保存先として使うと扱いにくいです。

特に以下は注意です。

```text
・ページ内容がHTML構造になる
・更新は差分PATCH形式で少し面倒
・同期タイミングの影響を受ける
・認証が必要
・大量アクセスではGraph APIの制限を受ける可能性がある
```

Graph APIにはスロットリング制限もあります。大量アクセスや頻繁な同期を行う場合は、再試行や待機処理を考慮する必要があります。([Microsoft Learn][3])

## まとめ

C#からOneNoteを扱うなら、基本方針はこれでよいです。

```text
Microsoft Graph APIを使う。
C#からは Microsoft Graph SDK または HttpClient で呼び出す。
取得・作成・更新は可能。
ただし、ユーザー認証が必要で、ページ内容はHTMLとして扱う。
```

最初に試すなら、まずは **「C#コンソールアプリから自分のOneNoteページ一覧を取得する」** ところから始めるのがよいです。

[1]: https://learn.microsoft.com/en-us/graph/integrate-with-onenote?utm_source=chatgpt.com "OneNote API overview - Microsoft Graph"
[2]: https://learn.microsoft.com/ja-jp/graph/onenote-update-page?utm_source=chatgpt.com "OneNote ページ コンテンツを更新する - Microsoft Graph"
[3]: https://learn.microsoft.com/en-us/graph/throttling-limits?utm_source=chatgpt.com "Microsoft Graph service-specific throttling limits"
