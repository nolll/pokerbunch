namespace Core.Entities
{
    public class SmallLocation
    {
        public string Id { get; }
        public string Name { get; }

        public SmallLocation(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}