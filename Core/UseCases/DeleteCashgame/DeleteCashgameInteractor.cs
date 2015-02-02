using Core.Exceptions;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.DeleteCashgame
{
    public class DeleteCashgameInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public DeleteCashgameInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public DeleteCashgameResult Execute(DeleteCashgameRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameRepository.GetByDateString(bunch.Id, request.DateStr);
            
            if (cashgame.PlayerCount > 0)
                throw new CashgameHasResultsException();

            _cashgameRepository.DeleteGame(cashgame);

            var returnUrl = new CashgameIndexUrl(request.Slug);
            return new DeleteCashgameResult(returnUrl);
        }
    }
}
