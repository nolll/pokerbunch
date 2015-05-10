using Core.UseCases.BunchContext;
using Core.UseCases.BunchList;
using Core.UseCases.Home;
using Web.Models.HomegameModels.List;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.HomeModels
{
    public class HomePageModel : BunchPageModel
    {
        public bool IsLoggedIn { get; private set; }
        public string AddBunchUrl { get; private set; }
        public string LoginUrl { get; private set; }
        public string RegisterUrl { get; private set; }
        public NavigationModel AdminNav { get; private set; }
        public BunchListModel BunchList { get; private set; }

        public HomePageModel(BunchContextResult contextResult, HomeResult homeResult, BunchListResult bunchListResult)
            : base("Poker Bunch", contextResult)
        {
			IsLoggedIn = homeResult.IsLoggedIn;
            AddBunchUrl = homeResult.AddBunchUrl.Relative;
            LoginUrl = homeResult.LoginUrl.Relative;
            RegisterUrl = homeResult.AddUserUrl.Relative;
            AdminNav = new AdminNavigationModel(homeResult);
            BunchList = new BunchListModel(bunchListResult);
        }
    }
}