# ロギング、NLogを使う場合

.NET Coreでは`ILogger`が用意されているので、これを使ってロギングを行う。

`ILogger`はインジェクションすることで取得可能

```cs
private readonly ILogger<Program> _logger;

public Program(
    ILogger<Program> logger)
{
    _logger = logger;
}
```
出力する場合
```cs
_logger.LogInformation("hogehoge");
```

`ILogger`を使ってロギングを行うことで
ロギングライブラリに依存することなくログ出力のロジックが記述できる。

Javaの`commons-logging`みたいな感じかな



## NLogを使う場合

### コンソールアプリの場合
依存関係に
- `NLog`
- `NLog.Extensions.Logging`

ファイルは`NLog.config`が必要

設定はNLogのサイトのままですが
```cs
//NLogを使うための設定
service.AddLogging(builder =>
{
    builder.SetMinimumLevel(LogLevel.Trace);
    builder.AddNLog(new NLogProviderOptions
    {
        CaptureMessageTemplates = true,
        CaptureMessageProperties = true
    });
});
```

でロギングはNLogを使って行われるようになる。

別のライブラリに差し替えたい場合も、設定に関する部分だけを変更すればよい

### webの場合
依存は
- `NLog`
- `NLog.Web.AspNetCore`

CreateWebHostBuilderで
```cs
.ConfigureLogging(logging =>
{
    //既存のログ設定をクリアする
    logging.ClearProviders();
    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
})
.UseNLog();
```