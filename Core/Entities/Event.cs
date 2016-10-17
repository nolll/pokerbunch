namespace Core.Entities
{
    public class Event : IEntity
    {
        public string Id { get; }
        public string Bunch { get; private set; }
        public string BunchId { get; private set; }
        public string Name { get; private set; }
        public string LocationId { get; }
        public Date StartDate { get; }
        public Date EndDate { get; }
        public string CacheId => Id;

        public Event(string id, string bunch, string bunchId, string name)
        {
            Id = id;
            Bunch = bunch;
            BunchId = bunchId;
            Name = name;
        }

        public Event(string id, string bunch, string bunchId, string name, string locationId, Date startDate, Date endDate)
            : this(id, bunch, bunchId, name)
        {
            LocationId = locationId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public bool HasGames => HasLocation && HasStartDate && HasEndDate;
        private bool HasLocation => !string.IsNullOrEmpty(LocationId);
        private bool HasStartDate => !StartDate.IsNull;
        private bool HasEndDate => !EndDate.IsNull;
    }
}