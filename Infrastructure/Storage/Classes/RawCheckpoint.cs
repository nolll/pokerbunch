using System;
using Core.Entities.Checkpoints;

namespace Infrastructure.Storage.Classes
{
	public class RawCheckpoint
    {
        public string CashgameId { get; }
        public string PlayerId { get; }
	    public int Amount { get; }
	    public int Stack { get; }
	    public DateTime Timestamp { get; }
	    public string Id { get; }
        public int Type { get; }

	    public RawCheckpoint(string cashgameId, string playerId, int amount, int stack, DateTime timestamp, string id, int type)
	    {
	        CashgameId = cashgameId;
	        PlayerId = playerId;
	        Amount = amount;
	        Stack = stack;
	        Timestamp = timestamp;
	        Id = id;
	        Type = type;
	    }

        public static RawCheckpoint Create(Checkpoint checkpoint)
        {
            return new RawCheckpoint(
                checkpoint.CashgameId,
                checkpoint.PlayerId,
                checkpoint.Amount,
                checkpoint.Stack,
                checkpoint.Timestamp,
                checkpoint.Id,
                (int)checkpoint.Type);
        }

        public static Checkpoint CreateReal(RawCheckpoint rawCheckpoint)
        {
            return Checkpoint.Create(
                rawCheckpoint.CashgameId,
                rawCheckpoint.PlayerId,
                rawCheckpoint.Timestamp,
                (CheckpointType)rawCheckpoint.Type,
                rawCheckpoint.Stack,
                rawCheckpoint.Amount,
                rawCheckpoint.Id);
        }
    }
}