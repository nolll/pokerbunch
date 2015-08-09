using Core.UseCases;
using Core.UseCases.CashgameList;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.List
{
    public class CashgameListPageModel : CashgamePageModel
    {
        public CashgameListTableModel ListTableModel { get; private set; }

        public CashgameListPageModel(CashgameContext.Result cashgameContextResult, CashgameListResult listResult)
            : base("Cashgame List", cashgameContextResult)
        {
            ListTableModel = new CashgameListTableModel(listResult);
        }
    }
}