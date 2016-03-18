using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using JetBrains.Annotations;

namespace Web.Models.CashgameModels.Toplist
{
    public class ToplistTableJsonModel
    {
        [UsedImplicitly]
        public string OrderBy { get; }

        [UsedImplicitly]
        public string CurrencyFormat { get; }

        [UsedImplicitly]
        public string ThousandSeparator { get; }

        [UsedImplicitly]
        public IList<CashgameToplistTableItemJsonModel> Players { get; }

        public ToplistTableJsonModel(TopList.Result topListResult)
        {
            OrderBy = "winnings";
            CurrencyFormat = topListResult.CurrencyFormat;
            ThousandSeparator = topListResult.ThousandSeparator;
            Players = topListResult.Items.Select(o => new CashgameToplistTableItemJsonModel(o)).ToList();
        }
    }
}