using System.Collections.Generic;
using System.Linq;
using Application.UseCases.CashgameDetails;

namespace Web.Models.CashgameModels.Details
{
    public class CashgameDetailsTableModel
    {
        public IList<CashgameDetailsTableItemModel> ResultModels { get; set; }

        public CashgameDetailsTableModel(IList<PlayerResultItem> playerItems)
        {
            ResultModels = playerItems.Select(o => new CashgameDetailsTableItemModel(o)).ToList();
        }
    }
}