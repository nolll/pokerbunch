using Core.UseCases;
using Newtonsoft.Json;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.List
{
    public class CashgameListPageModel : CashgamePageModel
    {
        public CashgameListTableModel ListTableModel { get; private set; }
        public string ListJson { get; private set; }

        public CashgameListPageModel(CashgameContext.Result cashgameContextResult, CashgameList.Result listResult)
            : base("Cashgame List", cashgameContextResult)
        {
            ListTableModel = new CashgameListTableModel(listResult);
            ListJson = JsonConvert.SerializeObject(new CashgameListTableJsonModel(listResult));
        }
    }
}