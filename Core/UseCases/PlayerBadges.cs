using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class PlayerBadges
    {
        private readonly BunchService _bunchService;
        private readonly CashgameService _cashgameService;
        private readonly PlayerService _playerService;
        private readonly UserService _userService;

        public PlayerBadges(BunchService bunchService, CashgameService cashgameService, PlayerService playerService, UserService userService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _playerService = playerService;
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var player = _playerService.Get(request.PlayerId);
            var user = _userService.GetByNameOrEmail(request.UserName);
            RequireRole.Player(user, player);
            var bunch = _bunchService.Get(player.Slug);
            var cashgames = _cashgameService.GetFinished(bunch.Id);

            return new Result(player.Id, cashgames);
        }

        public class Request
        {
            public string UserName { get; }
            public string PlayerId { get; }

            public Request(string userName, string playerId)
            {
                UserName = userName;
                PlayerId = playerId;
            }
        }

        public class Result
        {
            public bool PlayedOneGame { get; private set; }
            public bool PlayedTenGames { get; private set; }
            public bool Played50Games { get; private set; }
            public bool Played100Games { get; private set; }
            public bool Played200Games { get; private set; }
            public bool Played500Games { get; private set; }

            public Result(string playerId, IEnumerable<Cashgame> cashgames)
            {
                var gameCount = GetNumberOfPlayedGames(playerId, cashgames);

                PlayedOneGame = PlayedEnoughGames(gameCount, 1);
                PlayedTenGames = PlayedEnoughGames(gameCount, 10);
                Played50Games = PlayedEnoughGames(gameCount, 50);
                Played100Games = PlayedEnoughGames(gameCount, 100);
                Played200Games = PlayedEnoughGames(gameCount, 200);
                Played500Games = PlayedEnoughGames(gameCount, 500);
            }

            private int GetNumberOfPlayedGames(string playerId, IEnumerable<Cashgame> cashgames)
            {
                return cashgames.Count(cashgame => cashgame.IsInGame(playerId));
            }

            private bool PlayedEnoughGames(int gameCount, int requiredGameCount)
            {
                return gameCount >= requiredGameCount;
            }
        }

    }
}