using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class EventDetails
    {
        private readonly EventService _eventService;
        private readonly UserService _userService;
        private readonly PlayerService _playerService;
        private readonly IBunchRepository _bunchRepository;

        public EventDetails(EventService eventService, UserService userService, PlayerService playerService, IBunchRepository bunchRepository)
        {
            _eventService = eventService;
            _userService = userService;
            _playerService = playerService;
            _bunchRepository = bunchRepository;
        }

        public Result Execute(Request request)
        {
            var e = _eventService.Get(request.EventId);
            var bunch = _bunchRepository.Get(e.Bunch);
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