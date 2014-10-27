using System.Configuration;

namespace Core.Services
{
    public static class Env
    {
        public static bool IsInProduction
        {
            get { return bool.Parse(ConfigurationManager.AppSettings.Get("InProduction")); }
        }
    }
}
