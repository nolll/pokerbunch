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

		public string GetDatabaseHost(){
            return WebConfigurationManager.AppSettings.Get("DatabaseHost");
		}

		public string GetDatabaseName(){
            return WebConfigurationManager.AppSettings.Get("DatabaseName");
		}

		public string GetDatabaseUserName(){
            return WebConfigurationManager.AppSettings.Get("DatabaseUserName");
		}

		public string GetDatabasePassword()
		{
            return WebConfigurationManager.AppSettings.Get("DatabasePassword");
		}

        public string GetSiteUrl()
        {
            return WebConfigurationManager.AppSettings.Get("SiteUrl");
        }

	}

}