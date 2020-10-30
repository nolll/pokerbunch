using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public static class TimeZoneService
    {
        public static List<TimeZoneItem> GetTimeZones()
        {
            var timeZones = Globalization.GetTimezones();
            return timeZones.Select(t => new TimeZoneItem(t.Id, t.DisplayName)).ToList();
        }
    }

    public class TimeZoneItem
    {
        public string Id { get; }
        public string Name { get; }

        public TimeZoneItem(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}