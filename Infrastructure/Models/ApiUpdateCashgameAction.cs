using System;

namespace Infrastructure.Api.Models
{
    internal class ApiUpdateCashgameAction
    {
        public DateTime Timestamp { get; }
        public int Stack { get; }
        public int Added { get; }

        public ApiUpdateCashgameAction(DateTime timestamp, int stack, int added)
        {
            Timestamp = timestamp;
            Stack = stack;
            Added = added;
        }
    }
}