using Core.Settings;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.VueModels
{
    public class VuePageModel : PageModel
    {
        public VuePageModel(AppSettings appSettings)
            : base(appSettings)
        {
        }

        public override string HtmlCssClass => "page-bg";
        public override string BodyCssClass => null;
        public override string BrowserTitle => "Poker Bunch";

        public override View GetView()
        {
            return new View("~/Views/Pages/Vue/Root.cshtml");
        }
    }
}