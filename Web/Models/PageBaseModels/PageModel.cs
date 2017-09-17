using Core.UseCases;
using Web.Extensions;
using Web.Models.MiscModels;

namespace Web.Models.PageBaseModels
{
    public abstract class PageModel : IViewModel
    {
        public string CssUrl { get; }
        public string Version { get; }
        public GoogleAnalyticsModel GoogleAnalyticsModel { get; }
        public string JsPath { get; }
        public string JsLoaderPath { get; }
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

        public abstract View GetView();
    }
}