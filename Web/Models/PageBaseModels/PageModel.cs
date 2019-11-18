using Core.Settings;
using Web.Extensions;
using Web.Models.MiscModels;

namespace Web.Models.PageBaseModels
{
    public abstract class PageModel : IViewModel
    {
        public string Version { get; }
        public GoogleAnalyticsModel GoogleAnalyticsModel { get; }
        public virtual string Layout => ContextLayout.Base;
        public virtual string HtmlCssClass => null;
        public virtual string BodyCssClass => "body-wide";
        public VueConfigModel VueConfig { get; }
        public abstract string BrowserTitle { get; }
        public static string StyleView => "~/Views/Generated/Style.cshtml";
        public static string ScriptView => "~/Views/Generated/Script.cshtml";

        protected PageModel(AppSettings appSettings)
        {
            Version = appSettings.Version;
            GoogleAnalyticsModel = new GoogleAnalyticsModel(appSettings);
            VueConfig = new VueConfigModel(appSettings);
        }

        public abstract View GetView();
    }
}