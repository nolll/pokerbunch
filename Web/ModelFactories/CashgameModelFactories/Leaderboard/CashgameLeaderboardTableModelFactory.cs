using System.Collections.Generic;
using Core.Classes;
using Core.Repositories;
using Web.Models.CashgameModels.Leaderboard;

namespace Web.ModelFactories.CashgameModelFactories.Leaderboard
{
    public class CashgameLeaderboardTableModelFactory : ICashgameLeaderboardTableModelFactory
    {
        private readonly ICashgameLeaderboardTableItemModelFactory _cashgameLeaderboardTableItemModelFactory;
        private readonly IPlayerRepository _playerRepository;

        public CashgameLeaderboardTableModelFactory(
            ICashgameLeaderboardTableItemModelFactory cashgameLeaderboardTableItemModelFactory,
            IPlayerRepository playerRepository)
        {
            _cashgameLeaderboardTableItemModelFactory = cashgameLeaderboardTableItemModelFactory;
            _playerRepository = playerRepository;
        }

        public CashgameLeaderboardTableModel Create(Homegame homegame, CashgameSuite suite)
        {
            return new CashgameLeaderboardTableModel
                {
                    ItemModels = GetItemModels(homegame, suite)
                };
        }

        private List<CashgameLeaderboardTableItemModel> GetItemModels(Homegame homegame, CashgameSuite suite)
        {
            var results = suite.TotalResults;
            var models = new List<CashgameLeaderboardTableItemModel>();
            var rank = 1;
            foreach (var result in results)
            {
                var player = _playerRepository.GetById(result.PlayerId);
                models.Add(_cashgameLeaderboardTableItemModelFactory.Create(homegame, player, result, rank));
                rank++;
            }
            return models;
        }
    }
}