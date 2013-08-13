using System;

namespace Core.Classes.Checkpoints{

	public class ReportCheckpoint : Checkpoint{
	    
        public ReportCheckpoint(DateTime timestamp, int stack) : base(timestamp, stack)
	    {
	    }

	    public override CheckpointType Type
	    {
	        get { return CheckpointType.Report; }
	    }

	    public override string Description
	    {
	        get { return "Report"; }
	    }
	}

}