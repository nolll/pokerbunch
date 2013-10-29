using System.Collections.Generic;

namespace Infrastructure.Data.Classes {

	public class RawCashgameWithResults : RawCashgame{

	    public List<RawCashgameResult> Results { get; set; }

		public void AddResult(RawCashgameResult result){
			Results.Add(result);
		}

	}

}