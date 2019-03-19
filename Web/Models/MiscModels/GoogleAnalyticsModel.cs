using Web.Extensions;
using Web.Services;
using Web.Settings;

namespace Web.Models.MiscModels
{
    public class GoogleAnalyticsModel : IViewModel
    {
        public bool EnableAnalytics { get; }

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