using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;
using Web.Services;

namespace Web.Models.CashgameModels.List
{
    public class CashgameListPageModel : CashgamePageModel
    {
        public string ListJson { get; }

        public CashgameListPageModel(CashgameContext.Result cashgameContextResult, CashgameList.Result listResult)
            : base(cashgameContextResult)
        {
            ListJson = JsonHelper.Serialize(new CashgameListTableJsonModel(listResult));
        }

        public override string BrowserTitle => "Cashgame List";

        public override View GetView()
        {
            return new View("~/Views/Pages/CashgameList/List.cshtml");
        }
    }
}