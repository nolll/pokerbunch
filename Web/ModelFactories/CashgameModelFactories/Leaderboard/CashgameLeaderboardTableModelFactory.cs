using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Leaderboard;

namespace Web.ModelFactories.CashgameModelFactories.Leaderboard
{
    public class CashgameLeaderboardTableModelFactory : ICashgameLeaderboardTableModelFactory
    {
        private readonly ICashgameLeaderboardTableItemModelFactory _cashgameLeaderboardTableItemModelFactory;

        public CashgameLeaderboardTableModelFactory(ICashgameLeaderboardTableItemModelFactory cashgameLeaderboardTableItemModelFactory)
        {
            _cashgameLeaderboardTableItemModelFactory = cashgameLeaderboardTableItemModelFactory;
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
                models.Add(_cashgameLeaderboardTableItemModelFactory.Create(homegame, result, rank));
                rank++;
            }
            return models;
        }
    }
}