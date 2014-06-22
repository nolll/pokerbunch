using Application.UseCases.BunchContext;
using Core.Repositories;
using Web.Models.CashgameModels.Buyin;

namespace Web.ModelFactories.CashgameModelFactories.Buyin
{
    public class BuyinPageBuilder : IBuyinPageBuilder
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IBunchContextInteractor _contextInteractor;

        public BuyinPageBuilder(
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository,
            IBunchContextInteractor contextInteractor)
        {
            _homegameRepository = homegameRepository;
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
            _contextInteractor = contextInteractor;
        }

        public BuyinPageModel Build(string slug, int playerId, BuyinPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var player = _playerRepository.GetById(playerId);
            var runningGame = _cashgameRepository.GetRunning(homegame);

            var contextResult = _contextInteractor.Execute(new BunchContextRequest(homegame.Slug));

            var model = new BuyinPageModel(contextResult)
            {
                StackFieldEnabled = runningGame.IsInGame(player.Id),
                BuyinAmount = homegame.DefaultBuyin
            };

            if (postModel != null)
            {
                model.BuyinAmount = postModel.BuyinAmount;
                model.StackAmount = postModel.StackAmount;
            }

            return model;
        }
    }
}