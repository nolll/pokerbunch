using System.Collections.Generic;
using System.Linq;
using Core.UseCases.CashgameCurrentRankings;

namespace Web.Models.CashgameModels.CurrentRankings
{
    public class CurrentRankingsTableModel
    {
        public IList<CurrentRankingsTableItemModel> ItemModels { get; private set; }

        public CurrentRankingsTableModel(CurrentRankingsResult currentRankings)
        {
            ItemModels = currentRankings.Items.Select(o => new CurrentRankingsTableItemModel(o)).ToList();
        }
    }
}