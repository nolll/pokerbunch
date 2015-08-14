using System;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases
{
    public class EditCheckpointForm
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICheckpointRepository _checkpointRepository;

        public EditCheckpointForm(IBunchRepository bunchRepository, ICheckpointRepository checkpointRepository)
        {
            _bunchRepository = bunchRepository;
            _checkpointRepository = checkpointRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var checkpoint = _checkpointRepository.GetCheckpoint(request.CheckpointId);
            var stack = checkpoint.Stack;
            var amount = checkpoint.Amount;
            var timestamp = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, bunch.Timezone);
            var deleteUrl = new DeleteCheckpointUrl(request.Slug, request.DateString, request.PlayerId, request.CheckpointId);
            var cancelUrl = new CashgameActionUrl(request.Slug, request.DateString, request.PlayerId);
            var canEditAmount = checkpoint.Type == CheckpointType.Buyin;

            return new Result(stack, amount, timestamp, deleteUrl, cancelUrl, canEditAmount);
        }

        public class Request
        {
            public string Slug { get; private set; }
            public int CheckpointId { get; private set; }
            public string DateString { get; private set; }
            public int PlayerId { get; private set; }

            public Request(string slug, string dateString, int playerId, int checkpointId)
            {
                Slug = slug;
                CheckpointId = checkpointId;
                DateString = dateString;
                PlayerId = playerId;
            }
        }

        public class Result
        {
            public int Stack { get; private set; }
            public int Amount { get; private set; }
            public DateTime TimeStamp { get; private set; }
            public Url DeleteUrl { get; private set; }
            public Url CancelUrl { get; private set; }
            public bool CanEditAmount { get; private set; }

            public Result(int stack, int amount, DateTime timeStamp, Url deleteUrl, Url cancelUrl, bool canEditAmount)
            {
                TimeStamp = timeStamp;
                Stack = stack;
                Amount = amount;
                DeleteUrl = deleteUrl;
                CancelUrl = cancelUrl;
                CanEditAmount = canEditAmount;
            }
        }
    }
}