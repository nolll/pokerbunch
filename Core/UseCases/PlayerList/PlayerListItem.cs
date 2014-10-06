using Core.Entities;

namespace Core.UseCases.PlayerList
{
    public class PlayerListItem
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public PlayerListItem(Player player)
        {
            Id = player.Id;
            Name = player.DisplayName;
        }
    }
}