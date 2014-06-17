using Web.Models.CashgameModels.Add;

namespace Web.ModelFactories.CashgameModelFactories.Add
{
    public interface IAddCashgamePageBuilder
    {
        AddCashgamePageModel Build(string slug, AddCashgamePostModel postModel = null);
    }
}