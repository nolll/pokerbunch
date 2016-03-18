using Core.UseCases;
using Web.Common.Services;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.List
{
    public class CashgameListPageModel : CashgamePageModel
    {
        public string ListJson { get; private set; }

        public CashgameListPageModel(CashgameContext.Result cashgameContextResult, CashgameList.Result listResult)
            : base("Cashgame List", cashgameContextResult)
        {
            ListJson = Json.Serialize(new CashgameListTableJsonModel(listResult));
        }
    }
}