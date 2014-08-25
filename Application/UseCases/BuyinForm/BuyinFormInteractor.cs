using Core.Repositories;

namespace Application.UseCases.BuyinForm
{
    public class BuyinFormInteractor : IBuyinFormInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public BuyinFormInteractor(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
        }

        public BuyinFormResult Execute(BuyinFormRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var runningGame = _cashgameRepository.GetRunning(homegame.Id);
            var canEnterStack = runningGame.IsInGame(request.PlayerId);

            return new BuyinFormResult(homegame.DefaultBuyin, canEnterStack);
        }
    }
}