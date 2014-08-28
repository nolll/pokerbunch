using Core.Repositories;

namespace Application.UseCases.BuyinForm
{
    public class BuyinFormInteractor : IBuyinFormInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public BuyinFormInteractor(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public BuyinFormResult Execute(BuyinFormRequest request)
        {
            var homegame = _bunchRepository.GetBySlug(request.Slug);
            var runningGame = _cashgameRepository.GetRunning(homegame.Id);
            var canEnterStack = runningGame.IsInGame(request.PlayerId);

            return new BuyinFormResult(homegame.DefaultBuyin, canEnterStack);
        }
    }
}