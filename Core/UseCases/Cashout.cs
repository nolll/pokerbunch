using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class Cashout
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameService _cashgameService;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;

        public Cashout(IBunchRepository bunchRepository, ICashgameService cashgameService, IPlayerRepository playerRepository, IUserRepository userRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameService = cashgameService;
            _playerRepository = playerRepository;
            _userRepository = userRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.Get(request.Slug);
            var currentUser = _userRepository.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerRepository.GetByUser(bunch.Id, currentUser.Id);
            RequireRole.Me(currentUser, currentPlayer, request.PlayerId);
            var cashgame = _cashgameService.GetRunning(bunch.Id);
            var result = cashgame.GetResult(request.PlayerId);

            var existingCashoutCheckpoint = result.CashoutCheckpoint;
            var postedCheckpoint = Checkpoint.Create(
                cashgame.Id,
                request.PlayerId,
                request.CurrentTime,
                CheckpointType.Cashout,
                request.Stack,
                0,
                existingCashoutCheckpoint != null ? existingCashoutCheckpoint.Id : "");

            if (existingCashoutCheckpoint != null)
                cashgame.UpdateCheckpoint(postedCheckpoint);
            else
                cashgame.AddCheckpoint(postedCheckpoint);
            _cashgameService.Update(cashgame);

            return new Result(cashgame.Id);
        }

        public class Request
        {
            public string UserName { get; }
            public string Slug { get; }
            public string PlayerId { get; }
            [Range(0, int.MaxValue, ErrorMessage = "Stack can't be negative")]
            public int Stack { get; }
            public DateTime CurrentTime { get; }

            public Request(string userName, string slug, string playerId, int stack, DateTime currentTime)
            {
                UserName = userName;
                Slug = slug;
                PlayerId = playerId;
                Stack = stack;
                CurrentTime = currentTime;
            }
        }

        public class Result
        {
            public string CashgameId { get; private set; }

            public Result(string cashgameId)
            {
                CashgameId = cashgameId;
            }
        }
    }
}
