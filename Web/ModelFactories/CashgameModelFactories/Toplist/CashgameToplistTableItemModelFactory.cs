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
            return new CashgameToplistTableItemModel
            {
                Rank = toplistItem.Rank,
                TotalResult = _globalization.FormatResult(toplistItem.Winnings),
                ResultSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Winnings),
                Buyin = _globalization.FormatCurrency(toplistItem.Buyin),
                BuyinSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Buyin),
                Cashout = _globalization.FormatCurrency(toplistItem.Cashout),
                CashoutSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Cashout),
                ResultClass = _resultFormatter.GetWinningsCssClass(toplistItem.Winnings),
                GameTime = _globalization.FormatDuration(toplistItem.MinutesPlayed),
                GameTimeSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.TimePlayed),
                GameCount = toplistItem.GamesPlayed,
                GameCountSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.GamesPlayed),
                WinRate = _globalization.FormatWinrate(toplistItem.WinRate),
                WinRateSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.WinRate),
                Name = toplistItem.Name,
                UrlEncodedName = HttpUtility.UrlPathEncode(toplistItem.Name),
                PlayerUrl = _urlProvider.GetPlayerDetailsUrl(slug, toplistItem.Name)
            };
        }

        public IList<CashgameToplistTableItemModel> CreateList(CashgameTopListResult topListResult)
        {
            return topListResult.Items.Select(o => Create(o, topListResult.Slug, topListResult.OrderBy)).ToList();
        }

        private string GetSortCssClass(ToplistSortOrder selectedSortOrder, ToplistSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
    }
}