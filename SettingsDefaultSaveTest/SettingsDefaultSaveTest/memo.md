# Settings.settings に項目を追加

Visual Studio で：

プロジェクトのプロパティ
→ 設定
→ 以下を追加

名前	型	スコープ	値
UserName	string	User	初期ユーザー
WindowWidth	int	User	800
IsEnabled	bool	User	true

※ 保存テストをするなら スコープは User にしてください。
Application スコープは実行時に変更保存できません。



Save() の動作

これは User スコープの設定をユーザー設定ファイルに保存します。

内部的には概ね：

Properties.Settings.Default.Save();

↓

user.config に XML で書き込まれる



--------
保存ファイルを見る

実行後：

%LOCALAPPDATA%

を開く。

普通は

C:\Users\<User>\AppData\Local\
  CompanyName\
    アプリ名.exe_Url_xxxxx\
      バージョン\
         user.config
中身（例）
<?xml version="1.0" encoding="utf-8"?>
<configuration>
 <userSettings>
  <TestApp.Properties.Settings>
   <setting name="UserName" serializeAs="String">
    <value>2026/04/26 15:02:31</value>
   </setting>
  </TestApp.Properties.Settings>
 </userSettings>
</configuration>

Saveのたび更新されます。


-----



https://learn.microsoft.com/ja-jp/DOTNET/api/system.configuration.localfilesettingsprovider?view=netframework-4.6