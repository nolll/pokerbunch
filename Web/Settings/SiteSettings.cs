namespace Web.Settings
{
    public static class SiteSettings
    {
        public static string SiteHost => SettingsReader.Get("SiteHost") ?? "pokerbunch.com";
        public static bool HandleErrors => SettingsReader.GetBool("HandleErrors");
        public static bool EnableAnalytics => SettingsReader.GetBool("EnableAnalytics");
        public static bool EnableApplicationInsights => SettingsReader.GetBool("EnableApplicationInsights");
        public static string ApplicationInsightsKey => SettingsReader.Get("ApplicationInsightsKey");
        public static string ApiHost => SettingsReader.Get("ApiHost") ?? "api.pokerbunch.com";
        public static string ApiKey => SettingsReader.Get("ApiKey");
        public static string EnvironmentName => SettingsReader.Get("Environment");
        public static bool DetailedErrorsForApi => SettingsReader.GetBool("DetailedErrorsForApi");
        public static string ApiProtocol => HttpsForApi ? "https" : "http";
        public static bool UseFakeData => SettingsReader.GetBool("UseFakeData");
        private static bool HttpsForApi => SettingsReader.GetBool("HttpsForApi");
        public static string Version => SettingsReader.Get("Version");
    }
}
