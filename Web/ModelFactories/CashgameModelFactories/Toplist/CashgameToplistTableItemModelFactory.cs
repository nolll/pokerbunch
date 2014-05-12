using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.Services;
using Application.UseCases.CashgameTopList;
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
        
        public CashgameToplistTableItemModel Create(TopListItem toplistItem, string slug, ToplistSortOrder sortOrder)
        {
            var rank = toplistItem.Rank;
            var totalResult = toplistItem.Winnings.ToString();
            var buyin = toplistItem.Buyin.ToString();
            var cashout = toplistItem.Cashout.ToString();
            var resultClass = _resultFormatter.GetWinningsCssClass(toplistItem.Winnings);
            var gameTime = _globalization.FormatDuration(toplistItem.TimePlayed);
            var gameCount = toplistItem.GamesPlayed;
            var winRate = toplistItem.WinRate.ToString();
            var name = toplistItem.Name;
            var urlEncodedName = HttpUtility.UrlPathEncode(toplistItem.Name);
            var playerUrl = _urlProvider.GetPlayerDetailsUrl(slug, toplistItem.Name);
            var resultSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Winnings);
            var buyinSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Buyin);
            var cashoutSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Cashout);
            var gameTimeSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.TimePlayed);
            var gameCountSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.GamesPlayed);
            var winRateSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.WinRate);

            return new CashgameToplistTableItemModel
            {
                Rank = rank,
                TotalResult = totalResult,
                Buyin = buyin,
                Cashout = cashout,
                ResultClass = resultClass,
                GameTime = gameTime,
                GameCount = gameCount,
                WinRate = winRate,
                Name = name,
                UrlEncodedName = urlEncodedName,
                PlayerUrl = playerUrl,
                ResultSortClass = resultSortClass,
                BuyinSortClass = buyinSortClass,
                CashoutSortClass = cashoutSortClass,
                GameTimeSortClass = gameTimeSortClass,
                GameCountSortClass = gameCountSortClass,
                WinRateSortClass = winRateSortClass
            };
        }

        private string GetSortCssClass(ToplistSortOrder selectedSortOrder, ToplistSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
    }
}