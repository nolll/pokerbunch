using Web.Models.CashgameModels.Action;

namespace Web.ModelFactories.CashgameModelFactories.Action
{
    public interface IActionPageBuilder
    {
        ActionPageModel Build(string slug, string dateStr, int playerId);
    }
}