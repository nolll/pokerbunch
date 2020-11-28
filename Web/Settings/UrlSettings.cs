using System;

namespace Web.Settings
{
    public class UrlSettings
    {
        public string Site { get; set; } = "pokerbunch.com";
        public Uri SiteUri => SettingsConverter.ToUri(Site);
        public string SiteHost => SiteUri.Host;
        public int SitePort => SiteUri.Port;
        public string Api { get; set; } = "api.pokerbunch.com";
        public Uri ApiUri => SettingsConverter.ToUri(Api);
        public string ApiHost => ApiUri.Host;
        public int ApiPort => ApiUri.Port;
    }
}