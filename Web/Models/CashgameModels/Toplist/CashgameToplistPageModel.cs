using Core.UseCases;
using Web.Models.PageBaseModels;
using Web.Services;

namespace Web.Models.CashgameModels.Toplist
{
    public class CashgameToplistPageModel : CashgamePageModel
    {
        public string ToplistJson { get; }

        public CashgameToplistPageModel(CashgameContext.Result cashgameContextResult, TopList.Result topListResult)
            : base(cashgameContextResult)
        {
            ToplistJson = JsonHelper.Serialize(new ToplistTableJsonModel(topListResult));
        }

        public override string BrowserTitle => "Cashgame Toplist";
    }
}