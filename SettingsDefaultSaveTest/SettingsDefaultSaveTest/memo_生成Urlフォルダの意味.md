これ、最初見るとかなり謎ですよね。

```text
App.exe_Url_xxxxx
```

の `_Url_xxxxx` は、**アプリ配置場所（実行ファイルのURL/パス）から生成されたハッシュ**です。

## 何のためにあるか

同じアプリ名でも、実行場所が違うと設定を分けるためです。

例えば：

```text
C:\Dev\TestApp\TestApp.exe
```

と

```text
D:\Backup\TestApp\TestApp.exe
```

は同じ EXE 名でも別アプリ扱いに近いので
設定が衝突しないよう分離される。

---

## イメージ

```text
user.config 保存先

CompanyName
 ├─ TestApp.exe_Url_a1b2c3...
 │    └─1.0.0.0
 │       └─user.config

 └─ TestApp.exe_Url_d4e5f6...
      └─1.0.0.0
         └─user.config
```

実行場所が違うと別フォルダ。

---

## "Url" なのはなぜ？

昔の .NET は ClickOnce やネットワーク配置も意識していて、
実行場所を「URL」として扱う設計でした。

例：

```text
file:///C:/Dev/TestApp/TestApp.exe
```

あるいは昔は

```text
http://server/app/TestApp.exe
```

もありえた。

この URL を元にハッシュ化してるので

```text
_Url_xxxxx
```

という名前になっている。

---

## xxxxx は何？

元パスから作った一意ハッシュ。

概念的にはこういう感じ：

```text
Hash(
 file:///C:/Dev/TestApp/TestApp.exe
)
```

→

```text
Url_q3mhnj0xj5...
```

（実際は内部アルゴリズムでもう少し複雑）

---

## なぜ必要か

もしこれが無いと

```text
C:\Work\App.exe
D:\Test\App.exe
```

で同じ user.config を奪い合う。

危険。

---

## 面白い確認テスト

EXE を別フォルダにコピーして起動すると、

```text
新しい _Url_xxxxx
```

ができて設定が初期化されたように見えます。

これはバグではなく仕様。

---

## バージョンもさらに分かれる

```text
TestApp.exe_Url_xxxx
   ├─1.0.0.0
   └─1.1.0.0
```

バージョンごとにも分離。

---

## 内部的には

LocalFileSettingsProvider が

* CompanyName
* FriendlyName(exe名)
* Evidence(配置URL)
* Version

からパスを組み立てています。

概念式：

```text
Path =
%LOCALAPPDATA%
 + Company
 + AppName + "_Url_" + Hash(Location)
 + Version
```

---

### 実務上ちょっと困る点

開発中、

```text
Debug
Release
別フォルダコピー
```

で設定が別になることがある。

「設定消えた？」となる原因の一つです。

---

もし興味あれば、この `_Url_xxxxx` を使わず固定パスにする（独自 SettingsProvider を作る）方法もあります。これは業務アプリではたまにやります。
