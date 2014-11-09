using Core.Entities;
using Core.Urls;

namespace Core.UseCases.PlayerList
{
    public class PlayerListItem
    {
        public string Name { get; private set; }
        public Url Url { get; private set; }

        public PlayerListItem(string slug, Player player)
        {
            Name = player.DisplayName;
            Url = new PlayerDetailsUrl(slug, player.Id);
        }
    }
}