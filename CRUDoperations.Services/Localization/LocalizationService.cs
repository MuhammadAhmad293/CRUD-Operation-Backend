namespace CRUDoperations.Services.Localization
{
    public class LocalizationService : LocalizationFileReader.LocalizationFileReader, ILocalizationService
    {
        public LocalizationService() : base("localizationFile") { }

        public string GeneralError => GetKeyValue("GeneralError", "altValue");

        public string GeneralSuccess => GetKeyValue("GeneralSuccess", "altValue");

        public string InvalidRequest => GetKeyValue("InvalidRequest", "altValue");
        public string NoDataFound => GetKeyValue("NoDataFound", "altValue");
    }
}
