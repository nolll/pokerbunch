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

            var postedCheckpoint = CheckpointFactory.Create(
                existingCheckpoint.CashgameId,
                existingCheckpoint.PlayerId,
                TimeZoneInfo.ConvertTimeToUtc(request.Timestamp, bunch.Timezone),
                existingCheckpoint.Type,
                request.Stack,
                request.Amount,
                existingCheckpoint.Id);

            checkpointRepository.UpdateCheckpoint(cashgame, postedCheckpoint);

            var returnUrl = new CashgameActionUrl(request.Slug, request.DateStr, request.PlayerId);
            return new EditCheckpointResult(returnUrl);
        }
    }
}
