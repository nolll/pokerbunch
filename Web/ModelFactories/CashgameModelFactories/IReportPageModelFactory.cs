using Core.Classes;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Report;

namespace Web.ModelFactories.CashgameModelFactories
{
    public interface IReportPageModelFactory
    {
        ReportPageModel Create(User user, Homegame homegame, Player player, Cashgame runningGame);
        ReportPageModel Create(User user, Homegame homegame, Player player, Cashgame runningGame, ReportPostModel postModel);
    }
}