using Core.Entities;
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

        private BuyinPageModel Create(Homegame homegame, Player player, Cashgame runningGame)
        {
            return new BuyinPageModel
                {
                    BrowserTitle = "Buy In",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                    StackFieldEnabled = runningGame.IsInGame(player.Id),
                    BuyinAmount = homegame.DefaultBuyin
                };
        }

        public BuyinPageModel Create(Homegame homegame, Player player, Cashgame runningGame, BuyinPostModel postModel)
        {
            var model = Create(homegame, player, runningGame);
            if (postModel != null)
            {
                model.BuyinAmount = postModel.BuyinAmount;
                model.StackAmount = postModel.StackAmount;
            }
            return model;
        }
    }
}