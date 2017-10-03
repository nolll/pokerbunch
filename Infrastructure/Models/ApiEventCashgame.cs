using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiEventCashgame
    {
        [UsedImplicitly]
        public string EventId { get; set; }

        [UsedImplicitly]
        public string CashgameId { get; set; }

        public ApiEventCashgame(string eventId, string cashgameId)
        {
            EventId = eventId;
            CashgameId = cashgameId;
        }
    }
}