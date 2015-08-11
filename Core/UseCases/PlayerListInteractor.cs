using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases.PlayerList
{
    public class PlayerListInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public PlayerListInteractor(IBunchRepository bunchRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public PlayerListResult Execute(PlayerListRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            var players = _playerRepository.GetList(bunch.Id);
            var isManager = RoleHandler.IsInRole(user, player, Role.Manager);

            return new PlayerListResult(bunch, players, isManager);
        }

        public class PlayerListRequest
        {
            public string Slug { get; private set; }
            public string UserName { get; private set; }

            public PlayerListRequest(string slug, string userName)
            {
                Slug = slug;
                UserName = userName;
            }
        }

        public class PlayerListResult
        {
            public IList<PlayerListInteractor.PlayerListItem> Players { get; private set; }
            public bool CanAddPlayer { get; private set; }
            public Url AddUrl { get; private set; }

            public PlayerListResult(Bunch bunch, IEnumerable<Player> players, bool isManager)
            {
                Players = players.Select(o => new PlayerListItem(o)).OrderBy(o => o.Name).ToList();
                CanAddPlayer = isManager;
                AddUrl = new AddPlayerUrl(bunch.Slug);
            }
        }

        public class PlayerListItem
        {
            public string Name { get; private set; }
            public int Id { get; private set; }

            public PlayerListItem(Player player)
            {
                Name = player.DisplayName;
                Id = player.Id;
            }
        }
    }
}