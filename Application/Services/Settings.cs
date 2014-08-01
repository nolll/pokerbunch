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