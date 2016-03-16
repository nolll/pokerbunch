using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Web.Models.CashgameModels.Toplist
{
    public class ToplistTableModel
    {
        public TopListTableColumns ColumnsModel { get; private set; }
        public IList<CashgameToplistTableItemModel> ItemModels { get; private set; }

        public ToplistTableModel(TopList.Result topListResult)
        {
            ColumnsModel = new TopListTableColumns(topListResult);
            ItemModels = topListResult.Items.Select(o => new CashgameToplistTableItemModel(o, topListResult.OrderBy)).ToList();
        }
    }

    public class ToplistTableJsonModel
    {
        [UsedImplicitly]
        [JsonProperty("orderBy")]
        public string OrderBy { get; }

        [UsedImplicitly]
        [JsonProperty("currencyFormat")]
        public string CurrencyFormat { get; }

        [UsedImplicitly]
        [JsonProperty("players")]
        public IList<CashgameToplistTableItemJsonModel> ItemModels { get; }

        public ToplistTableJsonModel(TopList.Result topListResult)
        {
            OrderBy = "winnings";
            CurrencyFormat = "{0} kr";
            ItemModels = topListResult.Items.Select(o => new CashgameToplistTableItemJsonModel(o)).ToList();
        }
    }
}