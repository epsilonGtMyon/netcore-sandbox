# ローカライズ

## `Startup`での記述

### `ConfigureServices`

```cs
services.AddLocalization(options => options.ResourcesPath = "Resources");
```

`ResourcesPath`でリソースのルートとなるディレクトリを指定する。


### `Configure`

ミドルウェアの設定、サポートしているカルチャやデフォルトのカルチャ、Cookie名を設定している。
```cs
app.UseRequestLocalization(options =>
{
    CultureInfo[] supportedCultures = LocalizationConstants.SupportedCultures
                                        .Select(x => new CultureInfo(x))
                                        .ToArray();

    //cookie名の変更
    CookieRequestCultureProvider cookieCultureProvider = options.RequestCultureProviders.FirstOrDefault(prov => prov is CookieRequestCultureProvider) as CookieRequestCultureProvider;
    cookieCultureProvider.CookieName = LocalizationConstants.CookieName;

    options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
    // Formatting numbers, dates, etc.
    options.SupportedCultures = supportedCultures;
    // UI strings that we have localized.
    options.SupportedUICultures = supportedCultures;
});
```

```cs
public class LocalizationConstants
{
    public static readonly string CookieName = "cultureCookie";

    public static IReadOnlyCollection<string> SupportedCultures { get; private set; } = new List<string>
    {
        "ja-JP",
        "en-US",
    }.AsReadOnly();
}
```

## リソースの用意

### メッセージ用のクラスを用意
メッセージの取得は`IStringLocalizer<T>`を使う。

このTの部分は任意のクラスが使えるがある程度統一しておいたほうがよさそうなので
そのためのクラスを用意
```cs
namespace LocalizationSandbox.Localization
{
    public class AppMessage
    {
        public static readonly string Message01 = nameof(Message01);
    }
}
```

リソースは`Resources/Localization`フォルダに

- `AppMessage.ja-JP.resx`
- `AppMessage.en-US.resx`

と用意する。


## ロケールの決定

- リクエストパラメータ
- Cookie
- リクエストヘッダ

の順番で決定される。

今回はCookieに保存する

## ロケールの設定

```cs
//ja-JPにする場合
Response.Cookies.Append(LocalizationConstants.CookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture('ja-JP')));
```

## メッセージの取得
Razorは使ってないです...

コンストラクタで注入
```cs
private readonly IStringLocalizer<AppMessage> _localizer;

public HomeController(IStringLocalizer<AppMessage> localizer)
{
    _localizer = localizer;
}
```

利用
```cs
_localizer[AppMessage.Message01];
```