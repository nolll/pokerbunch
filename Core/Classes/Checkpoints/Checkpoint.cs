using System;

namespace Core.Classes.Checkpoints
{
	public class Checkpoint
    {
	    public int Amount { get; private set; }
        public int Stack { get; private set; }
        public DateTime Timestamp { get; private set; }
        public CheckpointType Type { get; private set; }
        public int Id { get; private set; }
        
	    public Checkpoint(
            DateTime timestamp, 
            CheckpointType type,
            int stack,
            int amount = default(int),
            int id = default(int))
	    {
	        Timestamp = timestamp;
	        Type = type;
            Stack = stack;
            Amount = amount;
            Id = id;
	    }
	}
}