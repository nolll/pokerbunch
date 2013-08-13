using System.Collections.Generic;

namespace Core.Classes{
    public class CashgameSuite {

	    public List<Cashgame> Cashgames { get; set; }
	    public List<CashgameTotalResult> TotalResults { get; set; }
	    public int GameCount { get; set; }
	    public CashgameTotalResult BestTotalResult { get; set; }
	    public CashgameResult BestResult { get; set; }
        public CashgameResult WorstResult { get; set; }
        public CashgameTotalResult MostTimeResult { get; set; }
	    public int TotalGameTime { get; set; }

	    public CashgameSuite()
	    {
	        Cashgames = new List<Cashgame>();
            TotalResults = new List<CashgameTotalResult>();
	    }

	}

}