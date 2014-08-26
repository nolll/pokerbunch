using Core.Repositories;
using Web.ModelMappers;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Checkpoints;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.Report;

namespace Web.Commands.CashgameCommands
{
    public class CashgameCommandProvider : ICashgameCommandProvider
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICheckpointModelMapper _checkpointModelMapper;
        private readonly ICheckpointRepository _checkpointRepository;

        public CashgameCommandProvider(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            ICheckpointModelMapper checkpointModelMapper,
            ICheckpointRepository checkpointRepository)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _checkpointModelMapper = checkpointModelMapper;
            _checkpointRepository = checkpointRepository;
        }

        public Command GetEndGameCommand(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetRunning(homegame);
            return new EndGameCommand(_cashgameRepository, homegame, cashgame);
        }

        public Command GetEditCommand(string slug, string dateStr, CashgameEditPostModel postModel)
        {
            return new EditCashgameCommand(_homegameRepository, _cashgameRepository, slug, dateStr, postModel);
        }

        public Command GetReportCommand(string slug, int playerId, ReportPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetRunning(homegame);
            var player = _playerRepository.GetById(playerId);
            return new ReportCommand(_checkpointModelMapper, _checkpointRepository, cashgame, player, postModel);
        }

        public Command GetDeleteCheckpointCommand(string slug, string dateStr, int checkpointId)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            return new DeleteCheckpointCommand(_checkpointRepository, cashgame, checkpointId);
        }

        public Command GetCashoutCommand(string slug, int playerId, CashoutPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var player = _playerRepository.GetById(playerId);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            var result = runningGame.GetResult(player.Id);
            return new CashoutCommand(_checkpointRepository, _checkpointModelMapper, runningGame, player, result, postModel);
        }

        public Command GetDeleteCommand(string slug, string dateStr)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            return new DeleteCommand(_cashgameRepository, cashgame);
        }

        public Command GetEditCheckpointCommand(string slug, string dateStr, int checkpointId, EditCheckpointPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            var existingCheckpoint = _checkpointRepository.GetCheckpoint(checkpointId);
            return new EditCheckpointCommand(_checkpointRepository, _checkpointModelMapper, cashgame, postModel, existingCheckpoint, homegame.Timezone);
        }
    }
}