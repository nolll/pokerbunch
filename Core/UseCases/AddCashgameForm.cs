using System.Collections.Generic;
using System.Linq;
using Core.Exceptions;
using Core.Services;

namespace Core.UseCases
{
    public class AddCashgameForm
    {
        private readonly BunchService _bunchService;
        private readonly CashgameService _cashgameService;
        private readonly UserService _userService;
        private readonly PlayerService _playerService;
        private readonly LocationService _locationService;

        public AddCashgameForm(BunchService bunchService, CashgameService cashgameService, UserService userService, PlayerService playerService, LocationService locationService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _userService = userService;
            _playerService = playerService;
            _locationService = locationService;
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
            var locations = _locationService.GetLocations(bunch.Id);
            var locationNames = locations.Select(o => o.Name).ToList();
            return new Result(locationNames);
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