using System;
using Core.Entities.Checkpoints;
using Core.Exceptions;
using Core.Repositories;
using Core.Urls;

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
    }
}
