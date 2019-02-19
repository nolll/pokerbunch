using Core.UseCases;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public abstract class AppPageModel : WrappedPageModel
    {
        public NavigationModel UserNavModel { get; }

        protected AppPageModel(CoreContext.Result appContextResult)
        {
            UserNavModel = new UserNavigationModel(appContextResult);
        }

        public override string Layout => ContextLayout.App;
    }
}