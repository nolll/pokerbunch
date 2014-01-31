namespace Application.Services
{
    public interface IConfigService
    {
        string GetAppSetting(string key);
        string GetConnectionString(string key);
    }
}