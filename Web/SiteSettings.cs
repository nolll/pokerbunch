namespace Web
{
    public class SiteSettings : CommonSettings
    {
        public static string SiteHost => Get("SiteHost") ?? "pokerbunch.com";
        public static bool HandleErrors => GetBool("HandleErrors");
        public static bool UseAssets => GetBool("UseAssets");
        public static bool EnableAnalytics => GetBool("EnableAnalytics");
        public static bool EnableApplicationInsights => GetBool("EnableApplicationInsights");
        public static string ApplicationInsightsKey => Get("ApplicationInsightsKey");
        public static string ApiHost => Get("ApiHost") ?? "api.pokerbunch.com";
        public static string ApiKey => Get("ApiKey");
        public static string EnvironmentName => Get("Environment");
        public static string ApiUrl => $"{ApiProtocol}://{ApiHost}";
        public static bool DetailedErrorsForApi => GetBool("DetailedErrorsForApi");

        private static string ApiProtocol => HttpsForApi ? "https" : "http";
        private static bool HttpsForApi => GetBool("HttpsForApi");
    }
}
