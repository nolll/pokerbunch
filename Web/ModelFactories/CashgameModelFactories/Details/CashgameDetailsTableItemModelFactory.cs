using System;
using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Web.Models.CashgameModels.Details;
using Web.Services;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public class CashgameDetailsTableItemModelFactory : ICashgameDetailsTableItemModelFactory
    {
        private readonly IUrlProvider _urlProvider;
        private readonly IResultFormatter _resultFormatter;
        private readonly IGlobalization _globalization;

        public CashgameDetailsTableItemModelFactory(
            IUrlProvider urlProvider,
            IResultFormatter resultFormatter,
            IGlobalization globalization)
        {
            _urlProvider = urlProvider;
            _resultFormatter = resultFormatter;
            _globalization = globalization;
        }

        public CashgameDetailsTableItemModel Create(Homegame homegame, Cashgame cashgame, CashgameResult result)
        {
            return new CashgameDetailsTableItemModel
                {
                    Name = result.Player != null ? result.Player.DisplayName : null,
                    PlayerUrl = result.Player != null ? _urlProvider.GetCashgameActionUrl(homegame, cashgame, result.Player) : null,
                    Buyin = _globalization.FormatCurrency(homegame.Currency, result.Buyin),
                    Cashout = _globalization.FormatCurrency(homegame.Currency, result.Stack),
                    Winnings = _globalization.FormatResult(homegame.Currency, result.Winnings),
                    WinningsClass = _resultFormatter.GetWinningsCssClass(result.Winnings),
                    Winrate = GetWinRate(result, homegame.Currency)
                };
        }

        private string GetWinRate(CashgameResult result, CurrencySettings currency)
        {
            if (result.PlayedTime > 0)
            {
                var winrate = (int)Math.Round((double)result.Winnings / result.PlayedTime * 60);
                return _globalization.FormatWinrate(currency, winrate);
            }
            return string.Empty;
        }
    }
}