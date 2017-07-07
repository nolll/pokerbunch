using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class EventList
    {
        private readonly IEventService _eventService;

        public EventList(IEventService eventService)
        {
            _eventService = eventService;
        }

        public Result Execute(Request request)
        {
            var events = _eventService.ListByBunch(request.Slug);

            var eventItems = events.OrderByDescending(o => o, new EventComparer()).Select(CreateEventItem).ToList();

            return new Result(eventItems);
        }

        private static Item CreateEventItem(Event e)
        {
            if(e.HasGames)
                return new Item(e.Id, e.Name, e.Location.Name, e.StartDate);
            return new Item(e.Id, e.Name);
        }

        public class Request
        {
            public string Slug { get; }

            public Request(string slug)
            {
                Slug = slug;
            }
        }

        public class Result
        {
            public IList<Item> Events { get; private set; }

            public Result(IList<Item> events)
            {
                Events = events;
            }
        }

        public class Item
        {
            public string EventId { get; private set; }
            public string Name { get; private set; }
            public string Location { get; private set; }
            public Date StartDate { get; private set; }
            public bool HasGames { get; private set; }

            public Item(string id, string name)
            {
                EventId = id;
                Name = name;
                HasGames = false;
            }
            
            public Item(string id, string name, string location, Date startDate)
                : this(id, name)
            {
                Location = location;
                StartDate = startDate;
                HasGames = true;
            }
        }

        private class EventComparer : IComparer<Event>
        {
            public int Compare(Event x, Event y)
            {
                if (x.HasGames && y.HasGames)
                    return x.StartDate.CompareTo(y.StartDate);
                if (x.HasGames && !y.HasGames)
                    return -1;
                if (!x.HasGames && y.HasGames)
                    return 1;
                return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
            }
        }
    }
}