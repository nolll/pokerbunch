using System.Collections.Generic;
using Core.Entities;

namespace Application.UseCases.PlayerFacts
{
    public class PlayerFactsResult
    {
        public MoneyResult Winnings { get; private set; }
        public MoneyResult BestResult { get; private set; }
        public MoneyResult WorstResult { get; private set; }
        public int GamesPlayed { get; private set; }
        public Time TimePlayed { get; private set; }
        public int BestResultCount { get; private set; }
        public int WinningStreak { get; private set; }
        public int LosingStreak { get; private set; }

        public PlayerFactsResult(IEnumerable<Cashgame> cashgames, int playerId, Currency currency)
        {
            var evaluator = new PlayerFactsEvaluator(cashgames, playerId);

            Winnings = new MoneyResult(evaluator.Winnings, currency);
            BestResult = new MoneyResult(evaluator.BestResult, currency);
            WorstResult = new MoneyResult(evaluator.WorstResult, currency);
            GamesPlayed = evaluator.GameCount;
            TimePlayed = Time.FromMinutes(evaluator.MinutesPlayed);
            BestResultCount = evaluator.BestResultCount;
            WinningStreak = evaluator.WinningStreak;
            LosingStreak = evaluator.LosingStreak;
        }
    }
}