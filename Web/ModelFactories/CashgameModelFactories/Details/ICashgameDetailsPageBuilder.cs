using Web.Models.CashgameModels.Details;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public interface ICashgameDetailsPageBuilder
    {
        CashgameDetailsPageModel Build(string slug, string dateStr);
    }
}