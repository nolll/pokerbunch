using Core.Entities;
using Core.Services;
using Core.UseCases;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Web.Common.Urls.SiteUrls;

namespace Web.Models.CashgameModels.List
{
    public class CashgameListTableItemModel
    {
        public int PlayerCount { get; private set; }
        public string Location { get; private set; }
        public string Duration { get; private set; }
        public string Turnover { get; private set; }
        public string AvgBuyin { get; private set; }
        public string DetailsUrl { get; private set; }
        public string DisplayDate { get; private set; }
        public string PlayerCountSortClass { get; private set; }
        public string LocationSortClass { get; private set; }
        public string DurationSortClass { get; private set; }
        public string TurnoverSortClass { get; private set; }
        public string AvgBuyinSortClass { get; private set; }

        public CashgameListTableItemModel(CashgameList.Item item, CashgameList.SortOrder sortOrder, bool showYear)
        {
            PlayerCount = item.PlayerCount;
            PlayerCountSortClass = GetSortCssClass(sortOrder, CashgameList.SortOrder.PlayerCount);
            Location = item.Location;
            LocationSortClass = GetSortCssClass(sortOrder, CashgameList.SortOrder.Location);
            Duration = item.Duration.String;
            DurationSortClass = GetSortCssClass(sortOrder, CashgameList.SortOrder.Duration);
            Turnover = item.Turnover.String;
            TurnoverSortClass = GetSortCssClass(sortOrder, CashgameList.SortOrder.Turnover);
            AvgBuyin = item.AverageBuyin.String;
            AvgBuyinSortClass = GetSortCssClass(sortOrder, CashgameList.SortOrder.AverageBuyin);
            DetailsUrl = new CashgameDetailsUrl(item.CashgameId).Relative;
            DisplayDate = Globalization.FormatShortDate(item.Date, showYear);
        }

        private string GetSortCssClass(CashgameList.SortOrder selectedSortOrder, CashgameList.SortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "table-list--sortable__sort-item" : "";
        }
    }

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