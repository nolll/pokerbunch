using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class PlayerBadges
    {
        private readonly ICashgameRepository _cashgameRepository;

        public PlayerBadges(ICashgameRepository cashgameRepository)
        {
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var cashgames = _cashgameRepository.PlayerList(request.PlayerId).Where(o => !o.IsRunning);

            return new Result(cashgames);
        }

        public class Request
        {
            public string PlayerId { get; }

            public Request(string playerId)
            {
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

            public Result(IEnumerable<ListCashgame> cashgames)
            {
                var gameCount = cashgames.Count();

                PlayedOneGame = PlayedEnoughGames(gameCount, 1);
                PlayedTenGames = PlayedEnoughGames(gameCount, 10);
                Played50Games = PlayedEnoughGames(gameCount, 50);
                Played100Games = PlayedEnoughGames(gameCount, 100);
                Played200Games = PlayedEnoughGames(gameCount, 200);
                Played500Games = PlayedEnoughGames(gameCount, 500);
            }

            private bool PlayedEnoughGames(int gameCount, int requiredGameCount)
            {
                return gameCount >= requiredGameCount;
            }
        }

    }
}