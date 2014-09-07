using System;
using System.Collections.Generic;
using Application.UseCases.BunchContext;
using Core.Repositories;
using System.Linq;

namespace Application.UseCases.CashgameContext
{
    public class CashgameContextInteractor
    {
        public static CashgameContextResult Execute(Func<BunchContextRequest, BunchContextResult> bunchContext, ICashgameRepository cashgameRepository, CashgameContextRequest request)
        {
            var bunchContextResult = bunchContext(new BunchContextRequest(request.Slug));

            var runningGame = cashgameRepository.GetRunning(bunchContextResult.BunchId);

            var gameIsRunning = runningGame != null;
            var years = cashgameRepository.GetYears(bunchContextResult.BunchId);
            var latestYear = GetLatestYear(years);

            return new CashgameContextResult(
                bunchContextResult,
                gameIsRunning,
                request.SelectedPage,
                years,
                request.Year,
                latestYear);
        }

        private static int? GetLatestYear(IList<int> years)
        {
            if (years.Count == 0)
                return null;
            return years.Max();
        }
    }
}