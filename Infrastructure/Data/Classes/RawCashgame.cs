using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Classes {

	public class RawCashgame{

	    public int Id { get; set; }
	    public string Location { get; set; }
	    public int Status { get; set; }
	    public DateTime Date { get; set; }
	    public List<RawCashgameResult> Results { get; set; }

		public void AddResult(RawCashgameResult result){
			Results.Add(result);
		}

	}

}