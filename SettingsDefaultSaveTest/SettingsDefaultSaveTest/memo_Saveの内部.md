`Settings.Default.Save()` の内部は、ざっくり言うと次の流れです。

```text
Properties.Settings.Default.Save()
    ↓
ApplicationSettingsBase.Save()
    ↓
SettingsProvider に保存依頼
    ↓
通常は LocalFileSettingsProvider
    ↓
user.config に XML として保存
```

## 1. `Settings.Default` の正体

`Properties.Settings.Default` は、Visual Studio が自動生成した `Settings` クラスのインスタンスです。

だいたい以下のようなクラスです。

```csharp
namespace YourApp.Properties
{
    internal sealed partial class Settings 
        : System.Configuration.ApplicationSettingsBase
    {
        private static Settings defaultInstance =
            ((Settings)(Synchronized(new Settings())));

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("初期値")]
        public string UserName
        {
            get
            {
                return ((string)(this["UserName"]));
            }
            set
            {
                this["UserName"] = value;
            }
        }
    }
}
```

つまり、

```csharp
Settings.Default.UserName = "ABC";
```

は内部的には、

```csharp
this["UserName"] = "ABC";
```

に近いです。

---

## 2. `Save()` で何が起きるか

```csharp
Properties.Settings.Default.Save();
```

を呼ぶと、基底クラスの

```csharp
ApplicationSettingsBase.Save()
```

が呼ばれます。

内部では大まかに以下を行います。

```text
1. Userスコープの設定値を集める
2. SettingsSaving イベントを発生させる
3. SettingsProvider に保存を依頼する
4. 保存成功後、設定値を「保存済み状態」にする
```

---

## 3. 実際に保存する担当

通常の WinForms / .NET Framework では、保存担当はこれです。

```csharp
System.Configuration.LocalFileSettingsProvider
```

このクラスが、

```text
user.config
```

へ保存します。

---

## 4. 保存される場所

通常はこのあたりです。

```text
C:\Users\<ユーザー名>\AppData\Local\<会社名>\<アプリ名>.exe_Url_xxxxx\<バージョン>\user.config
```

中身は XML です。

```xml
<configuration>
  <userSettings>
    <YourApp.Properties.Settings>
      <setting name="UserName" serializeAs="String">
        <value>ABC</value>
      </setting>
    </YourApp.Properties.Settings>
  </userSettings>
</configuration>
```

---

## 5. 重要な点

`Save()` が保存するのは基本的に **Userスコープ** の設定です。

| スコープ        | 実行時変更 | Save対象 |
| ----------- | ----: | -----: |
| User        |    可能 |     対象 |
| Application |  基本不可 |    対象外 |

つまり、

```csharp
Settings.Default.UserName = "ABC";
Settings.Default.Save();
```

は保存されますが、`Application` スコープの設定は通常保存されません。

---

## 6. 一番簡単な理解

```csharp
Settings.Default.UserName = "ABC";
```

これはメモリ上の値変更。

```csharp
Settings.Default.Save();
```

これは `user.config` への書き込み。

```csharp
Settings.Default.Reload();
```

これは `user.config` から再読み込み。

```csharp
Settings.Default.Reset();
```

これはユーザー設定を初期値に戻す。

---

内部構造としては、

```text
Settingsクラス
  ↓ 継承
ApplicationSettingsBase
  ↓ 使用
SettingsProvider
  ↓ 通常実装
LocalFileSettingsProvider
  ↓ 保存先
user.config
```

という理解でよいです。
