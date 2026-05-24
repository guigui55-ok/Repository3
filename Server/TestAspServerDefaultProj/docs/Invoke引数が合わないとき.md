User: 各WebServiceメソッドのInvokeによる呼び出し時の引数設定について、
out と in が混ざっている場合、渡すのはin 変数だけでよいんですよね？
（そして、object{} としている個所は、in の左から渡されるという認識で会っていますよね？）

また、引数が (out string, in string)となっている場合は、object{"任意の文字列", "任意の文字列"} のようにして呼び出すとどうなりますか？

GitHub Copilot: ## 回答

はい。その認識で合っています。

- `Invoke(..., new object[] { ... })` に渡すのは、`in`（入力）として送る値だけです。
- `out` だけのパラメータは呼び出し時には送らず、戻り値の配列から受け取ります。

たとえば

```csharp
public ResultInfo TestWebMethod_OutIn(out string outValue1, string inValue1)
{
    object[] results = this.Invoke("TestWebMethod_OutIn", new object[] { inValue1 });
    outValue1 = (string)results[1];
    return (ResultInfo)results[0];
}
```

このように、`object[]` の中身は `in` パラメータのみで、宣言順の左から右へ並べます。

---

## `(out string, in string)` の場合

- 正しい呼び出しは `object[] { inValue1 }`
- `object[] { "任意の文字列", "任意の文字列" }` とすると、期待される引数の数とずれるため誤りです

その場合は:

- `Invoke` 側でパラメータ数不一致になる
- SOAP リクエストが不正になって `SoapException` などの例外になる可能性が高い

---

## まとめ

- `out` と `in` が混在していても、`Invoke` には `in` の値だけ渡す
- `object[]` の順番は `in` の宣言順（左から右）でよい
- `out` の位置にダミー値を入れたり、`in` 以上の値を渡したりしてはいけない

もし `ref` パラメータなら話が変わり、`ref` は入力も兼ねるので `object[]` に含める必要があります。

User: object[] { "任意の文字列", "任意の文字列" } とした場合は、例外になる可能性が高い、ということですが、実際に動かしてみないとわからない、ということですか？

GitHub Copilot: ## 返答

いいえ、実際に動かしてみないと分からない、という意味ではありません。

- `SoapHttpClientProtocol.Invoke` に渡す `object[]` は、`in` / `ref` パラメータの値だけです。
- `out` パラメータは呼び出し時に渡さず、戻り値配列 `results` から受け取ります。

---

## つまり

`(out string, in string)` のメソッドで

```csharp
object[] { "任意の文字列", "任意の文字列" }
```

のようにすると、明らかに不正です。

- `out` の位置に値を入れてはいけない
- `in` が 1つなのに `object[]` の長さが 2 になっている

その結果は、通常は

- `Invoke` 側で引数個数不一致の例外
- あるいは SOAP 実行時に不正なリクエストとなって `SoapException` など

になります。

---

## 結論

- これは「動かしてみないと分からない」タイプのあいまいな問題ではなく、仕様上の誤りです
- 正しい呼び方は `object[] { inValue1 }` のみ
- `out` を含めて余分な値を渡すのは避けてください

User: SOAPエラーとなるとのことでしたが、実際にout引数を渡してもエラーとはならないようですね。

実行結果
# TestWebMethod_OutIn_Fail_Example
OK: OutIn called. inValue1=outValue1 / out=out value1

GitHub Copilot: 調査して、実行結果の背景と `Invoke` の挙動を確認します。

Searched for regex `TestWebMethod_OutIn_Fail_Example|OutIn called|TestWebMethod_OutIn|Invoke\("TestWebMethod_OutIn` (`**/*.{cs,md}`), 32 results

Read [](file:///d%3A/git/Repository3/Server/TestAspServerDefaultProj/TestAspClientDefault/TestAspClientDefault/Program.cs)

確認できました。`Fail_Example` も実装されていて、実際に動いた理由をコードから説明します。

Read [](file:///d%3A/git/Repository3/Server/TestAspServerDefaultProj/TestAspClientDefault/TestAspClientDefault/WebServiceClient.cs#55-55), lines 55 to 90

Read [](file:///d%3A/git/Repository3/Server/TestAspServerDefaultProj/TestServerDefault/TestAspServerDefault/WebService.asmx.cs)

Searched for regex `out parameter|SoapHttpClientProtocol.Invoke|ParameterStyle.Wrapped|SoapDocumentMethod|SOAP できない|out だけ` (`**/*.{md,cs}`), 34 results

## まとめ

`Fail_Example` の実行結果を見た限り、`object[] { "outValue1", inValue1 }` が必ず例外になるわけではありません。

---

## なぜエラーにならなかったのか

- `SoapHttpClientProtocol.Invoke` は、`out` パラメータを送信するための値を要求しません。
- そのため、`new object[] { "outValue1", inValue1 }` のように余分な要素を渡しても、
  - 実際の SOAP 送信では非 `out` パラメータだけが使われる
  - `out` パラメータはサーバーからの応答で返される

結果として、クライアント側で最初の要素が `inValue1` として扱われた可能性が高いです。

あなたの出力がこうなった理由は:

- `inValue1=outValue1`
- `out=out value1`

つまり、渡した `"outValue1"` が `inValue1` として動いており、
`outValue1` の本当の値はサーバー側で設定されて返ってきた、という挙動です。

---

## 重要な注意点

これが「正しい書き方」ではありません。

- `out` パラメータは送信しない
- `Invoke` に渡すのは `in`（と `ref`）だけ
- `object[]` の順番は「入力扱いされる引数の順番」であるべき

そのため、正しい呼び方は:

```csharp
object[] { inValue1 }
```

です。

---

## 結論

- あなたのケースでは例外にならないことがある
- しかしそれは「正しく動いた」わけではなく、あいまいな動作に依存しているだけ
- 安全なのは、`out` を含めず `in` だけを渡すことです

User: 引数が合わない場合は、SOAPエラーとならず、後ろの余計な引数は無視されるということですね。（仕様には書いていませんが）

GitHub Copilot: はい、今回の実装では `Invoke` に渡した余分な引数が無視されているように見えます。

- `SoapHttpClientProtocol.Invoke` では、生成された SOAP メッセージの構築処理が「期待される入力引数だけ」を使い、それ以外は無視する場合があります。
- そのため `object[] { "outValue1", inValue1 }` のように余分な先頭要素を渡しても、`"outValue1"` が `inValue1` として扱われてしまい、実行結果上はエラーにならないケースがあります。

ただし重要なのは:

- これは公式仕様で保証された動作ではない
- 実装依存であり、別バージョンや別の SOAP クライアントでは例外になる可能性がある
- 正しく安全なのは、`out` は渡さず `in` の値だけを `object[]` に入れること

なので結論としては、

> はい、今回の挙動では余分な引数は無視されたようですが、仕様としてそういう使い方をしてよいわけではありません。

正しい書き方は `object[] { inValue1 }` だけです。