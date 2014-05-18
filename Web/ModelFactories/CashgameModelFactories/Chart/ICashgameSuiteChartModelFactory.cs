using Core.Entities;
using Web.Models.ChartModels;

namespace Web.ModelFactories.CashgameModelFactories.Chart
{
    public interface ICashgameSuiteChartModelFactory
    {
        ChartModel Create(CashgameSuite suite);
    }
}