using Web.Models.CashgameModels.Facts;

namespace Web.ModelFactories.CashgameModelFactories.Facts
{
    public interface ICashgameFactsPageBuilder
    {
        CashgameFactsPageModel Build(string slug, int? year = null);
    }
}