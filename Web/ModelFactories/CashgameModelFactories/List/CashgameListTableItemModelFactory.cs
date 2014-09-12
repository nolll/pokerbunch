using Application.Services;
using Application.UseCases.CashgameList;
using Web.Models.CashgameModels.List;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public class CashgameListTableItemModelFactory
    {
        public CashgameListTableItemModel Create(CashgameItem item, ListSortOrder sortOrder, bool showYear)
        {
            return new CashgameListTableItemModel
                {
                    PlayerCount = item.PlayerCount,
                    PlayerCountSortClass = GetSortCssClass(sortOrder, ListSortOrder.PlayerCount),
                    Location = item.Location,
                    LocationSortClass = GetSortCssClass(sortOrder, ListSortOrder.Location),
                    Duration = item.Duration.ToString(),
                    DurationSortClass = GetSortCssClass(sortOrder, ListSortOrder.Duration),
                    Turnover = item.Turnover.ToString(),
                    TurnoverSortClass = GetSortCssClass(sortOrder, ListSortOrder.Turnover),
                    AvgBuyin = item.AverageBuyin.ToString(),
                    AvgBuyinSortClass = GetSortCssClass(sortOrder, ListSortOrder.AverageBuyin),
                    DetailsUrl = item.Url.Relative,
                    DisplayDate = Globalization.FormatShortDate(item.Date, showYear),
                    DateSortClass = GetSortCssClass(sortOrder, ListSortOrder.Date)
                };
        }

        private string GetSortCssClass(ListSortOrder selectedSortOrder, ListSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
    }
}