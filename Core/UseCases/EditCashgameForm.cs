using System.Collections.Generic;
using System.Linq;
using Core.Services;

namespace Core.UseCases
{
    public class EditCashgameForm
    {
        private readonly BunchService _bunchService;
        private readonly CashgameService _cashgameService;
        private readonly UserService _userService;
        private readonly PlayerService _playerService;
        private readonly LocationService _locationService;

        public EditCashgameForm(BunchService bunchService, CashgameService cashgameService, UserService userService, PlayerService playerService, LocationService locationService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _userService = userService;
            _playerService = playerService;
            _locationService = locationService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetById(request.Id);
            var bunch = _bunchService.Get(cashgame.BunchId);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(cashgame.BunchId, user.Id);
            RoleHandler.RequireManager(user, player);

            var locationName = _locationService.Get(cashgame.LocationId).Name;
            var locations = _locationService.GetByBunch(cashgame.BunchId);
            var locationNames = locations.Select(o => o.Name).ToList();

            return new Result(cashgame.DateString, cashgame.Id, bunch.Slug, locationName, locationNames);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public int Id { get; private set; }

            public Request(string userName, int id)
            {
                UserName = userName;
                Id = id;
            }
        }

        public class Result
        {
            public string Date { get; private set; }
            public int CashgameId { get; private set; }
            public string Slug { get; private set; }
            public string Location { get; private set; }
            public IList<string> Locations { get; private set; }

            public Result(string date, int cashgameId, string slug, string location, IList<string> locations)
            {
                Date = date;
                CashgameId = cashgameId;
                Slug = slug;
                Location = location;
                Locations = locations;
            }
        }
    }
}