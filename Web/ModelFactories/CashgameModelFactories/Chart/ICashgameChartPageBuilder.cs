using Web.Models.CashgameModels.Chart;

namespace Web.ModelFactories.CashgameModelFactories.Chart
{
    public interface ICashgameChartPageBuilder
    {
        CashgameChartPageModel Build(string slug, int? year);
    }
}