using Web.Common;

namespace Web
{
    public class SiteSettings : CommonSettings
    {
        public static string SiteHost => Get("SiteHost");
        public static string ApiHost => Get("ApiHost");
        public static bool HandleErrors => GetBool("HandleErrors");
        public static bool ForceHttps => GetBool("ForceHttps");
        public static bool UseAssets => GetBool("UseAssets");
        public static bool EnableAnalytics => GetBool("EnableAnalytics");
        public static string ConnectionString => GetConnectionString("pokerbunch");
        public static bool EnableApplicationInsights => GetBool("EnableApplicationInsights");
        public static string ApplicationInsightsKey => Get("ApplicationInsightsKey");
    }
}
