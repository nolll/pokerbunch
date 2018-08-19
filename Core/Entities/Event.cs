namespace Core.Entities
{
    public class Event
    {
        public string Id { get; }
        public string BunchId { get; }
        public string Name { get; }
        public SmallLocation Location { get; }
        public Date StartDate { get; }

        public Event(string id, string bunchId, string name)
        {
            Id = id;
            BunchId = bunchId;
            Name = name;
        }

        public Event(string id, string bunchId, string name, SmallLocation location, Date startDate)
            : this(id, bunchId, name)
        {
            Location = location;
            StartDate = startDate;
        }

        public bool HasGames => HasLocation && HasStartDate;
        private bool HasLocation => Location != null;
        private bool HasStartDate => !StartDate.IsNull;
    }
}