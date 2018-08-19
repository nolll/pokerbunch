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
        public virtual string Layout => ContextLayout.Base;
        public virtual string HtmlCssClass => null;
        public virtual string BodyCssClass => "body-wide";
        public abstract string BrowserTitle { get; }

        protected PageModel(BaseContext.Result contextResult)
        {
            CssUrl = $"/dist/{contextResult.Version}/1.main.css";
            Version = contextResult.Version;
            GoogleAnalyticsModel = new GoogleAnalyticsModel();
            JsPath = $"/dist/{contextResult.Version}/main.js";
        }

        public abstract View GetView();
    }
}