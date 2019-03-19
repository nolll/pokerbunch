using Core.UseCases;
using Web.Extensions;
using Web.Models.MiscModels;
using Web.Services;
using Web.Settings;

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

        protected PageModel()
        {
            Version = SiteSettings.Version;
            GoogleAnalyticsModel = new GoogleAnalyticsModel();
            VueConfig = new VueConfigModel();
        }

        public abstract View GetView();
    }
}