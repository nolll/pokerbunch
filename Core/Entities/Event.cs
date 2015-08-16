namespace Core.Entities
{
    public class Event : IEntity
    {
        public int Id { get; private set; }
        public int BunchId { get; private set; }
        public string Name { get; private set; }
        public string Location { get; private set; }
        public Date StartDate { get; private set; }
        public Date EndDate { get; private set; }

        public Event(int id, int bunchId, string name, string location, Date startDate, Date endDate)
        {
            Id = id;
            BunchId = bunchId;
            Name = name;
            Location = location;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}