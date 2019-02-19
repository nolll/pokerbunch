namespace Web
{
    public class SiteSettings : CommonSettings
    {
        public static string SiteHost => Get("SiteHost") ?? "pokerbunch.com";
        public static bool HandleErrors => GetBool("HandleErrors");
        public static bool EnableAnalytics => GetBool("EnableAnalytics");
        public static bool EnableApplicationInsights => GetBool("EnableApplicationInsights");
        public static string ApplicationInsightsKey => Get("ApplicationInsightsKey");
        public static string ApiHost => Get("ApiHost") ?? "api.pokerbunch.com";
        public static string ApiKey => Get("ApiKey");
        public static string EnvironmentName => Get("Environment");
        public static bool DetailedErrorsForApi => GetBool("DetailedErrorsForApi");
        public static string ApiProtocol => HttpsForApi ? "https" : "http";
        public static bool UseFakeData => GetBool("UseFakeData");
        private static bool HttpsForApi => GetBool("HttpsForApi");
        public static string Version => Get("Version");
    }
}
