using Core.Services;
using Core.UseCases.CashgameList;

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

        public CashgameListTableItemModel(CashgameItem item, ListSortOrder sortOrder, bool showYear)
        {
            PlayerCount = item.PlayerCount;
            PlayerCountSortClass = GetSortCssClass(sortOrder, ListSortOrder.PlayerCount);
            Location = item.Location;
            LocationSortClass = GetSortCssClass(sortOrder, ListSortOrder.Location);
            Duration = item.Duration.ToString();
            DurationSortClass = GetSortCssClass(sortOrder, ListSortOrder.Duration);
            Turnover = item.Turnover.ToString();
            TurnoverSortClass = GetSortCssClass(sortOrder, ListSortOrder.Turnover);
            AvgBuyin = item.AverageBuyin.ToString();
            AvgBuyinSortClass = GetSortCssClass(sortOrder, ListSortOrder.AverageBuyin);
            DetailsUrl = item.Url.Relative;
            DisplayDate = Globalization.FormatShortDate(item.Date, showYear);
        }

        private string GetSortCssClass(ListSortOrder selectedSortOrder, ListSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
    }
}