using System.Linq;
using Core.Repositories;
using Core.Services;
using Core.UseCases.BunchContext;

namespace Core.UseCases.CashgameContext
{
    public class CashgameContextInteractor
    {
        private readonly IAuth _auth;
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameContextInteractor(IAuth auth, IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _auth = auth;
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public CashgameContextResult Execute(CashgameContextRequest request)
        {
            var bunchContextResult = new BunchContextInteractor(_auth, _bunchRepository).Execute(request);
            var runningGame = _cashgameRepository.GetRunning(bunchContextResult.BunchId);

            var gameIsRunning = runningGame != null;
            var years = _cashgameRepository.GetYears(bunchContextResult.BunchId);

            var selectedYear = request.Year;
            if (request.SelectedPage == CashgamePage.Start)
            {
                selectedYear = years.Max(o => o);
            }

            return new CashgameContextResult(
                bunchContextResult,
                request.Slug,
                gameIsRunning,
                request.SelectedPage,
                years,
                selectedYear);
        }
    }
}