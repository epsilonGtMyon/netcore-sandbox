using Microsoft.Extensions.Localization;
using System;
using System.Collections.Concurrent;

namespace MyLocalization
{
    /// <summary>
    /// MyStringLocalizer を作るクラスです。
    /// ResourceManagerStringLocalizerFactoryを参考にした。
    /// 
    /// IMyLocalizedStringsProviderにローカライズ文字列の一覧取得は任せている。
    /// 
    /// キャッシュのリフレッシュが未考慮
    /// </summary>
    public class MyStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly ConcurrentDictionary<RuntimeTypeHandle, MyStringLocalizer> _localizerCache =
            new ConcurrentDictionary<RuntimeTypeHandle, MyStringLocalizer>();

        private readonly IMyLocalizedStringsProvider _myLocalizedStringsProvider;

        public MyStringLocalizerFactory(IMyLocalizedStringsProvider myLocalizedStringsProvider)
        {
            _myLocalizedStringsProvider = myLocalizedStringsProvider;
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            throw new NotImplementedException();
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return _localizerCache.GetOrAdd(resourceSource.TypeHandle, _ => CreateMyStringLocalizer(resourceSource));
        }

        protected virtual MyStringLocalizer CreateMyStringLocalizer(Type resourceSource)
        {
            IMyLocalizedStrings localizedStrings = _myLocalizedStringsProvider.GetLocalizedStrings(resourceSource);
            return new MyStringLocalizer(localizedStrings);
        }
    }
}
