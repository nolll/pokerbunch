using Core.Classes;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.CashgameModels.Report;

namespace Web.Commands.CashgameCommands
{
    public class ReportCommand : Command
    {
        private readonly ICheckpointModelMapper _checkpointModelMapper;
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly Cashgame _cashgame;
        private readonly Player _player;
        private readonly ReportPostModel _model;

        public ReportCommand(
            ICheckpointModelMapper checkpointModelMapper,
            ICheckpointRepository checkpointRepository,
            Cashgame cashgame,
            Player player,
            ReportPostModel model)
        {
            _checkpointModelMapper = checkpointModelMapper;
            _checkpointRepository = checkpointRepository;
            _cashgame = cashgame;
            _player = player;
            _model = model;
        }

        public override bool Execute()
        {
            if (!IsValid(_model)) return false;
            var checkpoint = _checkpointModelMapper.GetCheckpoint(_model);
            _checkpointRepository.AddCheckpoint(_cashgame, _player, checkpoint);
            return true;
        }
    }
}