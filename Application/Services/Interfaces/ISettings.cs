namespace Application.Services
{
    public interface ISettings
    {
        string GetTwitterKey();
        string GetTwitterSecret();
        string GetSiteUrl();
        string GetConnectionString();
    }
}