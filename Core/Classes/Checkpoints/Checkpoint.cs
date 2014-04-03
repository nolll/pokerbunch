using System;

namespace Core.Classes.Checkpoints
{
	public abstract class Checkpoint
    {
	    public int Amount { get; private set; }
        public int Stack { get; private set; }
        public DateTime Timestamp { get; private set; }
        public CheckpointType Type { get; private set; }
        public int Id { get; private set; }

	    protected Checkpoint(
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
	    }

        public abstract string Description { get; }
	}
}