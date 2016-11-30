using System.Collections.Generic;
using System.Linq;
using Web.Urls.SiteUrls;

namespace Web.Models.CashgameModels.CurrentRankings
{
    public class CurrentRankingsTableModel
    {
        public IList<CurrentRankingsTableItemModel> ItemModels { get; private set; }
        public string LastGameUrl { get; set; }

        public CurrentRankingsTableModel(Core.UseCases.CurrentRankings.Result currentRankings)
        {
            ItemModels = currentRankings.Items.Select(o => new CurrentRankingsTableItemModel(o)).ToList();
            LastGameUrl = new CashgameDetailsUrl(currentRankings.LastGameId).Relative;
        }
    }
}