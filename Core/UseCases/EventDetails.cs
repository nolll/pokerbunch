using Core.Services;

namespace Core.UseCases
{
    public class EventDetails
    {
        private readonly IEventService _eventService;

        public EventDetails(IEventService eventService)
        {
            _eventService = eventService;
        }

        public Result Execute(Request request)
        {
            var e = _eventService.Get(request.EventId);
            
            return new Result(e.Name, e.BunchId);
        }

        public class Request
        {
            public string EventId { get; }

            public Request(string eventId)
            {
                EventId = eventId;
            }
        }

        public class Result
        {
            public string Name { get; }
            public string Slug { get; }

            public Result(string name, string slug)
            {
                Name = name;
                Slug = slug;
            }
        }
    }
}