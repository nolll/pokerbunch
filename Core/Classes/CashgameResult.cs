using System;
using System.Collections.Generic;
using Core.Classes.Checkpoints;

namespace Core.Classes{
    public class CashgameResult{

        public int PlayerId { get; private set; }
        public int Buyin { get; private set; }
        public int Winnings { get; private set; }
        public IList<Checkpoint> Checkpoints { get; private set; }
        public DateTime? BuyinTime { get; private set; }
        public DateTime? CashoutTime { get; private set; }
        public int PlayedTime { get; private set; }
        public int Stack { get; private set; }
        public DateTime? LastReportTime { get; private set; }
        public Checkpoint CashoutCheckpoint { get; private set; }
        public bool HasReported { get; private set; }

	    public CashgameResult(
            int playerId,
            int buyin, 
            int winnings, 
            IList<Checkpoint> checkpoints, 
            DateTime? buyinTime, 
            DateTime? cashoutTime, 
            int playedTime, 
            int stack, 
            DateTime? lastReportTime, 
            Checkpoint cashoutCheckpoint, 
            bool hasReported)
	    {
	        PlayerId = playerId;
	        Buyin = buyin;
	        Winnings = winnings;
	        Checkpoints = checkpoints;
	        BuyinTime = buyinTime;
	        CashoutTime = cashoutTime;
	        PlayedTime = playedTime;
	        Stack = stack;
	        LastReportTime = lastReportTime;
	        CashoutCheckpoint = cashoutCheckpoint;
	        HasReported = hasReported;
	    }
	}

}