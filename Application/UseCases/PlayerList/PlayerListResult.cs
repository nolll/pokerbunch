using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Application.UseCases.PlayerList
{
    public class PlayerListResult
    {
        public IList<PlayerListItem> Players { get; private set; }
        public string Slug { get; private set; }
        public bool CanAddPlayer { get; private set; }

        public PlayerListResult(Bunch bunch, IEnumerable<Player> players, bool isManager)
        {
            Slug = bunch.Slug;
            Players = players.Select(o => new PlayerListItem(o)).OrderBy(o => o.Name).ToList();
            CanAddPlayer = isManager;
        }
    }
}