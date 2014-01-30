namespace Application.Services.Interfaces
{
    public interface IConfigService
    {
        string GetAppSetting(string key);
        string GetConnectionString(string key);
    }
}