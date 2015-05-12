using Core.UseCases.CashgameContext;
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

	    public CashgamePageNavigationModel(CashgameContextResult cashgameContextResult)
	    {
	        var selectedPage = cashgameContextResult.SelectedPage;

            SelectedName = GetPageName(selectedPage);
            OverviewUrl = cashgameContextResult.StartPageUrl.Relative;
	        OverviewName = GetPageName(CashgamePage.Overview);
            OverviewSelectedClass = GetSelectedClass(CashgamePage.Overview, selectedPage);
            MatrixUrl = cashgameContextResult.MatrixUrl.Relative;
            MatrixName = GetPageName(CashgamePage.Matrix);
            MatrixSelectedClass = GetSelectedClass(CashgamePage.Matrix, selectedPage);
            ToplistUrl = cashgameContextResult.ToplistUrl.Relative;
            ToplistName = GetPageName(CashgamePage.Toplist);
            ToplistSelectedClass = GetSelectedClass(CashgamePage.Toplist, selectedPage);
            ChartUrl = cashgameContextResult.ChartUrl.Relative;
            ChartName = GetPageName(CashgamePage.Chart);
            ChartSelectedClass = GetSelectedClass(CashgamePage.Chart, selectedPage);
            ListUrl = cashgameContextResult.ListUrl.Relative;
            ListName = GetPageName(CashgamePage.List);
            ListSelectedClass = GetSelectedClass(CashgamePage.List, selectedPage);
            FactsUrl = cashgameContextResult.FactsUrl.Relative;
            FactsName = GetPageName(CashgamePage.Facts);
            FactsSelectedClass = GetSelectedClass(CashgamePage.Facts, selectedPage);
	    }

	    private string GetPageName(CashgamePage page)
	    {
	        if (page == CashgamePage.Matrix)
	            return "Matrix";
	        if (page == CashgamePage.Toplist)
	            return "Toplist";
	        if (page == CashgamePage.Chart)
	            return "Chart";
	        if (page == CashgamePage.List)
	            return "List";
	        if (page == CashgamePage.Facts)
	            return "Facts";
            return "Overview";
	    }

        private string GetSelectedClass(CashgamePage current, CashgamePage selected)
        {
            return current.Equals(selected) ? "selected" : null;
        }
    }
}
