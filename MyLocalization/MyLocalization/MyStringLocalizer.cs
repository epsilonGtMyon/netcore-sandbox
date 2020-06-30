using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MyLocalization
{
    /// <summary>
    /// IStringLocalizerの実装
    /// 
    /// IMyLocalizedStringsに文字列の解決は任せている。
    /// </summary>
    public class MyStringLocalizer : IStringLocalizer
    {
        private readonly IMyLocalizedStrings _myLocalizedStrings;

        public MyStringLocalizer(IMyLocalizedStrings myLocalizedStrings)
        {
            _myLocalizedStrings = myLocalizedStrings;
        }

        /// <inheritdoc/>
        public LocalizedString this[string name]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var value = GetString(name);

                return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: null);
            }
        }

        /// <inheritdoc/>
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);

                return new LocalizedString(name, value, resourceNotFound: format == null, searchedLocation: null);
            }
        }

        private string GetString(string name, CultureInfo culture = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var keyCulture = culture ?? CultureInfo.CurrentUICulture;

            return _myLocalizedStrings.GetString(name, keyCulture);
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            //includeParentCulturesがよーわからん

            IEnumerable<string> allKey = _myLocalizedStrings.GetAllKey();

            var culture = CultureInfo.CurrentUICulture;
            foreach (var key in allKey)
            {
                var value = GetString(key, culture);
                yield return new LocalizedString(key, value ?? key, resourceNotFound: value == null, searchedLocation: null);
            }
        }

        [Obsolete("This method is obsolete. Use `CurrentCulture` and `CurrentUICulture` instead.")]
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
