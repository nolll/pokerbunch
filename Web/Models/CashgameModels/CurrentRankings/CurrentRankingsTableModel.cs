using System.Collections.Generic;
using System.Linq;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;

namespace Web.Models.CashgameModels.CurrentRankings
{
    public class CurrentRankingsTableModel : IViewModel
    {
        public IList<CurrentRankingsTableItemModel> ItemModels { get; }
        public string LastGameUrl { get; }

        public CurrentRankingsTableModel(Core.UseCases.CurrentRankings.Result currentRankings)
        {
            ItemModels = currentRankings.Items.Select(o => new CurrentRankingsTableItemModel(o)).ToList();
            LastGameUrl = new CashgameDetailsUrl(currentRankings.LastGameId).Relative;
        }

        public View GetView()
        {
            return new View("CurrentRankings/CurrentRankingsTable");
        }
    }
}