using System;
using System.Collections.Generic;
using Core.Classes.Checkpoints;

namespace Core.Classes{

	public class CashgameResult{

	    public Player Player { get; set; }
	    public int Buyin { get; set; }
	    public int Winnings { get; set; }
	    public List<Checkpoint> Checkpoints { get; set; }
	    public DateTime? BuyinTime { get; set; }
	    public DateTime? CashoutTime { get; set; }
	    public int PlayedTime { get; set; }
	    public int Stack { get; set; }
	    public DateTime? LastReportTime { get; set; }
	    public Checkpoint CashoutCheckpoint { get; set; }
	    public bool HasReported { get; set; }

	    public CashgameResult()
	    {
	        Checkpoints = new List<Checkpoint>();
	    }

	}

}