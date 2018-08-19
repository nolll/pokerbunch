namespace Core.Entities
{
    public class CashgameEvent
    {
        public string Id { get; }
        public string Name { get; }

        public CashgameEvent(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}