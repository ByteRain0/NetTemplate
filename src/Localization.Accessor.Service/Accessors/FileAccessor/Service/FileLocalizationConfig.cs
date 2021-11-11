namespace Localization.Accessor.Service.Accessors.FileAccessor.Service
{
    public class FileLocalizationConfig
    {
        /// <summary>
        /// Path where the json localization files are located
        /// </summary>
        public string Path { get; set; } = "Localization/";

        public string DefaultLocale { get; set; } = "EN-en";
    }
}