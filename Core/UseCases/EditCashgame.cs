using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class EditCashgame
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public EditCashgame(ICashgameRepository cashgameRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _cashgameRepository = cashgameRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var cashgame = _cashgameRepository.GetById(request.Id);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(cashgame.BunchId, user.Id);
            RoleHandler.RequireManager(user, player);
            cashgame = new Cashgame(cashgame.BunchId, request.Location, cashgame.Status, cashgame.Id);
            _cashgameRepository.UpdateGame(cashgame);
            
            return new Result(cashgame.Id);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public int Id { get; private set; }
            [Required(ErrorMessage = "Please select or enter a location")]
            public string Location { get; private set; }

            public Request(string userName, int id, string location)
            {
                UserName = userName;
                Id = id;
                Location = location;
            }
        }
        public class Result
        {
            public int CashgameId { get; private set; }

            public Result(int cashgameId)
            {
                CashgameId = cashgameId;
            }
        }
    }
}
