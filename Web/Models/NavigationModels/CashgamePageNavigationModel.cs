using Application.UseCases.CashgameContext;
using Web.Services;

namespace Web.Models.NavigationModels
{
	public class CashgamePageNavigationModel
    {
	    public UrlModel MatrixLink { get; private set; }
        public string MatrixSelectedClass { get; private set; }
        public UrlModel ToplistLink { get; private set; }
        public string ToplistSelectedClass { get; private set; }
        public UrlModel ChartLink { get; private set; }
        public string ChartSelectedClass { get; private set; }
        public UrlModel ListLink { get; private set; }
        public string ListSelectedClass { get; private set; }
        public UrlModel FactsLink { get; private set; }
        public string FactsSelectedClass { get; private set; }

        public CashgamePageNavigationModel(CashgameContextResult cashgameContextResult, CashgamePage cashgamePage)
            : this(cashgameContextResult.Slug, cashgameContextResult.SelectedYear, cashgamePage)
	    {
	    }

	    public CashgamePageNavigationModel(string slug, int? latestYear, CashgamePage cashgamePage)
	    {
            MatrixLink = new MatrixUrlModel(slug, latestYear);
            MatrixSelectedClass = GetSelectedClass(CashgamePage.Matrix, cashgamePage);
            ToplistLink = new TopListUrlModel(slug, latestYear);
            ToplistSelectedClass = GetSelectedClass(CashgamePage.Toplist, cashgamePage);
            ChartLink = new ChartUrlModel(slug, latestYear);
            ChartSelectedClass = GetSelectedClass(CashgamePage.Chart, cashgamePage);
            ListLink = new ListUrlModel(slug, latestYear);
            ListSelectedClass = GetSelectedClass(CashgamePage.List, cashgamePage);
            FactsLink = new FactsUrlModel(slug, latestYear);
            FactsSelectedClass = GetSelectedClass(CashgamePage.Facts, cashgamePage);
	    }

        private string GetSelectedClass(CashgamePage current, CashgamePage selected)
        {
            return current.Equals(selected) ? "selected" : null;
        }
    }
}
