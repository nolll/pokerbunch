using System.Configuration;

namespace Infrastructure.Config{
    public class Settings : ISettings
	{

		public string GetTwitterKey(){
            return ConfigurationManager.AppSettings.Get("twitterKey");
		}

		public string GetTwitterSecret(){
            return ConfigurationManager.AppSettings.Get("twitterSecret");
		}

		private string GetServerMode(){
            return ConfigurationManager.AppSettings.Get("mode");
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
            return ConfigurationManager.AppSettings.Get("databaseHost");
		}

		public string GetDatabaseName(){
            return ConfigurationManager.AppSettings.Get("databaseName");
		}

		public string GetDatabaseUserName(){
            return ConfigurationManager.AppSettings.Get("databaseUserName");
		}

		public string GetDatabasePassword()
		{
		    return ConfigurationManager.AppSettings.Get("databasePassword");
		}

        public string GetSiteUrl()
        {
            return ConfigurationManager.AppSettings.Get("siteUrl");
        }

	}

}