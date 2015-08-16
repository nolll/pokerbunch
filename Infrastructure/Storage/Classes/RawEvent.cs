using System;

namespace Infrastructure.Storage.Classes
{
    public class RawEvent
    {
        public int Id { get; private set; }
        public int BunchId { get; private set; }
        public string Name { get; private set; }
        public string Location { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public RawEvent(int id, int bunchId, string name, string location, DateTime startDate, DateTime endDate)
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