using System;

namespace Infrastructure.Storage.Classes
{
    public class RawEvent
    {
        public string Id { get; private set; }
        public string Slug { get; private set; }
        public string BunchId { get; private set; }
        public string Name { get; private set; }
        public string LocationId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public RawEvent(string id, string slug, string bunchId, string name, string locationId, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Slug = slug;
            BunchId = bunchId;
            Name = name;
            LocationId = locationId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}