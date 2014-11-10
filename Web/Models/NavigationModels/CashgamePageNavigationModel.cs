using Core.Urls;
using Core.UseCases.CashgameContext;

namespace Web.Models.NavigationModels
{
	public class CashgamePageNavigationModel
    {
	    public string MatrixUrl { get; private set; }
        public string MatrixSelectedClass { get; private set; }
        public string ToplistUrl { get; private set; }
        public string ToplistSelectedClass { get; private set; }
        public string ChartUrl { get; private set; }
        public string ChartSelectedClass { get; private set; }
        public string ListUrl { get; private set; }
        public string ListSelectedClass { get; private set; }
        public string FactsUrl { get; private set; }
        public string FactsSelectedClass { get; private set; }

	    public CashgamePageNavigationModel(CashgameContextResult cashgameContextResult)
	    {
	        var selectedPage = cashgameContextResult.SelectedPage;

            MatrixUrl = cashgameContextResult.MatrixUrl.Relative;
            MatrixSelectedClass = GetSelectedClass(CashgamePage.Matrix, selectedPage);
            ToplistUrl = cashgameContextResult.ToplistUrl.Relative;
            ToplistSelectedClass = GetSelectedClass(CashgamePage.Toplist, selectedPage);
            ChartUrl = cashgameContextResult.ChartUrl.Relative;
            ChartSelectedClass = GetSelectedClass(CashgamePage.Chart, selectedPage);
            ListUrl = cashgameContextResult.ListUrl.Relative;
            ListSelectedClass = GetSelectedClass(CashgamePage.List, selectedPage);
            FactsUrl = cashgameContextResult.FactsUrl.Relative;
            FactsSelectedClass = GetSelectedClass(CashgamePage.Facts, selectedPage);
	    }

        private string GetSelectedClass(CashgamePage current, CashgamePage selected)
        {
            return current.Equals(selected) ? "selected" : null;
        }
    }
}
