using Application.Services;
using Core.Classes.Checkpoints;
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

        public Checkpoint GetCheckpoint(CashoutPostModel postModel, Checkpoint existingCashoutCheckpoint)
        {
            return new Checkpoint
                (
                _timeProvider.GetTime(),
                CheckpointType.Cashout,
                postModel.StackAmount.HasValue ? postModel.StackAmount.Value : 0,
                id: existingCashoutCheckpoint != null ? existingCashoutCheckpoint.Id : 0
            );
        }

        public Checkpoint GetCheckpoint(ReportPostModel postModel)
        {
            return new Checkpoint
            (
                _timeProvider.GetTime(),
                CheckpointType.Report,
                postModel.StackAmount.HasValue ? postModel.StackAmount.Value : 0
            );
        }

        public Checkpoint GetCheckpoint(BuyinPostModel postModel)
        {
            return new Checkpoint
            (
                _timeProvider.GetTime(),
                CheckpointType.Buyin,
                postModel.StackAmount + postModel.BuyinAmount,
                postModel.BuyinAmount
            );
        }

    }
}