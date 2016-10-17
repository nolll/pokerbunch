using System;

namespace Infrastructure.Storage.Classes
{
    public class RawEventDay
    {
        public string Id { get; private set; }
        public string Slug { get; private set; }
        public string BunchId { get; private set; }
        public string Name { get; private set; }
        public string LocationId { get; private set; }
        public DateTime Date { get; private set; }

        public RawEventDay(string id, string slug, string bunchId, string name, string locationId, DateTime date)
        {
            Id = id;
            Slug = slug;
            BunchId = bunchId;
            Name = name;
            LocationId = locationId;
            Date = date;
        }
    }
}