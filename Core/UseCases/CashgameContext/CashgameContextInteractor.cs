using System;
using Core.Repositories;
using Core.UseCases.BunchContext;

namespace Core.UseCases.CashgameContext
{
    public class CashgameContextInteractor
    {
        public static CashgameContextResult Execute(Func<BunchContextRequest, BunchContextResult> bunchContext, ICashgameRepository cashgameRepository, CashgameContextRequest request)
        {
            var bunchContextResult = bunchContext(new BunchContextRequest(request.Slug));

            var runningGame = cashgameRepository.GetRunning(bunchContextResult.BunchId);

            var gameIsRunning = runningGame != null;
            var years = cashgameRepository.GetYears(bunchContextResult.BunchId);

            return new CashgameContextResult(
                bunchContextResult,
                gameIsRunning,
                request.SelectedPage,
                years,
                request.Year);
        }
    }
}