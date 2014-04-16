using System.Collections.Generic;
using Application.Services;
using Core.Classes;
using Core.Repositories;
using Core.UseCases.CashgameTopList;
using Web.Models.CashgameModels.Toplist;
using System.Linq;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public class CashgameToplistTableModelFactory : ICashgameToplistTableModelFactory
    {
        private readonly ICashgameToplistTableItemModelFactory _cashgameToplistTableItemModelFactory;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUrlProvider _urlProvider;

        public CashgameToplistTableModelFactory(
            ICashgameToplistTableItemModelFactory cashgameToplistTableItemModelFactory,
            IPlayerRepository playerRepository,
            IUrlProvider urlProvider)
        {
            _cashgameToplistTableItemModelFactory = cashgameToplistTableItemModelFactory;
            _playerRepository = playerRepository;
            _urlProvider = urlProvider;
        }

        public CashgameToplistTableModel Create(Homegame homegame, CashgameSuite suite, int? year, ToplistSortOrder sortOrder)
        {
            var results = SortResults(suite.TotalResults, sortOrder);
            var sortUrlFormat = string.Concat(_urlProvider.GetCashgameToplistUrl(homegame.Slug, year), "?orderby={0}");

            return new CashgameToplistTableModel
                {
                    ItemModels = GetItemModels(homegame, results, sortOrder),
                    ResultSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Winnings),
                    ResultSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.Winnings),
                    BuyinSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Buyin),
                    BuyinSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.Buyin),
                    CashoutSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.Cashout),
                    CashoutSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.Cashout),
                    GameTimeSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.TimePlayed),
                    GameTimeSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.TimePlayed),
                    GameCountSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.GamesPlayed),
                    GameCountSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.GamesPlayed),
                    WinRateSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.WinRate),
                    WinRateSortUrl = GetSortUrl(sortUrlFormat, ToplistSortOrder.WinRate)
                };
        }

        public CashgameToplistTableModel Create(CashgameTopListResult topListResult)
        {
            var sortUrlFormat = string.Concat(_urlProvider.GetCashgameToplistUrl(topListResult.Slug, topListResult.Year), "?orderby={0}");

            return new CashgameToplistTableModel
            {
                //ItemModels = GetItemModels(homegame, results, sortOrder),
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

        private List<CashgameToplistTableItemModel> GetItemModels(Homegame homegame, IEnumerable<CashgameTotalResult> results, ToplistSortOrder sortOrder)
        {
            var models = new List<CashgameToplistTableItemModel>();
            var rank = 1;
            foreach (var result in results)
            {
                var player = _playerRepository.GetById(result.PlayerId);
                models.Add(_cashgameToplistTableItemModelFactory.Create(homegame, player, result, rank, sortOrder));
                rank++;
            }
            return models;
        }

        private List<CashgameToplistTableItemModel> GetItemModels(CashgameTopListResult topListResult)
        {
            return topListResult.Items.Select(o => _cashgameToplistTableItemModelFactory.Create(o, topListResult.Slug, topListResult.Currency, topListResult.OrderBy)).ToList();
        }

        private IEnumerable<CashgameTotalResult> SortResults(IEnumerable<CashgameTotalResult> results, ToplistSortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case ToplistSortOrder.WinRate:
                    return results.OrderByDescending(o => o.WinRate).ToList();
                case ToplistSortOrder.Buyin:
                    return results.OrderByDescending(o => o.Buyin).ToList();
                case ToplistSortOrder.Cashout:
                    return results.OrderByDescending(o => o.Cashout).ToList();
                case ToplistSortOrder.TimePlayed:
                    return results.OrderByDescending(o => o.TimePlayed).ToList();
                case ToplistSortOrder.GamesPlayed:
                    return results.OrderByDescending(o => o.GameCount).ToList();
                default:
                    return results.OrderByDescending(o => o.Winnings).ToList();
            }
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