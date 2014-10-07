using Core.Repositories;
using Core.Services.Interfaces;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Checkpoints;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.Report;

namespace Web.Commands.CashgameCommands
{
    public class CashgameCommandProvider : ICashgameCommandProvider
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly ITimeProvider _timeProvider;

        public CashgameCommandProvider(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            ICheckpointRepository checkpointRepository,
            ITimeProvider timeProvider)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _checkpointRepository = checkpointRepository;
            _timeProvider = timeProvider;
        }

        public Command GetEndGameCommand(string slug)
        {
            var bunch = _bunchRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetRunning(bunch);
            return new EndGameCommand(_cashgameRepository, bunch, cashgame);
        }

        public Command GetEditCommand(string slug, string dateStr, CashgameEditPostModel postModel)
        {
            return new EditCashgameCommand(_bunchRepository, _cashgameRepository, slug, dateStr, postModel);
        }

        public Command GetReportCommand(string slug, int playerId, ReportPostModel postModel)
        {
            var bunch = _bunchRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetRunning(bunch);
            var player = _playerRepository.GetById(playerId);
            return new ReportCommand(_checkpointRepository, _timeProvider, cashgame, player, postModel);
        }

        public Command GetDeleteCheckpointCommand(string slug, string dateStr, int checkpointId)
        {
            var bunch = _bunchRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(bunch, dateStr);
            return new DeleteCheckpointCommand(_checkpointRepository, cashgame, checkpointId);
        }

        public Command GetCashoutCommand(string slug, int playerId, CashoutPostModel postModel)
        {
            var bunch = _bunchRepository.GetBySlug(slug);
            var player = _playerRepository.GetById(playerId);
            var runningGame = _cashgameRepository.GetRunning(bunch);
            var result = runningGame.GetResult(player.Id);
            return new CashoutCommand(_checkpointRepository, _timeProvider, runningGame, player, result, postModel);
        }

        public Command GetDeleteCommand(string slug, string dateStr)
        {
            var bunch = _bunchRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(bunch, dateStr);
            return new DeleteCommand(_cashgameRepository, cashgame);
        }

        public Command GetEditCheckpointCommand(string slug, string dateStr, int checkpointId, EditCheckpointPostModel postModel)
        {
            var bunch = _bunchRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(bunch, dateStr);
            var existingCheckpoint = _checkpointRepository.GetCheckpoint(checkpointId);
            return new EditCheckpointCommand(_checkpointRepository, cashgame, postModel, existingCheckpoint, bunch.Timezone);
        }
    }
}