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
        public int Rank { get; private set; }

        [UsedImplicitly]
        [JsonProperty("name")]
        public string Name { get; private set; }

        [UsedImplicitly]
        [JsonProperty("winnings")]
        public int Winnings { get; private set; }

        [UsedImplicitly]
        [JsonProperty("buyin")]
        public int Buyin { get; private set; }

        [UsedImplicitly]
        [JsonProperty("cashout")]
        public int Cashout { get; private set; }

        [UsedImplicitly]
        [JsonProperty("time")]
        public int GameTime { get; private set; }

        [UsedImplicitly]
        [JsonProperty("gameCount")]
        public int GameCount { get; private set; }

        [UsedImplicitly]
        [JsonProperty("winRate")]
        public int WinRate { get; private set; }

        [UsedImplicitly]
        [JsonProperty("url")]
        public string PlayerUrl { get; private set; }

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