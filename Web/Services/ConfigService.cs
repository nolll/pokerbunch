using System.Configuration;
using Application.Services;

namespace Web.Services
{
    public class ConfigService : IConfigService
    {
        public string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }

        public string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
}