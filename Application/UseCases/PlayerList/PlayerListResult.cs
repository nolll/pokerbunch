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

        public PlayerListResult(Homegame homegame, IEnumerable<Player> players, bool isManager)
        {
            Slug = homegame.Slug;
            Players = players.Select(o => new PlayerListItem(o)).OrderBy(o => o.Name).ToList();
            CanAddPlayer = isManager;
        }
    }
}