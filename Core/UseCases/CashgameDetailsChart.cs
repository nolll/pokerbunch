using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;

namespace Core.UseCases
{
    public class CashgameDetailsChart
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public CashgameDetailsChart(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = GetCashgame(bunch, request.DateStr);
            var playerIds = cashgame.Results.Select(result => result.PlayerId).ToList();
            var players = _playerRepository.GetList(playerIds).OrderBy(o => o.Id).ToList();

            var playerItems = GetPlayerItems(bunch, cashgame, players, request.CurrentTime);

            return new Result(playerItems);
        }

        private Cashgame GetCashgame(Bunch bunch, string dateStr)
        {
            if (string.IsNullOrEmpty(dateStr))
                return _cashgameRepository.GetRunning(bunch.Id);
            return _cashgameRepository.GetByDateString(bunch.Id, dateStr);
        }

        private static IList<PlayerItem> GetPlayerItems(Bunch bunch, Cashgame cashgame, IEnumerable<Player> players, DateTime now)
        {
            var playerItems = new List<PlayerItem>();
            foreach (var player in players)
            {
                var result = cashgame.GetResult(player.Id);
                var resultItems = new List<ResultItem>();
                var totalBuyin = 0;
                var checkpoints = result.Checkpoints;
                foreach (var checkpoint in checkpoints)
                {
                    if (checkpoint.Type == CheckpointType.Buyin)
                    {
                        totalBuyin += checkpoint.Amount;
                    }
                    var localTime = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, bunch.Timezone);
                    var winnings = checkpoint.Stack - totalBuyin;
                    resultItems.Add(new ResultItem(localTime, winnings));
                }
                if (cashgame.Status == GameStatus.Running)
                {
                    var timestamp = TimeZoneInfo.ConvertTime(now, bunch.Timezone);
                    resultItems.Add(new ResultItem(timestamp, result.Winnings));
                }
                playerItems.Add(new PlayerItem(player.Id, player.DisplayName, resultItems));
            }
            return playerItems;
        }

        public class Request
        {
            public string Slug { get; private set; }
            public DateTime CurrentTime { get; private set; }
            public string DateStr { get; private set; }

            public Request(string slug, DateTime currentTime, string dateStr = null)
            {
                Slug = slug;
                CurrentTime = currentTime;
                DateStr = dateStr;
            }
        }

        public class Result
        {
            public IList<PlayerItem> PlayerItems { get; private set; }

            public Result(IList<PlayerItem> playerItems)
            {
                PlayerItems = playerItems;
            }
        }

        public class PlayerItem
        {
            public int Id { get; private set; }
            public string Name { get; private set; }
            public IList<ResultItem> Results { get; private set; }

            public PlayerItem(int id, string name, IList<ResultItem> results)
            {
                Id = id;
                Name = name;
                Results = results;
            }
        }

        public class ResultItem
        {
            public DateTime Timestamp { get; private set; }
            public int Winnings { get; private set; }

            public ResultItem(DateTime timestamp, int winnings)
            {
                Timestamp = timestamp;
                Winnings = winnings;
            }
        }
    }
}