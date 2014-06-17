using Web.Models.ChartModels;

namespace Web.ModelFactories.CashgameModelFactories.Chart
{
    public interface ICashgameSuiteChartJsonBuilder
    {
        ChartModel Build(string slug, int? year);
    }
}