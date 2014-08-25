using Application.UseCases.AppContext;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public abstract class AppPageModel : PageModel
    {
        public NavigationModel UserNavModel { get; private set; }

        protected AppPageModel(string browserTitle, AppContextResult appContextResult) : base(browserTitle, appContextResult.BaseContext)
        {
            UserNavModel = new UserNavigationModel(appContextResult);
        }

        public override string Layout
        {
            get { return ContextLayout.App; }
        }
    }
}