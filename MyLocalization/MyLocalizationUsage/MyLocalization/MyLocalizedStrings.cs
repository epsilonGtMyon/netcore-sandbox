using MyLocalization;
using MyLocalizationUsage.MyLocalization.Dto;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MyLocalizationUsage.MyLocalization
{
    /// <summary>
    /// IMyLocalizedStringsの実装
    /// 
    /// とりあえずシンプルに 1行に複数ロケールの情報を持つようにした。
    /// </summary>
    public class MyLocalizedStrings : IMyLocalizedStrings
    {
        private readonly IDictionary<string, LocalizationRecord> _records;

        public MyLocalizedStrings(IDictionary<string, LocalizationRecord> records)
        {
            _records = records;
        }


        public static MyLocalizedStrings FromEnumerable(IEnumerable<LocalizationRecord> src)
        {
            IDictionary<string, LocalizationRecord> records = src.ToDictionary(x => x.Key);
            return new MyLocalizedStrings(records);
        }

        public IEnumerable<string> GetAllKey()
        {
            return _records.Keys;
        }

        public string GetString(string name, CultureInfo currentUICulture)
        {
            if (_records.TryGetValue(name, out LocalizationRecord record))
            {
                switch (currentUICulture.Name)
                {
                    case "ja": return record.JaJp;
                    case "ja-JP": return record.JaJp;

                    case "en-US": return record.EnUs;
                }
            }
            return null;
        }
    }
}
