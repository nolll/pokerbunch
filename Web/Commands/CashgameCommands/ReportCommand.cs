using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Factories;
using Core.Repositories;
using Core.Services.Interfaces;
using Web.Models.CashgameModels.Report;

namespace Web.Commands.CashgameCommands
{
    public class ReportCommand : Command
    {
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly ITimeProvider _timeProvider;
        private readonly Cashgame _cashgame;
        private readonly Player _player;
        private readonly ReportPostModel _model;

        public ReportCommand(
            ICheckpointRepository checkpointRepository,
            ITimeProvider timeProvider,
            Cashgame cashgame,
            Player player,
            ReportPostModel model)
        {
            _checkpointRepository = checkpointRepository;
            _timeProvider = timeProvider;
            _cashgame = cashgame;
            _player = player;
            _model = model;
        }

        public override bool Execute()
        {
            if (!IsValid(_model)) return false;
            var checkpoint = CreateCheckpoint(_model);
            _checkpointRepository.AddCheckpoint(_cashgame, _player, checkpoint);
            return true;
        }

        private Checkpoint CreateCheckpoint(ReportPostModel postModel)
        {
            return CheckpointFactory.Create(
                _timeProvider.GetTime(),
                CheckpointType.Report,
                postModel.StackAmount.HasValue ? postModel.StackAmount.Value : 0);
        }
    }
}