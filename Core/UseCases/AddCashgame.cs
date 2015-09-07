using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class AddCashgame
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public AddCashgame(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var cashgame = new Cashgame(bunch.Id, request.Location, GameStatus.Running);
            var cashgameId = _cashgameRepository.AddGame(bunch, cashgame);

            return new Result(request.Slug, cashgameId);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }
            [Required(ErrorMessage = "Please select or enter a location")]
            public string Location { get; private set; }

            public Request(string userName, string slug, string location)
            {
                UserName = userName;
                Slug = slug;
                Location = location;
            }
        }

        public class Result
        {
            public string Slug { get; private set; }
            public int CashgameId { get; private set; }

            public Result(string slug, int cashgameId)
            {
                Slug = slug;
                CashgameId = cashgameId;
            }
        }
    }
}