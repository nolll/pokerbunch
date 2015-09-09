using System.Collections.Generic;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class EditCashgameForm
    {
        private readonly BunchService _bunchService;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly UserService _userService;
        private readonly IPlayerRepository _playerRepository;

        public EditCashgameForm(BunchService bunchService, ICashgameRepository cashgameRepository, UserService userService, IPlayerRepository playerRepository)
        {
            _bunchService = bunchService;
            _cashgameRepository = cashgameRepository;
            _userService = userService;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameRepository.GetById(request.Id);
            var bunch = _bunchService.Get(cashgame.BunchId);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(cashgame.BunchId, user.Id);
            RoleHandler.RequireManager(user, player);
            
            var location = cashgame.Location;
            var locations = _cashgameRepository.GetLocations(cashgame.BunchId);

            return new Result(cashgame.DateString, cashgame.Id, bunch.Slug, location, locations);
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