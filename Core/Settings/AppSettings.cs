namespace Core.Settings
{
    public class AppSettings
    {
        public string Version { get; set; }
        public bool EnableAnalytics { get; set; }
        public bool DetailedErrorsForApi { get; set; }
        public bool UseFakeData { get; set; }
        public string ApiKey { get; set; }
        public UrlSettings Urls { get; set; }
        public LoggingSettings Logging { get; set; }
    }
}