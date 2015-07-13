using System.Linq;
using Core.Repositories;
using Core.UseCases.BunchContext;

namespace Core.UseCases.CashgameContext
{
    public class CashgameContextInteractor
    {
        private readonly IUserRepository _userRepository;
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public CashgameContextInteractor(IUserRepository userRepository, IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
        {
            _userRepository = userRepository;
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
        }

        public CashgameContextResult Execute(CashgameContextRequest request)
        {
            var bunchContextResult = new BunchContextInteractor(_userRepository, _bunchRepository, _playerRepository).Execute(request);
            var runningGame = _cashgameRepository.GetRunning(bunchContextResult.BunchId);

            var gameIsRunning = runningGame != null;
            var years = _cashgameRepository.GetYears(bunchContextResult.BunchId);

            var selectedYear = request.Year;
            if (request.SelectedPage == CashgamePage.Overview)
            {
                if(years.Count > 0)
                    selectedYear = years.Max(o => o);
                else
                    selectedYear = request.CurrentTime.Year; // todo: convert to local bunch time
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