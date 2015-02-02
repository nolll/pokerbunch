using Core.Repositories;

namespace Core.UseCases.EndCashgame
{
    public class EndCashgameInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public EndCashgameInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public void Execute(EndCashgameRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameRepository.GetRunning(bunch.Id);

            if(cashgame != null)
                _cashgameRepository.EndGame(bunch, cashgame);
        }
    }
}
