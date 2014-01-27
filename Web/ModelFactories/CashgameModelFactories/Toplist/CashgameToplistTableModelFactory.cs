using System.Collections.Generic;
using Application.Services.Interfaces;
using Core.Classes;
using Core.Repositories;
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
            var sortUrl = string.Concat(_urlProvider.GetCashgameToplistUrl(homegame.Slug, year), "?orderby={0}");

            return new CashgameToplistTableModel
                {
                    ItemModels = GetItemModels(homegame, results, sortOrder),
                    ResultSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.winnings),
                    ResultSortUrl = string.Format(sortUrl, ToplistSortOrder.winnings),
                    BuyinSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.buyin),
                    BuyinSortUrl = string.Format(sortUrl, ToplistSortOrder.buyin),
                    CashoutSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.cashout),
                    CashoutSortUrl = string.Format(sortUrl, ToplistSortOrder.cashout),
                    GameTimeSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.timeplayed),
                    GameTimeSortUrl = string.Format(sortUrl, ToplistSortOrder.timeplayed),
                    GameCountSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.gamesplayed),
                    GameCountSortUrl = string.Format(sortUrl, ToplistSortOrder.gamesplayed),
                    WinRateSortClass = GetSortCssClass(sortOrder, ToplistSortOrder.winrate),
                    WinRateSortUrl = string.Format(sortUrl, ToplistSortOrder.winrate)
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

        private IEnumerable<CashgameTotalResult> SortResults(IEnumerable<CashgameTotalResult> results, ToplistSortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case ToplistSortOrder.winrate:
                    return results.OrderByDescending(o => o.WinRate).ToList();
                case ToplistSortOrder.buyin:
                    return results.OrderByDescending(o => o.Buyin).ToList();
                case ToplistSortOrder.cashout:
                    return results.OrderByDescending(o => o.Cashout).ToList();
                case ToplistSortOrder.timeplayed:
                    return results.OrderByDescending(o => o.TimePlayed).ToList();
                case ToplistSortOrder.gamesplayed:
                    return results.OrderByDescending(o => o.GameCount).ToList();
                default:
                    return results.OrderByDescending(o => o.Winnings).ToList();
            }
        }

        private string GetSortCssClass(ToplistSortOrder selectedSortOrder, ToplistSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
    }
}