namespace Web.Models.MiscModels
{
    public class GoogleAnalyticsModel
    {
        public bool EnableAnalytics { get; private set; }

        public GoogleAnalyticsModel()
        {
            EnableAnalytics = SiteSettings.EnableAnalytics;
        }
    }
}