using System.Collections.Generic;
using Localization.Accessor.Contracts.Contracts;

namespace Localization.Accessor.Service.Service.Infrastructure
{
    /// <summary>
    /// Used on the presentation layer to get the information about the available languages.
    /// </summary>
    public class LocalizationStoreInformation
    {
        public HashSet<Locale> Locales { get; set; } = new HashSet<Locale>();

        public string DefaultLanguage { get; set; }
    }
}