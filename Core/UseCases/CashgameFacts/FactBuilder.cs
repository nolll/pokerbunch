using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Core.UseCases.CashgameFacts
{
    public class FactBuilder
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

        public FactBuilder(IList<Cashgame> cashgames, IList<Player> players)
        {
            var gameData = GetGameData(cashgames);
            var totalResults = GetTotalResults(players, cashgames);

            GameCount = gameData.GameCount;
            BestResult = gameData.BestResult;
            WorstResult = gameData.WorstResult;
            TotalGameTime = gameData.TotalGameTime;
            TotalTurnover = gameData.TotalTurnover;
            MostTimeResult = GetMostTimeResult(totalResults);
            BiggestBuyinTotalResult = GetBiggestTotalBuyinResult(totalResults);
            BiggestCashoutTotalResult = GetBiggestTotalCashoutResult(totalResults);
            BestTotalResult = totalResults.FirstOrDefault();
            WorstTotalResult = totalResults.LastOrDefault();
        }

        private GameData GetGameData(IList<Cashgame> cashgames)
        {
            CashgameResult bestResult = null;
            CashgameResult worstResult = null;
            var totalGameTime = 0;
            var totalTurnover = 0;

            foreach (var cashgame in cashgames)
            {
                var results = cashgame.Results;
                foreach (var result in results)
                {
                    if (bestResult == null || result.Winnings > bestResult.Winnings)
                    {
                        bestResult = result;
                    }
                    if (worstResult == null || result.Winnings < worstResult.Winnings)
                    {
                        worstResult = result;
                    }
                }
                totalGameTime += cashgame.Duration;
                totalTurnover += cashgame.Turnover;
            }

            return new GameData
                {
                    GameCount = cashgames.Count(),
                    BestResult = bestResult,
                    WorstResult = worstResult,
                    TotalGameTime = totalGameTime,
                    TotalTurnover = totalTurnover
                };
        }

        private class GameData
        {
            public int GameCount { get; set; }
            public CashgameResult BestResult { get; set; }
            public CashgameResult WorstResult { get; set; }
            public int TotalGameTime { get; set; }
            public int TotalTurnover { get; set; }
        }

        private IList<CashgameTotalResult> GetTotalResults(IEnumerable<Player> players, IEnumerable<Cashgame> cashgames)
        {
            var list = players.Select(player => new CashgameTotalResult(player, cashgames)).ToList();
            return list.Where(o => o.GameCount > 0).OrderByDescending(o => o.Winnings).ToList();
        }

        private CashgameTotalResult GetMostTimeResult(IEnumerable<CashgameTotalResult> results)
        {
            CashgameTotalResult mostTimeResult = null;
            foreach (var result in results)
            {
                if (mostTimeResult == null || result.TimePlayed > mostTimeResult.TimePlayed)
                {
                    mostTimeResult = result;
                }
            }
            return mostTimeResult;
        }

        private CashgameTotalResult GetBiggestTotalBuyinResult(IEnumerable<CashgameTotalResult> results)
        {
            CashgameTotalResult biggestTotalBuyinResult = null;
            foreach (var result in results)
            {
                if (biggestTotalBuyinResult == null || result.Buyin > biggestTotalBuyinResult.Buyin)
                {
                    biggestTotalBuyinResult = result;
                }
            }
            return biggestTotalBuyinResult;
        }

        private CashgameTotalResult GetBiggestTotalCashoutResult(IEnumerable<CashgameTotalResult> results)
        {
            CashgameTotalResult biggestTotalCashoutResult = null;
            foreach (var result in results)
            {
                if (biggestTotalCashoutResult == null || result.Cashout > biggestTotalCashoutResult.Cashout)
                {
                    biggestTotalCashoutResult = result;
                }
            }
            return biggestTotalCashoutResult;
        }

        protected FactBuilder(
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