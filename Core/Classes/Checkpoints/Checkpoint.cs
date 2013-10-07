using System;

namespace Core.Classes.Checkpoints{

	public class Checkpoint{

	    public int Amount { get; set; }
	    public int Stack { get; set; }
	    public DateTime Timestamp { get; set; }
	    public int Id { get; set; }
        public CheckpointType Type { get; set; }

    }

}