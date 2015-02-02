using System.Collections.Generic;
using System.Linq;
using Core.Entities.Checkpoints;

namespace Core.UseCases.RunningCashgame
{
    public class RunningCashgamePlayerItem
    {
        public int PlayerId { get; private set; }
        public string Name { get; private set; }
        public bool HasCashedOut { get; private set; }
        public IList<RunningCashgameCheckpointItem> Checkpoints { get; private set; }

        public RunningCashgamePlayerItem(int playerId, string name, bool hasCashedOut, IEnumerable<Checkpoint> checkpoints)
        {
            PlayerId = playerId;
            Name = name;
            HasCashedOut = hasCashedOut;
            Checkpoints = checkpoints.Select(o => new RunningCashgameCheckpointItem(o)).ToList();
        }
    }
}