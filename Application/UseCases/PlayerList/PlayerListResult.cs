using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Application.UseCases.PlayerList
{
    public class PlayerListResult
    {
        public readonly IList<PlayerListItem> Players;
        public readonly string Slug;

        public PlayerListResult(Homegame homegame, IEnumerable<Player> players)
        {
            Slug = homegame.Slug;
            Players = players.Select(o => new PlayerListItem(o)).OrderBy(o => o.Name).ToList();
        }
    }
}