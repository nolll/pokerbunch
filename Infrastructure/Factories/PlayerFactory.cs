using Core.Classes;

namespace Infrastructure.Factories{
    public class PlayerFactory : IPlayerFactory{

		public Player Create(string displayName, Role role = Role.Player, string userName = null, int id = 0){
            return new Player
                {
                    DisplayName = displayName, 
                    Role = role, 
                    UserName = userName, 
                    Id = id
                };
		}

	}

}