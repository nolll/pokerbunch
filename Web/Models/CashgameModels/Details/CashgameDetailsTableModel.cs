using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Extensions;

namespace Web.Models.CashgameModels.Details
{
    public class CashgameDetailsTableModel : IViewModel
    {
        public IList<CashgameDetailsTableItemModel> ResultModels { get; }

        public CashgameDetailsTableModel(IEnumerable<CashgameDetails.PlayerResultItem> playerItems)
        {
            ResultModels = playerItems.Select(o => new CashgameDetailsTableItemModel(o)).ToList();
        }

        public View GetView()
        {
            return new View("CashgameDetails/ResultTable");
        }
    }
}