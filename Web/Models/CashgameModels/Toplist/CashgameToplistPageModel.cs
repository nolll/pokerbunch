using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameTopList;

namespace Web.Models.CashgameModels.Toplist
{
    public class CashgameToplistPageModel : CashgameContextPageModel
    {
	    public ToplistTableModel TableModel { get; private set; }

        public CashgameToplistPageModel(
            CashgameContextResult cashgameContextResult,
            TopListResult topListResult)
            : base(
            "Cashgame Toplist",
            cashgameContextResult)
        {
            TableModel = new ToplistTableModel(topListResult);
        }
    }
}