using System.Configuration;
using System.Web.Configuration;
using Core.Services;
using Infrastructure.Config;

namespace Web.Services{
    public class Settings : ISettings
	{
        public string GetTwitterKey(){
            return WebConfigurationManager.AppSettings.Get("TwitterKey");
		}

		public string GetTwitterSecret(){
            return WebConfigurationManager.AppSettings.Get("TwitterSecret");
		}

		private string GetServerMode(){
            return WebConfigurationManager.AppSettings.Get("Mode");
		}

		public bool IsInProduction(){
			return GetServerMode() == ServerMode.Production;
		}

		public bool IsInTest(){
			return GetServerMode() == ServerMode.Test;
		}

		public bool IsInDevelopment(){
			return GetServerMode() == ServerMode.Development;
		}

		public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["pokerbunch"].ConnectionString;
        }

        public string GetSiteUrl()
        {
            return WebConfigurationManager.AppSettings.Get("SiteUrl");
        }

	}

}