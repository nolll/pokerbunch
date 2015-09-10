using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class EditCashgame
    {
        private readonly CashgameService _cashgameService;
        private readonly UserService _userService;
        private readonly IPlayerRepository _playerRepository;

        public EditCashgame(CashgameService cashgameService, UserService userService, IPlayerRepository playerRepository)
        {
            _cashgameService = cashgameService;
            _userService = userService;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var cashgame = _cashgameService.GetById(request.Id);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(cashgame.BunchId, user.Id);
            RoleHandler.RequireManager(user, player);
            cashgame = new Cashgame(cashgame.BunchId, request.Location, cashgame.Status, cashgame.Id);
            _cashgameService.UpdateGame(cashgame);
            
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
