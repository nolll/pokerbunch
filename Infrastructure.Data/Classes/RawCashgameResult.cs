using System.Collections.Generic;

namespace Infrastructure.Data.Classes {

	public class RawCashgameResult{

	    public int PlayerId { get; private set; }
        public List<RawCheckpoint> Checkpoints { get; private set; }

	    public RawCashgameResult(int playerId)
	    {
	        PlayerId = playerId;
			Checkpoints = new List<RawCheckpoint>();
	    }

		public void AddCheckpoint(RawCheckpoint checkpoint){
			Checkpoints.Add(checkpoint);
		}

	}

}