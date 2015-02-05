using Core.Exceptions;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.DeleteCashgame
{
    public class DeleteCashgameInteractor
    {
        private readonly ICashgameRepository _cashgameRepository;

        public DeleteCashgameInteractor(ICashgameRepository cashgameRepository)
        {
            _cashgameRepository = cashgameRepository;
        }

        public DeleteCashgameResult Execute(DeleteCashgameRequest request)
        {
            var cashgame = _cashgameRepository.GetById(request.CashgameId);
            
            if (cashgame.PlayerCount > 0)
                throw new CashgameHasResultsException();

            _cashgameRepository.DeleteGame(cashgame);

            var returnUrl = new CashgameIndexUrl(request.Slug);
            return new DeleteCashgameResult(returnUrl);
        }
    }
}
