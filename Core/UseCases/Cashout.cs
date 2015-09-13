using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities.Checkpoints;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class Cashout
    {
        private readonly BunchService _bunchService;
        private readonly CashgameService _cashgameService;
        private readonly PlayerService _playerService;
        private readonly CheckpointService _checkpointService;
        private readonly UserService _userService;

        public Cashout(BunchService bunchService, CashgameService cashgameService, PlayerService playerService, CheckpointService checkpointService, UserService userService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _playerService = playerService;
            _checkpointService = checkpointService;
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchService.GetBySlug(request.Slug);
            var currentUser = _userService.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerService.GetByUserId(bunch.Id, currentUser.Id);
            RoleHandler.RequireMe(currentUser, currentPlayer, request.PlayerId);
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
                existingCashoutCheckpoint != null ? existingCashoutCheckpoint.Id : 0);

            if (existingCashoutCheckpoint != null)
                _checkpointService.Update(postedCheckpoint);
            else
                _checkpointService.Add(postedCheckpoint);

            return new Result(cashgame.Id);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }
            public int PlayerId { get; private set; }
            [Range(0, int.MaxValue, ErrorMessage = "Stack can't be negative")]
            public int Stack { get; private set; }
            public DateTime CurrentTime { get; private set; }

            public Request(string userName, string slug, int playerId, int stack, DateTime currentTime)
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
            public int CashgameId { get; private set; }

            public Result(int cashgameId)
            {
                CashgameId = cashgameId;
            }
        }
    }
}
