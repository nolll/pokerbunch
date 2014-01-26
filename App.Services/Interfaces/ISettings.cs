namespace App.Services.Interfaces
{
    public interface ISettings
    {
        string GetTwitterKey();
        string GetTwitterSecret();
        bool IsInProduction();
        bool IsInTest();
        bool IsInDevelopment();
        string GetSiteUrl();
        string GetConnectionString();
    }
}