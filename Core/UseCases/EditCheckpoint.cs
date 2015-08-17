using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities;
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

        public EditCheckpoint(IBunchRepository bunchRepository, ICheckpointRepository checkpointRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _checkpointRepository = checkpointRepository;
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
            var existingCheckpoint = _checkpointRepository.GetCheckpoint(request.CheckpointId);

            var postedCheckpoint = Checkpoint.Create(
                existingCheckpoint.CashgameId,
                existingCheckpoint.PlayerId,
                TimeZoneInfo.ConvertTimeToUtc(request.Timestamp, bunch.Timezone),
                existingCheckpoint.Type,
                request.Stack,
                request.Amount,
                existingCheckpoint.Id);

            _checkpointRepository.UpdateCheckpoint(postedCheckpoint);

            var returnUrl = new CashgameActionUrl(request.Slug, request.DateStr, request.PlayerId);
            return new Result(returnUrl);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }
            public string DateStr { get; private set; }
            public int PlayerId { get; private set; }
            public int CheckpointId { get; private set; }
            public DateTime Timestamp { get; private set; }
            [Range(0, int.MaxValue, ErrorMessage = "Stack can't be negative")]
            public int Stack { get; private set; }
            [Range(0, int.MaxValue, ErrorMessage = "Amount can't be negative")]
            public int Amount { get; private set; }

            public Request(string userName, string slug, string dateStr, int playerId, int checkpointId, DateTime timestamp, int stack, int amount)
            {
                UserName = userName;
                Slug = slug;
                DateStr = dateStr;
                PlayerId = playerId;
                CheckpointId = checkpointId;
                Timestamp = timestamp;
                Stack = stack;
                Amount = amount;
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
