namespace Core.Entities
{
    public class Event : IEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Event(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}