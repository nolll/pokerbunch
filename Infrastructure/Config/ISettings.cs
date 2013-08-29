namespace Infrastructure.Config
{
    public interface ISettings
    {
        string GetTwitterKey();
        string GetTwitterSecret();
        bool IsInProduction();
        bool IsInTest();
        bool IsInDevelopment();
        string GetDatabaseHost();
        string GetDatabaseName();
        string GetDatabaseUserName();
        string GetDatabasePassword();
        string GetSiteUrl();
    }
}