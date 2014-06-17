using Core.Entities;
using Web.Models.CashgameModels.Buyin;

namespace Web.ModelFactories.CashgameModelFactories.Buyin
{
    public interface IBuyinPageBuilder
    {
        BuyinPageModel Build(Homegame homegame, Player player, Cashgame runningGame, BuyinPostModel postModel);
    }
}