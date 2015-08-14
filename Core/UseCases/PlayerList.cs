using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases
{
    public class PlayerList
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public PlayerList(IBunchRepository bunchRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            var players = _playerRepository.GetList(bunch.Id);
            var isManager = RoleHandler.IsInRole(user, player, Role.Manager);

            return new Result(bunch, players, isManager);
        }

        public class Request
        {
            public string Slug { get; private set; }
            public string UserName { get; private set; }

            public Request(string slug, string userName)
            {
                Slug = slug;
                UserName = userName;
            }
        }

        public class Result
        {
            public IList<PlayerListItem> Players { get; private set; }
            public bool CanAddPlayer { get; private set; }
            public Url AddUrl { get; private set; }

            public Result(Bunch bunch, IEnumerable<Player> players, bool isManager)
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