namespace Application.UseCases.CashgameFacts
{
    public class CashgameFactsResult
    {
        public int GameCount { get; set; }
        public int MinutesPlayed { get; set; }
        public int Turnover { get; set; }
        public AmountFact BestResult { get; set; }
        public AmountFact WorstResult { get; set; }
        public AmountFact BestTotalResult { get; set; }
        public AmountFact WorstTotalResult { get; set; }
        public DurationFact MostTimeResult { get; set; }
        public AmountFact BiggestBuyinTotalResult { get; set; }
        public AmountFact BiggestCashoutTotalResult { get; set; }
    }
}