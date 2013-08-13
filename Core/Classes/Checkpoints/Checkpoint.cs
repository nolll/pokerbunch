using System;

namespace Core.Classes.Checkpoints{

	abstract public class Checkpoint{

	    public int Amount { get; protected set; }
	    public int Stack { get; set; }
	    public DateTime Timestamp { get; set; }
	    public int Id { get; set; }
        public abstract string Description { get; }
        public abstract CheckpointType Type { get; }

	    public Checkpoint(DateTime timestamp, int stack)
	    {
	        Timestamp = timestamp;
	        Stack = stack;
	        Amount = 0;
	        Id = 1;
	    }
	}

}