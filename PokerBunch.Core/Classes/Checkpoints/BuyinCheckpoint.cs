using System;

namespace Core.Classes.Checkpoints
{

    public class BuyinCheckpoint : Checkpoint
    {

        public BuyinCheckpoint(DateTime timestamp, int stack, int amount)
            : base(timestamp, stack)
        {
            Amount = amount;
        }

        public override CheckpointType Type
        {
            get { return CheckpointType.Buyin; }
        }

        public override string Description
        {
            get { return "Buyin"; }
        }
    }

}