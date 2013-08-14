using System.Collections.Generic;
using Core.Classes.Checkpoints;

namespace Infrastructure.Data.Classes {

	public class RawCashgameResult{

	    public int PlayerId { get; private set; }
        public List<Checkpoint> Checkpoints { get; private set; }

	    public RawCashgameResult(int playerId)
	    {
	        PlayerId = playerId;
			Checkpoints = new List<Checkpoint>();
	    }

		public void AddCheckpoint(Checkpoint checkpoint){
			Checkpoints.Add(checkpoint);
		}

	}

}