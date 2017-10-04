namespace Infrastructure.Api.Models.Request
{
    public class ApiUpdateCashgame
    {
        public string CashgameId { get; }
        public string LocationId { get; }
        public string EventId { get; }

        public ApiUpdateCashgame(string cashgameId, string locationId, string eventId)
        {
            CashgameId = cashgameId;
            LocationId = locationId;
            EventId = eventId;
        }
    }
}