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

        public CashgameLeaderboardTableItemModel Create(Homegame homegame, Player player, CashgameTotalResult result, int rank, LeaderboardSortOrder sortOrder)
        {
            return new CashgameLeaderboardTableItemModel
                {
                    Rank = rank,
                    TotalResult = _globalization.FormatResult(homegame.Currency, result.Winnings),
                    ResultSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.winnings),
                    Buyin = _globalization.FormatCurrency(homegame.Currency, result.Buyin),
                    BuyinSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.buyin),
                    Cashout = _globalization.FormatCurrency(homegame.Currency, result.Cashout),
                    CashoutSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.cashout),
                    ResultClass = _resultFormatter.GetWinningsCssClass(result.Winnings),
                    GameTime = _globalization.FormatDuration(result.TimePlayed),
                    GameTimeSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.timeplayed),
                    GameCount = result.GameCount,
                    GameCountSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.gamesplayed),
                    WinRate = _globalization.FormatWinrate(homegame.Currency, result.WinRate),
                    WinRateSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.winrate),
                    Name = player.DisplayName,
                    UrlEncodedName = HttpUtility.UrlPathEncode(player.DisplayName),
                    PlayerUrl = _urlProvider.GetPlayerDetailsUrl(homegame, player)
                };
        }

        private string GetSortCssClass(LeaderboardSortOrder selectedSortOrder, LeaderboardSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
    }
}