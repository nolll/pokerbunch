using Core.Exceptions;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.DeleteCashgame
{
    public static class DeleteCashgameInteractor
    {
        public static DeleteCashgameResult Execute(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, DeleteCashgameRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgame = cashgameRepository.GetByDateString(bunch.Id, request.DateStr);
            
            if (cashgame.PlayerCount > 0)
                throw new CashgameHasResultsException();

            cashgameRepository.DeleteGame(cashgame);

            var returnUrl = new CashgameIndexUrl(request.Slug);
            return new DeleteCashgameResult(returnUrl);
        }
    }
}
