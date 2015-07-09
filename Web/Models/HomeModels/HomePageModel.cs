using Core.Urls;
using Core.UseCases.BunchContext;
using Core.UseCases.BunchList;
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

        public HomePageModel(BunchContextResult contextResult, BunchListResult bunchListResult)
            : base("Poker Bunch", contextResult)
        {
            IsLoggedIn = contextResult.AppContext.IsLoggedIn;
            AddBunchUrl = new AddBunchUrl().Relative;
            LoginUrl = new LoginUrl().Relative;
            RegisterUrl = new AddUserUrl().Relative;
            AdminNav = new AdminNavigationModel(contextResult.AppContext);
            BunchList = new BunchListModel(bunchListResult);
        }
    }
}