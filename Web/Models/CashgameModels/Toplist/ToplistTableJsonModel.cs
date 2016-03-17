using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Web.Models.CashgameModels.Toplist
{
    public class ToplistTableJsonModel
    {
        [UsedImplicitly]
        [JsonProperty("orderBy")]
        public string OrderBy { get; }

        [UsedImplicitly]
        [JsonProperty("currencyFormat")]
        public string CurrencyFormat { get; }

        [UsedImplicitly]
        [JsonProperty("thousandSeparator")]
        public string ThousandSeparator { get; }

        [UsedImplicitly]
        [JsonProperty("players")]
        public IList<CashgameToplistTableItemJsonModel> ItemModels { get; }

        public ToplistTableJsonModel(TopList.Result topListResult)
        {
            OrderBy = "winnings";
            CurrencyFormat = "{0} kr";
            ThousandSeparator = " ";
            ItemModels = topListResult.Items.Select(o => new CashgameToplistTableItemJsonModel(o)).ToList();
        }
    }
}