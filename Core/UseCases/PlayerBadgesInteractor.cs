using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases.PlayerBadges
{
    public class PlayerBadgesInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public PlayerBadgesInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
        }

        public PlayerBadgesResult Execute(PlayerBadgesRequest request)
        {
            var player = _playerRepository.GetById(request.PlayerId);
            var bunch = _bunchRepository.GetById(player.BunchId);
            var cashgames = _cashgameRepository.GetFinished(bunch.Id);

            return new PlayerBadgesResult(player.Id, cashgames);
        }

        public class PlayerBadgesRequest
        {
            public int PlayerId { get; private set; }

            public PlayerBadgesRequest(int playerId)
            {
                PlayerId = playerId;
            }
        }

        public class PlayerBadgesResult
        {
            public bool PlayedOneGame { get; private set; }
            public bool PlayedTenGames { get; private set; }
            public bool Played50Games { get; private set; }
            public bool Played100Games { get; private set; }
            public bool Played200Games { get; private set; }
            public bool Played500Games { get; private set; }

            public PlayerBadgesResult(int playerId, IEnumerable<Cashgame> cashgames)
            {
                var gameCount = GetNumberOfPlayedGames(playerId, cashgames);

                PlayedOneGame = PlayedEnoughGames(gameCount, 1);
                PlayedTenGames = PlayedEnoughGames(gameCount, 10);
                Played50Games = PlayedEnoughGames(gameCount, 50);
                Played100Games = PlayedEnoughGames(gameCount, 100);
                Played200Games = PlayedEnoughGames(gameCount, 200);
                Played500Games = PlayedEnoughGames(gameCount, 500);
            }

            private int GetNumberOfPlayedGames(int playerId, IEnumerable<Cashgame> cashgames)
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