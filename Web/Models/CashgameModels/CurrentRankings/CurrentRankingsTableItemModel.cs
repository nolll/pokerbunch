using Core.Services;
using Web.Extensions;
using Web.Services;
using Web.Urls.SiteUrls;

namespace Web.Models.CashgameModels.CurrentRankings
{
    public class CurrentRankingsTableItemModel : IViewModel
    {
        public int Rank { get; private set; }
        public string Name { get; private set; }
        public string TotalResult { get; private set; }
        public string TotalResultClass { get; private set; }
        public string LastGameResult { get; private set; }
        public string LastGameResultClass { get; private set; }
        public string PlayerUrl { get; private set; }

        public CurrentRankingsTableItemModel(Core.UseCases.CurrentRankings.Item currentRankingsItem)
        {
            Rank = currentRankingsItem.Rank;
            TotalResult = ResultFormatter.FormatWinnings(currentRankingsItem.TotalWinnings);
            TotalResultClass = CssService.GetWinningsCssClass(currentRankingsItem.TotalWinnings);
            var playedInLastGame = currentRankingsItem.LastGameWinnings != null;
            LastGameResult = playedInLastGame ? ResultFormatter.FormatWinnings(currentRankingsItem.LastGameWinnings) : string.Empty;
            LastGameResultClass = playedInLastGame ? CssService.GetWinningsCssClass(currentRankingsItem.LastGameWinnings) : string.Empty;
            Name = currentRankingsItem.Name;
            PlayerUrl = new PlayerDetailsUrl(currentRankingsItem.PlayerId).Relative;
        }

        public View GetView()
        {
            return new View("CurrentRankings/CurrentRankingsTableItem");
        }
    }
}