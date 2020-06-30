using System.Collections.Generic;
using System.Globalization;

namespace MyLocalization
{
    /// <summary>
    /// ローカライズ文字列の取得元
    /// </summary>
    public interface IMyLocalizedStrings
    {
        string GetString(string name, CultureInfo currentUICulture);

        IEnumerable<string> GetAllKey();
    }
}
