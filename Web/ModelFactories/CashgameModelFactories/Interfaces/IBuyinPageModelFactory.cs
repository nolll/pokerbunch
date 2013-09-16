using Core.Classes;
using Web.Models.CashgameModels.Buyin;

namespace Web.ModelFactories.CashgameModelFactories
{
    public interface IBuyinPageModelFactory
    {
        BuyinPageModel Create(User user, Homegame homegame, Player player, Cashgame runningGame);
        BuyinPageModel Create(User user, Homegame homegame, Player player, Cashgame runningGame, BuyinPostModel postModel);
    }
}