namespace CRUDoperations.Services.Localization
{
    public interface ILocalizationService
    {
        string GeneralError { get; }
        string GeneralSuccess { get; }
        string InvalidRequest { get; }
        string NoDataFound { get; }

    }
}
