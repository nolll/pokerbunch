using System;
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
        public IList<RunningCashgameCheckpointItem> Checkpoints { get; set; }

        public RunningCashgamePlayerItem(int playerId, string name, bool hasCashedOut, IList<Checkpoint> checkpoints)
        {
            PlayerId = playerId;
            Name = name;
            HasCashedOut = hasCashedOut;
            Checkpoints = checkpoints.Select(o => new RunningCashgameCheckpointItem(o)).ToList();
        }
    }

    public class RunningCashgameCheckpointItem
    {
        public DateTime Time { get; private set; }
        public int Stack { get; private set; }
        public int AddedMoney { get; private set; }

        public RunningCashgameCheckpointItem(Checkpoint checkpoint)
        {
            Time = checkpoint.Timestamp;
            Stack = checkpoint.Stack;
            AddedMoney = checkpoint.Amount;
        }
    }
}