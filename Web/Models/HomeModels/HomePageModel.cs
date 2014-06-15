using Application.Urls;
using Application.UseCases.BunchContext;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.HomeModels
{
    public class HomePageModel : PageModel
    {
        public bool IsLoggedIn { get; private set; }
        public Url AddHomegameUrl { get; private set; }
        public Url LoginUrl { get; private set; }
        public Url RegisterUrl { get; private set; }
        public NavigationModel AdminNav { get; private set; }

        public HomePageModel(BunchContextResult bunchContextResult)
            : base("Poker Bunch", bunchContextResult)
        {
			IsLoggedIn = bunchContextResult.IsLoggedIn;
            AddHomegameUrl = new AddHomegameUrl();
            LoginUrl = new LoginUrl();
            RegisterUrl = new AddUserUrl();
            AdminNav = new AdminNavigationModel(bunchContextResult);
        }
    }
}