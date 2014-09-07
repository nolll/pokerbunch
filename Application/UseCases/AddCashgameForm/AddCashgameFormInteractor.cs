using Application.Exceptions;
using Core.Repositories;

namespace Application.UseCases.AddCashgameForm
{
    public static class AddCashgameFormInteractor
    {
        public static AddCashgameFormResult Execute(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, AddCashgameFormRequest request)
        {
            var homegame = bunchRepository.GetBySlug(request.Slug);
            var runningGame = cashgameRepository.GetRunning(homegame);
            if (runningGame != null)
            {
                throw new CashgameRunningException();
            }
            var locations = cashgameRepository.GetLocations(homegame);
            return new AddCashgameFormResult(locations);
        }
    }
}