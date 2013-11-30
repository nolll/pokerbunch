using System.Web;
using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Web.Models.CashgameModels.Leaderboard;
using Web.Services;

namespace Web.ModelFactories.CashgameModelFactories.Leaderboard
{
    public class CashgameLeaderboardTableItemModelFactory : ICashgameLeaderboardTableItemModelFactory
    {
        private readonly IUrlProvider _urlProvider;
        private readonly IResultFormatter _resultFormatter;
        private readonly IGlobalization _globalization;

        public CashgameLeaderboardTableItemModelFactory(
            IUrlProvider urlProvider,
            IResultFormatter resultFormatter,
            IGlobalization globalization)
        {
            _urlProvider = urlProvider;
            _resultFormatter = resultFormatter;
            _globalization = globalization;
        }

        public CashgameLeaderboardTableItemModel Create(Homegame homegame, Player player, CashgameTotalResult result, int rank)
        {
            var winnings = result.Winnings;
			
            return new CashgameLeaderboardTableItemModel
                {
                    Rank = rank,
                    TotalResult = _globalization.FormatResult(homegame.Currency, winnings),
                    ResultClass = _resultFormatter.GetWinningsCssClass(winnings),
                    GameTime = _globalization.FormatDuration(result.TimePlayed),
                    WinRate = _globalization.FormatWinrate(homegame.Currency, result.WinRate),
                    Name = player.DisplayName,
                    UrlEncodedName = HttpUtility.UrlPathEncode(player.DisplayName),
                    PlayerUrl = _urlProvider.GetPlayerDetailsUrl(homegame, player)
                };
        }
    }
}