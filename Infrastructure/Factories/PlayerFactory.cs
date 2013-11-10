using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories{
    public class PlayerFactory : IPlayerFactory{

		public Player Create(RawPlayer rawPlayer)
        {
            return new Player
            (
                rawPlayer.Id,
                rawPlayer.UserName,
                rawPlayer.DisplayName,
                (Role)rawPlayer.Role
            );
        }
    }

}