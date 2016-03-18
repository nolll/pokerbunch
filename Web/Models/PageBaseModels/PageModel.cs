using Core.UseCases;
using Web.Models.MiscModels;

namespace Web.Models.PageBaseModels
{
    public abstract class PageModel
    {
        public string BrowserTitle { get; private set; }
        public string CssUrl { get; private set; }
        public string Version { get; private set; }
        public GoogleAnalyticsModel GoogleAnalyticsModel { get; private set; }
        public string JsPath { get; private set; }
        public string JsLoaderPath { get; private set; }
        public virtual string Layout => ContextLayout.Base;
        public virtual string HtmlCssClass => null;
        public virtual string BodyCssClass => "body-wide";

        protected PageModel(string browserTitle, BaseContext.Result contextResult)
        {
            BrowserTitle = browserTitle;
            CssUrl = $"/assets/styles.css?v={contextResult.Version}";
            Version = contextResult.Version;
            GoogleAnalyticsModel = new GoogleAnalyticsModel(contextResult);
            JsPath = contextResult.IsInProduction ? $"/assets/scripts.js?v={contextResult.Version}" : "/Frontend/js/lib/require.js";
            JsLoaderPath = contextResult.IsInProduction ? null : "/FrontEnd/js/require.loader";
        }
    }
}