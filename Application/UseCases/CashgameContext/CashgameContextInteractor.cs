using System.Collections.Generic;
using Core.Repositories;
using System.Linq;

namespace Application.UseCases.CashgameContext
{
    public class CashgameContextInteractor : ICashgameContextInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameContextInteractor(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
        }

        public CashgameContextResult Execute(CashgameContextRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var runningGame = _cashgameRepository.GetRunning(homegame);

            var gameIsRunning = runningGame != null;
            var years = _cashgameRepository.GetYears(homegame);
            var latestYear = GetLatestYear(years);
            var bunchName = homegame.DisplayName;

            return new CashgameContextResult
                {
                    GameIsRunning = gameIsRunning,
                    Years = years,
                    Slug = request.Slug,
                    BunchName = bunchName,
                    SelectedYear = request.Year,
                    LatestYear = latestYear
                };
        }

        private int? GetLatestYear(IList<int> years)
        {
            if (years.Count == 0)
                return null;
            return years.Max();
        }
    }
}