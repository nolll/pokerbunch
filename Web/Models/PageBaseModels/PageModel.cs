using Core.UseCases;
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
        public string StyleView => "~/Views/Generated/Style.cshtml";
        public string ScriptView => "~/Views/Generated/Script.cshtml";

        protected PageModel(BaseContext.Result contextResult)
        {
            Version = contextResult.Version;
            GoogleAnalyticsModel = new GoogleAnalyticsModel();
            VueConfig = new VueConfigModel();
        }

        public abstract View GetView();
    }
}