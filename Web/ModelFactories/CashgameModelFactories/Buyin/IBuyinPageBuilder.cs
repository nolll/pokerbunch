using Web.Models.CashgameModels.Buyin;

namespace Web.ModelFactories.CashgameModelFactories.Buyin
{
    public interface IBuyinPageBuilder
    {
        BuyinPageModel Build(string slug, int playerId, BuyinPostModel postModel = null);
    }
}