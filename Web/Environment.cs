namespace Web
{
    public static class Environment
    {
        public static bool IsDev => SiteSettings.EnvironmentName.Equals("dev");
        public static bool IsTest => SiteSettings.EnvironmentName.Equals("test");
        public static bool IsStage => SiteSettings.EnvironmentName.Equals("stage");
        public static bool IsProd => SiteSettings.EnvironmentName.Equals("prod");
    }
}