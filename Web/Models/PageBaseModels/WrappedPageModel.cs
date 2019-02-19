namespace Web.Models.PageBaseModels
{
    public abstract class WrappedPageModel : PageModel
    {
        public override string Layout => ContextLayout.Wrapped;
        public override string HtmlCssClass => "page-bg";
        public override string BodyCssClass => null;
    }
}