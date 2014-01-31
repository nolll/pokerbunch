using Application.Config;

namespace Application.Services
{
    public class Settings : ISettings
	{
        private readonly IConfigService _configService;

        public Settings(IConfigService configService)
        {
            _configService = configService;
        }

        public string GetTwitterKey(){
            return _configService.GetAppSetting("TwitterKey");
		}

		public string GetTwitterSecret(){
            return _configService.GetAppSetting("TwitterSecret");
		}

		private string GetServerMode(){
            return _configService.GetAppSetting("Mode");
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
            return _configService.GetConnectionString("pokerbunch");
        }

        public string GetSiteUrl()
        {
            return _configService.GetAppSetting("SiteUrl");
        }

	}
}