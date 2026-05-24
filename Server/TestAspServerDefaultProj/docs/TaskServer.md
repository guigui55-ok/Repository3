User: 本ファイルの実装をするため、まずサーバー側を構築・実装したいです。
まずタスクを出力してください。

GitHub Copilot: Concept.md の内容を基に、サーバー側（TestAspServerDefault）の実装タスクを整理します。

## サーバー側実装タスク

### **フェーズ1: プロジェクト基盤の構築**
- [ ] TestAspServerDefault プロジェクトの確認/作成（C#, .Net Framework 4.7, ASP.Net）
- [ ] プロジェクトフォルダ・ファイル構成の確認

### **フェーズ2: 共通クラスの実装**
- [ ] `ResultCode` Enum の実装
  - None, OK, Fail, Error, Unexpected
- [ ] `ResultInfo` クラスの実装
  - `ResultCode Result` プロパティ
  - `string Data` プロパティ

### **フェーズ3: asmx Web サービスの構築**
- [ ] asmx Web サービスファイル（Service.asmx）の作成

### **フェーズ4: WebMethod の実装**
以下の9つのテストメソッドを実装（out 引数の有無パターン）：
- [ ] `TestWebMethod_Default()` - 引数無し
- [ ] `TestWebMethod_OutOne()` - out string
- [ ] `TestWebMethod_InOne()` - string
- [ ] `TestWebMethod_OutIn()` - out string, string
- [ ] `TestWebMethod_InIn()` - string, string
- [ ] `TestWebMethod_OutOutIn()` - out string, out string, string
- [ ] `TestWebMethod_InOutOut()` - string, out string, out string
- [ ] `TestWebMethod_InOutIn()` - string, out string, string
- [ ] `TestWebMethod_OutInOut()` - out string, string, out string

### **フェーズ5: 動作確認**
- [ ] サーバー起動確認
- [ ] WSDL が正常に生成されているか確認

---

まず、現在のプロジェクト構造を確認する必要があります。TestAspServerDefault フォルダに何が存在するか見てみましょうか？