using System.Collections.Generic;
using Application.Services;
using Application.UseCases.CashgameTopList;
using Web.Models.CashgameModels.Toplist;
using System.Linq;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public class CashgameToplistTableModelFactory : ICashgameToplistTableModelFactory
    {
        private readonly ICashgameToplistTableItemModelFactory _cashgameToplistTableItemModelFactory;
        private readonly IUrlProvider _urlProvider;

        public CashgameToplistTableModelFactory(
            ICashgameToplistTableItemModelFactory cashgameToplistTableItemModelFactory,
            IUrlProvider urlProvider)
        {
            _cashgameToplistTableItemModelFactory = cashgameToplistTableItemModelFactory;
            _urlProvider = urlProvider;
        }

        public CashgameToplistTableModel Create(CashgameTopListResult topListResult)
        {
            var sortUrlFormat = string.Concat(_urlProvider.GetCashgameToplistUrl(topListResult.Slug, topListResult.Year), "?orderby={0}");

            return new CashgameToplistTableModel
            {
                ItemModels = GetItemModels(topListResult),
                ResultSortClass = GetSortCssClass(topListResult.OrderBy, ToplistSortOrder.Winnings),
                ResultSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.Winnings),
                BuyinSortClass = GetSortCssClass(topListResult.OrderBy, ToplistSortOrder.Buyin),
                BuyinSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.Buyin),
                CashoutSortClass = GetSortCssClass(topListResult.OrderBy, ToplistSortOrder.Cashout),
                CashoutSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.Cashout),
                GameTimeSortClass = GetSortCssClass(topListResult.OrderBy, ToplistSortOrder.TimePlayed),
                GameTimeSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.TimePlayed),
                GameCountSortClass = GetSortCssClass(topListResult.OrderBy, ToplistSortOrder.GamesPlayed),
                GameCountSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.GamesPlayed),
                WinRateSortClass = GetSortCssClass(topListResult.OrderBy, ToplistSortOrder.WinRate),
                WinRateSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.WinRate)
            };
        }

        private List<CashgameToplistTableItemModel> GetItemModels(CashgameTopListResult topListResult)
        {
            return topListResult.Items.Select(o => _cashgameToplistTableItemModelFactory.Create(o, topListResult.Slug, topListResult.Currency, topListResult.OrderBy)).ToList();
        }

        private string GetSortCssClass(ToplistSortOrder selectedSortOrder, ToplistSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
        
        private string GetSortUrl(string format, ToplistSortOrder sortOrder)
        {
            return string.Format(format, GetSortOrderUrlName(sortOrder));
        }

        private string GetSortOrderUrlName(ToplistSortOrder sortOrder)
        {
            return sortOrder.ToString().ToLower();
        }
    }
}