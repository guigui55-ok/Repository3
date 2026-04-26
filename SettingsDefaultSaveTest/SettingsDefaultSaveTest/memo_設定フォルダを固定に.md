方法は大きく2つあります。

# 方法1：独自設定ファイルにする【おすすめ】

`Properties.Settings` の保存先を無理に変えず、固定パスに JSON / XML / INI で保存します。

例：

```text
C:\Users\<User>\AppData\Local\MyCompany\MyApp\settings.json
```

```csharp
using System;
using System.IO;
using Newtonsoft.Json;

public class AppConfig
{
    public string UserName { get; set; }
    public int WindowWidth { get; set; }
}

public class AppConfigManager
{
    private static readonly string ConfigPath =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "MyCompany",
            "MyApp",
            "settings.json");

    public static AppConfig Load()
    {
        if (!File.Exists(ConfigPath))
        {
            return new AppConfig
            {
                UserName = "初期ユーザー",
                WindowWidth = 800
            };
        }

        string json = File.ReadAllText(ConfigPath);
        return JsonConvert.DeserializeObject<AppConfig>(json);
    }

    public static void Save(AppConfig config)
    {
        string dir = Path.GetDirectoryName(ConfigPath);

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string json = JsonConvert.SerializeObject(config, Formatting.Indented);
        File.WriteAllText(ConfigPath, json);
    }

    public static string GetConfigPath()
    {
        return ConfigPath;
    }
}
```

使い方：

```csharp
AppConfig config = AppConfigManager.Load();

config.UserName = "TEST";
config.WindowWidth = this.Width;

AppConfigManager.Save(config);

MessageBox.Show(AppConfigManager.GetConfigPath());
```

この方式なら `_Url_xxxxx` は出ません。

---

# 方法2：独自 SettingsProvider を作る

`Properties.Settings.Default.Save()` の仕組みを残したまま、保存先だけ変えたい場合は、`SettingsProvider` を自作します。

ただし、少し面倒です。

## 例：固定パス SettingsProvider

```csharp
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Xml.Linq;

public class FixedPathSettingsProvider : SettingsProvider
{
    private string _applicationName;

    public override string ApplicationName
    {
        get { return _applicationName; }
        set { _applicationName = value; }
    }

    public override string Name
    {
        get { return "FixedPathSettingsProvider"; }
    }

    private string ConfigPath
    {
        get
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "MyCompany",
                "MyApp",
                "user.config");
        }
    }

    public override void Initialize(string name, NameValueCollection config)
    {
        base.Initialize(Name, config);
    }

    public override SettingsPropertyValueCollection GetPropertyValues(
        SettingsContext context,
        SettingsPropertyCollection properties)
    {
        SettingsPropertyValueCollection values =
            new SettingsPropertyValueCollection();

        XDocument doc = null;

        if (File.Exists(ConfigPath))
        {
            doc = XDocument.Load(ConfigPath);
        }

        foreach (SettingsProperty property in properties)
        {
            SettingsPropertyValue value =
                new SettingsPropertyValue(property);

            string savedValue = null;

            if (doc != null)
            {
                XElement element = doc.Root.Element(property.Name);
                if (element != null)
                {
                    savedValue = element.Value;
                }
            }

            if (savedValue != null)
            {
                value.SerializedValue = savedValue;
            }
            else
            {
                value.SerializedValue = property.DefaultValue;
            }

            value.IsDirty = false;
            values.Add(value);
        }

        return values;
    }

    public override void SetPropertyValues(
        SettingsContext context,
        SettingsPropertyValueCollection values)
    {
        string dir = Path.GetDirectoryName(ConfigPath);

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        XElement root = new XElement("settings");

        foreach (SettingsPropertyValue value in values)
        {
            root.Add(new XElement(
                value.Name,
                value.SerializedValue == null ? "" : value.SerializedValue.ToString()));
        }

        XDocument doc = new XDocument(root);
        doc.Save(ConfigPath);
    }
}
```

---

## Settings.settings 側で Provider を指定

自動生成された `Settings.Designer.cs` 側、または partial class 側で属性を付けます。

```csharp
[System.Configuration.SettingsProvider(typeof(FixedPathSettingsProvider))]
internal sealed partial class Settings
{
}
```

ただし、`Settings.Designer.cs` を直接編集すると再生成で消える可能性があります。