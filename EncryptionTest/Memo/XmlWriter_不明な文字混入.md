```markdown
# XmlWriter / StreamWriter のエンコーディング設定について

## 結論

- **両方使っている場合は、両方に明示的なエンコーディング指定が必要**
- ただし設計としては **どちらか一方に統一するのがベスト**
- 特に「先頭に1バイト余計な文字」が出る場合は **BOM（Byte Order Mark）** が原因の可能性が高い

---

# なぜ両方必要になるのか？

構成例：

```

XmlWriter → StreamWriter → MemoryStream

````

この場合、影響するのは：

- `XmlWriterSettings.Encoding`
- `StreamWriter` の `Encoding`

---

## 重要ポイント

### ① 実際にバイトを書いているのは StreamWriter

`XmlWriter` が `TextWriter（StreamWriter）` に書き込んでいる場合、

**実際のエンコーディングを決めるのは StreamWriter**

例：

```csharp
new StreamWriter(ms, new UTF8Encoding(false))
````

ここが最重要。

---

### ② XmlWriterSettings.Encoding の役割

`XmlWriterSettings.Encoding` は以下に影響します：

* XML宣言
  `<?xml version="1.0" encoding="utf-8"?>`
* 直接 Stream に書く場合のエンコーディング

ただし：

```csharp
XmlWriter.Create(TextWriter, settings)
```

のように **TextWriter を渡す場合は、StreamWriter の Encoding が優先される。**

---

# よくある事故パターン

```csharp
XmlWriterSettings.Encoding = UTF8
StreamWriter = デフォルト（BOM付きUTF8）
```

この場合：

* XML宣言は utf-8
* 実際は BOM付きUTF8

→ 先頭に BOM が入る
→ 「1バイト余計な文字」に見える

---

# 推奨設計パターン

## パターン1（推奨）

StreamWriter を使わない

```csharp
var settings = new XmlWriterSettings
{
    Encoding = new UTF8Encoding(false)
};

using (var ms = new MemoryStream())
using (var writer = XmlWriter.Create(ms, settings))
{
    // write
}
```

メリット：

* シンプル
* BOM事故が起きにくい

---

## パターン2（StreamWriterを使う場合）

```csharp
var encoding = new UTF8Encoding(false);

using (var ms = new MemoryStream())
using (var sw = new StreamWriter(ms, encoding))
using (var writer = XmlWriter.Create(sw))
{
}
```

この場合：

* XmlWriterSettings.Encoding は基本不要
* StreamWriter の Encoding が支配的

---

# 避けるべき構成

```csharp
XmlWriter(Encoding指定)
＋
StreamWriter(Encoding未指定)
```

→ デフォルトUTF8（BOM付き）になりやすい
→ 先頭に不要なバイトが入る

---

# 暗号化を含む場合のベストプラクティス

構成：

```
XmlWriter → MemoryStream → 文字列化 → 暗号化
```

より安全なのは：

* StreamWriterを使わない
* UTF8Encoding(false) を明示
* 文字列に変換せず **byte[] のまま暗号化**

理由：

* 文字列化すると BOM や正規化問題が入りやすい
* 暗号処理は byte[] ベースの方が安全

---

# 最終まとめ

| 状況                    | エンコーディング指定        |
| --------------------- | ----------------- |
| StreamWriterを使う       | 必須                |
| XmlWriterのみ（Stream直接） | 指定推奨              |
| 両方使う                  | 実質StreamWriterが支配 |

---

# 補足（BOM確認方法）

余計な文字が出る場合：

```csharp
(int)s[0]
```

* `65279` → BOM（U+FEFF）

または先頭バイトを確認：

```
EF BB BF → UTF-8 BOM
FF FE → UTF-16 LE
FE FF → UTF-16 BE
```

---

以上。

```

もし「byte[]のまま暗号化」の安全な設計例もMD化したい場合は言ってください。
```
