using System.Collections.Generic;
using Core.Entities;
using Web.Models.CashgameModels.Chart;

namespace Web.ModelFactories.CashgameModelFactories.Chart
{
    public interface ICashgameChartPageBuilder
    {
        CashgameChartPageModel Build(Homegame homegame, int? year, IList<int> years);
    }
}