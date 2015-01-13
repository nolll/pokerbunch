using System.Collections.Generic;
using System.Linq;
using Core.UseCases.CashgameDetails;

namespace Web.Models.CashgameModels.Details
{
    public class CashgameDetailsTableModel
    {
        public IList<CashgameDetailsTableItemModel> ResultModels { get; private set; }

        public CashgameDetailsTableModel(IEnumerable<PlayerResultItem> playerItems)
        {
            ResultModels = playerItems.Select(o => new CashgameDetailsTableItemModel(o)).ToList();
        }
    }
}