using Application.Services;
using Application.Urls;
using Core.Entities;
using Web.Models.CashgameModels.List;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public class CashgameListTableItemModelFactory : ICashgameListTableItemModelFactory
    {
        public CashgameListTableItemModel Create(Homegame homegame, Cashgame cashgame, bool showYear, ListSortOrder sortOrder)
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
                    Turnover = GetTurnover(homegame, cashgame),
                    TurnoverSortClass = GetSortCssClass(sortOrder, ListSortOrder.turnover),
                    AvgBuyin = GetAvgBuyin(homegame, cashgame),
                    AvgBuyinSortClass = GetSortCssClass(sortOrder, ListSortOrder.averagebuyin),
                    DetailsUrl = new CashgameDetailsUrl(homegame.Slug, cashgame.DateString),
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

        private string GetTurnover(Homegame homegame, Cashgame cashgame)
        {
            return Globalization.FormatCurrency(homegame.Currency, cashgame.Turnover);
        }

        private string GetAvgBuyin(Homegame homegame, Cashgame cashgame)
        {
            return Globalization.FormatCurrency(homegame.Currency, cashgame.AverageBuyin);
        }

        private string GetSortCssClass(ListSortOrder selectedSortOrder, ListSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
    }
}