using System.Web;
using Application.Services;
using Core.Classes;
using Web.Models.CashgameModels.Toplist;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public class CashgameToplistTableItemModelFactory : ICashgameToplistTableItemModelFactory
    {
        private readonly IUrlProvider _urlProvider;
        private readonly IResultFormatter _resultFormatter;
        private readonly IGlobalization _globalization;

        public CashgameToplistTableItemModelFactory(
            IUrlProvider urlProvider,
            IResultFormatter resultFormatter,
            IGlobalization globalization)
        {
            _urlProvider = urlProvider;
            _resultFormatter = resultFormatter;
            _globalization = globalization;
        }

        public CashgameToplistTableItemModel Create(Homegame homegame, Player player, CashgameTotalResult result, int rank, ToplistSortOrder sortOrder)
        {
            return new CashgameToplistTableItemModel
                {
                    Rank = rank,
                    TotalResult = _globalization.FormatResult(homegame.Currency, result.Winnings),
                    ResultSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.winnings),
                    Buyin = _globalization.FormatCurrency(homegame.Currency, result.Buyin),
                    BuyinSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.buyin),
                    Cashout = _globalization.FormatCurrency(homegame.Currency, result.Cashout),
                    CashoutSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.cashout),
                    ResultClass = _resultFormatter.GetWinningsCssClass(result.Winnings),
                    GameTime = _globalization.FormatDuration(result.TimePlayed),
                    GameTimeSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.timeplayed),
                    GameCount = result.GameCount,
                    GameCountSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.gamesplayed),
                    WinRate = _globalization.FormatWinrate(homegame.Currency, result.WinRate),
                    WinRateSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.winrate),
                    Name = player.DisplayName,
                    UrlEncodedName = HttpUtility.UrlPathEncode(player.DisplayName),
                    PlayerUrl = _urlProvider.GetPlayerDetailsUrl(homegame.Slug, player.DisplayName)
                };
        }

        private string GetSortCssClass(ToplistSortOrder selectedSortOrder, ToplistSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
    }
}