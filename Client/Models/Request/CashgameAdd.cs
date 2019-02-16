namespace PokerBunch.Client.Models.Request
{
    public class CashgameAdd
    {
        public string BunchId { get; }
        public string LocationId { get; }

        public CashgameAdd(string bunchId, string locationId)
        {
            BunchId = bunchId;
            LocationId = locationId;
        }
    }
}