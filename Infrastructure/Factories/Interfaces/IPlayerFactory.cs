using Core.Classes;

namespace Infrastructure.Factories{

	public interface IPlayerFactory{

		Player Create(string displayName, Role role = Role.Player, string userName = null, int id = 0);

	}

}