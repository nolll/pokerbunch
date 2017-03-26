using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class PlayerList
    {
        private readonly IBunchService _bunchService;
        private readonly IPlayerService _playerService;

        public PlayerList(IBunchService bunchService, IPlayerService playerService)
        {
            _bunchService = bunchService;
            _playerService = playerService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.Get(request.BunchId);
            var players = _playerService.List(bunch.Id);
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