using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class EditCashgame
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public EditCashgame(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequireManager(user, player);
            var cashgame = _cashgameRepository.GetByDateString(bunch.Id, request.DateStr);
            cashgame = new Cashgame(cashgame.BunchId, request.Location, cashgame.Status, cashgame.Id);
            _cashgameRepository.UpdateGame(cashgame);
            
            var returnUrl = new CashgameDetailsUrl(cashgame.Id);
            return new Result(returnUrl);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }
            public string DateStr { get; private set; }
            [Required(ErrorMessage = "Please select or enter a location")]
            public string Location { get; private set; }

            public Request(string userName, string slug, string dateStr, string location)
            {
                UserName = userName;
                Slug = slug;
                DateStr = dateStr;
                Location = location;
            }
        }
        public class Result
        {
            public Url ReturnUrl { get; private set; }

            public Result(Url returnUrl)
            {
                ReturnUrl = returnUrl;
            }
        }
    }
}
