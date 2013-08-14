using System;
using System.Collections.Generic;
using Core.Classes;

namespace Infrastructure.Data.Classes {

	public class RawCashgame{

	    public int Id { get; private set; }
	    public string Location { get; private set; }
	    public GameStatus Status { get; private set; }
	    public string Date { get; private set; }
	    public List<RawCashgameResult> Results { get; private set; }

	    public RawCashgame(int id, string location, GameStatus status, string dateStr)
	    {
	        Id = id;
	        Location = location;
	        Status = status;
	        Date = dateStr;
            Results = new List<RawCashgameResult>();
	    }

		public void AddResult(RawCashgameResult result){
			Results.Add(result);
		}

	}

}