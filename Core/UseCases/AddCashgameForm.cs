using System.Collections.Generic;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class AddCashgameForm
    {
        private readonly BunchService _bunchService;
        private readonly CashgameService _cashgameService;
        private readonly UserService _userService;
        private readonly PlayerService _playerService;

        public AddCashgameForm(BunchService bunchService, CashgameService cashgameService, UserService userService, PlayerService playerService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _userService = userService;
            _playerService = playerService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.GetBySlug(request.Slug);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var runningGame = _cashgameService.GetRunning(bunch.Id);
            if (runningGame != null)
            {
                throw new CashgameRunningException();
            }
            var locations = _cashgameService.GetLocations(bunch.Id);
            return new Result(locations);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { private set; get; }

            public Request(string userName, string slug)
            {
                UserName = userName;
                Slug = slug;
            }
        }

        public class Result
        {
            public IList<string> Locations { get; private set; }

            public Result(IList<string> locations)
            {
                Locations = locations;
            }
        }
    }
}