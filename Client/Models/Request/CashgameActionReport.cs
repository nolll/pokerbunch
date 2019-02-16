namespace PokerBunch.Client.Models.Request
{
    public class CashgameActionReport : CashgameActionAdd
    {
        public CashgameActionReport(string cashgameId, string playerId, int stack)
            : base("report", cashgameId, playerId, 0, stack)
        {
        }
    }
}