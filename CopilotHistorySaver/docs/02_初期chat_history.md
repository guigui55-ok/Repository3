User:
Github CopilotでAIオーケストレーション構成で、ソフトウェア開発を行いたいです。
ただ、そのためのナレッジもなければ、その手法もわからない状況で、必要なCopilot設定（カスタムエージェントなど）もほぼない状態です。

その内容を学習しながら、オーケストレーション環境を構築していきたいと考えています。
目的として、オーケストレーションを構築することと、その第１弾の題材として CopilotHistorySaver_concept.md （ChatGPTプロジェクトに追加しています） のファイルの内容のソフトウェアを作成したいと思います。

まず、全体の作業としてどのようにしたらよいでしょう？
（まず計画を練ったほうが良いですよね？）
（まだ、着手開始なので、AIでの作業は手動入力で依頼して、プロンプトなどを作成していく方針でもよいです。そして次の題材移行で段階的に、現状より自動化をしていければよいです。）

ChatGPT:
はい、最初に全体計画を作るべきです。
ただし、現時点で詳細な実装計画まで確定するのではなく、**「調査→試作→開発標準化→段階的なオーケストレーション」までのロードマップ**を作るのが適切です。

今回の `CopilotHistorySaver` を、単なるアプリ開発ではなく、**Copilotを使った開発方法そのものを確立する第1号プロジェクト**として扱うのがよいと思います。

## 推奨する全体方針

次の2つを同時並行で育てます。

1. **CopilotHistorySaverの開発**
2. **Copilotで開発するための仕組みの構築**

重要なのは、最初から完全自動化を目指さないことです。

> 手動で依頼する
> → 有効だった依頼を保存する
> → 再利用できるPromptにする
> → 手順が安定したらSkillにする
> → 担当範囲が明確になったらAgentにする
> → Agent間を連携する

という順番が安全です。

これは、構想書にある「まず手動でCopilotを使い、有益なプロンプトを吟味しながらAgent・Skillを増やす」という方針とも合っています。

---

# 全体ロードマップ

## フェーズ0：プロジェクトの土台を作る

最初に、CopilotHistorySaverとオーケストレーション構築を一つのリポジトリで管理します。

想定構成は次のようになります。

```text
CopilotHistorySaver/
├─ .github/
│  ├─ copilot-instructions.md
│  ├─ agents/
│  ├─ prompts/
│  └─ skills/
├─ docs/
│  ├─ 00_project/
│  ├─ 01_research/
│  ├─ 02_requirements/
│  ├─ 03_design/
│  ├─ 04_test/
│  ├─ 05_operation/
│  └─ 90_ai-development/
├─ src/
├─ tests/
├─ samples/
├─ tools/
└─ README.md
```

`90_ai-development` には、ソフトウェアの仕様ではなく、次のような**AI活用の記録**を置きます。

```text
90_ai-development/
├─ prompt-log/
├─ effective-prompts/
├─ failed-prompts/
├─ agent-candidates.md
├─ skill-candidates.md
├─ lessons-learned.md
└─ orchestration-roadmap.md
```

最初からAgentを大量に作る必要はありません。

---

## フェーズ1：実現可能性調査

今回、最初に行うべきなのは実装ではなく、次の調査です。

### 調査テーマ

1. VS CodeのCopilot Chat履歴がどこに保存されるか
2. 質問と回答がどの形式で保存されるか
3. ワークスペースと履歴の対応関係
4. タイトル、日時、タグなどを取得できるか
5. VS CodeやCopilot更新後も形式が維持されるか
6. 読み取りが利用規約・社内規則・機密管理上問題ないか
7. 公式なAPIやエクスポート機能が存在するか

### 構想書で修正した方がよい点

構想書では、履歴が `.vs` に入っている可能性を想定していますが、`.vs` は主にVisual Studio側のフォルダです。

VS Codeのワークスペース設定は通常 `.vscode` に保存されます。一方、Copilot Chatのセッション本体については、最近のVS Codeではユーザーデータ領域の `workspaceStorage` 以下に保存される事例が確認されています。VS Codeの課題報告には、次のような構造が現れています。

```text
<User Data>/workspaceStorage/<workspace-hash>/chatSessions/
```

ただし、これは**公開された安定APIではなく、VS Code内部の保存形式である可能性が高い**ため、製品更新で変わる前提で設計すべきです。VS CodeはChat Sessionを管理する機能自体を提供していますが、任意の外部プログラムから全履歴を正式取得できるAPIがあるとは、現時点の公式資料からは確認できません。([Visual Studio Code][1])

したがって、このフェーズの結論は次のどれかになります。

* A：履歴ファイルを安定して読み取れる
* B：読み取れるが、非公開形式なので試験用途に限定する
* C：VS Code拡張機能として取得する必要がある
* D：標準履歴を読むのではなく、入力時点で独自記録する
* E：Copilot CLIなど、正式なエクスポート手段のある経路も併用する

なお、Copilot CLIにはセッションをMarkdownやHTMLなどへエクスポートする機能がありますが、これはVS Code内の既存履歴を一括取得する機能とは別です。([GitHub Docs][2])

---

## フェーズ2：最小試作

調査後、いきなり完成アプリを作らず、**読み取り専用の調査ツール**を作ります。

### 最初の試作範囲

```text
入力：
- VS CodeのUser Dataフォルダ
- またはworkspaceStorageフォルダ

処理：
- フォルダを再帰検索
- チャット候補ファイルを検出
- JSON等として読み込めるか確認
- ファイル構造を解析
- 推定した項目を一覧化

出力：
- コンソール表示
- JSON出力
- Markdown調査レポート
```

最初の段階では、次の項目だけで十分です。

```text
- 保存ファイルのパス
- ワークスペース識別情報
- セッションID
- 質問本文
- 回答本文
- 作成日時
- 更新日時
- メッセージ数
- 使用モデル（存在する場合）
- 使用Agent／モード（存在する場合）
```

構想書にある「タグ」「回答数」「質問タイトル」は、保存データに存在するとは限りません。

そのため、仕様上は次のように分類します。

| 区分          | 例               |
| ----------- | --------------- |
| 保存データから直接取得 | 本文、日時、セッションID   |
| 別情報から関連付け   | ワークスペース名、フォルダパス |
| プログラムで算出    | 質問数、回答数         |
| 独自生成        | タイトル、要約、タグ      |
| 取得不能の可能性あり  | 正確な課金量、内部処理情報   |

特にタイトルやタグは、履歴から直接取得するのではなく、将来的にAIで生成する方が現実的かもしれません。

---

## フェーズ3：通常のソフトウェア開発工程

技術的に取得可能だと判明した後、通常の開発工程に進みます。

### 1. 要求整理

まず、次を確定します。

* 誰が使用するか
* 何のために保存するか
* 質問だけか、回答も保存するか
* 全ワークスペース対象か
* 手動実行か定期実行か
* 重複をどう判定するか
* 削除済み履歴をどう扱うか
* 機密情報をどう扱うか
* 出力形式をどうするか
* 将来、分析機能を追加するか

### 2. 基本設計

主なコンポーネントは次のようになると思われます。

```text
HistoryLocationDetector
    保存候補フォルダを検出する

WorkspaceResolver
    workspaceStorageと実際のワークスペースを対応付ける

ChatSessionReader
    セッションファイルを読み込む

ChatSessionParser
    内部形式を共通モデルへ変換する

HistoryNormalizer
    質問・回答・日時などを正規化する

DuplicateDetector
    過去に保存した履歴との重複を判定する

HistoryExporter
    JSON、CSV、Markdownなどへ出力する

ExecutionReportWriter
    実行結果とエラーを記録する
```

内部保存形式とアプリ内モデルを直接結び付けないことが重要です。

```text
VS Code内部形式
    ↓
バージョン別Parser
    ↓
共通ChatSessionモデル
    ↓
JSON／Markdown／DB
```

こうしておけば、VS Codeの保存形式が変わってもParserだけを交換できます。

### 3. 詳細設計・実装

構想書ではC#、.NET Framework、Consoleが指定されています。

ただし、Visual Studio 2016で扱えるC#・.NET Frameworkには制約があります。JSON処理や将来的なVS Code拡張との連携を考えると、次の2案を比較した方がよいです。

* **案A：構想どおり .NET Frameworkコンソール**
* **案B：新しい.NETのコンソールアプリ**

学習用サンプルとしてVisual Studio 2016を使う目的があるなら案Aでも構いません。しかし、長期運用や新しいライブラリとの互換性では案Bが有利です。

これは調査フェーズで決めればよく、現時点では確定不要です。

---

## フェーズ4：Copilot開発設定を育てる

最初に用意するCopilot設定は、次の3つ程度で十分です。

## 1. `copilot-instructions.md`

プロジェクト全体に常時適用する内容だけを書きます。

例：

```markdown
# Project instructions

- This project investigates and exports GitHub Copilot chat history from a local VS Code environment.
- Separate verified facts, hypotheses, and implementation decisions.
- Do not assume undocumented VS Code storage formats are stable.
- Perform investigation before implementation when technical feasibility is uncertain.
- Do not modify files outside the requested scope.
- Preserve the existing document structure and terminology.
- For code changes, identify affected files and risks before editing.
- Use small, testable classes and methods.
- Add validation before executing file-system operations.
- Never modify or delete original VS Code history files.
```

Instructionsは「常に守るルール」に限定します。VS Code公式資料でも、Instructionsはコーディング規約やプロジェクト要件など、AIの振る舞いを継続的に調整するものとして位置付けられています。([Visual Studio Code][3])

## 2. 調査用Agent

現在作成済みのPlan／Deep-Ask系を利用して構いません。

役割は次の範囲です。

```text
- 事実確認
- リポジトリ調査
- 仮説整理
- 不明点抽出
- 技術的リスク整理
- 調査レポート作成
```

実装はさせず、「調査のみ」と明示します。

## 3. 実装・レビュー用Prompt

最初はAgent化せず、手動Promptとして作ります。

候補は次の4つです。

```text
research-only.prompt.md
implementation-plan.prompt.md
implement-approved-plan.prompt.md
review-changes.prompt.md
```

Prompt filesは、繰り返し行う特定タスクを標準化する仕組みです。ワークスペース用Promptの標準配置先は `.github/prompts` です。([Visual Studio Code][4])

---

# Agent・Prompt・Skillの作成順

このプロジェクトでは、次の基準が分かりやすいです。

## 手動入力のままにするもの

* 一度しか行わない依頼
* 要件が毎回大きく変わる依頼
* まだ良い指示方法が分からない作業
* 試行錯誤中の作業

## Prompt化するもの

* 同じ依頼を2～3回以上行った
* 入力対象だけを変えて再利用できる
* 実行手順は短い
* 人が明示的に呼び出したい

## Skill化するもの

* 実行手順がある程度固定された
* 複数の手順や補助ファイルを使う
* Agentが必要に応じて自動選択できる
* スクリプトやテンプレートを同梱したい

Agent Skillsは、指示だけでなく、スクリプトやリソースを含めたフォルダとして構成できます。([Visual Studio Code][5])

## Agent化するもの

* 明確な役割がある
* 使用可能なツールを制限したい
* 独自の判断基準がある
* 複数のPromptやSkillを束ねる
* 次の担当Agentへ引き継ぐ必要がある

現在のVS CodeのCustom Agentには、計画担当から実装担当、レビュー担当へ移るようなhandoffも用意されています。([Visual Studio Code][6])

---

# 第1段階のオーケストレーション

最初は次の程度で十分です。

```text
ユーザー
  ↓
Investigation Agent
  技術調査・影響範囲・不明点を整理
  ↓
ユーザー承認
  ↓
Planning Agent
  実装計画を作成
  ↓
ユーザー承認
  ↓
Implementation Agent
  承認された範囲だけ実装
  ↓
Review Agent
  計画との差異、バグ、テスト不足を確認
  ↓
ユーザー最終判断
```

ただし、初回はAgent間を自動連携させる必要はありません。

ユーザーが手動で、

```text
調査Agentを選ぶ
↓
出力を確認する
↓
Plan Agentを選ぶ
↓
実装Agentを選ぶ
↓
Review Agentを選ぶ
```

という運用で問題ありません。

むしろ、この手動操作によって、

* どこで情報が欠落したか
* どの引き継ぎ情報が必要だったか
* どの確認が無駄だったか
* どのAgentの役割が重複したか

を観察できます。

その結果を基にhandoffやSkillを整備する方が、実用的なオーケストレーションになります。

---

# 後回しにした方がよいもの

現段階では、次はまだ作らなくてよいです。

* 多数の専門Agent
* 自動でAgentを選択するルーター
* MCPサーバー
* 複雑なHooks
* 完全自動のドキュメント同期
* 自動コミット・自動プッシュ
* AIによる無承認実装
* 独自の進捗管理システム

HooksはAgent実行中の特定タイミングで外部コマンドを実行する仕組みですが、現在の公式リファレンスでは主にCopilot CLIとクラウドエージェントを対象としています。VS Code内の最初の手動開発フローに、直ちに導入する必要はありません。([GitHub Docs][7])

---

# まず実施する具体的な作業

初回の作業単位としては、次の順番を推奨します。

## ステップ1：構想書レビュー

`CopilotHistorySaver_concept.md` を次の観点で整理します。

```text
- 確定事項
- 仮説
- 調査が必要な事項
- 要求
- 技術制約
- 将来構想
```

現在の構想書には、プロジェクト目的、将来のオーケストレーション構想、サンプル仕様、コーディングルールが一緒に書かれています。

そのため、まず分離した方がよいです。

## ステップ2：調査計画書の作成

最初の成果物は、実装計画書ではなく次の文書です。

```text
docs/01_research/
└─ vscode-copilot-history-investigation-plan.md
```

主な内容：

```text
1. 調査目的
2. 調査対象
3. 確認する保存場所
4. 確認するファイル形式
5. 実験手順
6. 取得可否の判定基準
7. セキュリティ上の注意
8. 調査結果の記録方法
```

## ステップ3：手作業で実環境を確認

Windows 11上で、既知の短いCopilot Chatを1件作ります。

例えば、

```text
TEST_HISTORY_20260802_001
```

のような一意の文字列を質問に含めます。

その後、VS Codeを終了して、ユーザーデータ領域を検索します。

これにより、

* どのファイルに保存されたか
* 質問本文が平文か
* 回答も保存されるか
* 日時が含まれるか
* ワークスペースとどう対応しているか

を確認できます。

## ステップ4：読み取り専用プローブを実装

保存場所が判明してから、C#で最小プログラムを作ります。

この時点では、エクスポート機能やAI分析は実装しません。

## ステップ5：調査結果から正式な要求を作成

試作で取得可能項目が確定した後に、要求定義・基本設計へ進みます。

---

# 現段階での結論

今回の進め方は、次の形が最も適しています。

```text
第1段階
手動Copilot開発＋作業記録

第2段階
有効な依頼をPrompt化

第3段階
安定した複数手順をSkill化

第4段階
調査・計画・実装・レビューをAgent分割

第5段階
Agent間handoffを整備

第6段階
Hooks、スクリプト、MCP等で一部を自動化

第7段階
次のソフトウェア題材で再利用・改善
```

したがって、**次に作るべきものは詳細な実装計画ではなく、「プロジェクト全体計画」と「Copilot履歴保存方式の調査計画」**です。

最初の開発では手動依頼を中心にし、その履歴自体を教材としてPrompt・Skill・Agent候補を抽出する方針で問題ありません。これは遠回りではなく、実際の作業に適合したオーケストレーションを作るための重要な観察期間になります。

[1]: https://code.visualstudio.com/docs/chat/chat-sessions?utm_source=chatgpt.com "Work with chat sessions in VS Code"
[2]: https://docs.github.com/en/copilot/how-tos/copilot-cli/use-copilot-cli/chronicle?utm_source=chatgpt.com "Using GitHub Copilot CLI session data"
[3]: https://code.visualstudio.com/docs/agent-customization/custom-instructions?utm_source=chatgpt.com "Use custom instructions in VS Code"
[4]: https://code.visualstudio.com/docs/agent-customization/prompt-files?utm_source=chatgpt.com "Use prompt files in VS Code"
[5]: https://code.visualstudio.com/docs/agent-customization/agent-skills?utm_source=chatgpt.com "Use Agent Skills in VS Code"
[6]: https://code.visualstudio.com/docs/agent-customization/custom-agents?utm_source=chatgpt.com "Custom agents in VS Code"
[7]: https://docs.github.com/en/copilot/reference/hooks-reference?utm_source=chatgpt.com "GitHub Copilot hooks reference"
