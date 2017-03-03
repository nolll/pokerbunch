using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class AddEvent
    {
        private readonly IEventRepository _eventRepository;

        public AddEvent(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public Result Execute(Request request)
        {
            var e = new Event("", request.Slug, request.Name);
            _eventRepository.Add(e);

            return new Result(request.Slug);
        }

        public class Request
        {
            public string Slug { get; }
            public string Name { get; }

            public Request(string slug, string name)
            {
                Slug = slug;
                Name = name;
            }
        }

        public class Result
        {
            public string Slug { get; private set; }

            public Result(string slug)
            {
                Slug = slug;
            }
        }
    }
}