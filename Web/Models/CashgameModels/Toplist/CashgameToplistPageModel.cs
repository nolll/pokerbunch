using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameTopList;
using Web.Models.NavigationModels;

namespace Web.Models.CashgameModels.Toplist
{
    public class CashgameToplistPageModel : CashgameContextPageModel
    {
	    public ToplistTableModel TableModel { get; private set; }

        public CashgameToplistPageModel(
            ApplicationContextResult applicationContextResult,
            CashgameContextResult cashgameContextResult,
            TopListResult topListResult)
            : base(
            "Cashgame Toplist",
            applicationContextResult,
            cashgameContextResult,
            CashgamePage.Toplist)
        {
            TableModel = new ToplistTableModel(topListResult);
        }
    }
}