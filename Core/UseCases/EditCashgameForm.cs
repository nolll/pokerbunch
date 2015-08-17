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
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequireManager(user, player);
            var cashgame = _cashgameRepository.GetByDateString(bunch.Id, request.DateStr);
            
            var cancelUrl = new CashgameDetailsUrl(cashgame.Id);
            var deleteUrl = new DeleteCashgameUrl(cashgame.Id);
            var location = cashgame.Location;
            var locations = _cashgameRepository.GetLocations(bunch.Id);

            return new Result(cashgame.DateString, cancelUrl, deleteUrl, location, locations);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }
            public string DateStr { get; private set; }

            public Request(string userName, string slug, string dateStr)
            {
                UserName = userName;
                Slug = slug;
                DateStr = dateStr;
            }
        }

        public class Result
        {
            public string Date { get; private set; }
            public Url CancelUrl { get; private set; }
            public Url DeleteUrl { get; private set; }
            public string Location { get; private set; }
            public IList<string> Locations { get; private set; }

            public Result(string date, Url cancelUrl, Url deleteUrl, string location, IList<string> locations)
            {
                Date = date;
                CancelUrl = cancelUrl;
                DeleteUrl = deleteUrl;
                Location = location;
                Locations = locations;
            }
        }
    }
}