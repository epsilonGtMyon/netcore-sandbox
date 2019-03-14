using System.Collections.Generic;

namespace LocalizationSandbox.App.Common
{
    public class LocalizationConstants
    {
        public static readonly string CookieName = "cultureCookie";

        public static IReadOnlyCollection<string> SupportedCultures { get; private set; } = new List<string>
        {
            "ja-JP",
            "en-US",
        }.AsReadOnly();
    }
}
