using System.Linq;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class DeletePlayer
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly CashgameService _cashgameService;
        private readonly IUserRepository _userRepository;
        private readonly IBunchRepository _bunchRepository;

        public DeletePlayer(IPlayerRepository playerRepository, CashgameService cashgameService, IUserRepository userRepository, IBunchRepository bunchRepository)
        {
            _playerRepository = playerRepository;
            _cashgameService = cashgameService;
            _userRepository = userRepository;
            _bunchRepository = bunchRepository;
        }

        public Result Execute(Request request)
        {
            var player = _playerRepository.Get(request.PlayerId);
            var bunch = _bunchRepository.Get(player.BunchId);
            var currentUser = _userRepository.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerRepository.GetByUser(bunch.Id, currentUser.Id);
            RequireRole.Manager(currentUser, currentPlayer);
            var cashgames = _cashgameService.ListByPlayer(player.Id);
            var hasPlayed = cashgames.Any();
            var canDelete = !hasPlayed;

            if (canDelete)
            {
                _playerRepository.Delete(request.PlayerId);
            }

            return new Result(canDelete, bunch.Id, request.PlayerId);
        }

        public class Request
        {
            public string UserName { get; }
            public string PlayerId { get; }

            public Request(string userName, string playerId)
            {
                UserName = userName;
                PlayerId = playerId;
            }
        }

        public class Result
        {
            public bool Deleted { get; private set; }
            public string Slug { get; private set; }
            public string PlayerId { get; private set; }

            public Result(bool deleted, string slug, string playerId)
            {
                Deleted = deleted;
                Slug = slug;
                PlayerId = playerId;
            }
        }
    }
}