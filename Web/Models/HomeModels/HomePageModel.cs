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
        public bool IsLoggedIn { get; }
        public string AddBunchUrl { get; }
        public string LoginUrl { get; }
        public string RegisterUrl { get; }
        public string ApiDocsUrl { get; }
        public NavigationModel AdminNav { get; }
        public BunchListModel BunchList { get; }

        public HomePageModel(BunchContext.Result contextResult, BunchList.Result bunchListResult)
            : base(contextResult)
        {
            IsLoggedIn = contextResult.AppContext.IsLoggedIn;
            AddBunchUrl = new AddBunchUrl().Relative;
            LoginUrl = new LoginUrl().Relative;
            RegisterUrl = new AddUserUrl().Relative;
            ApiDocsUrl = new ApiDocsIndexUrl().Relative;
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