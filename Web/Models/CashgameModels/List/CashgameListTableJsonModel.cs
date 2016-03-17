using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Web.Models.CashgameModels.List
{
    public class CashgameListTableJsonModel
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
        [JsonProperty("games")]
        public List<CashgameListTableItemJsonModel> ListItemModels { get; private set; }

        public CashgameListTableJsonModel(CashgameList.Result result)
        {
            OrderBy = "date";
            CurrencyFormat = "{0} kr";
            ThousandSeparator = " ";
            ListItemModels = GetListItemModels(result);
        }

        private List<CashgameListTableItemJsonModel> GetListItemModels(CashgameList.Result result)
        {
            return result.List.Select(o => new CashgameListTableItemJsonModel(o)).ToList();
        }
    }
}