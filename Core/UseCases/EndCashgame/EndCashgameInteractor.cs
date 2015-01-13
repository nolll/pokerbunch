using Core.Repositories;

namespace Core.UseCases.EndCashgame
{
    public static class EndCashgameInteractor
    {
        public static void Execute(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, EndCashgameRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgame = cashgameRepository.GetRunning(bunch.Id);

            if(cashgame != null)
                cashgameRepository.EndGame(bunch, cashgame);
        }
    }
}
