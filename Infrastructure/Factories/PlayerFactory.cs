using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories{
    public class PlayerFactory : IPlayerFactory{

		public Player Create(RawPlayer rawPlayer)
        {
            return new Player
            {
                DisplayName = rawPlayer.DisplayName,
                Role = (Role)rawPlayer.Role,
                UserName = rawPlayer.UserName,
                Id = rawPlayer.Id
            };
        }
    }

}