using System.Collections.Generic;
using System.Linq;
using Core.UseCases.AddBunchForm;

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
}