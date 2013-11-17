using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Buyin;

namespace Web.ModelFactories.CashgameModelFactories.Buyin
{
    public class BuyinPageModelFactory : IBuyinPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public BuyinPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public BuyinPageModel Create(User user, Homegame homegame, Player player, Cashgame runningGame)
        {
            return new BuyinPageModel
                {
                    BrowserTitle = "Buy In",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame),
                    StackFieldEnabled = runningGame.IsInGame(player),
                    BuyinAmount = homegame.DefaultBuyin
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