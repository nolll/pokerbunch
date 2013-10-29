using System;
using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.System;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Report;

namespace Web.ModelMappers
{
    public class CheckpointModelMapper : ICheckpointModelMapper
    {
        private readonly ITimeProvider _timeProvider;

        public CheckpointModelMapper(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public Checkpoint GetCheckpoint(CashoutPostModel postModel, Checkpoint existingCashoutCheckpoint, TimeZoneInfo timeZone)
        {
            return new Checkpoint
            {
                Stack = postModel.StackAmount,
                Timestamp = _timeProvider.GetTime(timeZone),
                Type = CheckpointType.Cashout,
                Id = existingCashoutCheckpoint != null ? existingCashoutCheckpoint.Id : 0
            };
        }

        public Checkpoint GetCheckpoint(ReportPostModel postModel, TimeZoneInfo timeZone)
        {
            return new Checkpoint
            {
                Stack = postModel.StackAmount,
                Timestamp = _timeProvider.GetTime(timeZone),
                Type = CheckpointType.Report
            };
        }

        public Checkpoint GetCheckpoint(BuyinPostModel postModel, TimeZoneInfo timeZone)
        {
            return new Checkpoint
            {
                Amount = postModel.BuyinAmount,
                Stack = postModel.StackAmount + postModel.BuyinAmount,
                Timestamp = _timeProvider.GetTime(timeZone),
                Type = CheckpointType.Buyin
            };
        }

    }
}