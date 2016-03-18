using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using JetBrains.Annotations;

namespace Web.Models.CashgameModels.List
{
    public class CashgameListTableJsonModel
    {
        [UsedImplicitly]
        public string OrderBy { get; }

        [UsedImplicitly]
        public string CurrencyFormat { get; }

        [UsedImplicitly]
        public string ThousandSeparator { get; }

        [UsedImplicitly]
        public List<CashgameListTableItemJsonModel> Games { get; private set; }

        public CashgameListTableJsonModel(CashgameList.Result result)
        {
            OrderBy = "date";
            CurrencyFormat = result.CurrencyFormat;
            ThousandSeparator = result.ThousandSeparator;
            Games = GetListItemModels(result);
        }

        private List<CashgameListTableItemJsonModel> GetListItemModels(CashgameList.Result result)
        {
            return result.List.Select(o => new CashgameListTableItemJsonModel(o)).ToList();
        }
    }
}