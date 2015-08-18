using System.Collections.Generic;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases
{
    public class EditCashgameForm
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public EditCashgameForm(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameRepository.GetById(request.Id);
            var bunch = _bunchRepository.GetById(cashgame.BunchId);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(cashgame.BunchId, user.Id);
            RoleHandler.RequireManager(user, player);
            
            var cancelUrl = new CashgameDetailsUrl(cashgame.Id);
            var deleteUrl = new DeleteCashgameUrl(cashgame.Id);
            var location = cashgame.Location;
            var locations = _cashgameRepository.GetLocations(cashgame.BunchId);

            return new Result(cashgame.DateString, cancelUrl, deleteUrl, bunch.Slug, location, locations);
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
            public Url CancelUrl { get; private set; }
            public Url DeleteUrl { get; private set; }
            public string Slug { get; private set; }
            public string Location { get; private set; }
            public IList<string> Locations { get; private set; }

            public Result(string date, Url cancelUrl, Url deleteUrl, string slug, string location, IList<string> locations)
            {
                Date = date;
                CancelUrl = cancelUrl;
                DeleteUrl = deleteUrl;
                Slug = slug;
                Location = location;
                Locations = locations;
            }
        }
    }
}