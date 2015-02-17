using Core.Repositories;
using Core.Services;

namespace Core.UseCases.CashgameStatus
{
    public class CashgameStatusInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameStatusInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public CashgameStatusResult Execute(CashgameStatusRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var runningGame = _cashgameRepository.GetRunning(bunch.Id);

            var gameIsRunning = runningGame != null;

            return new CashgameStatusResult(
                request.Slug,
                gameIsRunning);
        }
    }
}