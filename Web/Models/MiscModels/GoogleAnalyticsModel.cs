using Web.Extensions;

namespace Web.Models.MiscModels
{
    public class GoogleAnalyticsModel : IViewModel
    {
        public bool EnableAnalytics { get; private set; }

        public GoogleAnalyticsModel()
        {
            EnableAnalytics = SiteSettings.EnableAnalytics;
        }

        public View GetView()
        {
            return new View("~/Views/Misc/GoogleAnalytics.cshtml");
        }
    }
}