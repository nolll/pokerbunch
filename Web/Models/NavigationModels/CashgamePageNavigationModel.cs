using Application.UseCases.CashgameContext;
using Web.Models.UrlModels;

namespace Web.Models.NavigationModels
{
	public class CashgamePageNavigationModel
    {
	    public UrlModel MatrixUrl { get; private set; }
        public string MatrixSelectedClass { get; private set; }
        public UrlModel ToplistUrl { get; private set; }
        public string ToplistSelectedClass { get; private set; }
        public UrlModel ChartUrl { get; private set; }
        public string ChartSelectedClass { get; private set; }
        public UrlModel ListUrl { get; private set; }
        public string ListSelectedClass { get; private set; }
        public UrlModel FactsUrl { get; private set; }
        public string FactsSelectedClass { get; private set; }

        public CashgamePageNavigationModel(CashgameContextResult cashgameContextResult, CashgamePage cashgamePage)
            : this(cashgameContextResult.Slug, cashgameContextResult.SelectedYear, cashgamePage)
	    {
	    }

	    public CashgamePageNavigationModel(string slug, int? latestYear, CashgamePage cashgamePage)
	    {
            MatrixUrl = new MatrixUrlModel(slug, latestYear);
            MatrixSelectedClass = GetSelectedClass(CashgamePage.Matrix, cashgamePage);
            ToplistUrl = new TopListUrlModel(slug, latestYear);
            ToplistSelectedClass = GetSelectedClass(CashgamePage.Toplist, cashgamePage);
            ChartUrl = new ChartUrlModel(slug, latestYear);
            ChartSelectedClass = GetSelectedClass(CashgamePage.Chart, cashgamePage);
            ListUrl = new ListUrlModel(slug, latestYear);
            ListSelectedClass = GetSelectedClass(CashgamePage.List, cashgamePage);
            FactsUrl = new FactsUrlModel(slug, latestYear);
            FactsSelectedClass = GetSelectedClass(CashgamePage.Facts, cashgamePage);
	    }

        private string GetSelectedClass(CashgamePage current, CashgamePage selected)
        {
            return current.Equals(selected) ? "selected" : null;
        }
    }
}
