using System;
using Core.Entities.Checkpoints;

namespace Infrastructure.Storage
{
	public class RawCheckpoint
    {
        public int CashgameId { get; private set; }
        public int PlayerId { get; private set; }
	    public int Amount { get; private set; }
	    public int Stack { get; private set; }
	    public DateTime Timestamp { get; private set; }
	    public int Id { get; private set; }
        public int Type { get; private set; }

	    public RawCheckpoint(int cashgameId, int playerId, int amount, int stack, DateTime timestamp, int id, int type)
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
                checkpoint.Id,
                checkpoint.Id,
                checkpoint.Amount,
                checkpoint.Stack,
                checkpoint.Timestamp,
                checkpoint.Id,
                (int)checkpoint.Type);
        }
    }
}