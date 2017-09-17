using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class AddEvent
    {
        private readonly IEventService _eventService;

        public AddEvent(IEventService eventService)
        {
            _eventService = eventService;
        }

        public Result Execute(Request request)
        {
            var e = new Event("", request.Slug, request.Name);
            _eventService.Add(e);

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
            public string Slug { get; }

            public Result(string slug)
            {
                Slug = slug;
            }
        }
    }
}