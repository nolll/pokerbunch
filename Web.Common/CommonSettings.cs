using System;

namespace Web.Common
{
    public class CommonSettings
    {
        private static CommonSettings _instance;

        public string SiteHost { get; }
        public string ApiHost { get; }

        protected CommonSettings(string siteHost, string apiHost)
        {
            SiteHost = siteHost;
            ApiHost = apiHost;
        }

        public static void Init(string siteHost, string apiHost)
        {
            _instance = new CommonSettings(siteHost, apiHost);
        }

        public static void Init(CommonSettings commonSettings)
        {
            _instance = commonSettings;
        }

        public static CommonSettings Instance
        {
            get
            {
                if(_instance == null)
                    throw new Exception("No instance of CommonSettings");
                return _instance;
            }
        }
    }
}