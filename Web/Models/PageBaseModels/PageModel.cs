using Core.UseCases;
using Web.Models.MiscModels;

namespace Web.Models.PageBaseModels
{
    public abstract class PageModel
    {
        private string _browserTitle;

        public string CssUrl { get; private set; }
        public string Version { get; private set; }
        public GoogleAnalyticsModel GoogleAnalyticsModel { get; private set; }
        public string JsPath { get; private set; }
        public string JsLoaderPath { get; private set; }
        public virtual string Layout => ContextLayout.Base;
        public virtual string HtmlCssClass => null;
        public virtual string BodyCssClass => "body-wide";
        public abstract string BrowserTitle { get; }

        protected PageModel(BaseContext.Result contextResult)
        {
            CssUrl = $"/assets/{contextResult.Version}/styles.css";
            Version = contextResult.Version;
            GoogleAnalyticsModel = new GoogleAnalyticsModel();
            JsPath = SiteSettings.UseAssets ? $"/assets/{contextResult.Version}/scripts.js" : "/Frontend/js/lib/require.js";
            JsLoaderPath = SiteSettings.UseAssets ? null : "/FrontEnd/js/require.loader";
        }
    }
}