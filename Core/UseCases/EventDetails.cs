using Core.Repositories;

namespace Core.UseCases
{
    public class EventDetails
    {
        private readonly IEventRepository _eventRepository;

        public EventDetails(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public Result Execute(Request request)
        {
            var e = _eventRepository.Get(request.EventId);
            
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
            public string Name { get; private set; }
            public string Slug { get; private set; }

            public Result(string name, string slug)
            {
                Name = name;
                Slug = slug;
            }
        }
    }
}