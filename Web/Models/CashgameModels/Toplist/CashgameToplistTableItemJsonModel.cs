using Core.UseCases;
using JetBrains.Annotations;
using Web.Urls.SiteUrls;

namespace Web.Models.CashgameModels.Toplist
{
    public class CashgameToplistTableItemJsonModel
    {
        [UsedImplicitly]
        public int Rank { get; }

        [UsedImplicitly]
        public string Name { get; }

        [UsedImplicitly]
        public int Winnings { get; }

        [UsedImplicitly]
        public int Buyin { get; }

        [UsedImplicitly]
        public int Cashout { get; }

        [UsedImplicitly]
        public int Time { get; }

        [UsedImplicitly]
        public int GameCount { get; }

        [UsedImplicitly]
        public int WinRate { get; }

        [UsedImplicitly]
        public string Url { get; }

        public CashgameToplistTableItemJsonModel(TopList.Item toplistItem)
        {
            Rank = toplistItem.Rank;
            Winnings = toplistItem.Winnings.Amount;
            Buyin = toplistItem.Buyin.Amount;
            Cashout = toplistItem.Cashout.Amount;
            Time = toplistItem.TimePlayed.Minutes;
            GameCount = toplistItem.GamesPlayed;
            WinRate = toplistItem.WinRate.Amount;
            Name = toplistItem.Name;
            Url = new PlayerDetailsUrl(toplistItem.PlayerId).Relative;
        }
    }
}