using System;
using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Web.Models.CashgameModels.Details;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public class CashgameDetailsTableItemModelFactory : ICashgameDetailsTableItemModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public CashgameDetailsTableItemModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public CashgameDetailsTableItemModel Create(Homegame homegame, Cashgame cashgame, CashgameResult result)
        {
            return new CashgameDetailsTableItemModel
                {
                    Name = result.Player != null ? result.Player.DisplayName : null,
                    PlayerUrl = result.Player != null ? _urlProvider.GetCashgameActionUrl(homegame, cashgame, result.Player) : null,
                    Buyin = Globalization.FormatCurrency(homegame.Currency, result.Buyin),
                    Cashout = Globalization.FormatCurrency(homegame.Currency, result.Stack),
                    Winnings = Globalization.FormatResult(homegame.Currency, result.Winnings),
                    WinningsClass = Util.GetWinningsCssClass(result.Winnings),
                    Winrate = GetWinRate(result, homegame.Currency)
                };
        }

        private string GetWinRate(CashgameResult result, CurrencySettings currency)
        {
            if (result.PlayedTime > 0)
            {
                var winrate = (int)Math.Round((double)result.Winnings / result.PlayedTime * 60);
                return Globalization.FormatWinrate(currency, winrate);
            }
            return string.Empty;
        }
    }
}