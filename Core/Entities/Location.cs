namespace Core.Entities
{
    public class Location : IEntity
    {
        public int Id { get; }
        public string Name { get; }
        public string Slug { get; }

        public Location(int id, string name, string slug)
        {
            Id = id;
            Name = name;
            Slug = slug;
        }
    }
}