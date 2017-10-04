using System;

namespace Infrastructure.Api.Models
{
    public class ApiUpdateCashgameAction
    {
        public string CashgameId { get; }
        public string ActionId { get; }
        public DateTime Timestamp { get; }
        public int Stack { get; }
        public int Added { get; }

        public ApiUpdateCashgameAction(string cashgameId, string actionId, DateTime timestamp, int stack, int added)
        {
            CashgameId = cashgameId;
            ActionId = actionId;
            Timestamp = timestamp;
            Stack = stack;
            Added = added;
        }
    }
}