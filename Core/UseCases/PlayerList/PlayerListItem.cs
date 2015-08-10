using Core.Entities;

namespace Core.UseCases.PlayerList
{
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