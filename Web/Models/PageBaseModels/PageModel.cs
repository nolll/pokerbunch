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

        protected PageModel(string browserTitle, BaseContext.Result contextResult)
        {
            BrowserTitle = browserTitle;
            CssUrl = BundleConfig.BundleUrl;
            Version = contextResult.Version;
            GoogleAnalyticsModel = new GoogleAnalyticsModel(contextResult);
            JsPath = contextResult.IsInProduction ? string.Format("/Scripts/main.js?v={0}", contextResult.Version) : "/Scripts/require.js";
            JsLoaderPath = contextResult.IsInProduction ? null : "/FrontEnd/js/require.loader.dev";
        }

        public virtual string Layout
        {
            get { return ContextLayout.Base; }
        }

        public virtual string HtmlCssClass
        {
            get { return null; }
        }

        public virtual string BodyCssClass
        {
            get { return "body-wide"; }
        }
    }
}