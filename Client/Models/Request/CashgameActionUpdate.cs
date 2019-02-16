using System;

namespace PokerBunch.Client.Models.Request
{
    public class CashgameActionUpdate
    {
        public string CashgameId { get; }
        public string ActionId { get; }
        public DateTime Timestamp { get; }
        public int Stack { get; }
        public int Added { get; }

        public CashgameActionUpdate(string cashgameId, string actionId, DateTime timestamp, int stack, int added)
        {
            CashgameId = cashgameId;
            ActionId = actionId;
            Timestamp = timestamp;
            Stack = stack;
            Added = added;
        }
    }
}