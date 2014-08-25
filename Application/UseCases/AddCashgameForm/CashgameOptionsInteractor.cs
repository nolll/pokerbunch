using Application.Exceptions;
using Core.Repositories;

namespace Application.UseCases.AddCashgameForm
{
    public class CashgameOptionsInteractor : ICashgameOptionsInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameOptionsInteractor(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
        }

        public CashgameOptionsResult Execute(CashgameOptionsRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            if (runningGame != null)
            {
                throw new CashgameRunningException();
            }
            var locations = _cashgameRepository.GetLocations(homegame);
            return new CashgameOptionsResult(locations);
        }
    }
}