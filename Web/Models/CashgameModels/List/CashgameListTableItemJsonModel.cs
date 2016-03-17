using Core.UseCases;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Web.Common.Urls.SiteUrls;

namespace Web.Models.CashgameModels.List
{
    public class CashgameListTableItemJsonModel
    {
        [UsedImplicitly]
        [JsonProperty("date")]
        public string DisplayDate { get; private set; }

        [UsedImplicitly]
        [JsonProperty("playerCount")]
        public int PlayerCount { get; private set; }

        [UsedImplicitly]
        [JsonProperty("duration")]
        public int Duration { get; private set; }

        [UsedImplicitly]
        [JsonProperty("turnover")]
        public int Turnover { get; private set; }

        [UsedImplicitly]
        [JsonProperty("averageBuyin")]
        public int AvgBuyin { get; private set; }

        [UsedImplicitly]
        [JsonProperty("url")]
        public string DetailsUrl { get; private set; }

        [UsedImplicitly]
        [JsonProperty("location")]
        public string Location { get; private set; }

        public CashgameListTableItemJsonModel(CashgameList.Item item)
        {
            DisplayDate = item.Date.IsoString;
            PlayerCount = item.PlayerCount;
            Duration = item.Duration.Minutes;
            Turnover = item.Turnover.Amount;
            AvgBuyin = item.AverageBuyin.Amount;
            DetailsUrl = new CashgameDetailsUrl(item.CashgameId).Relative;
            Location = item.Location;
        }
    }
}