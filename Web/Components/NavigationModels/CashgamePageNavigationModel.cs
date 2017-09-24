using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;

namespace Web.Components.NavigationModels
{
	public class CashgamePageNavigationModel : Component
    {
        public string SelectedName { get; }
        public string OverviewUrl { get; }
	    public string OverviewName { get; }
        public string OverviewSelectedClass { get; }
        public string MatrixUrl { get; }
        public string MatrixName { get; }
        public string MatrixSelectedClass { get; }
        public string ToplistUrl { get; }
        public string ToplistName { get; }
        public string ToplistSelectedClass { get; }
        public string ChartUrl { get; }
        public string ChartName { get; }
        public string ChartSelectedClass { get; }
        public string ListUrl { get; }
        public string ListName { get; }
        public string ListSelectedClass { get; }
        public string FactsUrl { get; }
        public string FactsName { get; }
        public string FactsSelectedClass { get; }

	    public CashgamePageNavigationModel(CashgameContext.Result cashgameContextResult)
	    {
	        var selectedPage = cashgameContextResult.SelectedPage;

	        var bunchId = cashgameContextResult.BunchId;
	        var year = cashgameContextResult.SelectedYear;
            var hasYear = year.HasValue;
	        var matrixUrl = hasYear ? (SiteUrl)new MatrixWithYearUrl(bunchId, year) : new MatrixUrl(bunchId);
	        var toplistUrl = hasYear ? (SiteUrl)new TopListWithYearUrl(bunchId, year) : new TopListUrl(bunchId);
	        var chartUrl = hasYear ? (SiteUrl)new ChartWithYearUrl(bunchId, year) : new ChartUrl(bunchId);
	        var listUrl = hasYear ? (SiteUrl)new ListWithYearUrl(bunchId, year) : new ListUrl(bunchId);
	        var factsUrl = hasYear ? (SiteUrl)new FactsWithYearUrl(bunchId, year) : new FactsUrl(bunchId);

            SelectedName = GetPageName(selectedPage);
            OverviewUrl = new CashgameIndexUrl(cashgameContextResult.BunchId).Relative;
	        OverviewName = GetPageName(CashgameContext.CashgamePage.Overview);
            OverviewSelectedClass = GetSelectedClass(CashgameContext.CashgamePage.Overview, selectedPage);
            MatrixUrl = matrixUrl.Relative;
            MatrixName = GetPageName(CashgameContext.CashgamePage.Matrix);
            MatrixSelectedClass = GetSelectedClass(CashgameContext.CashgamePage.Matrix, selectedPage);
            ToplistUrl = toplistUrl.Relative;
            ToplistName = GetPageName(CashgameContext.CashgamePage.Toplist);
            ToplistSelectedClass = GetSelectedClass(CashgameContext.CashgamePage.Toplist, selectedPage);
            ChartUrl = chartUrl.Relative;
            ChartName = GetPageName(CashgameContext.CashgamePage.Chart);
            ChartSelectedClass = GetSelectedClass(CashgameContext.CashgamePage.Chart, selectedPage);
            ListUrl = listUrl.Relative;
            ListName = GetPageName(CashgameContext.CashgamePage.List);
            ListSelectedClass = GetSelectedClass(CashgameContext.CashgamePage.List, selectedPage);
            FactsUrl = factsUrl.Relative;
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
