using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class EventDetails
    {
        private readonly EventService _eventService;
        private readonly UserService _userService;
        private readonly IPlayerRepository _playerRepository;
        private readonly BunchService _bunchService;

        public EventDetails(EventService eventService, UserService userService, IPlayerRepository playerRepository, BunchService bunchService)
        {
            _eventService = eventService;
            _userService = userService;
            _playerRepository = playerRepository;
            _bunchService = bunchService;
        }

        public Result Execute(Request request)
        {
            var e = _eventService.GetById(request.EventId);
            var bunch = _bunchService.Get(e.BunchId);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(e.BunchId, user.Id);
            RoleHandler.RequirePlayer(user, player);
            
            return new Result(e.Name, bunch.Slug);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public int EventId { get; private set; }

            public Request(string userName, int eventId)
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