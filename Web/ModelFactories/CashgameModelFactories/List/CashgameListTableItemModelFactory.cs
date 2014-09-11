using Application.Services;
using Application.Urls;
using Application.UseCases.CashgameList;
using Core.Entities;
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
                    Duration = GetDuration(item.Duration),
                    DurationSortClass = GetSortCssClass(sortOrder, ListSortOrder.Duration),
                    Turnover = Globalization.FormatCurrency(bunch.Currency, cashgame.Turnover); // todo: should be money
                    TurnoverSortClass = GetSortCssClass(sortOrder, ListSortOrder.Turnover),
                    AvgBuyin = GetAvgBuyin(bunch, cashgame),
                    AvgBuyinSortClass = GetSortCssClass(sortOrder, ListSortOrder.Averagebuyin),
                    DetailsUrl = new CashgameDetailsUrl(bunch.Slug, cashgame.DateString),
                    DisplayDate = cashgame.StartTime.HasValue ? Globalization.FormatShortDate(item.Date, showYear) : null,
                    DateSortClass = GetSortCssClass(sortOrder, ListSortOrder.date)
                };
        }

        private string GetDuration(int duration)
        {
            if (duration > 0)
                return Globalization.FormatDuration(duration);
            return string.Empty;
        }

        private string GetAvgBuyin(Bunch bunch, Cashgame cashgame)
        {
            return Globalization.FormatCurrency(bunch.Currency, cashgame.AverageBuyin);
        }

        private string GetSortCssClass(ListSortOrder selectedSortOrder, ListSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
    }
}