using System.Collections.Generic;

namespace Application.UseCases.PlayerList
{
    public class PlayerListResult
    {
        public IList<PlayerListItem> Players;
        public string Slug;
    }
}