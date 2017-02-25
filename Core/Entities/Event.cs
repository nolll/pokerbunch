namespace Core.Entities
{
    public class Event : IEntity
    {
        public string Id { get; }
        public string BunchId { get; private set; }
        public string Name { get; private set; }
        public SmallLocation Location { get; }
        public Date StartDate { get; }
        public Date EndDate { get; }
        public string CacheId => Id;

        public Event(string id, string bunchId, string name)
        {
            Id = id;
            BunchId = bunchId;
            Name = name;
        }

        public Event(string id, string bunchId, string name, SmallLocation location, Date startDate, Date endDate)
            : this(id, bunchId, name)
        {
            Location = location;
            StartDate = startDate;
            EndDate = endDate;
        }

        public bool HasGames => HasLocation && HasStartDate && HasEndDate;
        private bool HasLocation => Location != null;
        private bool HasStartDate => !StartDate.IsNull;
        private bool HasEndDate => !EndDate.IsNull;
    }
}