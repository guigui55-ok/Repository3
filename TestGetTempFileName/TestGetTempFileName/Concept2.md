## 概要
C#.net のGetTempFileNameを多量に繰り返し実行したときに、重複するファイル名が出力されるかを検証する

# 方針

## 詳細
- 1つ以上のスレッドで実行可能にする
- 複数のプロセスでも実行可能にする
- １つのプロセスに１つ以上のスレッドを持ち、１スレッドにつき１つのワーカー（＝ループ）を持つ
- １つのループ内でGetTempFileNameを繰り返し実行する。
- ループの実行時間の間隔を変更可能にする
- ループ内で生成したファイル名は、ワーカー内にListで保持する。
- プロセス数、１プロセス当たりのスレッド数、１スレッドあたりの反復回数、１スレッドあたりの１回実行当たりの時間（ウェイト）は個別に設定可能にする
- 上記各設定値は引数から指定する。
- デバッグモードではプログラム内（Main冒頭）で設定値を変更できる。
- １つのワーカーの処理が終わると、ListをHashSet<string>にも保持させる。
- ListとHashSetの個数を比較して、重複しているかを確認する。
- Listの中をチェックして重複しているかチェックする。
    - 重複している場合は別途データを保持する。
- Listの内容はLogに出力する。
    - ログの内容は PID、ThreadID、生成ファイル名
- ログの他にも生成ファイル名保存専用の結果ファイルにも出力する。
    - こちらは生成ファイル名のみを記録する。（フルパスではない）（生成ファイル名が競合するかのチェックのため）
- 結果ファイルは、１プロセスごとにまとめて出力する。
- 最終的に、結果ファイルは、プロセスごとの結果リストも１つにまとめる。
- ログや結果ファイル出力のタイミングは、1プロセスが終わった段階とする（ループ途中でファイル出力はしない）
- できればログファイルは最後に1つにまとめたい（1回実行＝1つのログファイル、1つの結果ファイル）（中間ファイルは取っておいてよい）
- Logクラスは既存実装済みのAppLoggerを使用。
    - Logの出力はConsole、Debug、ファイル出力の3つにする。
- 最後に生成したtmpファイルはすべて削除する。
- エラーが発生したときはLogに出力して継続する。
- 各workerは中間ログ/結果ファイルを出力する


## クラス、プロジェクト構成とプログラム実行モード
- 1つのプロジェクト内で完結する。
- 異なるプロセス間をまたぐ処理のため、複数モードを用意して、
　本exeを複数回実行する。
    - モード
        - masterモード （複数のプロセスワーカーを呼び出す＝exe実行）
        - workerモード（1つのプロセスワーカーを呼び出す）
            プロセスワーカー内で複数のスレッドワーカーを呼び出す
        - ログ出力モード
            最後に出力したファイルをまとめるモード
            （すべてのプロセス終了を待つため、別プロセスで扱う）
- モードは引数で切り替える。

- メインproj
    - プロセスワーカークラス
    - スレッドワーカークラス
    - Logger
    - メイン実行クラス
        （複数のプロセスワーカーを実行＝exe呼び出し）
    - プロセス実行クラス
        （プロセスワーカークラスを実行、クラス呼び出し）
    - ログ出力実行クラス

## フロー
- メインの実行（maseterモード）
    - 自exeをworkerモードで1つ～複数回実行する
    - 上記プロセスがすべて終わるまで待つ
    - 自exeをログ出力モードで実行する
        - 一時ファイルをまとめて、1つのログファイル、結果ファイルを作成

# 詳細

## 出力ファイル構成
logs/
  run_20260515_231000/
    worker_12345.log
    worker_12345_result.txt
    worker_23456.log
    worker_23456_result.txt
    merged.log
    merged_result.txt
    summary.txt

## クラス構成
TempFileNameTester
├─ Program.cs
├─ AppConfig.cs
├─ CommandLineParser.cs
├─ MasterRunner.cs
├─ WorkerProcessLauncher.cs
├─ ProcessWorker.cs
├─ ThreadWorker.cs
├─ WorkerResult.cs
├─ ResultFileWriter.cs
├─ ResultMerger.cs
├─ DuplicateChecker.cs
└─ AppLogger.cs

### 役割
Program
  引数を見て master / worker を切り替える

MasterRunner
  workerプロセスを複数起動
  終了待ち
  結果ファイル集約

ProcessWorker
  スレッドワーカーを複数起動
  プロセス単位の結果出力

ThreadWorker
  GetTempFileName を指定回数実行

DuplicateChecker
  List と HashSet で重複確認

ResultMerger
  workerごとの結果ファイルを1つにまとめる

## 引数例
TempFileNameTester.exe ^
  --mode master ^
  --process-count 4 ^
  --thread-count 8 ^
  --loop-count 10000 ^
  --wait-ms 0 ^
  --output-dir "C:\Temp\TestResult" ^
  --delete-created-files false

masterがworkerを起動するときは、同じexeをこう呼びます。

TempFileNameTester.exe ^
  --mode worker ^
  --worker-index 1 ^
  --thread-count 8 ^
  --loop-count 10000 ^
  --wait-ms 0 ^
  --output-dir "C:\Temp\TestResult\run_xxxxx"