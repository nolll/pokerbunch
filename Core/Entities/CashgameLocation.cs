namespace Core.Entities
{
    public class CashgameLocation
    {
        public string Id { get; }
        public string Name { get; }

        public CashgameLocation(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}