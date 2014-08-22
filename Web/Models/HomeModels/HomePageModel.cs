using Application.Urls;
using Application.UseCases.BunchContext;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.HomeModels
{
    public class HomePageModel : BunchPageModel
    {
        public bool IsLoggedIn { get; private set; }
        public Url AddHomegameUrl { get; private set; }
        public Url LoginUrl { get; private set; }
        public Url RegisterUrl { get; private set; }
        public NavigationModel AdminNav { get; private set; }

        public HomePageModel(BunchContextResult contextResult)
            : base("Poker Bunch", contextResult)
        {
			IsLoggedIn = contextResult.AppContext.IsLoggedIn;
            AddHomegameUrl = new AddHomegameUrl();
            LoginUrl = new LoginUrl();
            RegisterUrl = new AddUserUrl();
            AdminNav = new AdminNavigationModel(contextResult.AppContext);
        }
    }
}