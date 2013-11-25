using System.Collections.Generic;

namespace Core.Classes{
    public class CashgameSuite {

	    public IList<Cashgame> Cashgames { get; private set; }
        public IList<CashgameTotalResult> TotalResults { get; private set; }
        public int GameCount { get; private set; }
        public CashgameTotalResult BestTotalResult { get; private set; }
        public CashgameResult BestResult { get; private set; }
        public CashgameResult WorstResult { get; private set; }
        public CashgameTotalResult MostTimeResult { get; private set; }
        public int TotalGameTime { get; private set; }
        public int TotalTurnover { get; set; }

        public CashgameSuite(
            IList<Cashgame> cashgames, 
            IList<CashgameTotalResult> totalResults, 
            int gameCount, 
            CashgameTotalResult bestTotalResult, 
            CashgameResult bestResult, 
            CashgameResult worstResult, 
            CashgameTotalResult mostTimeResult, 
            int totalGameTime,
            int totalTurnover)
        {
            Cashgames = cashgames;
            TotalResults = totalResults;
            GameCount = gameCount;
            BestTotalResult = bestTotalResult;
            BestResult = bestResult;
            WorstResult = worstResult;
            MostTimeResult = mostTimeResult;
            TotalGameTime = totalGameTime;
            TotalTurnover = totalTurnover;
        }

	}
}