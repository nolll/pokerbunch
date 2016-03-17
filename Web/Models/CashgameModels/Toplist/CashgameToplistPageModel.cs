using Core.UseCases;
using Newtonsoft.Json;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Toplist
{
    public class CashgameToplistPageModel : CashgamePageModel
    {
        public string ToplistJson { get; }

        public CashgameToplistPageModel(
            CashgameContext.Result cashgameContextResult,
            TopList.Result topListResult)
            : base(
            "Cashgame Toplist",
            cashgameContextResult)
        {
            ToplistJson = JsonConvert.SerializeObject(new ToplistTableJsonModel(topListResult));
        }
    }
}