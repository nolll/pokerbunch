using System.Collections.Generic;
using System.Linq;
using Core.UseCases;

namespace Web.Models.CashgameModels.Details
{
    public class CashgameDetailsTableModel
    {
        public IList<CashgameDetailsTableItemModel> ResultModels { get; private set; }

        public CashgameDetailsTableModel(IEnumerable<CashgameDetails.PlayerResultItem> playerItems)
        {
            ResultModels = playerItems.Select(o => new CashgameDetailsTableItemModel(o)).ToList();
        }
    }
}