﻿using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class PlayerBadges
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;

        public PlayerBadges(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository, IUserRepository userRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _userRepository = userRepository;
        }

        public Result Execute(Request request)
        {
            var player = _playerRepository.GetById(request.PlayerId);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            RoleHandler.RequirePlayer(user, player);
            var bunch = _bunchRepository.GetById(player.BunchId);
            var cashgames = _cashgameRepository.GetFinished(bunch.Id);

            return new Result(player.Id, cashgames);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public int PlayerId { get; private set; }

            public Request(string userName, int playerId)
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

            public Result(int playerId, IEnumerable<Cashgame> cashgames)
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