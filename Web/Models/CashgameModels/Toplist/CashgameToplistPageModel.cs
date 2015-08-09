using Core.UseCases;
using Core.UseCases.CashgameTopList;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Toplist
{
    public class CashgameToplistPageModel : CashgamePageModel
    {
        public ToplistTableModel TableModel { get; private set; }

        public CashgameToplistPageModel(
            CashgameContext.Result cashgameContextResult,
            TopListResult topListResult)
            : base(
            "Cashgame Toplist",
            cashgameContextResult)
        {
            TableModel = new ToplistTableModel(topListResult);
        }
    }
}