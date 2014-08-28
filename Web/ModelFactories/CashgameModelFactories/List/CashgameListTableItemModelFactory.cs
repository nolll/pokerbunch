using Application.Services;
using Application.Urls;
using Core.Entities;
using Web.Models.CashgameModels.List;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public class CashgameListTableItemModelFactory : ICashgameListTableItemModelFactory
    {
        public CashgameListTableItemModel Create(Bunch bunch, Cashgame cashgame, bool showYear, ListSortOrder sortOrder)
        {
            var playerCount = cashgame.PlayerCount;

            return new CashgameListTableItemModel
                {
                    PlayerCount = playerCount,
                    PlayerCountSortClass = GetSortCssClass(sortOrder, ListSortOrder.playercount),
                    Location = cashgame.Location,
                    LocationSortClass = GetSortCssClass(sortOrder, ListSortOrder.location),
                    Duration = GetDuration(cashgame),
                    DurationSortClass = GetSortCssClass(sortOrder, ListSortOrder.duration),
                    Turnover = GetTurnover(bunch, cashgame),
                    TurnoverSortClass = GetSortCssClass(sortOrder, ListSortOrder.turnover),
                    AvgBuyin = GetAvgBuyin(bunch, cashgame),
                    AvgBuyinSortClass = GetSortCssClass(sortOrder, ListSortOrder.averagebuyin),
                    DetailsUrl = new CashgameDetailsUrl(bunch.Slug, cashgame.DateString),
                    DisplayDate = cashgame.StartTime.HasValue ? Globalization.FormatShortDate(cashgame.StartTime.Value, showYear) : null,
                    DateSortClass = GetSortCssClass(sortOrder, ListSortOrder.date)
                };
        }

        private string GetDuration(Cashgame cashgame)
        {
            var duration = cashgame.Duration;
            if (duration > 0)
            {
                return Globalization.FormatDuration(duration);
            }
            return string.Empty;
        }

        private string GetTurnover(Bunch bunch, Cashgame cashgame)
        {
            return Globalization.FormatCurrency(bunch.Currency, cashgame.Turnover);
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