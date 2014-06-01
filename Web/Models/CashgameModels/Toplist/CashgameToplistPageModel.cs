using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameTopList;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Toplist
{
    public class CashgameToplistPageModel : CashgameContextPageModel
    {
	    public ToplistTableModel TableModel { get; private set; }

        public CashgameToplistPageModel(
            PageProperties pageProperties, 
            CashgameContextResult contextResult,
            TopListResult topListResult)
            : base(
            "Cashgame Toplist",
            pageProperties,
            contextResult,
            CashgamePage.Toplist)
        {
            TableModel = new ToplistTableModel(topListResult);
        }
    }
}