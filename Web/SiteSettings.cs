using Web.Common;

namespace Web
{
    public class SiteSettings : CommonSettings
    {
        public static string SiteHost => Get("SiteHost");
        public static bool HandleErrors => GetBool("HandleErrors");
        public static bool UseAssets => GetBool("UseAssets");
        public static bool EnableAnalytics => GetBool("EnableAnalytics");
        public static string ConnectionString => Get("SqlConnectionString");
        public static bool EnableApplicationInsights => GetBool("EnableApplicationInsights");
        public static string ApplicationInsightsKey => Get("ApplicationInsightsKey");
        public static string ApiHost => Get("ApiHost");
        public static string ApiKey => Get("ApiKey");
        public static string EnvironmentName => Get("Environment");

        public static string ApiUrl => $"{ApiProtocol}://{ApiHost}";

        private static string ApiProtocol => Environment.IsProd ? "https" : "http";
    }
}
