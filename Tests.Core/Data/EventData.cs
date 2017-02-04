using System.Collections.Generic;
using Core.Entities;

namespace Tests.Core.Data
{
    public static class EventData
    {
        public const string Id1 = "event-id-1";
        public const string Name1 = "event-name-1";

        public const string Id2 = "event-id-2";
        public const string Name2 = "event-name-2";

        public static IList<Event> TwoEvents => new List<Event>
        {
            new Event(Id1, Name1, BunchData.Id1),
            new Event(Id2, Name2, BunchData.Id1)
        };
    }
}