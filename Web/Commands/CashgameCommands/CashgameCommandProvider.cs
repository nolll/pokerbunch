using Core.Repositories;
using Web.Models.CashgameModels.Checkpoints;

namespace Web.Commands.CashgameCommands
{
    public class CashgameCommandProvider : ICashgameCommandProvider
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICheckpointRepository _checkpointRepository;

        public CashgameCommandProvider(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            ICheckpointRepository checkpointRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _checkpointRepository = checkpointRepository;
        }

        public Command GetDeleteCheckpointCommand(string slug, string dateStr, int checkpointId)
        {
            var bunch = _bunchRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(bunch, dateStr);
            return new DeleteCheckpointCommand(_checkpointRepository, cashgame, checkpointId);
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