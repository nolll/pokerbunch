namespace PokerBunch.Client.Models.Request
{
    public class CashgameUpdate
    {
        public string CashgameId { get; }
        public string LocationId { get; }
        public string EventId { get; }

        public CashgameUpdate(string cashgameId, string locationId, string eventId)
        {
            CashgameId = cashgameId;
            LocationId = locationId;
            EventId = eventId;
        }
    }
}