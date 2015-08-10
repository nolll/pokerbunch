using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class CashgameChart
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public CashgameChart(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var players = _playerRepository.GetList(bunch.Id).OrderBy(o => o.DisplayName).ToList();
            var cashgames = _cashgameRepository.GetFinished(bunch.Id, request.Year);
            var suite = new CashgameSuite(cashgames, players);

            var playerItems = GetPlayerItems(suite.TotalResults);
            var gameItems = GetGameItems(suite.Cashgames, suite.TotalResults);

            return new Result(gameItems, playerItems);
        }

        private static IList<PlayerItem> GetPlayerItems(IEnumerable<CashgameTotalResult> results)
        {
            return results.Select(result => new PlayerItem(result.Player.Id, result.Player.DisplayName)).ToList();
        }

        private static IList<GameItem> GetGameItems(IList<Cashgame> cashgames, IList<CashgameTotalResult> results)
        {
            var playerSum = GetEmptyPlayerSumArray(results);
            var gameItems = new List<GameItem>();
            for (var i = 0; i < cashgames.Count; i++)
            {
                var cashgame = cashgames[cashgames.Count - i - 1];
                var currentSums = new Dictionary<int, int>();
                foreach (var totalResult in results)
                {
                    var singleResult = cashgame.GetResult(totalResult.Player.Id);
                    var playerId = totalResult.Player.Id;
                    if (singleResult != null || i == cashgames.Count - 1)
                    {
                        var res = singleResult != null ? singleResult.Stack - singleResult.Buyin : 0;
                        var sum = playerSum[playerId] + res;

                        playerSum[playerId] = sum;
                        currentSums.Add(playerId, sum.Value);
                    }
                }
                var gameItem = new GameItem(new Date(cashgame.StartTime.Value), currentSums);
                gameItems.Add(gameItem);
            }
            return gameItems;
        }

        private static IDictionary<int, int?> GetEmptyPlayerSumArray(IEnumerable<CashgameTotalResult> results)
        {
            return results.ToDictionary<CashgameTotalResult, int, int?>(result => result.Player.Id, result => 0);
        }

        public class Request
        {
            public string Slug { get; private set; }
            public int? Year { get; private set; }

            public Request(string slug, int? year)
            {
                Slug = slug;
                Year = year;
            }
        }

        public class Result
        {
            public IList<GameItem> GameItems { get; private set; }
            public IList<PlayerItem> PlayerItems { get; private set; }

            public Result(IList<GameItem> gameItems, IList<PlayerItem> playerItems)
            {
                GameItems = gameItems;
                PlayerItems = playerItems;
            }
        }

        public class GameItem
        {
            public Date Date { get; private set; }
            public IDictionary<int, int> Winnings { get; private set; }

            public GameItem(Date date, IDictionary<int, int> winnings)
            {
                Date = date;
                Winnings = winnings;
            }
        }

        public class PlayerItem
        {
            public int Id { get; private set; }
            public string Name { get; private set; }

            public PlayerItem(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }
    }
}
