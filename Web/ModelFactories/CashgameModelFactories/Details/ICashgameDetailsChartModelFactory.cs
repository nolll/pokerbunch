using Core.Classes;
using Web.Models.ChartModels;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public interface ICashgameDetailsChartModelFactory
    {
        ChartModel Create(Homegame homegame, Cashgame cashgame);
    }
}