using System;

namespace MyLocalization
{
    /// <summary>
    /// IMyLocalizedStringsを提供するもの
    /// </summary>
    public interface IMyLocalizedStringsProvider
    {
        IMyLocalizedStrings GetLocalizedStrings(Type resourceSource);
    }
}
