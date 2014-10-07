using Core.Exceptions;
using Core.Repositories;

namespace Core.UseCases.AddCashgameForm
{
    public static class AddCashgameFormInteractor
    {
        public static AddCashgameFormResult Execute(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, AddCashgameFormRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var runningGame = cashgameRepository.GetRunning(bunch);
            if (runningGame != null)
            {
                throw new CashgameRunningException();
            }
            var locations = cashgameRepository.GetLocations(bunch);
            return new AddCashgameFormResult(locations);
        }
    }
}