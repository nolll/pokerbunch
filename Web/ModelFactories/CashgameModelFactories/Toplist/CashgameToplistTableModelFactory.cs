using System.Linq;
using Application.Services;
using Application.UseCases.CashgameTopList;
using Web.Models.CashgameModels.Toplist;

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

            var itemModels = topListResult.Items.Select(o => _cashgameToplistTableItemModelFactory.Create(o, topListResult.Slug, topListResult.OrderBy)).ToList();
                
            var resultSortClass = GetSortCssClass(topListResult.OrderBy, ToplistSortOrder.Winnings);
            var resultSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.Winnings);
                
            var buyinSortClass = GetSortCssClass(topListResult.OrderBy, ToplistSortOrder.Buyin);
            var buyinSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.Buyin);
                
            var cashoutSortClass = GetSortCssClass(topListResult.OrderBy, ToplistSortOrder.Cashout);
            var cashoutSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.Cashout);
                
            var gameTimeSortClass = GetSortCssClass(topListResult.OrderBy, ToplistSortOrder.TimePlayed);
            var gameTimeSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.TimePlayed);
                
            var gameCountSortClass = GetSortCssClass(topListResult.OrderBy, ToplistSortOrder.GamesPlayed);
            var gameCountSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.GamesPlayed);
                
            var winRateSortClass = GetSortCssClass(topListResult.OrderBy, ToplistSortOrder.WinRate);
            var winRateSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.WinRate);

            return new CashgameToplistTableModel
            {
                ItemModels = itemModels,
                
                ResultSortClass = resultSortClass,
                ResultSortUrl = resultSortUrl,
                
                BuyinSortClass = buyinSortClass,
                BuyinSortUrl = buyinSortUrl,
                
                CashoutSortClass = cashoutSortClass,
                CashoutSortUrl = cashoutSortUrl,
                
                GameTimeSortClass = gameTimeSortClass,
                GameTimeSortUrl = gameTimeSortUrl,
                
                GameCountSortClass = gameCountSortClass,
                GameCountSortUrl = gameCountSortUrl,
                
                WinRateSortClass = winRateSortClass,
                WinRateSortUrl = winRateSortUrl
            };
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