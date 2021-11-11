using System.Data;

namespace Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor
{
    public class GetLocalizationQuery
    {
        public string Key { get; set; }

        public string Locale { get; set; }
    }
}