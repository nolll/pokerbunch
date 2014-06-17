using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Buyin;

namespace Web.ModelFactories.CashgameModelFactories.Buyin
{
    public class BuyinPageBuilder : IBuyinPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public BuyinPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _homegameRepository = homegameRepository;
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
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

        public BuyinPageModel Build(string slug, int playerId, BuyinPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var player = _playerRepository.GetById(playerId);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            
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