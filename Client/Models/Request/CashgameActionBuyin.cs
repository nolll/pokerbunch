namespace PokerBunch.Client.Models.Request
{
    public class CashgameActionBuyin : CashgameActionAdd
    {
        public CashgameActionBuyin(string cashgameId, string playerId, int added, int stack)
            : base("buyin", cashgameId, playerId, added, stack)
        {
        }
    }
}