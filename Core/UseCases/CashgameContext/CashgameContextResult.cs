using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Urls;
using Core.UseCases.BunchContext;

namespace Core.UseCases.CashgameContext
{
    public class CashgameContextResult
    {
        public bool GameIsRunning { get; private set; }
        public CashgamePage SelectedPage { get; private set; }
        public int? SelectedYear { get; private set; }
        public Url MatrixUrl { get; private set; }
        public Url ToplistUrl { get; private set; }
        public Url ChartUrl { get; private set; }
        public Url ListUrl { get; private set; }
        public Url FactsUrl { get; private set; }
        public Url RunningCashgameUrl { get; private set; }
        public Url AddCashgameUrl { get; private set; }
        public IList<YearItem> YearItems { get; private set; }
        public BunchContextResult BunchContext { get; private set; }

        public CashgameContextResult(
            BunchContextResult bunchContextResult,
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
            MatrixUrl = new MatrixUrl(slug, selectedYear);
            ToplistUrl = new TopListUrl(slug, selectedYear);
            ChartUrl = new ChartUrl(slug, selectedYear);
            ListUrl = new ListUrl(slug, selectedYear);
            FactsUrl = new FactsUrl(slug, selectedYear);
            RunningCashgameUrl = new RunningCashgameUrl(slug);
            AddCashgameUrl = new AddCashgameUrl(slug);
            YearItems = CreateYearItems(slug, years, selectedPage);
        }

        private IList<YearItem> CreateYearItems(string slug, IEnumerable<int> years, CashgamePage selectedPage)
        {
            var yearItems = years.Select(year => new YearItem(year.ToString(CultureInfo.InvariantCulture), GetYearUrl(slug, selectedPage, year))).ToList();
            yearItems.Add(new YearItem("All Time", GetYearUrl(slug, selectedPage)));
            return yearItems;
        }

        private Url GetYearUrl(string slug, CashgamePage cashgamePage, int? year = null)
        {
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
}