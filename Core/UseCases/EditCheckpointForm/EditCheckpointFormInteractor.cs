using System;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.EditCheckpointForm
{
    public static class EditCheckpointFormInteractor
    {
        public static EditCheckpointFormResult Execute(
            IBunchRepository bunchRepository,
            ICheckpointRepository checkpointRepository,
            EditCheckpointFormRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var checkpoint = checkpointRepository.GetCheckpoint(request.CheckpointId);
            var stack = checkpoint.Stack;
            var amount = checkpoint.Amount;
            var timestamp = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, bunch.Timezone);
            var deleteUrl = new DeleteCheckpointUrl(request.Slug, request.DateString, request.PlayerId, request.CheckpointId);
            var cancelUrl = new CashgameActionUrl(request.Slug, request.DateString, request.PlayerId);
            var canEditAmount = checkpoint.Type == CheckpointType.Buyin;

            return new EditCheckpointFormResult(stack, amount, timestamp, deleteUrl, cancelUrl, canEditAmount);
        }
    }
}