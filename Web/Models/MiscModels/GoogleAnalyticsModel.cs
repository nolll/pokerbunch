using Web.Extensions;
using Web.Settings;

namespace Web.Models.MiscModels
{
    public class GoogleAnalyticsModel : IViewModel
    {
        public bool EnableAnalytics { get; }

        public GoogleAnalyticsModel(AppSettings appSettings)
        {
            EnableAnalytics = appSettings.EnableAnalytics;
        }

        public View GetView()
        {
            return new View("~/Views/Misc/GoogleAnalytics.cshtml");
        }
    }
}