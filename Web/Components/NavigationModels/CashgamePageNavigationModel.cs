using Core.Urls;
using Core.UseCases;
using Web.Extensions;

namespace Web.Components.NavigationModels
{
	public class CashgamePageNavigationModel : Component
    {
        public string SelectedName { get; private set; }
        public string OverviewUrl { get; private set; }
	    public string OverviewName { get; private set; }
        public string OverviewSelectedClass { get; private set; }
        public string MatrixUrl { get; private set; }
        public string MatrixName { get; private set; }
        public string MatrixSelectedClass { get; private set; }
        public string ToplistUrl { get; private set; }
        public string ToplistName { get; private set; }
        public string ToplistSelectedClass { get; private set; }
        public string ChartUrl { get; private set; }
        public string ChartName { get; private set; }
        public string ChartSelectedClass { get; private set; }
        public string ListUrl { get; private set; }
        public string ListName { get; private set; }
        public string ListSelectedClass { get; private set; }
        public string FactsUrl { get; private set; }
        public string FactsName { get; private set; }
        public string FactsSelectedClass { get; private set; }

	    public CashgamePageNavigationModel(CashgameContext.Result cashgameContextResult)
	    {
	        var selectedPage = cashgameContextResult.SelectedPage;

            SelectedName = GetPageName(selectedPage);
            OverviewUrl = new CashgameIndexUrl(cashgameContextResult.Slug).Relative;
	        OverviewName = GetPageName(CashgameContext.CashgamePage.Overview);
            OverviewSelectedClass = GetSelectedClass(CashgameContext.CashgamePage.Overview, selectedPage);
            MatrixUrl = new MatrixUrl(cashgameContextResult.Slug, cashgameContextResult.SelectedYear).Relative;
            MatrixName = GetPageName(CashgameContext.CashgamePage.Matrix);
            MatrixSelectedClass = GetSelectedClass(CashgameContext.CashgamePage.Matrix, selectedPage);
            ToplistUrl = new TopListUrl(cashgameContextResult.Slug, cashgameContextResult.SelectedYear).Relative;
            ToplistName = GetPageName(CashgameContext.CashgamePage.Toplist);
            ToplistSelectedClass = GetSelectedClass(CashgameContext.CashgamePage.Toplist, selectedPage);
            ChartUrl = new ChartUrl(cashgameContextResult.Slug, cashgameContextResult.SelectedYear).Relative;
            ChartName = GetPageName(CashgameContext.CashgamePage.Chart);
            ChartSelectedClass = GetSelectedClass(CashgameContext.CashgamePage.Chart, selectedPage);
            ListUrl = new ListUrl(cashgameContextResult.Slug, cashgameContextResult.SelectedYear).Relative;
            ListName = GetPageName(CashgameContext.CashgamePage.List);
            ListSelectedClass = GetSelectedClass(CashgameContext.CashgamePage.List, selectedPage);
            FactsUrl = new FactsUrl(cashgameContextResult.Slug, cashgameContextResult.SelectedYear).Relative;
            FactsName = GetPageName(CashgameContext.CashgamePage.Facts);
            FactsSelectedClass = GetSelectedClass(CashgameContext.CashgamePage.Facts, selectedPage);
	    }

	    private string GetPageName(CashgameContext.CashgamePage page)
	    {
	        if (page == CashgameContext.CashgamePage.Matrix)
	            return "Matrix";
	        if (page == CashgameContext.CashgamePage.Toplist)
	            return "Toplist";
	        if (page == CashgameContext.CashgamePage.Chart)
	            return "Chart";
	        if (page == CashgameContext.CashgamePage.List)
	            return "List";
	        if (page == CashgameContext.CashgamePage.Facts)
	            return "Facts";
            return "Overview";
	    }

        private string GetSelectedClass(CashgameContext.CashgamePage current, CashgameContext.CashgamePage selected)
        {
            return current.Equals(selected) ? "selected" : null;
        }
    }
}
