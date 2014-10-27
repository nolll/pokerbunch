using System;

namespace Core.Entities.Checkpoints
{
	public abstract class Checkpoint
    {
        public int CashgameId { get; private set; }
        public int PlayerId { get; private set; }
        public int Amount { get; private set; }
        public int Stack { get; private set; }
        public DateTime Timestamp { get; private set; }
        public CheckpointType Type { get; private set; }
        public int Id { get; private set; }
        
	    protected Checkpoint(
            int cashgameId,
            int playerId,
            DateTime timestamp, 
            CheckpointType type,
            int stack,
            int amount,
            int id)
	    {
	        Timestamp = timestamp;
	        Type = type;
            Stack = stack;
            Amount = amount;
            Id = id;
	        PlayerId = playerId;
	        CashgameId = cashgameId;
	    }

        public abstract string Description { get; }
	}
}