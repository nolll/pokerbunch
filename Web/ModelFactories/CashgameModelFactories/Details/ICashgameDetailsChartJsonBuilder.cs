using Web.Models.ChartModels;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public interface ICashgameDetailsChartJsonBuilder
    {
        ChartModel Build(string slug, string dateStr);
    }
}