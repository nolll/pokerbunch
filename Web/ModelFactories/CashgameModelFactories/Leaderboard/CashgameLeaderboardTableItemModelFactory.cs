using System.Web;
using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Web.Models.CashgameModels.Leaderboard;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories.Leaderboard
{
    public class CashgameLeaderboardTableItemModelFactory : ICashgameLeaderboardTableItemModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public CashgameLeaderboardTableItemModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public CashgameLeaderboardTableItemModel Create(Homegame homegame, CashgameTotalResult result, int rank)
        {
            var winnings = result.Winnings;
            var player = result.Player;
			
            return new CashgameLeaderboardTableItemModel
                {
                    Rank = rank,
                    TotalResult = Globalization.FormatResult(homegame.Currency, winnings),
                    ResultClass = Util.GetWinningsCssClass(winnings),
                    GameTime = Globalization.FormatDuration(result.TimePlayed),
                    WinRate = Globalization.FormatWinrate(homegame.Currency, result.WinRate),
                    Name = player.DisplayName,
                    UrlEncodedName = HttpUtility.UrlPathEncode(player.DisplayName),
                    PlayerUrl = _urlProvider.GetPlayerDetailsUrl(homegame, player)
                };
        }
    }
}