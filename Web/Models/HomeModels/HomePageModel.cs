using Application.Urls;
using Application.UseCases.BunchContext;
using Application.UseCases.Home;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.HomeModels
{
    public class HomePageModel : BunchPageModel
    {
        public bool IsLoggedIn { get; private set; }
        public Url AddBunchUrl { get; private set; }
        public Url LoginUrl { get; private set; }
        public Url RegisterUrl { get; private set; }
        public NavigationModel AdminNav { get; private set; }

        public HomePageModel(BunchContextResult contextResult, HomeResult homeResult)
            : base("Poker Bunch", contextResult)
        {
			IsLoggedIn = homeResult.IsLoggedIn;
            AddBunchUrl = homeResult.AddBunchUrl;
            LoginUrl = homeResult.LoginUrl;
            RegisterUrl = homeResult.AddUserUrl;
            AdminNav = new AdminNavigationModel(homeResult);
        }
    }
}