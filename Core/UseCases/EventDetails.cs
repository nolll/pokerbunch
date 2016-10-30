using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class EventDetails
    {
        private readonly IEventRepository _eventRepository;
        private readonly UserService _userService;
        private readonly PlayerService _playerService;
        private readonly IBunchRepository _bunchRepository;

        public EventDetails(IEventRepository eventRepository, UserService userService, PlayerService playerService, IBunchRepository bunchRepository)
        {
            _eventRepository = eventRepository;
            _userService = userService;
            _playerService = playerService;
            _bunchRepository = bunchRepository;
        }

        public Result Execute(Request request)
        {
            var e = _eventRepository.Get(request.EventId);
            var bunch = _bunchRepository.Get(e.BunchId);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(bunch.Id, user.Id);
            RequireRole.Player(user, player);
            
            return new Result(e.Name, bunch.Id);
        }

        public class Request
        {
            public string UserName { get; }
            public string EventId { get; }

            public Request(string userName, string eventId)
            {
                UserName = userName;
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