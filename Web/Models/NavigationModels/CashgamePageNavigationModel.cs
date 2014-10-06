using Core.Urls;
using Core.UseCases.CashgameContext;

namespace Web.Models.NavigationModels
{
	public class CashgamePageNavigationModel
    {
	    public Url MatrixUrl { get; private set; }
        public string MatrixSelectedClass { get; private set; }
        public Url ToplistUrl { get; private set; }
        public string ToplistSelectedClass { get; private set; }
        public Url ChartUrl { get; private set; }
        public string ChartSelectedClass { get; private set; }
        public Url ListUrl { get; private set; }
        public string ListSelectedClass { get; private set; }
        public Url FactsUrl { get; private set; }
        public string FactsSelectedClass { get; private set; }
	    public bool IsEmpty { get; protected set; }

	    protected CashgamePageNavigationModel()
            : this("", null, CashgamePage.Unknown)
	    {
	    }

	    public CashgamePageNavigationModel(CashgameContextResult cashgameContextResult)
            : this(cashgameContextResult.BunchContext.Slug, cashgameContextResult.SelectedYear, cashgameContextResult.SelectedPage)
	    {
	    }

	    private CashgamePageNavigationModel(string slug, int? latestYear, CashgamePage cashgamePage)
	    {
            MatrixUrl = new MatrixUrl(slug, latestYear);
            MatrixSelectedClass = GetSelectedClass(CashgamePage.Matrix, cashgamePage);
            ToplistUrl = new TopListUrl(slug, latestYear);
            ToplistSelectedClass = GetSelectedClass(CashgamePage.Toplist, cashgamePage);
            ChartUrl = new ChartUrl(slug, latestYear);
            ChartSelectedClass = GetSelectedClass(CashgamePage.Chart, cashgamePage);
            ListUrl = new ListUrl(slug, latestYear);
            ListSelectedClass = GetSelectedClass(CashgamePage.List, cashgamePage);
            FactsUrl = new FactsUrl(slug, latestYear);
            FactsSelectedClass = GetSelectedClass(CashgamePage.Facts, cashgamePage);
	    }

        private string GetSelectedClass(CashgamePage current, CashgamePage selected)
        {
            return current.Equals(selected) ? "selected" : null;
        }

        public static CashgamePageNavigationModel Empty
        {
            get
            {
                return new EmptyCashgamePageNavigationModel();
            }
        }

        private class EmptyCashgamePageNavigationModel : CashgamePageNavigationModel
        {
            public EmptyCashgamePageNavigationModel()
            {
                IsEmpty = true;
            }
        }
    }
}
