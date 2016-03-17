using Core.UseCases;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Web.Common.Urls.SiteUrls;

namespace Web.Models.CashgameModels.Toplist
{
    public class CashgameToplistTableItemJsonModel
    {
        [UsedImplicitly]
        [JsonProperty("rank")]
        public int Rank { get; }

        [UsedImplicitly]
        [JsonProperty("name")]
        public string Name { get; }

        [UsedImplicitly]
        [JsonProperty("winnings")]
        public int Winnings { get; }

        [UsedImplicitly]
        [JsonProperty("buyin")]
        public int Buyin { get; }

        [UsedImplicitly]
        [JsonProperty("cashout")]
        public int Cashout { get; }

        [UsedImplicitly]
        [JsonProperty("time")]
        public int GameTime { get; }

        [UsedImplicitly]
        [JsonProperty("gameCount")]
        public int GameCount { get; }

        [UsedImplicitly]
        [JsonProperty("winRate")]
        public int WinRate { get; }

        [UsedImplicitly]
        [JsonProperty("url")]
        public string PlayerUrl { get; }

        public CashgameToplistTableItemJsonModel(TopList.Item toplistItem)
        {
            Rank = toplistItem.Rank;
            Winnings = toplistItem.Winnings.Amount;
            Buyin = toplistItem.Buyin.Amount;
            Cashout = toplistItem.Cashout.Amount;
            GameTime = toplistItem.TimePlayed.Minutes;
            GameCount = toplistItem.GamesPlayed;
            WinRate = toplistItem.WinRate.Amount;
            Name = toplistItem.Name;
            PlayerUrl = new PlayerDetailsUrl(toplistItem.PlayerId).Relative;
        }
    }
}