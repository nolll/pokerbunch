using Core.UseCases;
using Web.Extensions;
using Web.Models.HomegameModels.List;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

namespace Web.Models.HomeModels
{
    public class HomePageModel : BunchPageModel
    {
        public bool IsLoggedIn { get; private set; }
        public string AddBunchUrl { get; private set; }
        public string LoginUrl { get; private set; }
        public string RegisterUrl { get; private set; }
        public string ApiDocsUrl { get; private set; }
        public NavigationModel AdminNav { get; private set; }
        public BunchListModel BunchList { get; private set; }

        public HomePageModel(BunchContext.Result contextResult, BunchList.Result bunchListResult)
            : base(contextResult)
        {
            IsLoggedIn = contextResult.AppContext.IsLoggedIn;
            AddBunchUrl = new AddBunchUrl().Relative;
            LoginUrl = new LoginUrl().Relative;
            RegisterUrl = new AddUserUrl().Relative;
            ApiDocsUrl = new ApiDocsUrl().Relative;
            AdminNav = new AdminNavigationModel(contextResult.AppContext);
            BunchList = new BunchListModel(bunchListResult);
        }

        public override string BrowserTitle => "Poker Bunch";

        public override View GetView()
        {
            return new View("~/Views/Pages/Home/Index.cshtml");
        }
    }
}