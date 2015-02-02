using Core.Exceptions;
using Core.Repositories;

namespace Core.UseCases.AddCashgameForm
{
    public class AddCashgameFormInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public AddCashgameFormInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public AddCashgameFormResult Execute(AddCashgameFormRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var runningGame = _cashgameRepository.GetRunning(bunch.Id);
            if (runningGame != null)
            {
                throw new CashgameRunningException();
            }
            var locations = _cashgameRepository.GetLocations(bunch.Id);
            return new AddCashgameFormResult(locations);
        }
    }
}