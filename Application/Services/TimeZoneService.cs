using System.Collections.Generic;
using System.Linq;
using Application.UseCases.AddBunchForm;

namespace Application.Services
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