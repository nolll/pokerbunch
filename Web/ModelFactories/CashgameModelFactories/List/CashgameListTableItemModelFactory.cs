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

        public CashgameListTableItemModel Create(Homegame homegame, Cashgame cashgame, bool showYear)
        {
            var playerCount = cashgame.PlayerCount;

            return new CashgameListTableItemModel
                {
                    PlayerCount = playerCount,
                    Location = cashgame.Location,
                    Duration = GetDuration(cashgame),
                    Turnover = GetTurnover(homegame, cashgame),
                    AvgBuyin = GetAvgBuyin(homegame, cashgame, playerCount),
                    DetailsUrl = _urlProvider.GetCashgameDetailsUrl(homegame.Slug, cashgame.DateString),
                    DisplayDate = cashgame.StartTime.HasValue ? _globalization.FormatShortDate(cashgame.StartTime.Value, showYear) : null,
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

    }
}