namespace Localization.Accessor.Service.Accessors.CacheAccessor.Service;

public class CacheLocalizationConfig
{        
    /// <summary>
    /// Name of the key in Session that stores the
    /// current selected language.
    /// </summary>
    public string SessionStoreKeyName { get; set; } = "locale";

    public string DefaultLanguage { get; set; }
}