namespace Infrastructure.Api.Models.Request
{
    internal class ApiUpdateCashgame
    {
        public string LocationId { get; }
        public string EventId { get; }

        public ApiUpdateCashgame(string locationId, string eventId)
        {
            LocationId = locationId;
            EventId = eventId;
        }
    }
}