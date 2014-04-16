using System.Web;
using Application.Services;
using Core.Classes;
using Core.UseCases.CashgameTopList;
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
                ResultSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Winnings),
                Buyin = _globalization.FormatCurrency(homegame.Currency, result.Buyin),
                BuyinSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Buyin),
                Cashout = _globalization.FormatCurrency(homegame.Currency, result.Cashout),
                CashoutSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Cashout),
                ResultClass = _resultFormatter.GetWinningsCssClass(result.Winnings),
                GameTime = _globalization.FormatDuration(result.TimePlayed),
                GameTimeSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.TimePlayed),
                GameCount = result.GameCount,
                GameCountSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.GamesPlayed),
                WinRate = _globalization.FormatWinrate(homegame.Currency, result.WinRate),
                WinRateSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.WinRate),
                Name = player.DisplayName,
                UrlEncodedName = HttpUtility.UrlPathEncode(player.DisplayName),
                PlayerUrl = _urlProvider.GetPlayerDetailsUrl(homegame.Slug, player.DisplayName)
            };
        }

        public CashgameToplistTableItemModel Create(TopListItem toplistItem, string slug, CurrencySettings currency, ToplistSortOrder sortOrder)
        {
            return new CashgameToplistTableItemModel
            {
                Rank = toplistItem.Rank,
                TotalResult = _globalization.FormatResult(currency, toplistItem.Winnings),
                ResultSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Winnings),
                Buyin = _globalization.FormatCurrency(currency, toplistItem.Buyin),
                BuyinSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Buyin),
                Cashout = _globalization.FormatCurrency(currency, toplistItem.Cashout),
                CashoutSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Cashout),
                ResultClass = _resultFormatter.GetWinningsCssClass(toplistItem.Winnings),
                GameTime = _globalization.FormatDuration(toplistItem.MinutesPlayed),
                GameTimeSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.TimePlayed),
                GameCount = toplistItem.GamesPlayed,
                GameCountSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.GamesPlayed),
                WinRate = _globalization.FormatWinrate(currency, toplistItem.WinRate),
                WinRateSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.WinRate),
                Name = toplistItem.Name,
                UrlEncodedName = HttpUtility.UrlPathEncode(toplistItem.Name),
                PlayerUrl = _urlProvider.GetPlayerDetailsUrl(slug, toplistItem.Name)
            };
        }

        private string GetSortCssClass(ToplistSortOrder selectedSortOrder, ToplistSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
    }
}