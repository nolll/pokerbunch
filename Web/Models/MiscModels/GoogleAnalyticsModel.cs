using Web.Extensions;
using Web.Services;
using Web.Settings;

namespace Web.Models.MiscModels
{
    public class GoogleAnalyticsModel : IViewModel
    {
        public string Script { get; }
        public bool IsEnabled { get; }

        public GoogleAnalyticsModel(AppSettings appSettings)
        {
            Script = GaScriptService.ScriptTag;
            IsEnabled = appSettings.EnableAnalytics;
        }

        public View GetView()
        {
            return new View("~/Views/Misc/GoogleAnalytics.cshtml");
        }
    }
}