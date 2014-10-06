using Core.Entities;

namespace Core.UseCases.CashgameFacts
{
    public class CashgameFactsResult
    {
        public int GameCount { get; set; }
        public Time TotalTimePlayed { get; set; }
        public Money Turnover { get; set; }
        public MoneyFact BestResult { get; set; }
        public MoneyFact WorstResult { get; set; }
        public MoneyFact BestTotalResult { get; set; }
        public MoneyFact WorstTotalResult { get; set; }
        public DurationFact MostTimePlayed { get; set; }
        public MoneyFact BiggestBuyin { get; set; }
        public MoneyFact BiggestCashout { get; set; }
    }
}