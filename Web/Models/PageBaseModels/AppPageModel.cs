using Core.Settings;
using Core.UseCases;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public abstract class AppPageModel : WrappedPageModel
    {
        public NavigationModel UserNavModel { get; }

        protected AppPageModel(AppSettings appSettings, CoreContext.Result appContextResult)
            : base(appSettings)
        {
            UserNavModel = new UserNavigationModel(appContextResult);
        }

        public override string Layout => ContextLayout.App;
    }
}