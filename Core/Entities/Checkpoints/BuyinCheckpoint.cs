using System;

namespace Core.Entities.Checkpoints
{
    public class BuyinCheckpoint : Checkpoint
    {
        public BuyinCheckpoint(DateTime timestamp, int stack, int amount, int id = 0) : base(timestamp, CheckpointType.Buyin, stack, amount, id)
        {
        }

        public override string Description
        {
            get { return "Buyin"; }
        }
    }
}