using System;

namespace Core.Classes.Checkpoints
{
    public class CashoutCheckpoint : Checkpoint
    {
        public CashoutCheckpoint(DateTime timestamp, int stack, int amount, int id) : base(timestamp, CheckpointType.Cashout, stack, amount, id)
        {
        }

        public override string Description
        {
            get { return "Cashout"; }
        }
    }
}