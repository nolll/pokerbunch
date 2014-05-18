using Core.Entities;
using Web.Models.ChartModels;

namespace Web.ModelFactories.CashgameModelFactories.Action
{
    public interface IActionChartModelFactory
    {
        ChartModel Create(Homegame homegame, Cashgame cashgame, CashgameResult result);
    }
}