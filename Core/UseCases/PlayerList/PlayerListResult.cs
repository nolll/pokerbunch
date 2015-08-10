using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Urls;

namespace Core.UseCases.PlayerList
{
    public class PlayerListResult
    {
        public IList<PlayerListItem> Players { get; private set; }
        public bool CanAddPlayer { get; private set; }
        public Url AddUrl { get; private set; }

        public PlayerListResult(Bunch bunch, IEnumerable<Player> players, bool isManager)
        {
            Players = players.Select(o => new PlayerListItem(o)).OrderBy(o => o.Name).ToList();
            CanAddPlayer = isManager;
            AddUrl = new AddPlayerUrl(bunch.Slug);
        }
    }
}