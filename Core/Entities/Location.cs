namespace Core.Entities
{
    public class Location : IEntity
    {
        public string Id { get; }
        public string Name { get; }
        public string Slug { get; }
        public string CacheId => Id;

        public Location(string id, string name, string slug)
        {
            Id = id;
            Name = name;
            Slug = slug;
        }
    }
}