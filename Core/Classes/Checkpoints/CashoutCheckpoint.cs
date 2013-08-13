using System;

namespace Core.Classes.Checkpoints
{

    public class CashoutCheckpoint : Checkpoint
    {

        public CashoutCheckpoint(DateTime timestamp, int stack)
            : base(timestamp, stack)
        {
        }

        public override CheckpointType Type
        {
            get { return CheckpointType.Cashout; }
        }

        public override string Description
        {
            get { return "Cashout"; }
        }
    }

}