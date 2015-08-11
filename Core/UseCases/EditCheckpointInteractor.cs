using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases.EditCheckpoint
{
    public class EditCheckpointInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICheckpointRepository _checkpointRepository;

        public EditCheckpointInteractor(IBunchRepository bunchRepository, ICheckpointRepository checkpointRepository)
        {
            _bunchRepository = bunchRepository;
            _checkpointRepository = checkpointRepository;
        }

        public EditCheckpointResult Execute(EditCheckpointRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.GetBySlug(request.Slug);
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
            return new EditCheckpointResult(returnUrl);
        }

        public class EditCheckpointRequest
        {
            public string Slug { get; private set; }
            public string DateStr { get; private set; }
            public int PlayerId { get; private set; }
            public int CheckpointId { get; private set; }
            public DateTime Timestamp { get; private set; }
            [Range(0, int.MaxValue, ErrorMessage = "Stack can't be negative")]
            public int Stack { get; private set; }
            [Range(0, int.MaxValue, ErrorMessage = "Amount can't be negative")]
            public int Amount { get; private set; }

            public EditCheckpointRequest(string slug, string dateStr, int playerId, int checkpointId, DateTime timestamp, int stack, int amount)
            {
                Slug = slug;
                DateStr = dateStr;
                PlayerId = playerId;
                CheckpointId = checkpointId;
                Timestamp = timestamp;
                Stack = stack;
                Amount = amount;
            }
        }

        public class EditCheckpointResult
        {
            public Url ReturnUrl { get; private set; }

            public EditCheckpointResult(Url returnUrl)
            {
                ReturnUrl = returnUrl;
            }
        }
    }
}
