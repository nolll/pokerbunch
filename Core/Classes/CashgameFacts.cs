namespace Core.Classes{
    public class CashgameFacts {

        public int GameCount { get; private set; }
        public CashgameTotalResult BestTotalResult { get; private set; }
        public CashgameResult BestResult { get; private set; }
        public CashgameResult WorstResult { get; private set; }
        public CashgameTotalResult MostTimeResult { get; private set; }
        public int TotalGameTime { get; private set; }
        public int TotalTurnover { get; private set; }

        public CashgameFacts(
            int gameCount, 
            CashgameTotalResult bestTotalResult, 
            CashgameResult bestResult, 
            CashgameResult worstResult, 
            CashgameTotalResult mostTimeResult, 
            int totalGameTime,
            int totalTurnover)
        {
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