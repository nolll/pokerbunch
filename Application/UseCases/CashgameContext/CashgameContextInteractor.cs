﻿using System.Collections.Generic;
using Application.UseCases.BunchContext;
using Core.Repositories;
using System.Linq;

namespace Application.UseCases.CashgameContext
{
    public class CashgameContextInteractor : ICashgameContextInteractor
    {
        private readonly IBunchContextInteractor _bunchContextInteractor;
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameContextInteractor(
            IBunchContextInteractor bunchContextInteractor,
            ICashgameRepository cashgameRepository)
        {
            _bunchContextInteractor = bunchContextInteractor;
            _cashgameRepository = cashgameRepository;
        }

        public CashgameContextResult Execute(CashgameContextRequest request)
        {
            var bunchContextResult = _bunchContextInteractor.Execute(new BunchContextRequest{Slug = request.Slug});

            var runningGame = _cashgameRepository.GetRunning(bunchContextResult.BunchId);

            var gameIsRunning = runningGame != null;
            var years = _cashgameRepository.GetYears(bunchContextResult.BunchId);
            var latestYear = GetLatestYear(years);

            return new CashgameContextResult(
                bunchContextResult,
                gameIsRunning,
                years,
                request.Year,
                latestYear);
        }

        private int? GetLatestYear(IList<int> years)
        {
            if (years.Count == 0)
                return null;
            return years.Max();
        }
    }
}