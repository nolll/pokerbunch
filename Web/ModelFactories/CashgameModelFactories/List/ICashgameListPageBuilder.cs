using Web.Models.CashgameModels.List;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public interface ICashgameListPageBuilder
    {
        CashgameListPageModel Build(string slug, int? year);
    }
}