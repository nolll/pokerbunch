using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Report;

namespace Web.ModelFactories.CashgameModelFactories.Report
{
    public class ReportPageModelFactory : IReportPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public ReportPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public ReportPageModel Create(User user, Homegame homegame, Player player, Cashgame runningGame)
        {
            return new ReportPageModel
                {
                    BrowserTitle = "Report Stack",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
                };
        }

        public ReportPageModel Create(User user, Homegame homegame, Player player, Cashgame runningGame, ReportPostModel postModel)
        {
            var model = Create(user, homegame, player, runningGame);
            model.StackAmount = postModel.StackAmount;
            return model;
        }
    }
}