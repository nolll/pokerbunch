using System.Collections.Generic;
using Core.Classes;
using Core.Repositories;
using Core.Services;
using Web.Models.CashgameModels.Leaderboard;
using System.Linq;

namespace Web.ModelFactories.CashgameModelFactories.Leaderboard
{
    public class CashgameLeaderboardTableModelFactory : ICashgameLeaderboardTableModelFactory
    {
        private readonly ICashgameLeaderboardTableItemModelFactory _cashgameLeaderboardTableItemModelFactory;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUrlProvider _urlProvider;

        public CashgameLeaderboardTableModelFactory(
            ICashgameLeaderboardTableItemModelFactory cashgameLeaderboardTableItemModelFactory,
            IPlayerRepository playerRepository,
            IUrlProvider urlProvider)
        {
            _cashgameLeaderboardTableItemModelFactory = cashgameLeaderboardTableItemModelFactory;
            _playerRepository = playerRepository;
            _urlProvider = urlProvider;
        }

        public CashgameLeaderboardTableModel Create(Homegame homegame, CashgameSuite suite, int? year, LeaderboardSortOrder sortOrder)
        {
            var results = SortResults(suite.TotalResults, sortOrder);
            var sortUrl = string.Concat(_urlProvider.GetCashgameLeaderboardUrl(homegame, year), "?orderby={0}");

            return new CashgameLeaderboardTableModel
                {
                    ItemModels = GetItemModels(homegame, results, sortOrder),
                    ResultSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.winnings),
                    ResultSortUrl = string.Format(sortUrl, LeaderboardSortOrder.winnings),
                    BuyinSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.buyin),
                    BuyinSortUrl = string.Format(sortUrl, LeaderboardSortOrder.buyin),
                    CashoutSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.cashout),
                    CashoutSortUrl = string.Format(sortUrl, LeaderboardSortOrder.cashout),
                    GameTimeSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.timeplayed),
                    GameTimeSortUrl = string.Format(sortUrl, LeaderboardSortOrder.timeplayed),
                    GameCountSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.gamesplayed),
                    GameCountSortUrl = string.Format(sortUrl, LeaderboardSortOrder.gamesplayed),
                    WinRateSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.winrate),
                    WinRateSortUrl = string.Format(sortUrl, LeaderboardSortOrder.winrate)
                };
        }

        private List<CashgameLeaderboardTableItemModel> GetItemModels(Homegame homegame, IEnumerable<CashgameTotalResult> results, LeaderboardSortOrder sortOrder)
        {
            var models = new List<CashgameLeaderboardTableItemModel>();
            var rank = 1;
            foreach (var result in results)
            {
                var player = _playerRepository.GetById(result.PlayerId);
                models.Add(_cashgameLeaderboardTableItemModelFactory.Create(homegame, player, result, rank, sortOrder));
                rank++;
            }
            return models;
        }

        private IEnumerable<CashgameTotalResult> SortResults(IEnumerable<CashgameTotalResult> results, LeaderboardSortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case LeaderboardSortOrder.winrate:
                    return results.OrderByDescending(o => o.WinRate).ToList();
                case LeaderboardSortOrder.buyin:
                    return results.OrderByDescending(o => o.Buyin).ToList();
                case LeaderboardSortOrder.cashout:
                    return results.OrderByDescending(o => o.Cashout).ToList();
                case LeaderboardSortOrder.timeplayed:
                    return results.OrderByDescending(o => o.TimePlayed).ToList();
                case LeaderboardSortOrder.gamesplayed:
                    return results.OrderByDescending(o => o.GameCount).ToList();
                case LeaderboardSortOrder.winnings:
                default:
                    return results.OrderByDescending(o => o.Winnings).ToList();
            }
        }

        private string GetSortCssClass(LeaderboardSortOrder selectedSortOrder, LeaderboardSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
    }
}