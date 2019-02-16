namespace PokerBunch.Client.Models.Response
{
    public class Event
    {
        public string Id { get; set; }
        public string BunchId { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public EventLocation Location { get; set; }
    }
}