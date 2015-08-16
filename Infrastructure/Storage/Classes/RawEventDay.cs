using System;

namespace Infrastructure.Storage.Classes
{
    public class RawEventDay
    {
        public int Id { get; private set; }
        public int BunchId { get; private set; }
        public string Name { get; private set; }
        public string Location { get; private set; }
        public DateTime Date { get; private set; }

        public RawEventDay(int id, int bunchId, string name, string location, DateTime date)
        {
            Id = id;
            BunchId = bunchId;
            Name = name;
            Location = location;
            Date = date;
        }
    }
}