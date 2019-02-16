namespace PokerBunch.Client.Models.Request
{
    public class EventAdd
    {
        public string Name { get; }

        public EventAdd(string name)
        {
            Name = name;
        }
    }
}