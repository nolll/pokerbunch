using Application.Exceptions;
using Core.Repositories;

namespace Application.UseCases.AddCashgameForm
{
    public class AddCashgameFormInteractor : IAddCashgameFormInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public AddCashgameFormInteractor(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public AddCashgameFormResult Execute(AddCashgameFormRequest request)
        {
            var homegame = _bunchRepository.GetBySlug(request.Slug);
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