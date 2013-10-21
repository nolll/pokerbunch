using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Web.Models.CashgameModels.Listing;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories.Listing
{
    public class CashgameListingTableItemModelFactory : ICashgameListingTableItemModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public CashgameListingTableItemModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public CashgameListingTableItemModel Create(Homegame homegame, Cashgame cashgame, bool showYear)
        {
            var playerCount = cashgame.PlayerCount;

            return new CashgameListingTableItemModel
                {
                    PlayerCount = playerCount,
                    Location = cashgame.Location,
                    Duration = GetDuration(cashgame),
                    Turnover = GetTurnover(homegame, cashgame),
                    AvgBuyin = GetAvgBuyin(homegame, cashgame, playerCount),
                    DetailsUrl = _urlProvider.GetCashgameDetailsUrl(homegame, cashgame),
                    DisplayDate = cashgame.StartTime.HasValue ? Globalization.FormatShortDate(cashgame.StartTime.Value, showYear) : null,
                    PublishedClass = GetPublishedClass(cashgame)
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

        private string GetAvgBuyin(Homegame homegame, Cashgame cashgame, int playerCount)
        {
            return Globalization.FormatCurrency(homegame.Currency, cashgame.AverageBuyin);
        }

        private string GetPublishedClass(Cashgame cashgame)
        {
            return cashgame.Status == GameStatus.Published ? string.Empty : "unpublished";
        }

    }
}