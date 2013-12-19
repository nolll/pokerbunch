using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Web.Models.CashgameModels.List;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public class CashgameListTableItemModelFactory : ICashgameListTableItemModelFactory
    {
        private readonly IUrlProvider _urlProvider;
        private readonly IGlobalization _globalization;

        public CashgameListTableItemModelFactory(
            IUrlProvider urlProvider,
            IGlobalization globalization)
        {
            _urlProvider = urlProvider;
            _globalization = globalization;
        }

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
                    AvgBuyin = GetAvgBuyin(homegame, cashgame, playerCount),
                    AvgBuyinSortClass = GetSortCssClass(sortOrder, ListSortOrder.averagebuyin),
                    DetailsUrl = _urlProvider.GetCashgameDetailsUrl(homegame.Slug, cashgame.DateString),
                    DisplayDate = cashgame.StartTime.HasValue ? _globalization.FormatShortDate(cashgame.StartTime.Value, showYear) : null,
                    DateSortClass = GetSortCssClass(sortOrder, ListSortOrder.date),
                    PublishedClass = GetPublishedClass(cashgame)
                };
        }

        private string GetDuration(Cashgame cashgame)
        {
            var duration = cashgame.Duration;
            if (duration > 0)
            {
                return _globalization.FormatDuration(duration);
            }
            return string.Empty;
        }

        private string GetTurnover(Homegame homegame, Cashgame cashgame)
        {
            return _globalization.FormatCurrency(homegame.Currency, cashgame.Turnover);
        }

        private string GetAvgBuyin(Homegame homegame, Cashgame cashgame, int playerCount)
        {
            return _globalization.FormatCurrency(homegame.Currency, cashgame.AverageBuyin);
        }

        private string GetPublishedClass(Cashgame cashgame)
        {
            return cashgame.Status == GameStatus.Published ? string.Empty : "unpublished";
        }

        private string GetSortCssClass(ListSortOrder selectedSortOrder, ListSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
    }
}