using Web.Extensions;
using Web.InlineCode;
using Web.Settings;

namespace Web.Models.MiscModels
{
    public class GoogleAnalyticsModel : IViewModel
    {
        public string Html { get; }
        public bool IsEnabled { get; }

        public GoogleAnalyticsModel(AppSettings appSettings)
        {
            var gaScript = new GoogleAnalyticsScript();
            Html = gaScript.Html;
            IsEnabled = appSettings.EnableAnalytics;
        }

        public View GetView()
        {
            return new View("~/Views/Misc/GoogleAnalytics.cshtml");
        }
    }
}