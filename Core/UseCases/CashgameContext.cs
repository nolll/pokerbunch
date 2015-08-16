using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases
{
    public class CashgameContext
    {
        private readonly IUserRepository _userRepository;
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public CashgameContext(IUserRepository userRepository, IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
        {
            _userRepository = userRepository;
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var bunchContextResult = new BunchContext(_userRepository, _bunchRepository, _playerRepository).Execute(request);
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

            return new Result(
                bunchContextResult,
                request.Slug,
                gameIsRunning,
                request.SelectedPage,
                years,
                selectedYear);
        }

        public class Request : BunchContext.BunchRequest
        {
            public DateTime CurrentTime { get; private set; }
            public CashgamePage SelectedPage { get; private set; }
            public int? Year { get; private set; }

            public Request(string userName, string slug, DateTime currentTime, CashgamePage selectedPage = CashgamePage.Unknown, int? year = null)
                : base(userName, slug)
            {
                CurrentTime = currentTime;
                SelectedPage = selectedPage;
                Year = year;
            }
        }

        public class Result
        {
            public bool GameIsRunning { get; private set; }
            public CashgamePage SelectedPage { get; private set; }
            public int? SelectedYear { get; private set; }
            public Url StartPageUrl { get; private set; }
            public Url MatrixUrl { get; private set; }
            public Url ToplistUrl { get; private set; }
            public Url ChartUrl { get; private set; }
            public Url ListUrl { get; private set; }
            public Url FactsUrl { get; private set; }
            public IList<YearItem> YearItems { get; private set; }
            public BunchContext.Result BunchContext { get; private set; }

            public Result(
                BunchContext.Result bunchContextResult,
                string slug,
                bool gameIsRunning,
                CashgamePage selectedPage,
                IEnumerable<int> years,
                int? selectedYear)
            {
                BunchContext = bunchContextResult;
                GameIsRunning = gameIsRunning;
                SelectedPage = selectedPage;
                SelectedYear = selectedYear;
                StartPageUrl = new CashgameIndexUrl(slug);
                MatrixUrl = new MatrixUrl(slug, selectedYear);
                ToplistUrl = new TopListUrl(slug, selectedYear);
                ChartUrl = new ChartUrl(slug, selectedYear);
                ListUrl = new ListUrl(slug, selectedYear);
                FactsUrl = new FactsUrl(slug, selectedYear);
                YearItems = CreateYearItems(slug, years, selectedPage, selectedYear);
            }

            private IList<YearItem> CreateYearItems(string slug, IEnumerable<int> years, CashgamePage selectedPage, int? selectedYear)
            {
                var yearItems = years.Select(year => new YearItem(year.ToString(CultureInfo.InvariantCulture), GetYearUrl(slug, selectedPage, year), selectedYear == year)).ToList();
                yearItems.Add(new YearItem("All", GetYearUrl(slug, selectedPage), !selectedYear.HasValue));
                return yearItems;
            }

            private Url GetYearUrl(string slug, CashgamePage cashgamePage, int? year = null)
            {
                if (cashgamePage.Equals(CashgamePage.Overview))
                    return new CashgameIndexUrl(slug);
                if (cashgamePage.Equals(CashgamePage.Matrix))
                    return new MatrixUrl(slug, year);
                if (cashgamePage.Equals(CashgamePage.Toplist))
                    return new TopListUrl(slug, year);
                if (cashgamePage.Equals(CashgamePage.Chart))
                    return new ChartUrl(slug, year);
                if (cashgamePage.Equals(CashgamePage.List))
                    return new ListUrl(slug, year);
                if (cashgamePage.Equals(CashgamePage.Facts))
                    return new FactsUrl(slug, year);
                return null;
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

        public class YearItem
        {
            public string Label { get; private set; }
            public Url Url { get; private set; }
            public bool IsSelected { get; private set; }

            public YearItem(string label, Url url, bool isSelected)
            {
                Label = label;
                Url = url;
                IsSelected = isSelected;
            }
        }
    }
}