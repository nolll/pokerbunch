namespace Core.Entities
{
    public class Event : IEntity
    {
        public int Id { get; private set; }
        public int BunchId { get; private set; }
        public string Name { get; private set; }
        public int LocationId { get; private set; }
        public Date StartDate { get; private set; }
        public Date EndDate { get; private set; }

        public Event(int id, int bunchId, string name)
        {
            Id = id;
            BunchId = bunchId;
            Name = name;
        }

        public Event(int id, int bunchId, string name, int locationId, Date startDate, Date endDate)
            : this(id, bunchId, name)
        {
            LocationId = locationId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public bool HasGames
        {
            get { return HasLocation && HasStartDate && HasEndDate; }
        }

        private bool HasLocation
        {
            get { return LocationId != 0; }
        }

        private bool HasStartDate
        {
            get { return !StartDate.IsNull; }
        }

        private bool HasEndDate
        {
            get { return !EndDate.IsNull; }
        }
    }
}