using System;

namespace Core.Entities.Checkpoints
{
    public class ReportCheckpoint : Checkpoint
    {
        public ReportCheckpoint(DateTime timestamp, int stack, int amount, int id) : base(timestamp, CheckpointType.Report, stack, amount, id)
        {
        }

        public override string Description
        {
            get { return "Report"; }
        }
    }
}