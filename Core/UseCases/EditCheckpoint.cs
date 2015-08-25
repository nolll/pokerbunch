using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class EditCheckpoint
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public EditCheckpoint(IBunchRepository bunchRepository, ICheckpointRepository checkpointRepository, IUserRepository userRepository, IPlayerRepository playerRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _checkpointRepository = checkpointRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var existingCheckpoint = _checkpointRepository.GetCheckpoint(request.CheckpointId);
            var cashgame = _cashgameRepository.GetById(existingCheckpoint.CashgameId);
            var bunch = _bunchRepository.GetById(cashgame.BunchId);
            var currentUser = _userRepository.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerRepository.GetByUserId(bunch.Id, currentUser.Id);
            RoleHandler.RequireManager(currentUser, currentPlayer);
            
            var postedCheckpoint = Checkpoint.Create(
                existingCheckpoint.CashgameId,
                existingCheckpoint.PlayerId,
                TimeZoneInfo.ConvertTimeToUtc(request.Timestamp, bunch.Timezone),
                existingCheckpoint.Type,
                request.Stack,
                request.Amount,
                existingCheckpoint.Id);

            _checkpointRepository.UpdateCheckpoint(postedCheckpoint);

            return new Result(cashgame.Id, existingCheckpoint.PlayerId);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public int CheckpointId { get; private set; }
            public DateTime Timestamp { get; private set; }
            [Range(0, int.MaxValue, ErrorMessage = "Stack can't be negative")]
            public int Stack { get; private set; }
            [Range(0, int.MaxValue, ErrorMessage = "Amount can't be negative")]
            public int Amount { get; private set; }

            public Request(string userName, int checkpointId, DateTime timestamp, int stack, int amount)
            {
                UserName = userName;
                CheckpointId = checkpointId;
                Timestamp = timestamp;
                Stack = stack;
                Amount = amount;
            }
        }

        public class Result
        {
            public int CashgameId { get; private set; }
            public int PlayerId { get; private set; }

            public Result(int cashgameId, int playerId)
            {
                CashgameId = cashgameId;
                PlayerId = playerId;
            }
        }
    }
}
