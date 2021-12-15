namespace Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor.Commands;

public class UpsertLocalizationCommand
{
    public string Key { get; set; }

    public string Locale { get; set; }

    public string Value { get; set; }
}