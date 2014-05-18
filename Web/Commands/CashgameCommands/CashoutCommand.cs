using Core.Entities;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.CashgameModels.Cashout;

namespace Web.Commands.CashgameCommands
{
    public class CashoutCommand : Command
    {
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly ICheckpointModelMapper _checkpointModelMapper;
        private readonly Cashgame _cashgame;
        private readonly Player _player;
        private readonly CashgameResult _result;
        private readonly CashoutPostModel _model;

        public CashoutCommand(
            ICheckpointRepository checkpointRepository,
            ICheckpointModelMapper checkpointModelMapper,
            Cashgame cashgame,
            Player player,
            CashgameResult result,
            CashoutPostModel model)
        {
            _checkpointRepository = checkpointRepository;
            _checkpointModelMapper = checkpointModelMapper;
            _cashgame = cashgame;
            _player = player;
            _result = result;
            _model = model;
        }

        public override bool Execute()
        {
            if (!IsValid(_model)) return false;
            var postedCheckpoint = _checkpointModelMapper.GetCheckpoint(_model, _result.CashoutCheckpoint);
            if (_result.CashoutCheckpoint != null)
            {
                _checkpointRepository.UpdateCheckpoint(_cashgame, postedCheckpoint);
            }
            else
            {
                _checkpointRepository.AddCheckpoint(_cashgame, _player, postedCheckpoint);
            }
            return true;
        }
    }
}