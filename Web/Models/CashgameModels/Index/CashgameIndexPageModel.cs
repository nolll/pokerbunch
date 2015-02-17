using Core.UseCases.CashgameContext;
using Core.UseCases.CashgameStatus;
using Core.UseCases.CashgameTopList;
using Web.Models.CashgameModels.Status;
using Web.Models.CashgameModels.Toplist;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Index
{
    public class CashgameIndexPageModel : CashgamePageModel
    {
        public CashgameStatusModel StatusModel { get; private set; }
        public ToplistTableModel TopListModel { get; private set; }

        public CashgameIndexPageModel(CashgameContextResult contextResult, CashgameStatusResult statusResult, TopListResult topListResult)
            : base(
                "Cashgames",
                contextResult)
        {
            StatusModel = new CashgameStatusModel(statusResult);
            TopListModel = new ToplistTableModel(topListResult);
        }
    }
}