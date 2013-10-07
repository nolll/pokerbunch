using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories
{
    public class CheckpointFactory : ICheckpointFactory
    {
        public Checkpoint Create(RawCheckpoint rawCheckpoint)
        {
            if (rawCheckpoint.Type == (int)CheckpointType.Buyin)
            {
                return CreateBuyin(rawCheckpoint);
            }
            if (rawCheckpoint.Type == (int)CheckpointType.Cashout)
            {
                return CreateCashout(rawCheckpoint);
            }
            return CreateReport(rawCheckpoint);
        }

        private Checkpoint CreateBuyin(RawCheckpoint rawCheckpoint)
        {
            return new Checkpoint
                {
                    Type = CheckpointType.Buyin,
                    Timestamp = rawCheckpoint.Timestamp,
                    Stack = rawCheckpoint.Stack,
                    Amount = rawCheckpoint.Amount,
                    Id = 1
                };
        }

        private Checkpoint CreateCashout(RawCheckpoint rawCheckpoint)
        {
            return new Checkpoint
            {
                Type = CheckpointType.Cashout,
                Timestamp = rawCheckpoint.Timestamp,
                Stack = rawCheckpoint.Stack,
                Amount = 0,
                Id = 1
            };
        }

        private Checkpoint CreateReport(RawCheckpoint rawCheckpoint)
        {
            return new Checkpoint
            {
                Type = CheckpointType.Report,
                Timestamp = rawCheckpoint.Timestamp,
    	        Stack = rawCheckpoint.Stack,
	            Amount = 0,
	            Id = 1
            };
        }
    }
}