using System;
using Application.Factories;
using Application.Services;
using Core.Entities.Checkpoints;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Checkpoints;
using Web.Models.CashgameModels.Report;

namespace Web.ModelMappers
{
    public class CheckpointModelMapper : ICheckpointModelMapper
    {
        private readonly ITimeProvider _timeProvider;

        public CheckpointModelMapper(
            ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public Checkpoint GetCheckpoint(EditCheckpointPostModel postModel, Checkpoint existingCheckpoint, TimeZoneInfo timeZone)
        {
            return CheckpointFactory.Create(
                TimeZoneInfo.ConvertTimeToUtc(postModel.Timestamp, timeZone),
                existingCheckpoint.Type,
                postModel.Stack,
                postModel.Amount,
                existingCheckpoint.Id);
        }

        public Checkpoint GetCheckpoint(CashoutPostModel postModel, Checkpoint existingCashoutCheckpoint)
        {
            return CheckpointFactory.Create(
                _timeProvider.GetTime(),
                CheckpointType.Cashout,
                postModel.StackAmount.HasValue ? postModel.StackAmount.Value : 0,
                existingCashoutCheckpoint != null ? existingCashoutCheckpoint.Id : 0);
        }

        public Checkpoint GetCheckpoint(ReportPostModel postModel)
        {
            return CheckpointFactory.Create(
                _timeProvider.GetTime(),
                CheckpointType.Report,
                postModel.StackAmount.HasValue ? postModel.StackAmount.Value : 0);
        }
    }
}