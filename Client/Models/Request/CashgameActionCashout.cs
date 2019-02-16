namespace PokerBunch.Client.Models.Request
{
    public class CashgameActionCashout : CashgameActionAdd
    {
        public CashgameActionCashout(string cashgameId, string playerId, int stack)
            : base("cashout", cashgameId, playerId, 0, stack)
        {
        }
    }
}