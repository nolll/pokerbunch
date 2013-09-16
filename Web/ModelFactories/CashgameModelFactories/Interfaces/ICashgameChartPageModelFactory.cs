using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Chart;

namespace Web.ModelFactories.CashgameModelFactories
{
    public interface ICashgameChartPageModelFactory
    {
        CashgameChartPageModel Create(User user, Homegame homegame, int? year, IList<int> years, Cashgame runningGame);
    }
}