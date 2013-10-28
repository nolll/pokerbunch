using System.Collections.Generic;
using Core.Classes;

namespace Core.Repositories {

	public interface IPlayerRepository{

		List<Player> GetAll(Homegame homegame);
		Player GetPlayerById(int id);
		Player GetByName(Homegame homegame, string name);
        Player GetByUserName(Homegame homegame, string userName);
		int AddPlayer(Homegame homegame, string playerName);
		int AddPlayerWithUser(Homegame homegame, User user, Role role);
		bool JoinHomegame(Player player, Homegame homegame, User user);
		bool DeletePlayer(Homegame homegame, Player player);

	}

}