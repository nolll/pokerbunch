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

        public Result Execute(Request input)
        {
            var e = _eventRepository.GetById(input.EventId);

            return new Result(e.Name);
        }

        public class Request
        {
            public int EventId { get; private set; }

            public Request(int eventId)
            {
                EventId = eventId;
            }
        }

        public class Result
        {
            public string Name { get; private set; }

            public Result(string name)
            {
                Name = name;
            }
        }
    }
}