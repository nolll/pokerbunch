using Core.Classes;
using Web.Models.CashgameModels.Buyin;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.CashgameModelFactories.Buyin
{
    public class BuyinPageModelFactory : IBuyinPageModelFactory
    {
        public BuyinPageModel Create(User user, Homegame homegame, Player player, Cashgame runningGame)
        {
            return new BuyinPageModel
                {
                    BrowserTitle = "Buy In",
                    PageProperties = new PageProperties(user, homegame, runningGame),
                    StackFieldEnabled = runningGame.IsInGame(player)
                };
        }

        public BuyinPageModel Create(User user, Homegame homegame, Player player, Cashgame runningGame, BuyinPostModel postModel)
        {
            var model = Create(user, homegame, player, runningGame);
            model.BuyinAmount = postModel.BuyinAmount;
            model.StackAmount = postModel.StackAmount;
            return model;
        }
    }
}