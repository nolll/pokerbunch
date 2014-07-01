using Application.Exceptions;
using Core.Repositories;

namespace Application.UseCases.CashgameOptions
{
    public class AddCashgameFormInteractor : IAddCashgameFormInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public AddCashgameFormInteractor(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
        }

        public AddCashgameFormResult Execute(AddCashgameFormRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            if (runningGame != null)
            {
                throw new CashgameRunningException();
            }
            var locations = _cashgameRepository.GetLocations(homegame);
            return new AddCashgameFormResult(locations);
        }
    }
}