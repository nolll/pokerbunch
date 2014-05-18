namespace Core.Entities
{
    public class CashgameFacts
    {
        public int GameCount { get; private set; }
        public CashgameResult BestResult { get; private set; }
        public CashgameResult WorstResult { get; private set; }
        public CashgameTotalResult BestTotalResult { get; private set; }
        public CashgameTotalResult WorstTotalResult { get; private set; }
        public CashgameTotalResult MostTimeResult { get; private set; }
        public CashgameTotalResult BiggestBuyinTotalResult { get; private set; }
        public CashgameTotalResult BiggestCashoutTotalResult { get; private set; }
        public int TotalGameTime { get; private set; }
        public int TotalTurnover { get; private set; }

        public CashgameFacts(
            int gameCount,
            CashgameResult bestResult,
            CashgameResult worstResult,
            CashgameTotalResult bestTotalResult,
            CashgameTotalResult worstTotalResult,
            CashgameTotalResult mostTimeResult,
            CashgameTotalResult biggestBuyinTotalResult,
            CashgameTotalResult biggestCashoutTotalResult,
            int totalGameTime,
            int totalTurnover)
        {
            GameCount = gameCount;
            BestResult = bestResult;
            WorstResult = worstResult;
            BestTotalResult = bestTotalResult;
            WorstTotalResult = worstTotalResult;
            MostTimeResult = mostTimeResult;
            BiggestBuyinTotalResult = biggestBuyinTotalResult;
            BiggestCashoutTotalResult = biggestCashoutTotalResult;
            TotalGameTime = totalGameTime;
            TotalTurnover = totalTurnover;
        }
	}
}