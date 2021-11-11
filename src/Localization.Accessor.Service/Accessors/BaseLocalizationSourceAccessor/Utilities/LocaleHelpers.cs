using System;

namespace Localization.Accessor.Service.Accessors.CacheAccessor.Service
{
    public static class LocaleHelpers
    {
        /// <summary>
        /// Strip locale from '-' if it contains it.
        /// </summary>
        /// <param name="locale"></param>
        /// <returns></returns>
        public static string StripLocale(this string locale)
        {
            if (locale.Contains('-'))
            {
                locale = locale[..locale.IndexOf("-", StringComparison.Ordinal)];
            }

            return locale;
        }
    }
}