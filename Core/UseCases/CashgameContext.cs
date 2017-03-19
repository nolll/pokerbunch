﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Repositories;

namespace Core.UseCases
{
    public class CashgameContext
    {
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameContext(ICashgameRepository cashgameRepository)
        {
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(BunchContext.Result bunchContext, Request request)
        {
            var runningGame = _cashgameRepository.GetCurrent(bunchContext.BunchId);

            var gameIsRunning = runningGame != null;
            var years = _cashgameRepository.GetYears(bunchContext.BunchId).OrderByDescending(o => o).ToList();
            var selectedYear = GetSelectedYear(request, years);

            return new Result(
                bunchContext,
                request.BunchId,
                gameIsRunning,
                request.SelectedPage,
                years,
                selectedYear);
        }

        private static int? GetSelectedYear(Request request, List<int> years)
        {
            if (request.SelectedPage != CashgamePage.Overview)
                return request.Year;
            if (years.Count > 0)
                return years.Max(o => o);
            return request.CurrentTime.Year; // todo: convert to local bunch time
        }

        public class Request
        {
            public string BunchId { get; }
            public DateTime CurrentTime { get; }
            public CashgamePage SelectedPage { get; }
            public int? Year { get; }

            public Request(string bunchId, DateTime currentTime, CashgamePage selectedPage = CashgamePage.Unknown, int? year = null)
            {
                BunchId = bunchId;
                CurrentTime = currentTime;
                SelectedPage = selectedPage;
                Year = year;
            }
        }

        public class Result
        {
            public string BunchId { get; private set; }
            public bool GameIsRunning { get; private set; }
            public CashgamePage SelectedPage { get; private set; }
            public int? SelectedYear { get; private set; }
            public IList<int> Years { get; private set; }
            public BunchContext.Result BunchContext { get; private set; }

            public Result(
                BunchContext.Result bunchContextResult,
                string bunchId,
                bool gameIsRunning,
                CashgamePage selectedPage,
                IList<int> years,
                int? selectedYear)
            {
                BunchId = bunchId;
                BunchContext = bunchContextResult;
                GameIsRunning = gameIsRunning;
                SelectedPage = selectedPage;
                SelectedYear = selectedYear;
                Years = years;
            }
        }

        public enum CashgamePage
        {
            Unknown,
            Overview,
            Matrix,
            Toplist,
            Chart,
            List,
            Facts
        }
    }
}