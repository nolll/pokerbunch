using Core.Classes;
using Core.Repositories;

namespace Web.Commands.CashgameCommands
{
    public class DeleteCheckpointCommand : Command
    {
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly Cashgame _cashgame;
        private readonly int _checkpointId;

        public DeleteCheckpointCommand(
            ICheckpointRepository checkpointRepository,
            Cashgame cashgame,
            int checkpointId)
        {
            _checkpointRepository = checkpointRepository;
            _cashgame = cashgame;
            _checkpointId = checkpointId;
        }

        public override bool Execute()
        {
            _checkpointRepository.DeleteCheckpoint(_cashgame, _checkpointId);
            return true;
        }
    }
}