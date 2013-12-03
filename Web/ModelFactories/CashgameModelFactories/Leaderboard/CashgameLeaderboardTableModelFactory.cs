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

        public CashgameLeaderboardTableModel Create(Homegame homegame, CashgameSuite suite, LeaderboardSortOrder sortOrder)
        {
            var results = SortResults(suite.TotalResults, sortOrder);

            return new CashgameLeaderboardTableModel
                {
                    ItemModels = GetItemModels(homegame, results, sortOrder),
                    ResultSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.winnings),
                    ResultSortUrl = "",
                    BuyinSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.buyin),
                    BuyinSortUrl = "",
                    CashoutSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.cashout),
                    CashoutSortUrl = "",
                    GameTimeSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.timeplayed),
                    GameTimeSortUrl = "",
                    GameCountSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.gamesplayed),
                    GameCountSortUrl = "",
                    WinRateSortClass = GetSortCssClass(sortOrder, LeaderboardSortOrder.winrate),
                    WinRateSortUrl = ""
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