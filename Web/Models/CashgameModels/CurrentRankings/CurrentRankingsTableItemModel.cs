using Core.Services;
using Core.Urls;
using Web.Urls;

namespace Web.Models.CashgameModels.CurrentRankings
{
    public class CurrentRankingsTableItemModel
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
            TotalResult = currentRankingsItem.TotalWinnings.String;
            TotalResultClass = ResultFormatter.GetWinningsCssClass(currentRankingsItem.TotalWinnings);
            var playedInLastGame = currentRankingsItem.LastGameWinnings != null;
            LastGameResult = playedInLastGame ? currentRankingsItem.LastGameWinnings.String : string.Empty;
            LastGameResultClass = playedInLastGame ? ResultFormatter.GetWinningsCssClass(currentRankingsItem.LastGameWinnings) : string.Empty;
            Name = currentRankingsItem.Name;
            PlayerUrl = new PlayerDetailsUrl(currentRankingsItem.PlayerId).Relative;
        }
    }
}