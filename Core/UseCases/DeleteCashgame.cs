﻿using Core.Exceptions;
using Core.Services;

namespace Core.UseCases
{
    public class DeleteCashgame
    {
        private readonly CashgameService _cashgameService;
        private readonly BunchService _bunchService;
        private readonly UserService _userService;
        private readonly PlayerService _playerService;

        public DeleteCashgame(CashgameService cashgameService, BunchService bunchService, UserService userService, PlayerService playerService)
        {
            _cashgameService = cashgameService;
            _bunchService = bunchService;
            _userService = userService;
            _playerService = playerService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetById(request.Id);
            var bunch = _bunchService.Get(cashgame.Bunch);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(bunch.Slug, user.Id);
            RequireRole.Manager(user, player);

            if (cashgame.PlayerCount > 0)
                throw new CashgameHasResultsException();

            _cashgameService.DeleteGame(cashgame.Id);

            return new Result(bunch.Slug);
        }

        public class Request
        {
            public string UserName { get; }
            public int Id { get; }

            public Request(string userName, int id)
            {
                UserName = userName;
                Id = id;
            }
        }

        public class Result
        {
            public string Slug { get; private set; }

            public Result(string slug)
            {
                Slug = slug;
            }
        }
    }
}
