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
        public string VueConfigScriptTag { get; }
        public abstract string BrowserTitle { get; }
        public static string StyleView => "~/Views/Generated/Style.cshtml";
        public static string ScriptView => "~/Views/Generated/Script.cshtml";
        public string FontStyleTag => FontStyleService.StyleTag;

        protected PageModel(AppSettings appSettings)
        {
            Version = appSettings.Version;
            GoogleAnalyticsModel = new GoogleAnalyticsModel(appSettings);
            VueConfigScriptTag = VueConfigScriptService.ScriptTag(appSettings);
        }

        public abstract View GetView();
    }
}