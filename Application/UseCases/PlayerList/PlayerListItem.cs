using Core.Entities;

namespace Application.UseCases.PlayerList
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