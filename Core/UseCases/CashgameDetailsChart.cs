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
        private readonly BunchService _bunchService;
        private readonly CashgameService _cashgameService;
        private readonly IPlayerRepository _playerRepository;
        private readonly UserService _userService;

        public CashgameDetailsChart(BunchService bunchService, CashgameService cashgameService, IPlayerRepository playerRepository, UserService userService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _playerRepository = playerRepository;
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetById(request.CashgameId);
            var bunch = _bunchService.Get(cashgame.BunchId);
            var playerIds = cashgame.Results.Select(result => result.PlayerId).ToList();
            var players = _playerRepository.GetList(playerIds).OrderBy(o => o.Id).ToList();
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);

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
                playerItems.Add(new PlayerItem(player.Id, player.DisplayName, resultItems));
            }
            return playerItems;
        }

        public class Request
        {
            public string UserName { get; private set; }
            public DateTime CurrentTime { get; private set; }
            public int CashgameId { get; private set; }

            public Request(string userName, DateTime currentTime, int cashgameId)
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