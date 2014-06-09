using System;
using Application.Services;
using Core.Entities;
using Web.Models.CashgameModels.Details;
using Web.Services;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public class CashgameDetailsTableItemModelFactory : ICashgameDetailsTableItemModelFactory
    {
        private readonly IGlobalization _globalization;

        public CashgameDetailsTableItemModelFactory(IGlobalization globalization)
        {
            _globalization = globalization;
        }

        public CashgameDetailsTableItemModel Create(Homegame homegame, Cashgame cashgame, Player player, CashgameResult result)
        {
            return new CashgameDetailsTableItemModel
                {
                    Name = player.DisplayName,
                    PlayerUrl = new CashgameActionUrlModel(homegame.Slug, cashgame.DateString, player.Id),
                    Buyin = _globalization.FormatCurrency(homegame.Currency, result.Buyin),
                    Cashout = _globalization.FormatCurrency(homegame.Currency, result.Stack),
                    Winnings = _globalization.FormatResult(homegame.Currency, result.Winnings),
                    WinningsClass = ResultFormatter.GetWinningsCssClass(result.Winnings),
                    Winrate = GetWinRate(result, homegame.Currency)
                };
        }

        private string GetWinRate(CashgameResult result, Currency currency)
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