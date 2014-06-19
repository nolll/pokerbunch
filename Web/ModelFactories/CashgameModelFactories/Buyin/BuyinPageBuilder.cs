using Application.UseCases.BunchContext;
using Core.Entities;
using Core.Repositories;
using Web.Models.CashgameModels.Buyin;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.CashgameModelFactories.Buyin
{
    public class BuyinPageBuilder : IBuyinPageBuilder
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IBunchContextInteractor _bunchContextInteractor;

        public BuyinPageBuilder(
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository,
            IBunchContextInteractor bunchContextInteractor)
        {
            _homegameRepository = homegameRepository;
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
            _bunchContextInteractor = bunchContextInteractor;
        }

        public BuyinPageModel Build(string slug, int playerId, BuyinPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var player = _playerRepository.GetById(playerId);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            
            var model = Build(homegame, player, runningGame);
            if (postModel != null)
            {
                model.BuyinAmount = postModel.BuyinAmount;
                model.StackAmount = postModel.StackAmount;
            }
            return model;
        }

        private BuyinPageModel Build(Homegame homegame, Player player, Cashgame runningGame)
        {
            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest { Slug = homegame.Slug });

            return new BuyinPageModel
                {
                    BrowserTitle = "Buy In",
                    PageProperties = new PageProperties(contextResult),
                    StackFieldEnabled = runningGame.IsInGame(player.Id),
                    BuyinAmount = homegame.DefaultBuyin
                };
        }
    }
}