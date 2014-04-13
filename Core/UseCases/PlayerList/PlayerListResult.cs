using System.Collections.Generic;

namespace Core.UseCases.PlayerList
{
    public class PlayerListResult
    {
        public IList<PlayerListItem> Players;
        public string Slug;
    }
}