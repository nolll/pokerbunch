using System;
using Core.Entities.Checkpoints;
using Core.Exceptions;
using Core.Factories;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.EditCheckpoint
{
    public static class EditCheckpointInteractor
    {
        public static EditCheckpointResult Execute(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, ICheckpointRepository checkpointRepository, EditCheckpointRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgame = cashgameRepository.GetByDateString(bunch, request.DateStr);
            var existingCheckpoint = checkpointRepository.GetCheckpoint(request.CheckpointId);

            var postedCheckpoint = CreateCheckpoint(request, existingCheckpoint, bunch.Timezone);
            checkpointRepository.UpdateCheckpoint(cashgame, postedCheckpoint);

            var returnUrl = new CashgameActionUrl(request.Slug, request.DateStr, request.PlayerId);
            return new EditCheckpointResult(returnUrl);
        }

        private static Checkpoint CreateCheckpoint(EditCheckpointRequest request, Checkpoint existingCheckpoint, TimeZoneInfo timeZone)
        {
            return CheckpointFactory.Create(
                TimeZoneInfo.ConvertTimeToUtc(request.Timestamp, timeZone),
                existingCheckpoint.Type,
                request.Stack,
                request.Amount,
                existingCheckpoint.Id);
        }
    }
}
