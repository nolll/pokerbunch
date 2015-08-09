using Core.UseCases;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public abstract class AppPageModel : WrappedPageModel
    {
        public NavigationModel UserNavModel { get; private set; }

        protected AppPageModel(string browserTitle, AppContext.Result appContextResult) : base(browserTitle, appContextResult.BaseContext)
        {
            UserNavModel = new UserNavigationModel(appContextResult);
        }

        public override string Layout
        {
            get { return ContextLayout.App; }
        }
    }
}