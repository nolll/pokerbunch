using Web.Models.ChartModels;

namespace Web.ModelFactories.CashgameModelFactories.Action
{
    public interface IActionChartJsonBuilder
    {
        ChartModel Build(string slug, string dateStr, int playerId);
    }
}