using System.Configuration;
using System.Web.Configuration;
using Application.Services.Interfaces;

namespace Web.Services
{
    public class ConfigService : IConfigService
    {
        public string GetAppSetting(string key)
        {
            return WebConfigurationManager.AppSettings.Get(key);
        }

        public string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
}