using Core.UseCases.BaseContext;

namespace Web.Models.PageBaseModels
{
    public abstract class WrappedPageModel : PageModel
    {
        protected WrappedPageModel(string browserTitle, BaseContextResult contextResult)
            : base(browserTitle, contextResult)
        {
        }

        public override string Layout
        {
            get { return ContextLayout.Wrapped; }
        }

        public override string BodyCssClass
        {
            get { return ""; }
        }
    }
}