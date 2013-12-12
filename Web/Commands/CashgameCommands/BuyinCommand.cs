using Core.Classes;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.CashgameModels.Buyin;

namespace Web.Commands.CashgameCommands
{
    public class BuyinCommand : Command
    {
        private readonly ICheckpointModelMapper _checkpointModelMapper;
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly Player _player;
        private readonly Cashgame _cashgame;
        private readonly BuyinPostModel _model;

        public BuyinCommand(
            ICheckpointModelMapper checkpointModelMapper,
            ICheckpointRepository checkpointRepository,
            ICashgameRepository cashgameRepository,
            Player player,
            Cashgame cashgame,
            BuyinPostModel model)
        {
            _checkpointModelMapper = checkpointModelMapper;
            _checkpointRepository = checkpointRepository;
            _cashgameRepository = cashgameRepository;
            _player = player;
            _cashgame = cashgame;
            _model = model;
        }

        public override bool Execute()
        {
            if (!IsValid(_model)) return false;
            var checkpoint = _checkpointModelMapper.GetCheckpoint(_model);
            _checkpointRepository.AddCheckpoint(_cashgame, _player, checkpoint);
            if (!_cashgame.IsStarted)
            {
                _cashgameRepository.StartGame(_cashgame);
            }
            return true;
        }

    }
}