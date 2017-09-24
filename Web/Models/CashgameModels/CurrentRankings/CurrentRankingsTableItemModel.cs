using Core.Services;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;
using Web.Services;

namespace Web.Models.CashgameModels.CurrentRankings
{
    public class CurrentRankingsTableItemModel : IViewModel
    {
        public int Rank { get; }
        public string Name { get; }
        public string TotalResult { get; }
        public string TotalResultClass { get; }
        public string LastGameResult { get; }
        public string LastGameResultClass { get; }
        public string PlayerUrl { get; }

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