namespace Localization.Accessor.Service.Service.Infrastructure
{
    public class DefaultValueParam
    {
        public DefaultValueParam(string value, string language = "")
        {
            Value = value;
            if (!string.IsNullOrWhiteSpace(language) && language.Length >= 2)
            {
                Language = language?.ToUpperInvariant().Substring(0, 2);
            }
        }

        public string Value { get; private set; }

        public string Language { get; private set; }
    }
}