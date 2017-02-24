using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class PlayerList
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;

        public PlayerList(IBunchRepository bunchRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.Get(request.BunchId);
            var players = _playerRepository.List(bunch.Id);
            var isManager = RoleHandler.IsInRole(bunch.Role, Role.Manager);

            return new Result(bunch, players, isManager);
        }

        public class Request
        {
            public string BunchId { get; }

            public Request(string bunchId)
            {
                BunchId = bunchId;
            }
        }

        public class Result
        {
            public IList<PlayerListItem> Players { get; private set; }
            public bool CanAddPlayer { get; private set; }
            public string Slug { get; private set; }

            public Result(Bunch bunch, IEnumerable<Player> players, bool isManager)
            {
                Players = players.Select(o => new PlayerListItem(o)).OrderBy(o => o.Name).ToList();
                CanAddPlayer = isManager;
                Slug = bunch.Id;
            }
        }

        public class PlayerListItem
        {
            public string Name { get; }
            public string Id { get; }
            public string Color { get; }

            public PlayerListItem(Player player)
            {
                Name = player.DisplayName;
                Id = player.Id;
                Color = player.Color;
            }
        }
    }
}