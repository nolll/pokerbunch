using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class EventDetails
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public EventDetails(IEventRepository eventRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var e = _eventRepository.GetById(request.EventId);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(e.BunchId, user.Id);
            RoleHandler.RequirePlayer(user, player);
            
            return new Result(e.Name);
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

            public Result(string name)
            {
                Name = name;
            }
        }
    }
}