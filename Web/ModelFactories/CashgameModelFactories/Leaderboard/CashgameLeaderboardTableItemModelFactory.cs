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

        public CashgameLeaderboardTableItemModelFactory(
            IUrlProvider urlProvider,
            IResultFormatter resultFormatter)
        {
            _urlProvider = urlProvider;
            _resultFormatter = resultFormatter;
        }

        public CashgameLeaderboardTableItemModel Create(Homegame homegame, CashgameTotalResult result, int rank)
        {
            var winnings = result.Winnings;
            var player = result.Player;
			
            return new CashgameLeaderboardTableItemModel
                {
                    Rank = rank,
                    TotalResult = StaticGlobalization.FormatResult(homegame.Currency, winnings),
                    ResultClass = _resultFormatter.GetWinningsCssClass(winnings),
                    GameTime = StaticGlobalization.FormatDuration(result.TimePlayed),
                    WinRate = StaticGlobalization.FormatWinrate(homegame.Currency, result.WinRate),
                    Name = player.DisplayName,
                    UrlEncodedName = HttpUtility.UrlPathEncode(player.DisplayName),
                    PlayerUrl = _urlProvider.GetPlayerDetailsUrl(homegame, player)
                };
        }
    }
}