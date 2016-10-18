using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class CashgameDetailsChart
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly CashgameService _cashgameService;
        private readonly PlayerService _playerService;
        private readonly UserService _userService;

        public CashgameDetailsChart(IBunchRepository bunchRepository, CashgameService cashgameService, PlayerService playerService, UserService userService)
        {
            _bunchRepository = bunchRepository;
            _cashgameService = cashgameService;
            _playerService = playerService;
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetById(request.CashgameId);
            var bunch = _bunchRepository.Get(cashgame.BunchId);
            var playerIds = cashgame.Results.Select(result => result.PlayerId).ToList();
            var players = _playerService.Get(playerIds).OrderBy(o => o.Id).ToList();
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(bunch.Id, user.Id);
            RequireRole.Player(user, player);

            var playerItems = GetPlayerItems(bunch, cashgame, players, request.CurrentTime);

            return new Result(playerItems);
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
                playerItems.Add(new PlayerItem(player.Id, player.DisplayName, player.Color, resultItems));
            }
            return playerItems;
        }

        public class Request
        {
            public string UserName { get; }
            public DateTime CurrentTime { get; }
            public string CashgameId { get; }

            public Request(string userName, DateTime currentTime, string cashgameId)
            {
                UserName = userName;
                CurrentTime = currentTime;
                CashgameId = cashgameId;
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
            public string Id { get; private set; }
            public string Name { get; private set; }
            public string Color { get; private set; }
            public IList<ResultItem> Results { get; private set; }

            public PlayerItem(string id, string name, string color, IList<ResultItem> results)
            {
                Id = id;
                Name = name;
                Color = color;
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