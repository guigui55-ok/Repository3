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